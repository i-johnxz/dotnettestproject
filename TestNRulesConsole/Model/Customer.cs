namespace TestNRulesConsole.Model
{
    public class Customer
    {
        public string Name { get; protected set; }

        public bool IsPreferred { get; set; }

        public Customer(string name)
        {
            Name = name;
        }
    }
}
