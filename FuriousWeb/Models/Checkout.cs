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
        public string Email { get; set; }
        public double Amount { get; set; }
        public string Card_number { get; set; }
        public string Card_holder { get; set; }
        public int Exp_year { get; set; }
        public int Exp_month { get; set; }
        public string Card_cvv { get; set; }

        private List<ShoppingCartItem> Items;
        private Payment payment;
        private string response;

        public bool InitPayment()
        {
            ShoppingCart shoppingCart = null;
            //Glebai please check
            //shoppingCart = (ShoppingCart)HttpContext.Session["shoppingCart"];
            this.Amount = shoppingCart.CalculatePrice();
            if (CallAPI())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CallAPI()
        {
            Payment payment = new Payment(Amount, Card_number, Card_holder, Exp_year, Exp_month, Card_cvv);
            WebClient client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("technologines:platformos"));
            client.Headers[HttpRequestHeader.Authorization] = "Basic "+ credentials;
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = new JavaScriptSerializer().Serialize(payment);
            try
            {
                var result = client.UploadString(new Uri("https://mock-payment-processor.appspot.com/v1/payment"), "POST", data);
                this.response = result;
                this.payment = new JavaScriptSerializer().Deserialize<Payment>(response);
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
            CredentialCache credentialCache = new CredentialCache
            {
                { new System.Uri(url), "Basic", new NetworkCredential(ConfigurationManager.AppSettings["technologines"], ConfigurationManager.AppSettings["platformos"]) }
            };
            return credentialCache;
        }



    }
}
     