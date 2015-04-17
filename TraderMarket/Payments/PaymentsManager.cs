using System.Collections.Generic;
using System.Configuration;
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;

namespace TraderMarket.Payments
{
    public class PaymentsManager: IPaymentsManager
    {
        public string Pay(string senderEmail, string receiverEmail, decimal amount, string returnUrl, string cancelUrl)
        {
            var service = new AdaptivePaymentsService();
            var request = new PayRequest(new RequestEnvelope("en_US"),
                "PAY",
                cancelUrl,
                "EUR",
                new ReceiverList(new List<Receiver>()
                                     {
                                         new Receiver() { email = receiverEmail, amount = amount }
                                     }), returnUrl) { senderEmail = senderEmail };
            
            var response = service.Pay(request);
            var responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.responseEnvelope.ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.responseEnvelope.ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"] + "_ap-payment&paykey=" + response.payKey;
            }

            return redirectUrl;
        }
    }
}