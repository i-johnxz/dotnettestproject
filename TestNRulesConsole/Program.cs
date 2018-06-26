using System;
using NRules;
using NRules.Fluent;
using TestNRulesConsole.Model;
using TestNRulesConsole.Rules;

namespace TestNRulesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(PreferredCustomerDiscountRule).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            var session = factory.CreateSession();

            //Load domain model
            var customer = new Customer("John Doe") { IsPreferred = false };
            var order1 = new Order(123456, customer, 2, 25.0);
            var order2 = new Order(123457, customer, 1, 100.0);

            session.Insert(customer);
            session.Insert(order1);
            session.Insert(order2);

            var fire = session.Fire();
            Console.WriteLine(fire);
            Console.Read();
        }
    }
}
