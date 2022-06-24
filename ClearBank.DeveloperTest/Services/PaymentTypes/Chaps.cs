using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentTypes
{
    public class Chaps : IPaymentSchemeProvider
    {
        public MakePaymentResult GetPaymentResult(Account account, MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            if (account == null)
            {
                result.Success = false;
            }
            else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                result.Success = false;
            }
            else if (account.Status != AccountStatus.Live)
            {
                result.Success = false;
            }
            else
                result.Success = true;

            return result;
        }
    }
}
