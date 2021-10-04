/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    public abstract class OrderMessage
    {
        protected int orderId;

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
    }
}
