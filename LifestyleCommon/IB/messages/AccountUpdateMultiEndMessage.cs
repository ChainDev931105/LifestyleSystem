/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class AccountUpdateMultiEndMessage 
    {
        public AccountUpdateMultiEndMessage(int reqId)
        {
            ReqId = ReqId;
        }

        public int ReqId { get; set; }
    }
}
