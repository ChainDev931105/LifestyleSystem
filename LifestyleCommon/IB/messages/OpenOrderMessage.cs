/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class OpenOrderMessage : OrderMessage
    {
        public OpenOrderMessage(int orderId, Contract contract, Order order, OrderState orderState)
        {
            OrderId = orderId;
            Contract = contract;
            Order = order;
            OrderState = orderState;
        }
        
        public Contract Contract { get; set; }

        public Order Order { get; set; }

        public OrderState OrderState { get; set; }
    }
}
