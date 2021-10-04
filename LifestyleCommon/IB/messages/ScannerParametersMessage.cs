/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class ScannerParametersMessage
    {
        public ScannerParametersMessage(string data)
        {
            XmlData = data;
        }

        public string XmlData { get; set; }
    }
}
