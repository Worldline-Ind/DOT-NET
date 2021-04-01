namespace Ingenico.Models
{
    public class Authentication
    {
        public string token { get; set; }
        public string type { get; set; }
        public string subType { get; set; }

        public Authentication()
        {
            token = "";
            type = "";
            subType = "";
        }
    }
}
