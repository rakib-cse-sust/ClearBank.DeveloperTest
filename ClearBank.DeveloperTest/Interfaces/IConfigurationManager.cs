using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Interfaces
{
    public interface IConfigurationManager
    {
        DataStoreType DataStoreType { get; set; }
    }    
}