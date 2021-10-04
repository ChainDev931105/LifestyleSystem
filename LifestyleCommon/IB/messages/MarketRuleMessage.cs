/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    public class MarketRuleMessage
    {
        public int MarketruleId { get; private set; }
        public PriceIncrement[] PriceIncrements { get; private set; }

        public MarketRuleMessage(int marketRuleId, PriceIncrement[] priceIncrements)
        {
            MarketruleId = marketRuleId;
            PriceIncrements = priceIncrements;
        }
    }
}
