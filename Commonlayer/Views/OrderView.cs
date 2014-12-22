using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commonlayer.Views
{
    public class OrderView
    {
        public int OrderID { get; set; }
        public string Date { get; set; }
        public string Username { get; set; }
        public System.Nullable<int> OrderStatus { get; set; }
        public string UsernameSeller { get; set; }
    }
}
