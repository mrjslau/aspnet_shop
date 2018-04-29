using FuriousWeb.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FuriousWeb.Business
{
    public class Products
    {

        public static List<DetailedProduct> LoadDetailedProducts(SqlConnection conn)
        {
            var products = new List<DetailedProduct>();

            using (SqlCommand command = conn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter())
            using (System.Data.DataTable dt = new System.Data.DataTable())
            {
                command.CommandText = "SELECT * FROM DetailedProducts";
                da.SelectCommand = command;
                da.Fill(dt);

                SqlDataReader reader = command.ExecuteReader();

                foreach (var row in dt.Rows)
                {
                    if (reader.Read())
                    {
                        var product = new DetailedProduct();

                        product.ProductId = (int)reader["ProductId"];
                        product.Code = reader["Code"].ToString();
                        product.Name = reader["Name"].ToString();
                        product.Description = reader["Description"].ToString();
                        product.WarehouseId = (int)reader["WarehouseId"];
                        product.Quantity = (long)reader["Quantity"];
                        product.Price = (double)reader["Price"];

                        products.Add(product);
                    }
                }

                reader.Close();
            }       

            return products;
        }
    }
}