namespace Ingenico.Models
{
    public class Transaction
    {
        public string deviceIdentifier { get; set; }
        public string smsSending { get; set; }
        public string amount { get; set; }
        public string forced3DSCall { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public string isRegistration { get; set; }
        public string identifier { get; set; }
        public string dateTime { get; set; }
        public string token { get; set; }
        public string securityToken { get; set; }
        public string subType { get; set; }
        public string requestType { get; set; }
        public string reference { get; set; }
        public string merchantInitiated { get; set; }
        public string merchantRefNo { get; set; }

        public Transaction()
        {
            deviceIdentifier = "";
            smsSending = "";
            amount = "";
            forced3DSCall = "";
            type = "";
            description = "";
            currency = "";
            isRegistration = "";
            identifier = "";
            dateTime = "";
            token = "";
            securityToken = "";
            subType = "";
            requestType = "";
            reference = "";
            merchantInitiated = "";
            merchantRefNo = "";
        }
    }
}
