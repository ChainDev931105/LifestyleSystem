using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleStrategy
{
    public class Logic
    {
        public const int INDICATOR_COUNT = 4;
        public string[] ALL_TIMEFRAMES = { "M1", "M2", "M3", "M4", "M5", "M6", "M8", "M9", "M10", "M12", "M14", "M15", "M16", "M18", "M20", "M24", "M25", "M30", "M32", "M36", "M40", "M45", "M48", "M50", "M60", "M72", "M80", "M90", "M96", "M100", "M120", "M144", "M150", "M160", "M180", "M200", "M225", "M240", "M288", "M300", "M360", "M400", "M450", "M540", "M600", "M720", "M800", "M900", "H18", "H36", "DAILY", "2DAY", "3DAY", "4DAY", "5DAY", "7DAY", "8DAY", "10DAY", "14DAY", "2WEEK", "4WEEK", "1MONTH", "2MONTH", "3MONTH", "4MONTH", "6MONTH", "1YEAR" };

        public Action<int, double> _plot_indicator = new Action<int, double>((x, y) => { });
        public Action<string, double, string, string> _plot_pnt = new Action<string, double, string, string>((x, y, z, t) => { });
        public Action<string, double, string, string, string> _plot_pnt_past = new Action<string, double, string, string, string>((x, y, z, t, u) => { });
        public Func<double> _lots = new Func<double>(() => { return 0; });
        public Func<DateTime> _time = new Func<DateTime>(() => { return new DateTime(); });
        public Action<double, string> _order = new Action<double, string>((x, y) => { });

        // sTimeFrame, sPattern, sShift
        public Func<string, string, string, bool> _check = new Func<string, string, string, bool>((x, y, z) => { return false; });

        // sTF, sPattern, c, sShift
        public Func<string, string, string, string, double> _value = new Func<string, string, string, string, double>((x, y, z, t) => { return 0.0; });

        // sTF, sPattern, nShift
        public Func<string, string, int, int> _find = new Func<string, string, int, int>((x, y, z) => { return 0; });

        public Action<string, string> _set = new Action<string, string>((x, y) => { });

        public Action<string, string, double, double> _print_to_table = new Action<string, string, double, double>((x, y, z, t) => { });

        public virtual void OnTick()
        {

        }
    }
}
