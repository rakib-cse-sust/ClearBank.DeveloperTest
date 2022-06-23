using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Interfaces
{
    public interface IAccountGetProviderFactory
    {
        IAccountDataStore GetAccountProvider(DataStoreType dataStoreType);
    }
}   