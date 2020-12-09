using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
   public class BasketItem:BaseEntity
    {
        [ForeignKey("BasketId")]
        public Basket Basket { get; set; }
        public string BasketId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
