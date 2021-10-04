/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using System.Collections.Generic;

namespace IBTradingSystem.Broker.IB.messages
{
    class ManagedAccountsMessage
    {
        public ManagedAccountsMessage(string managedAccounts)
        {
            ManagedAccounts = new List<string>(managedAccounts.Split(','));
        }

        public List<string> ManagedAccounts { get; set; }
    }
}
