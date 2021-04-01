namespace Ingenico.Models
{
    public class Consumer
    {
        public string mobileNumber { get; set; }
        public string emailID { get; set; }
        public string identifier { get; set; }
        public string accountNo { get; set; }

        public Consumer()
        {
            mobileNumber = "";
            emailID = "";
            identifier = "";
            accountNo = "";
        }
    }
}
