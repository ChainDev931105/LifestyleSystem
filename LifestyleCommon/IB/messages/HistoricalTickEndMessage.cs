/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class HistoricalTickEndMessage
    {
        public int ReqId { get; private set; }

        public HistoricalTickEndMessage(int reqId)
        {
            ReqId = reqId;
        }
    }
}
