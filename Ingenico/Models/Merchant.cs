namespace Ingenico.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Merchant
    {
        public string webhookEndpointURL { get; set; }
        public string responseType { get; set; }
        public string responseEndpointURL { get; set; }
        public string description { get; set; }
        public string identifier { get; set; }
        public string webhookType { get; set; }

        public Merchant()
        {
            webhookEndpointURL = "";
            responseType = "";
            responseEndpointURL = "";
            description = "";
            identifier = "";
            webhookType = "";
        }
    }
}
