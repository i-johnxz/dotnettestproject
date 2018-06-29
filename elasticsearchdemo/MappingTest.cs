using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Xunit;
using Xunit.Abstractions;

namespace elasticsearchdemo
{
    public class MappingTest : BaseTest
    {
        public MappingTest(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task Test_CustomMappingCreateIndex()
        {
            var createIndexResponse = await Client.CreateIndexAsync("megacorp", c => c
                .Mappings(ms => ms
                    .Map<Company>(m => m
                        .Properties(ps => ps
                            .Text(s => s
                                .Name(n => n.Name))
                            .Object<Employee>(o => o
                                .Name(n => n.Employees)
                                .Properties(eps => eps
                                    .Text(s => s
                                        .Name(e => e.FirstName)
                                    )
                                    .Text(s => s
                                        .Name(e => e.LastName)
                                    )
                                    .Number(n => n
                                        .Name(e => e.Salary)
                                        .Type(NumberType.Integer)
                                    )
                                )
                            )
                        )
                    )
                )
            );
            Assert.True(createIndexResponse.IsValid);
        }

        [Fact]
        public async Task Test_AutoMappingCreateIndex()
        {
            var createIndexResponse = await Client.CreateIndexAsync("megacorp", c => c
                .Mappings(ms => ms
                    .Map<Company>(m => m
                        .AutoMap()
                        .Properties(ps => ps
                            .Nested<Employee>(n => n
                                .Name(nn => nn.Employees)
                            )
                        )
                    )
                )
            );

            Assert.True(createIndexResponse.IsValid);
        }

        public class Company
        {

            public string Name { get; set; }

            public List<Employee> Employees { get; set; }

        }

        public class Employee
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public int Salary { get; set; }

            public DateTime Birthday { get; set; }

            public bool IsManager { get; set; }

            public TimeSpan Hours { get; set; }

        }

    }



}

