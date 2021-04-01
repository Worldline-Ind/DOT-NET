namespace Ingenico.Models
{
    public class Payment
    {
        public Method method { get; set; }
        public Instrument instrument { get; set; }
        public Instruction instruction { get; set; }

        public Payment()
        {
            method = new Method();
            instrument = new Instrument();
            instruction = new Instruction();
        }
    }
}
