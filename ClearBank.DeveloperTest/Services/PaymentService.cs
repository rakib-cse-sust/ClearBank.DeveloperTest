using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly DataStoreType _dataStoreType;
        private readonly IPaymentSchemeProviderFactory _paymentSchemeProviderFactory;
        private readonly IAccountGetProviderFactory _accountGetProviderFactory;

        public PaymentService(
            IPaymentSchemeProviderFactory paymentSchemeProviderFactory,
            IConfigurationManager configurationManager,
            IAccountGetProviderFactory accountGetProviderFactory
            )
        {
            _dataStoreType = configurationManager.DataStoreType;
            _paymentSchemeProviderFactory = paymentSchemeProviderFactory;
            _accountGetProviderFactory = accountGetProviderFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountGetProvider = _accountGetProviderFactory.GetAccountProvider(_dataStoreType);
            var account = accountGetProvider.GetAccount(request.DebtorAccountNumber);

            var paymentProvider = _paymentSchemeProviderFactory.GetPaymentSchemeProvider(request.PaymentScheme);
            var result = paymentProvider.GetPaymentResult(account, request);

            if (result.Success)
            {
                account.Balance -= request.Amount;

                var accountUpdateProvider = _accountGetProviderFactory.GetAccountProvider(_dataStoreType);
                accountUpdateProvider.UpdateAccount(account);
            }

            return result;
        }
    }
}