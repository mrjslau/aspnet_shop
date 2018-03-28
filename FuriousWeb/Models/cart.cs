using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuriousWeb.Models
{
    public class Cart
    {
        private Dictionary<string, string> CartProducts = new Dictionary<string, string>();

        public void Set(string productId, string quantity)
        {
            if (CartProducts.ContainsKey(productId))
            {
                CartProducts[productId] = quantity;
            }
            else
            {
                CartProducts.Add(productId, quantity);
            }
        }

        public string Get(string productId)
        {
            string result = null;

            if (CartProducts.ContainsKey(productId))
            {
                result = CartProducts[productId];
            }

            return result;
        }

        public Dictionary<string, string> GetDictionary()
        {
            return CartProducts;
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
