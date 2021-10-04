/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class HistoricalTickLastMessage
    {
        public int ReqId { get; private set; }
        public long Time { get; private set; }
        public TickAttribLast TickAttribLast { get; private set; }
        public double Price { get; private set; }
        public long Size { get; private set; }
        public string Exchange { get; private set; }
        public string SpecialConditions { get; private set; }

        public HistoricalTickLastMessage(int reqId, long time, TickAttribLast tickAttribLast, double price, long size, string exchange, string specialConditions)
        {
            ReqId = reqId;
            Time = time;
            TickAttribLast = tickAttribLast;
            Price = price;
            Size = size;
            Exchange = exchange;
            SpecialConditions = specialConditions;
        }
    }
}
