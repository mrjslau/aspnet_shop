using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuriousWeb.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}