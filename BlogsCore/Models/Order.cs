namespace BlogsCore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public StreetAddress StreetAddress { get; set; }
    }

    public class StreetAddress
    {
        public string Street { get; set; }

        public string City { get; set; }
    }
}
