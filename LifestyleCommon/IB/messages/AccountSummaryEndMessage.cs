/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class AccountSummaryEndMessage 
    {
        public AccountSummaryEndMessage(int requestId)
        {
            RequestId = requestId;
        }

        public int RequestId { get; set; }
    }
}
