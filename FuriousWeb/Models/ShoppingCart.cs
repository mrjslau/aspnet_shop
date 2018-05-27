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

        public void Clear()
        {
            Items.Clear();
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

        public double CalculatePrice()
        {
            double total = 0;
            if (Items.Count > 0)
            {
                foreach (ShoppingCartItem item in Items)
                {
                    total += item.Product.Price * item.Quantity;
                }
            }
            return total;
        }
    }
}
     