namespace Ingenico.Models
{
    public class Instrument
    {
        public Expiry expiry { get; set; }
        public string provider { get; set; }
        public string iFSC { get; set; }
        public Holder holder { get; set; }
        public string bIC { get; set; }
        public string type { get; set; }
        public string action { get; set; }
        public string mICR { get; set; }
        public string verificationCode { get; set; }
        public string iBAN { get; set; }
        public string processor { get; set; }
        public Issuance issuance { get; set; }
        public string alias { get; set; }
        public string identifier { get; set; }
        public string token { get; set; }
        public Authentication authentication { get; set; }
        public string subType { get; set; }
        public string issuer { get; set; }
        public string acquirer { get; set; }

        public Instrument()
        {
            expiry = new Expiry();
            provider = "";
            iFSC = "";
            holder = new Holder();
            bIC = "";
            type = "";
            action = "";
            mICR = "";
            verificationCode = "";
            iBAN = "";
            processor = "";
            issuance = new Issuance();
            alias = "";
            identifier = "";
            token = "";
            authentication = new Authentication();
            subType = "";
            issuer = "";
            acquirer = "";
        }
    }
}
