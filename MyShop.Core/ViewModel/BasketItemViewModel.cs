using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModel
{
 public  class BasketItemViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string PrdouctName{ get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}
