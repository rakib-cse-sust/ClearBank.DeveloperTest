using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentTypes
{
    public class Bacs : IPaymentSchemeProvider
    {
        public MakePaymentResult GetPaymentResult(Account account, MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            if (account == null)
            {
                result.Success = false;
            }
            else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                result.Success = false;
            }

            return result;
        }
    }
}
