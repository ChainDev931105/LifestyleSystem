/* IBTradingSystem. IB Client.
 * Get all data from IB gateway.. */

using IBApi;

namespace IBTradingSystem.Broker.IB.messages
{
    class FamilyCodesMessage
    {
        public FamilyCode[] FamilyCodes { get; private set; }

        public FamilyCodesMessage(FamilyCode[] familyCodes)
        {
            FamilyCodes = familyCodes;
        }
    }
}
