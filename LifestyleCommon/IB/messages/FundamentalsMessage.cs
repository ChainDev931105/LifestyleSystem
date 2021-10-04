/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class FundamentalsMessage
    {
        public FundamentalsMessage(string data)
        {
            Data = data;
        }

        public string Data { get; set; }
    }
}
