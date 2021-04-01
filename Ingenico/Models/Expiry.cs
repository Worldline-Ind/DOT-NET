namespace Ingenico.Models
{
    public class Expiry
    {
        public string year { get; set; }
        public string month { get; set; }
        public string dateTime { get; set; }

        public Expiry()
        {
            year = "";
            month = "";
            dateTime = "";
        }
    }
}
