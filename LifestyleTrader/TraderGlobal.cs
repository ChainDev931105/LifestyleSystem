using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleTrader
{
    class TraderGlobal
    {
        // size
        public const int MAX_LOG_DISPLAY_LENGTH = 2048;
    }

    enum RUN_MODE
    {
        NONE = 0,
        BACKTEST = 1,
        REAL_TRADE = 2,
        MERGE_MODE = 3 // backtest + real_trade
    }
}
