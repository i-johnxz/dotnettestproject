using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Nest;
using Xunit;
using Xunit.Abstractions;

namespace elasticsearchdemo
{
    public class UnitTest1 : BaseTest
    {



        public UnitTest1(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public async Task Test_Index()
        {
            var employee1 = new Employee("1", "John", "Smith", 25, "I love to go rock climbing", new[]
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
            var response1 = await Client.IndexAsync(employee1, idx => idx.Index("megacorp"));
            var response2 = await Client.IndexAsync(employee2, idx => idx.Index("megacorp"));
            var response3 = await Client.IndexAsync(employee3, idx => idx.Index("megacorp"));
            Assert.True(response1.IsValid && response2.IsValid && response3.IsValid);
        }

        [Fact]
        public async Task Test_Get()
        {
            var response = await Client.GetAsync<Employee>(1, idx => idx.Index("megacorp"));
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
                        new MatchQuery { Field = "about", Query = "like" }
            };
            var response = await Client.SearchAsync<Employee>(searchModel);
            Assert.True(response.Documents.Count == 2);

        }

        [Fact]
        public async Task Test_ComplexSearch()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Query = new DateRangeQuery { Field = "age", GreaterThan = DateMath.FromString("30") } &&
                        new MatchQuery { Field = "last_name", Query = "smith" }
            };
            var response = await Client.SearchAsync<Employee>(searchModel);
            Assert.True(response.Documents.Count == 1);
        }

        [Fact]
        public async Task Test_AllSearch()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Query = new MatchQuery { Field = "about", Query = "rock climbing" }
            };
            var response = await Client.SearchAsync<Employee>(searchModel);

            Assert.True(response.Documents.Count == 2);
        }

        [Fact]
        public async Task Test_MuitpleSearch()
        {
            var searchModel = new MultiSearchRequest(Indices.Index("megacorp", "myindex"));
            var response = await Client.MultiSearchAsync(searchModel);

            Assert.True(response.IsValid);
        }

        [Fact]
        public async Task Test_Search_Match_Phrase()
        {
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Query = new MatchPhraseQuery { Field = "about", Query = "rock climbing" }
            };
            var response = await Client.SearchAsync<Employee>(searchModel);
            Output.WriteLine(response.MaxScore.ToString());
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
            var response = await Client.SearchAsync<Employee>(searchModel);
            foreach (var hit in response.Hits)
            {
                foreach (var hitHighlight in hit.Highlights)
                {
                    Output.WriteLine(string.Join(" ", hitHighlight.Value.Highlights));
                }
            }
            Assert.True(response.Documents.Count == 1);
        }

        [Fact]
        public async Task Test_Aggregations()
        {
            // ¿É²Î¿¼ https://stackoverflow.com/questions/38145991/how-to-set-fielddata-true-in-kibana#42092877
            var searchModel = new SearchRequest<Employee>(Indices.Parse("megacorp"))
            {
                Aggregations = new TermsAggregation("all_interests")
                {
                    Field = "interests.keyword",
                },

            };
            var response = await Client.SearchAsync<Employee>(searchModel);

            var aggregate = response.Aggregations.Terms("all_interests");

            foreach (var bucket in aggregate.Buckets)
            {
                Output.WriteLine($"Key:{bucket.Key}, doccount:{bucket.DocCount}");
            }

            Assert.NotNull(aggregate);
        }

        [Fact]
        public async Task Test_LowLevel()
        {
            var searchResponse = await Client.LowLevel.SearchAsync<SearchResponse<Employee>>("megacorp", "employee",
                PostData.Serializable(new
                {
                    from = 0,
                    size = 10
                }));

            var responseJson = searchResponse;
            Assert.NotNull(responseJson);
        }

        [Fact]
        public void Test_Should()
        {
            var pool = new SingleNodeConnectionPool(Uri);
            pool.Nodes.Should().HaveCount(1);
            var node = pool.Nodes.First();
            node.Uri.Port.Should().Be(9200);

            pool.SupportsReseeding.Should().BeFalse();
            pool.SupportsPinging.Should().BeFalse();
        }

        [Fact]
        public async void Test_CreateIndex()
        {
            var createIndex = await Client.CreateIndexAsync("megacorp", c => c
                .Mappings(ms => ms
                    .Map<Employee>(m => m
                        .AutoMap()
                        .Properties(pps => pps
                            .Text(s => s
                                .Name(e => e.FirstName)
                                .Fields(fs => fs
                                    .Keyword(ss => ss
                                        .Name("keyword"))))
                            .Text(s => s
                                .Name(e => e.LastName)
                                .Fields(fs => fs
                                    .Keyword(ss => ss
                                        .Name("keyword"))))
                            .Text(s => s
                                .Name(e => e.About)
                                .Fields(fs => fs
                                    .Keyword(ss => ss
                                        .Name("keyword"))))
                            .Text(s => s
                                .Name(e => e.Interests)
                                .Fields(fs => fs
                                    .Keyword(ss => ss
                                        .Name("keyword"))))
                        )
                    )
                ));
            Assert.True(createIndex.IsValid);
        }

        [Fact]
        public async Task Search_Sort_Aggregations()
        {
            var searchResponse = await Client.SearchAsync<Employee>(s => s
                .Index(Indices.Index("megacorp"))
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.LastName)
                        .Query("Smith")
                    )
                )
                .Sort(ss => ss
                    .Descending(f => f.FirstName.Suffix("keyword"))
                )
                .Aggregations(a => a
                    .Terms("first_names", t => t
                        .Field(f => f.FirstName.Suffix("keyword"))
                    )
                )
            );


            var aggregate = searchResponse.Aggregations.Terms("first_names");

            foreach (var bucket in aggregate.Buckets)
            {
                Output.WriteLine($"Key:{bucket.Key}, doccount:{bucket.DocCount}");
            }


            Assert.True(searchResponse.IsValid);
        }

        [ElasticsearchType(Name = "employee")]
        public class Employee
        {
            [Text(Name = "id")]
            public string Id { get; set; }

            [Text(Name = "first_name")]
            public string FirstName { get; set; }

            [Text(Name = "last_name")]
            public string LastName { get; set; }

            [Text(Name = "age")]
            public int Age { get; set; }

            [Text(Name = "about")]
            public string About { get; set; }

            [Text(Name = "interests")]
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

    
}
