using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Services.PaymentTypes;
using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentSchemeProviderFactory : IPaymentSchemeProviderFactory
    {
        public IPaymentSchemeProvider GetPaymentSchemeProvider(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.FasterPayments:
                    return new FasterPayments();
                case PaymentScheme.Bacs:
                    return new Bacs();
                case PaymentScheme.Chaps:
                    return new Chaps();
            }

            throw new ArgumentNullException(nameof(paymentScheme));
        }
    }
}