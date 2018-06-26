﻿using System;
using NRules.Fluent.Dsl;
using TestNRulesConsole.Model;

namespace TestNRulesConsole.Rules
{
    public class DicsountNotificationRule : Rule
    {
        public override void Define()
        {
            Customer customer = null;

            When().Match<Customer>(() => customer)
                  .Exists<Order>(o => o.Customer == customer, o => o.PercentDiscount > 0.0);

            Then().Do(_ => Console.WriteLine("Customer {0} was notified about a discount", customer.Name));
        }
    }
}
