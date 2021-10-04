/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class ContractDetailsMessage
    {
        public ContractDetailsMessage(int requestId, ContractDetails contractDetails)
        {
            RequestId = requestId;
            ContractDetails = contractDetails;
        }

        public ContractDetails ContractDetails { get; set; }

        public int RequestId { get; set; }
    }
}
