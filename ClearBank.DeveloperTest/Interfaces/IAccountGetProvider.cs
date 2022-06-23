using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Interfaces
{
    public interface IAccountGetProvider
    {
        Account GetAccountProvider(DataStoreType dataStoreType);
    }
}