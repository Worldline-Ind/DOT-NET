namespace Ingenico.Models
{
    public class Instruction
    {
        public string occurrence { get; set; }
        public string amount { get; set; }
        public string frequency { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string action { get; set; }
        public string limit { get; set; }
        public string endDateTime { get; set; }
        public string identifier { get; set; }
        public string reference { get; set; }
        public string startDateTime { get; set; }
        public string validity { get; set; }

        public Instruction()
        {
            occurrence = "";
            amount = "";
            frequency = "";
            type = "";
            description = "";
            action = "";
            limit = "";
            endDateTime = "";
            identifier = "";
            reference = "";
            startDateTime = "";
            validity = "";
        }
    }

}
