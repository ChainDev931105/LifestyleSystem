/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class TickSizeMessage : MarketDataMessage
    {
        public TickSizeMessage(int requestId, int field, int size) : base(requestId, field)
        {
            Size = size;
        }

        public int Size { get; set; }
    }
}
