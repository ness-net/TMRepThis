using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonlayer;
using System.Data.Entity;
using System.Data.Objects;

namespace DataAccessLayer
{
    public class CommissionRepository : ConnectionClass
    {
        public CommissionRepository() : base() { }

        //public void AllocateCommission(User user, Commission comm)
        //{
        //    user.Commissions.Add(comm);
        //    Entity.SaveChanges();

        //}       

        //public Commission GetComm(int commid)
        //{
        //    return Entity.Commissions.SingleOrDefault(c => c.CommissionID == commid);
        //}
    }
}
