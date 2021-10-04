/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class NewsArticleMessage
    {
        public int RequestId { get; private set; }
        public int ArticleType { get; private set; }
        public string ArticleText { get; private set; }

        public NewsArticleMessage(int requestId, int articleType, string articleText)
        {
            RequestId = requestId;
            ArticleType = articleType;
            ArticleText = articleText;
        }
    }
}
