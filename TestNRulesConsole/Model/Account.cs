namespace TestNRulesConsole.Model
{
    public class Account
    {
        public string AccountNumber { get; protected set; }

        public Customer Owner { get; protected set; }

        public bool IsActive { get; set; }

        public bool IsDelinquent { get; set; }

        public Account(string accountNumber, Customer owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            IsActive = true;
            IsDelinquent = false;
        }
    }
}
