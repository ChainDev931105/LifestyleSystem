/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class ExecutionMessage
    {
        public ExecutionMessage(int reqId, Contract contract, Execution execution)
        {
            ReqId = reqId;
            Contract = contract;
            Execution = execution;
        }

        public Contract Contract { get; set; }

        public Execution Execution { get; set; }

        public int ReqId { get; set; }
    }
}
