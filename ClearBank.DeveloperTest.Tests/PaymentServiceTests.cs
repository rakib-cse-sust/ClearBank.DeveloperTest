using System;
using Xunit;
using Moq;
using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        public PaymentServiceTests()
        {
        }

        [Fact]
        public void MakeBackupAccountPayment_ReturnPaymentResult_WhenPaymentResultSuccess()
        {
            IConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.DataStoreType = DataStoreType.BackupAccount;

            // Arrange


            // Act


            // Assert

        }
    }
}
