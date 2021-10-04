/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class HistoricalNewsEndMessage
    {
        public int RequestId { get; private set; }
        public bool HasMore { get; private set; }

        public HistoricalNewsEndMessage(int requestId, bool hasMore)
        {
            RequestId = requestId;
            HasMore = hasMore;
        }
    }
}
