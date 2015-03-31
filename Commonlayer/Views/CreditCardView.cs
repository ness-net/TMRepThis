using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commonlayer.Views
{
    public class CreditCardView
    {
        public string CardT { get; set; }
        public decimal Number { get; set; }
        public string CardOwner { get; set; }
        public string CVV { get; set; }
        public string email { get; set; }
    }
}
