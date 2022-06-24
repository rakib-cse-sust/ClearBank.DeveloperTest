using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Logging;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly DataStoreType _dataStoreType;
        private readonly IPaymentSchemeProviderFactory _paymentSchemeProviderFactory;
        private readonly IAccountGetProviderFactory _accountGetProviderFactory;

        public PaymentService(
            IPaymentSchemeProviderFactory paymentSchemeProviderFactory,            
            IAccountGetProviderFactory accountGetProviderFactory,
            IConfigurationManager configurationManager,
            ILogger<PaymentService> logger
            )
        {
            _dataStoreType = configurationManager.DataStoreType;
            _paymentSchemeProviderFactory = paymentSchemeProviderFactory;
            _accountGetProviderFactory = accountGetProviderFactory;
            _logger = logger;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var paymentValidatorResult = new MakePaymentRequestValidator().Validate(request);

            if(!paymentValidatorResult.IsValid)
                return new MakePaymentResult() { 
                    Success = false, 
                    Error = new ErrorDetails() { 
                        IsException = false, 
                        ErrorMessage = paymentValidatorResult.ToString("~")
                    }
                };

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