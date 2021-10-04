using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleStrategy
{
    class Logic0 : Logic
    {
        public override void OnTick()
        {
            double a = (_value("M480", "A1", "H", "2") + _value("M240", "A1", "C", "2") + _value("M45", "B1", "H", "0")) / 3;
            _plot_indicator(0, a);

            double b = (_value("M30", "A1", "C", "2") + 0.1) / 3;
            _plot_indicator(1, b);

            if (_check("D1", "A2") && _check("H4", "A2")) _set("H4", "aBa2");

            if (_time().DayOfWeek == DayOfWeek.Tuesday)
            {
                if (_time().Hour >= 17 && _time().Hour <= 18)
                {
                    if (_lots() == 1)
                    {
                        _order(1, "BUY");
                    }
                }
            }
        }
    }
}