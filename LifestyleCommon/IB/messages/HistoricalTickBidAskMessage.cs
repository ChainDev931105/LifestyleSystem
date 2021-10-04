/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class HistoricalTickBidAskMessage
    {
        public int ReqId { get; set; }
        public long Time { get; set; }
        public TickAttribBidAsk TickAttribBidAsk { get; set; }
        public double PriceBid { get; set; }
        public double PriceAsk { get; set; }
        public long SizeBid { get; set; }
        public long SizeAsk { get; set; }

        public HistoricalTickBidAskMessage(int reqId, long time, TickAttribBidAsk tickAttribBidAsk, double priceBid, double priceAsk, long sizeBid, long sizeAsk)
        {
            ReqId = reqId;
            Time = time;
            TickAttribBidAsk = tickAttribBidAsk;
            PriceBid = priceBid;
            PriceAsk = priceAsk;
            SizeBid = sizeBid;
            SizeAsk = sizeAsk;
        }
    }
}
