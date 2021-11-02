using System;
using System.Collections.Generic;

namespace EcommerceAPI.Models
{
    public class ProductHistory
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePurchased { get; set; }

        //Navigation Properties
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public List<Product> Products { get; set; }

    }
}


