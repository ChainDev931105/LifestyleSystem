/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class SoftDollarTiersMessage
    {
        public int ReqId { get; private set; }
        public IBApi.SoftDollarTier[] Tiers { get; private set; }

        public SoftDollarTiersMessage(int reqId, IBApi.SoftDollarTier[] tiers)
        {
            ReqId = reqId;
            Tiers = tiers;
        }
    }
}
