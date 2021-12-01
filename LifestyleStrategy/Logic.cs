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
        public Action<int, double> _plot_indicator = new Action<int, double>((x, y) => { });
        public Action<string, double, string, string> _plot_pnt = new Action<string, double, string, string>((x, y, z, t) => { });
        public Func<double> _lots = new Func<double>(() => { return 0; });
        public Func<DateTime> _time = new Func<DateTime>(() => { return new DateTime(); });
        public Action<double, string> _order = new Action<double, string>((x, y) => { });
        public Func<string, string, string, string, double> _value = new Func<string, string, string, string, double>((x, y, z, t) => { return 0.0; });
        public Func<string, string, string, bool> _check = new Func<string, string, string, bool>((x, y, z) => { return false; });
        public Action<string, string> _set = new Action<string, string>((x, y) => { });

        public virtual void OnTick()
        {

        }
    }
}
