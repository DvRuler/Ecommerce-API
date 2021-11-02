using System.Collections.Generic;

namespace EcommerceAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        //Navigation Properties
        public int UserId { get; set; }
        public User User { get; set; }


        public List<PurchaseHistory>? ProductHistories { get; set; }

     
    }
}
