using System;
using NRules;
using NRules.Fluent;
using NRules.Fluent.Dsl;
using TestNRules.Model;
using TestNRules.Rules;
using Xunit;

namespace TestNRules
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(PreferredCustomerDiscountRule).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            var session = factory.CreateSession();

            //Load domain model
            var customer = new Customer("John Doe") {IsPreferred = true};
            var order1 = new Order(123456, customer, 2, 25.0);
            var order2 = new Order(123457, customer, 1, 100.0);

            session.Insert(customer);
            session.Insert(order1);
            session.Insert(order2);

            session.Fire();
        }
    }
    
}
