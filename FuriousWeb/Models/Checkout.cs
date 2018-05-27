using FuriousWeb.Data;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace FuriousWeb.Models
{
    public class Checkout
    {
        public string Email { get; set; }
        public int Amount { get; set; }
        public string Card_number { get; set; }
        public string Card_holder { get; set; }
        public int Exp_year { get; set; }
        public int Exp_month { get; set; }
        public string Card_cvv { get; set; }
        public string User { get; set; }

        public ShoppingCart Cart;
        public PaymentInfo paymentInfo;
        public PaymentError paymentErr;
        private string response;

        public bool InitPayment(ShoppingCart shoppingCart)
        {
            this.Cart = shoppingCart;
            this.Amount = Convert.ToInt32(shoppingCart.CalculatePrice()*100);

            if (CallAPI())
            {
                return true;
            else
            {
                return false;
            }
        }

        public bool CallAPI()
        {
            PaymentInfo paymentInfo = new PaymentInfo(Amount, Card_number, Card_holder, Exp_year, Exp_month, Card_cvv);
            WebClient client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("technologines:platformos"));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = new JavaScriptSerializer().Serialize(paymentInfo);
            try
            {
                var result = client.UploadString(new Uri("https://mock-payment-processor.appspot.com/v1/payment"), "POST", data);
                this.response = result;
                this.paymentInfo = new JavaScriptSerializer().Deserialize<PaymentInfo>(response);
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    Debug.WriteLine(response);
                    this.paymentErr = new JavaScriptSerializer().Deserialize<PaymentError>(response);
                    this.response = response;
                }
                else
                {
                    this.response = null;
                }
                return false;
            }
        }
    }

    public class PaymentInfo
    {
        public int Amount { get; set; }
        public string Number { get; set; }
        public string Holder { get; set; }
        public int Exp_year { get; set; }
        public int Exp_month { get; set; }
        public string Cvv { get; set; }
        public string Created_at { get; set; }
        public string Id { get; set; }

        public PaymentInfo(int amount, string number, string holder, int exp_year, int exp_month, string cvv)
        {
            this.Amount = amount;
            this.Number = number;
            this.Holder = holder;
            this.Exp_year = exp_year;
            this.Exp_month = exp_month;
            this.Cvv = cvv;
        }

        public PaymentInfo() {}
    }

    public class PaymentError
    {
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
     