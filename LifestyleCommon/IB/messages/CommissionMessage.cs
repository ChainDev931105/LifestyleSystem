/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class CommissionMessage
    {
        public CommissionMessage(CommissionReport commissionReport)
        {
            CommissionReport = commissionReport;
        }

        public CommissionReport CommissionReport { get; set; }
    }
}
