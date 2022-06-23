using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Interfaces
{
    public interface IPaymentSchemeProviderFactory
    {
        IPaymentSchemeProvider GetPaymentSchemeProvider(PaymentScheme paymentScheme);
    }
}