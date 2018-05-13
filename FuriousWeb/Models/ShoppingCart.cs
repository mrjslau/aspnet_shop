using System.Collections.Generic;
using System.Linq;

namespace FuriousWeb.Models
{
    public class ShoppingCart
    {
        private List<ShoppingCartItem> Items;

        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public void Add(int productId, long quantity)
        {
            using (var db = new Data.DatabaseContext())
            {
                ShoppingCartItem cartItem = Items.SingleOrDefault(item => item.Product.Id == productId);

                if (cartItem == null)
                {
                    Product productToAdd = db.Products.Find(productId);

                    cartItem = new ShoppingCartItem();
                    cartItem.Product = productToAdd;
                    cartItem.Quantity = quantity;
                    Items.Add(cartItem);
                }
                else
                    cartItem.Quantity += quantity;
            }
        }

        public void Remove(int productId)
        {
            ShoppingCartItem itemToDelete = GetItem(productId);

            if (itemToDelete != null)
                Items.Remove(itemToDelete);
            else
                throw new System.Exception("Prekės {productId} krėpšelyje nėra!"); //geriau išmest CustomException'ą
        }

        public void EditQuantity(int productId, long quantity)
        {
            ShoppingCartItem cartItem = GetItem(productId);

            if (cartItem != null)
                cartItem.Quantity = quantity;
            else
                throw new System.Exception("Prekės {productId} krėpšelyje nėra!"); //geriau išmest CustomException'ą
        }

        public ShoppingCartItem GetItem(int productId)
        {
            return Items.SingleOrDefault(item => item.Product.Id == productId);
        }

        public List<ShoppingCartItem> GetItems()
        {
            return Items;
        }

        public long CountItems()
        {
            long itemsCount = 0;
            foreach (var item in Items)
                itemsCount += item.Quantity;

            return itemsCount;
        }
    }
}
        #region Iškelti
        //public void ExecutePayment(double amount, string number, string holder, int exp_year, int exp_month, string cvv)
        //{
        //    var payment = new
        //    {
        //        amount = amount,
        //        number = number,
        //        holder = holder,
        //        exp_year = exp_year,
        //        exp_month = exp_month,
        //        cvv = cvv
        //    };
        //    var json = JsonConvert.SerializeObject(payment);
        //    /* Where do I get the API ..*/
        //}

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    //public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    //{
    //    bool Active = true;

    //    public BasicAuthenticationFilter()
    //    { }
    //    public BasicAuthenticationFilter(bool active)
    //    {
    //        Active = active;
    //    }


    //    public override void OnAuthorization(HttpActionContext actionContext)
    //    {
    //        if (Active)
    //        {
    //            var identity = ParseAuthorizationHeader(actionContext);
    //            if (identity == null)
    //            {
    //                Challenge(actionContext);
    //                return;
    //            }


    //            if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
    //            {
    //                Challenge(actionContext);
    //                return;
    //            }

    //            var principal = new GenericPrincipal(identity, null);

    //            Thread.CurrentPrincipal = principal;
    //            base.OnAuthorization(actionContext);
    //        }
    //    }

    //    protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
    //    {
    //        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    //            return false;

    //        return true;
    //    }

    //    protected virtual BasicAuthenticationIdentity ParseAuthorizationHeader(HttpActionContext actionContext)
    //    {
    //        string authHeader = null;
    //        var auth = actionContext.Request.Headers.Authorization;
    //        if (auth != null && auth.Scheme == "Basic")
    //            authHeader = auth.Parameter;

    //        if (string.IsNullOrEmpty(authHeader))
    //            return null;

    //        authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

    //        var tokens = authHeader.Split(':');
    //        if (tokens.Length < 2)
    //            return null;

    //        return new BasicAuthenticationIdentity(tokens[0], tokens[1]);
    //    }

    //    void Challenge(HttpActionContext actionContext)
    //    {
    //        var host = actionContext.Request.RequestUri.DnsSafeHost;
    //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
    //        actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
    //    }
    //}
    #endregion
