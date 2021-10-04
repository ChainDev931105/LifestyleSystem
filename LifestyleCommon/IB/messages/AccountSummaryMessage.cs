/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class AccountSummaryMessage
    {
        public int RequestId { get; set; }

        public string Account { get; set; }

        public string Tag { get; set; }

        public string Value { get; set; }

        public string Currency { get; set; }

        public AccountSummaryMessage(int requestId, string account, string tag, string value, string currency)
        {
            RequestId = requestId;
            Account = account;
            Tag = tag;
            Value = value;
            Currency = currency;
        }
    }
}
