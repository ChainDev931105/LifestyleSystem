using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleStrategy
{
    public class Logic1 : Logic
    {//public class open bracket
        public override void OnTick()
        {
            // zzz2();   can add function here

            foreach (var sTF in ALL_TIMEFRAMES)
            {

            } ///close stf


            //}//public class close bracket

            // public void zzz2()
            {
                //  if Time = Hurr= 13:  

                //   Valuex= M15 = US1 the M5 = DBD

            }
        }

        void MMM240()
        {
            //this is  the first layer 
            if (_check("M6", "DS1", "1") && (_check("M5", "DS1", "1") && (_check("M4", "DS1", "1")
             && (_check("M3", "DS1", "1") && (_check("M2", "DS1", "1")))))) ;
            {
                _set("M3", "UB");
                //_plot_pnt("UB", _value("M15", "", "C", "1"), "UB_" + _value("M15", "", "C", "1"), "M15");
            }

        }
    }
}
