/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class AccountDownloadEndMessage
    {
        public AccountDownloadEndMessage(string account)
        {
            Account = account;
        }

        public string Account { get; set; }
    }
}
