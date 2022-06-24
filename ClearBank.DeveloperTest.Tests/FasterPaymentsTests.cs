﻿using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Services.PaymentTypes;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class FasterPaymentsTests
    {
        [Fact]
        public void FasterPayments_ReturnPaymentResult_Success()
        {
            var request = new MakePaymentRequest()
            {
                Amount = 500,
                DebtorAccountNumber = "AB1234",
                PaymentScheme = PaymentScheme.FasterPayments
            };

            var account = new Account()
            {
                AccountNumber = "AB1234",
                Status = AccountStatus.Live,
                Balance = 1500,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            IPaymentSchemeProvider provider = new FasterPayments();

            var result = provider.GetPaymentResult(account, request);

            Assert.True(result.Success);
        }
    }
}
