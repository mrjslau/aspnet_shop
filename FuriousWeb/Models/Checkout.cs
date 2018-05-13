using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FuriousWeb.Models
{
    public class Checkout
    {
        private List<ShoppingCartItem> Items;
        static HttpClient client;
        private Payment payment;
        private static HttpResponseMessage response;

        public Checkout(double amount, string number, string holder, int exp_year, int exp_month, string cvv)
        {
            payment = new Payment(amount, number, holder, exp_year, exp_month, cvv);
            CreateProductAsync(payment).Wait();
            var content = response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

        }

        static async Task CreateProductAsync(Payment payment)
        {
            var authData = string.Format("{0}:{1}", "technologines", "platformos");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://mock-payment-processor.appspot.com");
            HttpResponseMessage res = await client.PostAsJsonAsync("v1/payment", new JavaScriptSerializer().Serialize(payment));
            res.EnsureSuccessStatusCode();
            response = res;
        }

        public void sayHello()
        {
            Console.WriteLine("HELLO");
        }




        public void ExecutePayment(double amount, string number, string holder, int exp_year, int exp_month, string cvv)
        {
            var payment = new
            {
                amount = amount,
                number = number,
                holder = holder,
                exp_year = exp_year,
                exp_month = exp_month,
                cvv = cvv
            };
            var json = JsonConvert.SerializeObject(payment);
            /* Where do I get the API ..*/
        }
        

    }
}
     