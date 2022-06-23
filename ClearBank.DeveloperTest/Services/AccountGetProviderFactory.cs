using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountGetProviderFactory : IAccountGetProviderFactory
    {
        public IAccountDataStore GetAccountProvider(DataStoreType dataStoreType)
        {
            switch (dataStoreType)
            {
                case DataStoreType.Account:
                    return new AccountDataStore();
                case DataStoreType.BackupAccount:
                    return new BackupAccountDataStore();
            }

            throw new ArgumentNullException(nameof(dataStoreType));
        }
    }
}