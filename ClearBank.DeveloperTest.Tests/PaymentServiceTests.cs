using Xunit;
using Moq;
using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Logging;
using ClearBank.DeveloperTest.Services;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private readonly IConfigurationManager _configurationManager;

        public PaymentServiceTests()
        {
            _configurationManager = new ConfigurationManager();
            _configurationManager.DataStoreType = DataStoreType.BackupAccount;
        }

        [Fact]
        public void MakeBackupAccountPayment_ReturnPaymentResult_WhenPaymentResultSuccess()
        {
            // Arrange

            var request = new MakePaymentRequest() 
            { 
                Amount = 500,
                DebtorAccountNumber = "AB1234",
                PaymentScheme = PaymentScheme.Bacs
            };

            var account = new Account()
            {
                AccountNumber = "AB1234",
                Status = AccountStatus.Live,
                Balance = 1500
            };

            Mock<IAccountGetProviderFactory> _accountMock = new Mock<IAccountGetProviderFactory>();
            Mock<IPaymentSchemeProviderFactory> _paymentMock = new Mock<IPaymentSchemeProviderFactory>();
            Mock<ILogger<PaymentService>> _loggerMock = new Mock<ILogger<PaymentService>>();

            var sut = new PaymentService(_paymentMock.Object, _accountMock.Object, _configurationManager, _loggerMock.Object);

            // Act

            _accountMock.Setup(X => X.GetAccountProvider(_configurationManager.DataStoreType)
            .GetAccount(request.DebtorAccountNumber)).Returns(account);

            _paymentMock.Setup(X => X.GetPaymentSchemeProvider(request.PaymentScheme)
            .GetPaymentResult(account, request)).Returns(new MakePaymentResult()
            {
                Success = true
            });

            var result = sut.MakePayment(request).Success;

            // Assert

            Assert.True(result);
        }

        [Fact]
        public void MakeBackupAccountPayment_ReturnPaymentResult_WhenPaymentRequestInvalid()
        {
            // Arrange

            var request = new MakePaymentRequest()
            {
                Amount = 0,
                DebtorAccountNumber = "AB1234",
                PaymentScheme = PaymentScheme.Bacs
            };

            var account = new Account()
            {
                AccountNumber = "AB1234",
                Status = AccountStatus.Live,
                Balance = 1500
            };

            Mock<IAccountGetProviderFactory> _accountMock = new Mock<IAccountGetProviderFactory>();
            Mock<IPaymentSchemeProviderFactory> _paymentMock = new Mock<IPaymentSchemeProviderFactory>();
            Mock<ILogger<PaymentService>> _loggerMock = new Mock<ILogger<PaymentService>>();

            var sut = new PaymentService(_paymentMock.Object, _accountMock.Object, _configurationManager, _loggerMock.Object);

            // Act

            var result = sut.MakePayment(request).Success;

            // Assert

            Assert.True(result);
        }
    }
}