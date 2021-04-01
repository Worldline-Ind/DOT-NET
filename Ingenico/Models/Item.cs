namespace Ingenico.Models
{
    public class Item
    {
        public string description { get; set; }
        public string providerIdentifier { get; set; }
        public string surchargeOrDiscountAmount { get; set; }
        public string amount { get; set; }
        public string comAmt { get; set; }
        public string sKU { get; set; }
        public string reference { get; set; }
        public string identifier { get; set; }

        public Item()
        {
            description = "";
            providerIdentifier = "";
            surchargeOrDiscountAmount = "";
            amount = "";
            comAmt = "";
            sKU = "";
            reference = "";
            identifier = "";
        }
    }
}
