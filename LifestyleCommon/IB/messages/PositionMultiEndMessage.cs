/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class PositionMultiEndMessage 
    {
        public PositionMultiEndMessage(int reqId)
        {
            ReqId = reqId;
        }

        public int ReqId { get; set; }
    }
}
