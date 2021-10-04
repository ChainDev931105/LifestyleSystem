/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class MktDepthExchangesMessage
    {
        public IBApi.DepthMktDataDescription[] Descriptions { get; private set; }

        public MktDepthExchangesMessage(IBApi.DepthMktDataDescription[] descriptions)
        {
            Descriptions = descriptions;
        }
    }
}
