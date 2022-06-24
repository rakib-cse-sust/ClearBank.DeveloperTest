using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
            _logger.LogInformation(string.Format("MakePayment - Request payload: {0}", JsonSerializer.Serialize(request)));

            var paymentValidatorResult = new MakePaymentRequestValidator().Validate(request);

            if (!paymentValidatorResult.IsValid)
            {
                var error = new ErrorDetails()
                {
                    IsException = false,
                    ErrorMessage = paymentValidatorResult.ToString("~")
                };

                _logger.LogError(string.Format("MakePayment - {0}", error.ErrorMessage));

                return new MakePaymentResult()
                {
                    Success = false,
                    Error = error
                };
            }

            try
            {
                var accountGetProvider = _accountGetProviderFactory.GetAccountProvider(_dataStoreType);
                var account = accountGetProvider.GetAccount(request.DebtorAccountNumber);

                var paymentProvider = _paymentSchemeProviderFactory.GetPaymentSchemeProvider(request.PaymentScheme);
                var result = paymentProvider.GetPaymentResult(account, request);

                if (result.Success)
                {
                    account.Balance -= request.Amount;

                    if (account.Balance < 0)
                    {
                        var error = new ErrorDetails()
                        {
                            IsException = false,
                            ErrorMessage = "Account balance can't be negetive"
                        };

                        _logger.LogError(string.Format("{0} - Balance {1}", error.ErrorMessage, account.Balance));

                        return new MakePaymentResult()
                        {
                            Success = false,
                            Error = error
                        };
                    }                        

                    var accountUpdateProvider = _accountGetProviderFactory.GetAccountProvider(_dataStoreType);
                    accountUpdateProvider.UpdateAccount(account);

                    _logger.LogInformation("MakePayment - successful");
                }
                else
                    _logger.LogInformation("MakePayment - unsuccessful");

                return result;
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, "MakePayment - Unexpected error.");
                throw;
            }
        }
    }
}