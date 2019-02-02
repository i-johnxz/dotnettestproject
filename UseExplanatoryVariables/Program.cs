using System;
using System.Text.RegularExpressions;

namespace UseExplanatoryVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Address = "One Infinite Loop, Cupertino 95014";
            var cityZipCodeWithGroupRegex = @"/^[^,\]+[,\\s]+(?<city>.+?)\s*(?<zipCode>\d{5})?$/";
            var matchesWithGroup = Regex.Match(Address, cityZipCodeWithGroupRegex);
            var cityGroup = matchesWithGroup.Groups["city"];
            var zipCodeGroup = matchesWithGroup.Groups["zipcode"];
            if (cityGroup.Success && zipCodeGroup.Success)
            {
                SaveCityZipCode(cityGroup.Value, zipCodeGroup.Value);
            }


            var bankAccount = new BankAccount();

            var price = 100;

            bankAccount.WithdrawBalance(price);

            var balance = bankAccount.getBalance();



            Console.WriteLine("End");
            Console.Read();
        }

        private static void SaveCityZipCode(string cityGroupValue, string zipCodeGroupValue)
        {
            Console.WriteLine($"cityGroupValue:{cityGroupValue}");
            Console.WriteLine($"zipCodeGroupValue: {zipCodeGroupValue}");
        }
    }
}
