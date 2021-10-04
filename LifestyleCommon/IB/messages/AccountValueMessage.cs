/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class AccountValueMessage 
    {
        public AccountValueMessage(string key, string value, string currency, string accountName)
        {
            Key = key;
            Value = value;
            Currency = currency;
            AccountName = accountName;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Currency { get; set; }

        public string AccountName { get; set; }
    }
}
