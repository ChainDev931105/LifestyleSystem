/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class ConnectionStatusMessage
    {
        public bool IsConnected { get; }

        public ConnectionStatusMessage(bool isConnected)
        {
            IsConnected = isConnected;
        }

        
    }
}
