using System;
using System.Collections.Generic;

namespace EcommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Supplier { get; set; }

        //navigation properties

        public List<PurchaseHistory> PurchaseHistories { get; set; }

    }
}
