using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        private string response;

        public bool initPayment()
        {
            if (CallAPI())
            {
                //save payment;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CallAPI()
        {
            string url = @"https://mock-payment-processor.appspot.com/v1/payment";
            Payment payment = new Payment(200, card_number, card_holder, exp_year, exp_month, card_cvv);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("technologines:platformos"));
            client.Headers[HttpRequestHeader.Authorization] = "Basic "+ credentials;
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = new JavaScriptSerializer().Serialize(payment);
            try
            {
                var result = client.UploadString(new Uri("https://mock-payment-processor.appspot.com/v1/payment"), "POST", data);
                this.response = result;
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    Debug.WriteLine(response);
                    this.response = response;
                }
                else
                {
                    this.response = null;
                }
                return false;
            }
     

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
     