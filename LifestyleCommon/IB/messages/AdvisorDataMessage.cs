/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

namespace IBTradingSystem.Broker.IB.messages
{
    class AdvisorDataMessage 
    {
        public AdvisorDataMessage(int faDataType, string data)
        {
            FaDataType = faDataType;
            Data = data;
        }

        public int FaDataType { get; set; }

        public string Data { get; set; }
    }
}
