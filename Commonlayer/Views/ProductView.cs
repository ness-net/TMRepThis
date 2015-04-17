using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commonlayer.Views
{
    public class ProductView
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        public string ImageLink { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; }
        public bool? isActive { get; set; }
        public string SubC { get; set; }
    }
}
