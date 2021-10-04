/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class NewsProvidersMessage
    {
        public NewsProvider[] NewsProviders { get; private set; }

        public NewsProvidersMessage(NewsProvider[] newsProviders)
        {
            NewsProviders = newsProviders;
        }
    }
}
