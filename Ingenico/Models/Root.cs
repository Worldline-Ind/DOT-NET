namespace Ingenico.Models
{
    public class Root
    {
        public Merchant merchant { get; set; }
        public Cart cart { get; set; }
        public Payment payment { get; set; }
        public Transaction transaction { get; set; }
        public Consumer consumer { get; set; }

        public Root()
        {
            merchant = new Merchant();
            cart = new Cart();
            payment = new Payment();
            transaction = new Transaction();
            consumer = new Consumer();
        }
    }
}
