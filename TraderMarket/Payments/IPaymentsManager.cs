namespace TraderMarket.Payments
{
    public interface IPaymentsManager
    {
        string Pay(string senderEmail, string receiverEmail, decimal amount, string returnUrl, string cancelUrl);
    }
}