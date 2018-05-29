﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int PaymentID { get; set; }
        public int Status { get; set; }
        public string Created_at { get; set; }
    }
}