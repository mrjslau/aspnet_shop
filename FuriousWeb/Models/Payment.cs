namespace FuriousWeb.Models
{
    internal class Payment
    {
        public double amount;
        public string number;
        public string holder;
        public int exp_year;
        public int exp_month;
        public string cvv;

        public Payment(double amount, string number, string holder, int exp_year, int exp_month, string cvv)
        {
            this.amount = amount;
            this.number = number;
            this.holder = holder;
            this.exp_year = exp_year;
            this.exp_month = exp_month;
            this.cvv = cvv;
        }
    }
}