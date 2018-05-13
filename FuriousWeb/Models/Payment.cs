namespace FuriousWeb.Models
{
    internal class Payment
    {
        private double amount;
        private string number;
        private string holder;
        private int exp_year;
        private int exp_month;
        private string cvv;

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