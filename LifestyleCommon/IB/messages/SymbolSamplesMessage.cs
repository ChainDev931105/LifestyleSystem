/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class SymbolSamplesMessage
    {
        public int ReqId { get; private set; }
        public ContractDescription[] ContractDescriptions { get; private set; }

        public SymbolSamplesMessage(int reqId, ContractDescription[] contractDescriptions)
        {
            ReqId = reqId;
            ContractDescriptions = contractDescriptions;
        }
    }
}
