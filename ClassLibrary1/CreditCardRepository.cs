using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonlayer;
using System.Data.Entity;
using System.Data.Objects;
using Commonlayer.Views;

namespace DataAccessLayer
{
    public class CreditCardRepository : ConnectionClass
    {
        public CreditCardRepository() : base() { }

        public void AddCreditCard(CreditCard myCreditCard)
        {
            Entity.CreditCards.Add(myCreditCard);
            Entity.SaveChanges();
        }      

        

        public CreditCard GetLCreditCard(decimal number)
        {
            return Entity.CreditCards.SingleOrDefault(c => c.CardNumber == number);
        }
    }
}
