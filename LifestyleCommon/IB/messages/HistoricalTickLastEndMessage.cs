/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class HistoricalTickLastEndMessage
    {
        public int ReqId { get; private set; }

        public HistoricalTickLastEndMessage(int reqId)
        {
            ReqId = reqId;
        }
    }
}
