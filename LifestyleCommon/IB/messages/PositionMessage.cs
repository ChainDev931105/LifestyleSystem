/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class PositionMessage 
    {
        public PositionMessage(string account, Contract contract, double pos, double avgCost)
        {
            Account = account;
            Contract = contract;
            Position = pos;
            AverageCost = avgCost;
        }

        public string Account { get; set; }

        public Contract Contract { get; set; }

        public double Position { get; set; }

        public double AverageCost { get; set; }
    }
}
