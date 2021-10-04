/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class OrderBoundMessage
    {
        public long OrderId { get; private set; }
        public int ApiClientId { get; private set; }
        public int ApiOrderId { get; private set; }

        public OrderBoundMessage(long orderId, int apiClientId, int apiOrderId)
        {
            OrderId = orderId;
            ApiClientId = apiClientId;
            ApiOrderId = apiOrderId;
        }
    }
}
