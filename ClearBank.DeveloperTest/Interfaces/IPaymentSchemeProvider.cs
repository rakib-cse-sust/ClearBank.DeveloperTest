using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Interfaces
{
    public interface IPaymentSchemeProvider
    {
        MakePaymentResult GetPaymentResult(Account account, MakePaymentRequest request);
    }
}
