using ClearBank.DeveloperTest.Interfaces;

namespace ClearBank.DeveloperTest.Types
{
    public class ConfigurationManager : IConfigurationManager
    {
        private DataStoreType _dataStoreType;

        public DataStoreType DataStoreType
        {
            get
            {
                return this._dataStoreType;
            }
            set
            {
                this._dataStoreType = value;
            }
        }
    }
}
