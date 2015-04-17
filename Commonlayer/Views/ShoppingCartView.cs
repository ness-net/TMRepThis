using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commonlayer.Views
{
    public class ShoppingCartView
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageLink { get; set; }
        public string paykey { get; set; }
    }
}
