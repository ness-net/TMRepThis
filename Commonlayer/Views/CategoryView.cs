using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commonlayer.Views
{
    public class CategoryView
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public System.Nullable<int> ParentID { get; set; }
    }
}
