using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleStrategy
{
    public class Logic0 : Logic
    {
        public override void OnTick()
        {
			if (_check("M5", "D1", "0") && _check("M5", "D2", "1")) _set("M5", "UT");
			//if (_check("M5", "UT", "0") && ...)

			if (_time().DayOfWeek == DayOfWeek.Monday)
			{
				if (_check("D1", "A1", "0") && _check("H4", "A1", "0")) _set("H4", "aaa1");
				if (_check("D1", "A1", "0") && _check("H4", "A2", "0")) _set("H4", "aaa2");
				if ((_time().Hour >= 16 && _time().Hour < 20) && _check("H4", "aaa2", "0") && _check("H1", "A1", "0"))
				{
					_set("24060M", "bxb1");
				}
				if ((_time().Hour >= 16 && _time().Hour < 20) && _check("H4", "aaa2", "0") && _check("H1", "B2", "0"))
				{
					_set("24060M", "bcb1");
				}
			}
			if (_time().DayOfWeek == DayOfWeek.Tuesday)
            {

            }
			if (_time().Hour >= 16 && _time().Hour < 18)
			{
				if (_check("M5", "A1", "0") &&
					(_value("M10", "", "C", "3") < _value("M5", "", "C", "")) &&
					(_value("M5", "", "C", "3") < _value("M10", "", "C", "")))
				{
					_order(1, "BUY");
				}
				if (_check("M5", "A1", "0") &&
					(_value("M10", "", "C", "3") < _value("M5", "", "C", "")) &&
					(_value("M5", "", "C", "3") > _value("M10", "", "C", "")))
				{
					_order(1, "SELLCLOSE");
				}
				if (_check("M5", "A2", "0") &&
					(_value("M10", "", "C", "3") > _value("M5", "", "C", "")) &&
					(_value("M5", "", "C", "3") < _value("M10", "", "C", "")))
				{
					_order(1, "BUYCLOSE");
				}
				if (_check("M5", "A2", "0") &&
					(_value("M10", "", "C", "3") > _value("M5", "", "C", "")) &&
					(_value("M5", "", "C", "3") > _value("M10", "", "C", "")))
				{
					_order(1, "SELL");
				}
			}
        }
    }
}
/*
    "state_formula" :
	[
		{
			"DayOfWeek" : "Monday",
			"children" :
			[
				{ "D1" : "A1", "H4" : "A1", "set" : "H4 aaa1" },
				{ "D1" : "A1", "H4" : "A2", "set" : "H4 aaa2" },
				{ "Time" : "16:00 20:00", "H4" : "aaa2", "H1" : "A1", "set" : "24060M bxb1" },
				{ "Time" : "16:00 20:00", "H4" : "aaa2", "H1" : "B2", "set" : "24060M bcb1" },
			]
		},
		{
			"DayOfWeek" : "Tuesday"
		},
		{
			"Time" : "16:00 18:00",
			"children" :
			[
				{ "M5" : "A1", "cmp" : [ "M10", "", "C", "3", "<", "M5", "", "C", "" ], "cmp" : [ "M5", "", "C", "3", "<", "M10", "", "C", "" ], "order" : "BUY 1" },
				{ "M5" : "A1", "cmp" : [ "M10", "", "C", "3", "<", "M5", "", "C", "" ], "cmp" : [ "M5", "", "C", "3", ">", "M10", "", "C", "" ], "order" : "SELLCLOSE 1" },
				{ "M5" : "A2", "cmp" : [ "M10", "", "C", "3", ">", "M5", "", "C", "" ], "cmp" : [ "M5", "", "C", "3", "<", "M10", "", "C", "" ], "order" : "BUYCLOSE 1" },
				{ "M5" : "A2", "cmp" : [ "M10", "", "C", "3", ">", "M5", "", "C", "" ], "cmp" : [ "M5", "", "C", "3", ">", "M10", "", "C", "" ], "order" : "SELL 1" }
			]
		}
	] 
*/

