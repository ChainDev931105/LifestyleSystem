using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleStrategy
{
    public class Logic0 : Logic
    {/*
        public override void OnTick()
        {
            double a = (_value("M480", "A1", "H", "2") + _value("M240", "A1", "C", "2") + _value("M45", "B1", "H", "0")) / 3;
            _plot_indicator(1, a);

            double b = (_value("M30", "A1", "C", "2") + 0.1) / 3;
            _plot_indicator(2, b);

            if (_check("D1", "A2") && _check("H4", "A2")) _set("H4", "aBa2");

            if (_check("M5", "A1")) _plot_pnt("Pattern", _value("M5", "", "C", "0"), "aBa2");

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
        */

        public override void OnTick()
        {
            //double a = (value("M480", "A1", "H", "2") + value("M240", "A1", "C", "2") + _value("M45", "B1", "H", "0")) / 3;
            //_plot_indicator(1, a);

            double b = (_value("M30", "A1", "C", "2") + 0.1) / 3;
            _plot_indicator(2, b);

            //if (check("D1", "A2") && check("H4", "A2")) _set("H4", "aBa2");

            if (_check("M5", "A1"))
            {
                _plot_pnt("Pattern", _value("M240", "", "C", "0"), "Blobby", "M15");
            }

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