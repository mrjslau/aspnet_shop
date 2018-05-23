using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FuriousWeb.Models
{
    public class Checkout
    {
        [Required]
        public string email { get; set; }
        [Required]
        public double amount { get; set; }
        [Required]
        public string card_number { get; set; }
        [Required]
        public string card_holder { get; set; }
        [Required]
        public int exp_year { get; set; }
        [Required]
        public int exp_month { get; set; }
        [Required]
        public string card_cvv { get; set; }

        private List<ShoppingCartItem> Items;
        static HttpClient client;
        private Payment payment;
        private static HttpResponseMessage response;

        public bool initPayment()
        {
            var response = CallAPI();
            //var content = response.Content.ReadAsStringAsync();
            Console.WriteLine(response);
            return true;
        }

        public string CallAPI()
        {
            string url = @"https://mock-payment-processor.appspot.com/v1/payment";
            //Payment payment = new Payment(200, card_number, card_holder, exp_year, exp_month, card_cvv);
            //var client = new WebClient { Credentials = new NetworkCredential("technologines", "platformos") };

            WebClient client = new WebClient();
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("technologines:platformos"));
            client.Headers[HttpRequestHeader.Authorization] = "Basic dGVjaG5vbG9naW5lczpwbGF0Zm9ybW9z";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = "{amount: 100,  number: 4111111111111111,  holder: Vardenis Pavardenis,  exp_year: 2018,  exp_month: 9,  cvv: 123}";

            var result = client.UploadString(new Uri("https://mock-payment-processor.appspot.com/v1/payment"), "POST", data);
     
            return result;

        }

        private CredentialCache GetCredential()
        {
            string url = @"https://mock-payment-processor.appspot.com";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(ConfigurationManager.AppSettings["technologines"], ConfigurationManager.AppSettings["platformos"]));
            return credentialCache;
        }



    }
}
     