namespace FuriousWeb.Models
{
    internal class Payment
    {
        public double Amount { get; set; }
        public string Number { get; set; }
        public string Holder { get; set; }
        public int Exp_year { get; set; }
        public int Exp_month { get; set; }
        public string Cvv { get; set; }
        public string Created_at { get; set; }
        public string Id { get; set; }

        public Payment(double amount, string number, string holder, int exp_year, int exp_month, string cvv)
        {
            this.Amount = amount;
            this.Number = number;
            this.Holder = holder;
            this.Exp_year = exp_year;
            this.Exp_month = exp_month;
            this.Cvv = cvv;
        }

        public Payment()
        {

        }
    }
}