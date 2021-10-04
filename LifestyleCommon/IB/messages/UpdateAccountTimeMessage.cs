/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class UpdateAccountTimeMessage
    {
        public UpdateAccountTimeMessage(string timestamp)
        {
            Timestamp = timestamp;
        }

        public string Timestamp { get; set; }
    }
}
