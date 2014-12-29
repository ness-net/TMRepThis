using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Observer
{
    public class ObserverC:IObserver
    {
      public string ObserverEmail { get;private set; }
      public string ObserverPName { get; private set; }
      public ObserverC(string email, string pname)
      {
          this.ObserverEmail = email;
          this.ObserverPName = pname;
      }
      public void Update()
      {
          MailMessage mm = new MailMessage();
          mm.To.Add(this.ObserverEmail);
          
          mm.From = new MailAddress("glamazon.noreply@gmail.com");
          mm.Subject = "Product Stock";
          mm.Body = "Dear Seller, ";
          mm.Body += "<br/> Your Product ("+ this.ObserverPName +") has a low stock of below 3.";
          mm.Body += "<br/><br/>Yours faithfully,<br/> Traders Market";
          mm.IsBodyHtml = true;

          SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
          client.EnableSsl = true;
          client.UseDefaultCredentials = false;
          client.DeliveryMethod = SmtpDeliveryMethod.Network;
          client.Credentials = new NetworkCredential("glamazon.noreply@gmail.com", "noreply123");
          client.Send(mm);
      }
    }

    interface IObserver
    {
      void Update();
    }
}
