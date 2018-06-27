using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Nest;
using Xunit;
using Xunit.Abstractions;

namespace elasticsearchdemo
{
    public class UnitTest1
    {
        private readonly ElasticClient _client;

        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
            _client = new ElasticClient(new Uri(""));
        }

        [Fact]
        public async Task Test_Index()
        {
            var employee1 = new Employee("1", "John", "Smith", 25, "I love to go rock climbing", new []
            {
                "sports",
                "music"
            });
            var employee2 = new Employee("2", "Jane", "Smith", 32, "I like to collect rock albums", new[]
            {
                "music"
            });
            var employee3 = new Employee("3", "Douglas", "Fir", 35, "I like to build cabinets", new[]
            {
                "forestry"
            });
            var response1 = await _client.IndexAsync(employee1, idx => idx.Index("megacorp"));
            var response2 = await _client.IndexAsync(employee2, idx => idx.Index("megacorp"));
            var response3 = await _client.IndexAsync(employee3, idx => idx.Index("megacorp"));
            Assert.True(response1.IsValid && response2.IsValid && response3.IsValid);
        }

        [Fact]
        public async Task Test_Get()
        {
            var response = await _client.GetAsync<Employee>(1, idx => idx.Index("megacorp"));
            var employee = response.Source;
            Assert.NotNull(employee);
        }

        [Fact]
        public async Task Test_Search()
        {
            
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                From = 0,
                Size = 3,
                Query = new TermQuery { Field = "last_name", Value = "Smith" } ||
                        new MatchQuery { Field = "about", Query = "like"}
            };
            var response = await _client.SearchAsync<Employee>(searchModel);
            Assert.True(response.Documents.Count == 2);

        }

        [Fact]
        public async Task Test_ComplexSearch()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Query = new DateRangeQuery {Field = "age", GreaterThan = DateMath.FromString("30")} &&
                        new MatchQuery {Field = "last_name", Query = "smith"}
            };
            var response = await _client.SearchAsync<Employee>(searchModel);
            Assert.True(response.Documents.Count == 1);
        }
    
        [Fact]
        public async Task Test_AllSearch()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Query = new MatchQuery { Field = "about", Query = "rock climbing" }
            };
            var response = await _client.SearchAsync<Employee>(searchModel);
            
            Assert.True(response.Documents.Count == 2);
        }

        [Fact]
        public async Task Test_Search_Match_Phrase()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Query = new MatchPhraseQuery { Field = "about", Query = "rock climbing" }
            };
            var response = await _client.SearchAsync<Employee>(searchModel);
            _output.WriteLine(response.MaxScore.ToString());
            Assert.True(response.Documents.Count == 1);
        }

        [Fact]
        public async Task Test_Search_Highlight()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Highlight = Highlight.Field("about"),
                Query = new MatchPhraseQuery { Field = "about", Query = "rock climbing" }
            };
            var response = await _client.SearchAsync<Employee>(searchModel);
            foreach (var hit in response.Hits)
            {
                foreach (var hitHighlight in hit.Highlights)
                {
                    _output.WriteLine(string.Join(" ", hitHighlight.Value.Highlights));
                }
            }
            Assert.True(response.Documents.Count == 1);
        }

        [Fact]
        public async Task Test_Aggregations()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Aggregations = new TermsAggregation("all_interests")
                {
                    Field = Infer.Field<Employee>(e => e.Interests),
                    
                },

            };
            var response = await _client.SearchAsync<Employee>(searchModel);
            
            Assert.True(response.Documents.Count > 0);
        }
    }

    [DataContract(Name = "employee"), ElasticsearchType(Name = "employee")]
    public class Employee
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "about")]
        public string About { get; set; }

        [DataMember(Name = "interests")]
        public string[] Interests { get; set; }


        public Employee()
        {

        }

        public Employee(string id, string firstName, string lastName, int age, string about, string[] interests)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.About = about;
            this.Interests = interests;
        }
    }
}
