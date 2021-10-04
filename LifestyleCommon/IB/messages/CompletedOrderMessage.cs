/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class CompletedOrderMessage
    {
        public CompletedOrderMessage(Contract contract, Order order, OrderState orderState)
        {
            Contract = contract;
            Order = order;
            OrderState = orderState;
        }

        public Contract Contract { get; set; }

        public Order Order { get; set; }

        public OrderState OrderState { get; set; }
    }
}
