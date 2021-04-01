namespace Ingenico.Models
{
    public class Address
    {
        public string country { get; set; }
        public string street { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string county { get; set; }

        public Address()
        {
            country = "";
            street = "";
            state = "";
            city = "";
            zipCode = "";
            county = "";
        }
    }
}
