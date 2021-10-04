/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class HistogramDataMessage
    {
        public int ReqId { get; private set; }
        public IBApi.HistogramEntry[] Data { get; private set; }

        public HistogramDataMessage(int reqId, IBApi.HistogramEntry[] data)
        {
            ReqId = reqId;
            Data = data;
        }
    }
}
