using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

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

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        bool Active = true;

        public BasicAuthenticationFilter()
        { }
        public BasicAuthenticationFilter(bool active)
        {
            Active = active;
        }

        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Active)
            {
                var identity = ParseAuthorizationHeader(actionContext);
                if (identity == null)
                {
                    Challenge(actionContext);
                    return;
                }


                if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
                {
                    Challenge(actionContext);
                    return;
                }

                var principal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = principal;
                base.OnAuthorization(actionContext);
            }
        }
        
        protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            return true;
        }
        
        protected virtual BasicAuthenticationIdentity ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;
            if (auth != null && auth.Scheme == "Basic")
                authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader))
                return null;

            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            var tokens = authHeader.Split(':');
            if (tokens.Length < 2)
                return null;

            return new BasicAuthenticationIdentity(tokens[0], tokens[1]);
        }

        void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        }

    }
}
