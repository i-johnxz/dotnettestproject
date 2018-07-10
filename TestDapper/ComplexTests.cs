using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Xunit;
using Xunit.Abstractions;

namespace TestDapper
{
    public class ComplexTests : TestBase
    {

        private readonly ITestOutputHelper _output;

        public ComplexTests(ITestOutputHelper output)
        {
            _output = output;
        }



        private SqlConnection _marsConnection;
        private SqlConnection MarsConnection => _marsConnection ?? (_marsConnection = GetOpenConnection(true));


        [Fact]
        public async Task Test_GetPersonAsync()
        {
            var query = await connection.QueryAsync<Person>("select * from people").ConfigureAwait(false);

            var persons = query.ToArray();

            Assert.True(persons.Length > 0);
        }

        [Fact]
        public async Task Test_GetAddressAsync()
        {
            var query = await connection.QueryAsync<Address>("select * from addresses").ConfigureAwait(false);

            var addresses = query.ToArray();

            Assert.True(addresses.Length > 0);
        }

        [Fact]
        public async Task Test_ComplexObjectAsync()
        {
            string sql =
                @"select p.*, a.*
                  from People p 
                  inner join Addresses a on p.AddressId = a.Id";
            var query = await connection.QueryAsync<Person, Address, Person>(sql, (person, address) =>
            {
                person.Address = address;
                return person;
            }
                , splitOn: "AddressId"
                ).ConfigureAwait(false);

            var persons = query.ToArray();

            Assert.True(persons.Length > 0);
        }


        public class Person
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public Address Address { get; set; }
        }


        public class Address
        {
            public int Id { get; set; }

            public string StreetNumber { get; set; }

            public string StreetName { get; set; }

            public string City { get; set; }

            public string State { get; set; }

        }
    }
}
