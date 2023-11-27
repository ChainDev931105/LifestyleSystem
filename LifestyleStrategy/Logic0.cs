using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LifestyleStrategy
{
    public class Logic0 : Logic
    {//public class open bracket
        public override void OnTick()
        { ///ontick open



            foreach (var sTF in ALL_TIMEFRAMES)
            {///stf open


                if (_value(sTF, "", "C", "2") > _value(sTF, "", "L", "3")
                                  && _value(sTF, "", "C", "2") < _value(sTF, "", "H", "3")
                                  && _value(sTF, "", "C", "1") < _value(sTF, "", "L", "3"))
                {
                    _set(sTF, "BEAR");

                }


                if (_check("M15", "BEAR", "1")) { _plot_pnt("M15 BEAR", _value("M15", "", "C", "1"), "BEAR", "M15"); }




                if (_value(sTF, "", "C", "2") < _value(sTF, "", "L", "3")
                              // && _value(sTF, "", "C", "2") < _value(sTF, "", "H", "3")
                              && _value(sTF, "", "C", "1") < _value(sTF, "", "L", "3"))
                {
                    _set(sTF, "BEAR");

                }


                if (_check("M15", "BEAR", "1")) { _plot_pnt("M15 BEAR", _value("M15", "", "C", "1"), "BEAR", "M15"); }
                if (_check("M4", "BEAR", "1")) { _plot_pnt("M4 BEAR", _value("M4", "", "C", "1"), "BEAR", "M4"); }
                if (_check("M2", "BEAR", "1")) { _plot_pnt("M2 BEAR", _value("M2", "", "C", "1"), "BEAR", "M2"); }






                if (_value(sTF, "", "C", "2") > _value(sTF, "", "L", "3")
                                              && _value(sTF, "", "C", "2") < _value(sTF, "", "H", "3")
                                              && _value(sTF, "", "C", "1") > _value(sTF, "", "H", "3"))
                {
                    _set(sTF, "BULL");

                }


                if (_check("M15", "BULL", "1")) { _plot_pnt("M15 BULL", _value("M15", "", "C", "1"), "BULL", "M15"); }


                if (_value(sTF, "", "C", "2") > _value(sTF, "", "H", "3")
                                             //&& _value(sTF, "", "C", "2") < _value(sTF, "", "H", "3")
                                             && _value(sTF, "", "C", "1") > _value(sTF, "", "H", "3"))
                {
                    _set(sTF, "BULL");

                }


                if (_check("M15", "BULL", "1")) { _plot_pnt("M15 BULL", _value("M15", "", "C", "1"), "BULL", "M15"); }
                if (_check("M4", "BULL", "1")) { _plot_pnt("M4 BULL", _value("M4", "", "C", "1"), "BULL", "M4"); }










                if (_value(sTF, "", "C", "2") > _value(sTF, "", "H", "3")
                              && _value(sTF, "", "C", "1") < _value(sTF, "", "H", "2")
                              && _value(sTF, "", "C", "1") > _value(sTF, "", "C", "2"))
                {
                    _set(sTF, "UCC12");

                }

                if (_check("M15", "UCC12", "1")) { _plot_pnt("M15 UCC12", _value("M15", "", "C", "1"), "UCC12", "M15"); }



                if (_value(sTF, "", "C", "2") > _value(sTF, "", "L", "3")
                                              && _value(sTF, "", "C", "2") < _value(sTF, "", "H", "3")
                                               && _value(sTF, "", "C", "1") < _value(sTF, "", "H", "2")
                                              && _value(sTF, "", "C", "1") > _value(sTF, "", "L", "2"))
                {
                    _set(sTF, "SIDE");

                }


                if (_value(sTF, "", "C", "2") > _value(sTF, "", "L", "3")
                                               // && _value(sTF, "", "C", "2") < _value(sTF, "", "H", "3")
                                               && _value(sTF, "", "C", "1") < _value(sTF, "", "H", "2")
                                              && _value(sTF, "", "C", "1") > _value(sTF, "", "L", "2"))
                {
                    _set(sTF, "SIDE");

                }


                if (_check("M15", "SIDE", "1")) { _plot_pnt("M15 SIDE", _value("M15", "", "C", "1"), "SIDE", "M15"); }
                if (_check("M4", "SIDE", "1")) { _plot_pnt("M4 SIDE", _value("M4", "", "C", "1"), "SIDE", "M4"); }
                if (_check("M2", "SIDE", "1")) { _plot_pnt("M2 SIDE", _value("M2", "", "C", "1"), "SIDE", "M2"); }



                if (_value(sTF, "", "C", "2") > _value(sTF, "", "H", "3")
                              && _value(sTF, "", "C", "1") < _value(sTF, "", "L", "2")
                              && _value(sTF, "", "C", "1") > _value(sTF, "", "L", "3"))
                {
                    _set(sTF, "SUP");

                }

                if (_check("M15", "SUP", "1")) { _plot_pnt("M15 SUP", _value("M15", "", "C", "1"), "SUP", "M15"); }



                if (_value(sTF, "", "C", "2") < _value(sTF, "", "L", "3")
                              && _value(sTF, "", "C", "1") > _value(sTF, "", "L", "2")
                              && _value(sTF, "", "C", "1") < _value(sTF, "", "C", "2"))
                {
                    _set(sTF, "DPS");

                }

                if (_check("M15", "DPS", "1")) { _plot_pnt("M15 DPS", _value("M15", "", "C", "1"), "DPS", "M15"); }


                int nLastBULL = _find(sTF, "BULL", 0);
                int nLastBEAR = _find(sTF, "BEAR", 0);
                int nLastSIDE = _find(sTF, "SIDE", 0);

                if (_check(sTF, "BULL", "2") && (_check(sTF, "BEAR", "1")))

                {
                    _set(sTF, "SELL");

                }


                if (_check(sTF, "SIDE", "2") && (_check(sTF, "BEAR", "1")))

                {
                    _set(sTF, "SELL");

                }



                if (_check("M15", "SELL", "1")) { _plot_pnt("M15", _value("M15", "", "C", "1"), "Sell", "M15"); }



                if (_check(sTF, "BEAR", "2") && (_check(sTF, "BULL", "1")))

                {
                    _set(sTF, "BUY");

                }


                if (_check(sTF, "SIDE", "2") && (_check(sTF, "BULL", "1")))

                {
                    _set(sTF, "BUY");

                }

                if (_check("M15", "BUY", "1")) { _plot_pnt("M15", _value("M15", "", "C", "1"), "Buy", "M15"); }
                if (_check("M4", "BUY", "1")) { _plot_pnt("M4", _value("M4", "", "C", "1"), "Buy", "M4"); }









            } ///close stf



            if (_check("M15", "BUY", "2")
           && _value("M15", "", "C", "1") < _value("M30", "", "H", "3"))

            {
                _set("M15", "Only15");

            }

            if (_check("M15", "Only15", "1")) { _plot_pnt("M15 ", _value("M15", "", "C", "1"), "Only15", "M15"); }


            if (_check("M15", "BUY", "2")
                && _value("M15", "", "C", "1") > _value("M30", "", "H", "3"))

            {
                _set("M15", "Now30");

            }

            if (_check("M15", "Now30", "1")) { _plot_pnt("M15 ", _value("M15", "", "C", "1"), "Now30", "M15"); }









            if (_check("M1", "BUY", "1")
           && _value("M1", "", "C", "1") < _value("M2", "", "H", "3"))

            { _set("M2", "One"); }

            if (_check("M2", "One", "1")) { _plot_pnt("M2 ", _value("M2", "", "C", "1"), "One", "M2"); }


            if (_check("M1", "BUY", "1")
                && _value("M1", "", "C", "1") > _value("M2", "", "H", "3"))

            { _set("M2", "Two"); }

            if (_check("M2", "Two", "1")) { _plot_pnt("M2", _value("M2", "", "C", "1"), "two", "M2"); }





            if (_check("M2", "BUY", "1") && _check("M4", "BUY", "1"))


            { _set("M15", "Nowys"); }

            //if (_check("M15", "Nowys", "1")) { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "Nowys", "M15"); }


            if (_check("M2", "BUY", "1")
                && _value("M2", "", "C", "1") > _value("M4", "", "H", "3"))

            { _set("M4", "Four"); }

            if (_check("M4", "Four", "1")) { _plot_pnt("M4", _value("M4", "", "C", "1"), "Four", "M4"); }




            if (_check("M4", "BUY", "1")
             && _value("M4", "", "C", "1") < _value("M8", "", "H", "3"))

            { _set("M8", "Four"); }

            if (_check("M8", "Four", "1")) { _plot_pnt("M8", _value("M8", "", "C", "1"), "Four", "M8"); }


            if (_check("M4", "BUY", "1")
                && _value("M4", "", "C", "1") > _value("M8", "", "H", "3"))

            { _set("M8", "Eight"); }

            if (_check("M8", "Eight", "1")) { _plot_pnt("M8", _value("M8", "", "C", "1"), "Eight", "M8"); }




            if (_check("M8", "BUY", "1")
          && _value("M8", "", "C", "1") < _value("M16", "", "H", "3"))

            { _set("M16", "Eight"); }

            if (_check("M16", "Eight", "1")) { _plot_pnt("M16", _value("M16", "", "C", "1"), "Eight", "M8"); }


            if (_check("M8", "BUY", "1")
                && _value("M8", "", "C", "1") > _value("M16", "", "H", "3"))

            { _set("M16", "Sixteen"); }

            if (_check("M16", "Sixteen", "1")) { _plot_pnt("M16", _value("M16", "", "C", "1"), "Sixteen", "M16"); }




            if (_check("M8", "BUY", "1")
          && _value("M8", "", "C", "1") < _value("M32", "", "H", "3"))

            { _set("M32", "Sixteen"); }

            if (_check("M32", "Sixteen", "1")) { _plot_pnt("M32", _value("M32", "", "C", "1"), "Sixteen", "M16"); }


            if (_check("M8", "BUY", "1")
                && _value("M8", "", "C", "1") > _value("M32", "", "H", "3"))

            { _set("M32", "Thirty2"); }

            if (_check("M32", "Thirty2", "1")) { _plot_pnt("M32", _value("M32", "", "C", "1"), "Thirty2", "M32"); }


            if (_check("M5", "BUY", "1")
                && _value("M5", "", "C", "1") > _value("M15", "", "L", "2"))
            {
                _set("M5", "Buyzone");
            }

            if (_check("M15", "BUY", "1")
                && _value("M15", "", "C", "1") > _value("M60", "", "L", "2"))
            {
                _set("M15", "Buyzone");
            }

            if (_check("M15", "Buyzone", "1")) { _plot_pnt("M15Buyzone", _value("M15", "", "C", "1"), "Buyzone", "M15"); }

            if (_check("M5", "Buyzone", "1")) { _plot_pnt("M5Buyzone", _value("M5", "", "C", "1"), "Buyzone", "M5"); }



            if (_check("M4", "Bull", "2"))

            {
                _set("Trend1", "Gup");

            }
            if (_check("Trend1", "Gup", "1")) { _plot_pnt("M4Gup", _value("M4", "", "C", "1"), "Tren1Gup", "M4"); }





            if (_check("M4", "BULL", "2"))


            {
                _set("Trend1", "Gup");

            }
            if (_check("Trend1", "Gup", "1")) { _plot_pnt("M4Gup", _value("M4", "", "C", "1"), "Trend1Gup", "M4"); }




            if (_time().DayOfWeek == DayOfWeek.Monday)
            {
                if (_time().Hour >= 0 && _time().Hour <= 1)
                {

                    if (_time().Minute >= 1)
                    {
                    }
                    if (_time().Minute >= 2)
                    {
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 3)
                    {
                    }
                    if (_time().Minute >= 4)
                    {

                    }
                    if (_time().Minute >= 5)
                    {
                    }
                    if (_time().Minute >= 6)
                    {

                    }
                    if (_time().Minute >= 7)
                    {
                    }
                    if (_time().Minute >= 8)
                    {

                    }
                    if (_time().Minute >= 9)
                    {
                    }
                    if (_time().Minute >= 10)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 11)
                    {
                    }
                    if (_time().Minute >= 12)
                    {

                    }
                    if (_time().Minute >= 13)
                    {
                    }
                    if (_time().Minute >= 14)
                    {

                    }
                    if (_time().Minute >= 15)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 16)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 17)
                    {
                    }
                    if (_time().Minute >= 18)
                    {

                    }
                    if (_time().Minute >= 19)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 20)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 21)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 22)
                    {

                    }
                    if (_time().Minute >= 23)
                    {
                    }
                    if (_time().Minute >= 24)
                    {
                    }
                    if (_time().Minute >= 25)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 26)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 27)
                    {
                    }
                    if (_time().Minute >= 28)
                    {
                    }
                    if (_time().Minute >= 29)
                    {
                    }
                    if (_time().Minute >= 30)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }

                    if (_time().Minute >= 31)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 32)
                    {
                    }
                    if (_time().Minute >= 33)
                    {
                    }
                    if (_time().Minute >= 34)
                    {

                    }
                    if (_time().Minute >= 35)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 36)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 37)
                    {
                    }
                    if (_time().Minute >= 38)
                    {

                    }
                    if (_time().Minute >= 39)
                    {
                    }
                    if (_time().Minute >= 40)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 41)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 42)
                    {

                    }
                    if (_time().Minute >= 43)
                    {
                    }
                    if (_time().Minute >= 44)
                    {
                    }
                    if (_time().Minute >= 45)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 46)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }

                    }
                    if (_time().Minute >= 47)
                    {
                    }
                    if (_time().Minute >= 48)
                    {
                    }
                    if (_time().Minute >= 49)
                    {
                    }
                    if (_time().Minute >= 50)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 51)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 52)
                    {
                    }
                    if (_time().Minute >= 53)
                    {
                    }
                    if (_time().Minute >= 54)
                    {

                    }
                    if (_time().Minute >= 55)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 56)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 57)
                    {
                    }
                    if (_time().Minute >= 58)
                    {
                    }
                    if (_time().Minute >= 59)
                    {
                    }
                }
                if (_time().Hour >= 1 && _time().Hour <= 2)
                {

                    if (_time().Minute >= 1)
                    {
                    }
                    if (_time().Minute >= 2)
                    {
                    }
                    if (_time().Minute >= 3)
                    {
                    }
                    if (_time().Minute >= 4)
                    {
                    }
                    if (_time().Minute >= 5)
                    {
                    }
                    if (_time().Minute >= 6)
                    {
                    }
                    if (_time().Minute >= 7)
                    {
                    }
                    if (_time().Minute >= 8)
                    {
                    }
                    if (_time().Minute >= 9)
                    {
                    }
                    if (_time().Minute >= 10)
                    {
                    }
                    if (_time().Minute >= 11)
                    {
                    }
                    if (_time().Minute >= 12)
                    {
                    }
                    if (_time().Minute >= 13)
                    {
                    }
                    if (_time().Minute >= 14)
                    {
                    }
                    if (_time().Minute >= 15)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 16)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 17)
                    {
                    }
                    if (_time().Minute >= 18)
                    {
                    }
                    if (_time().Minute >= 19)
                    {
                    }
                    if (_time().Minute >= 20)
                    {
                    }
                    if (_time().Minute >= 21)
                    {
                    }
                    if (_time().Minute >= 22)
                    {
                    }
                    if (_time().Minute >= 23)
                    {
                    }
                    if (_time().Minute >= 24)
                    {
                    }
                    if (_time().Minute >= 25)
                    {
                    }
                    if (_time().Minute >= 26)
                    {
                    }
                    if (_time().Minute >= 27)
                    {
                    }
                    if (_time().Minute >= 28)
                    {
                    }
                    if (_time().Minute >= 28)
                    {
                    }
                    if (_time().Minute >= 31)
                    {
                        MMTimetest();
                    }
                    if (_time().Minute >= 32)
                    {
                    }
                    if (_time().Minute >= 33)
                    {
                    }
                    if (_time().Minute >= 34)
                    {
                    }
                    if (_time().Minute >= 35)
                    {
                    }
                    if (_time().Minute >= 36)
                    {
                    }
                    if (_time().Minute >= 37)
                    {
                    }
                    if (_time().Minute >= 38)
                    {
                    }
                    if (_time().Minute >= 39)
                    {
                    }
                    if (_time().Minute >= 40)
                    {
                    }
                    if (_time().Minute >= 41)
                    {
                    }
                    if (_time().Minute >= 42)
                    {
                    }
                    if (_time().Minute >= 43)
                    {
                    }
                    if (_time().Minute >= 44)
                    {
                    }
                    if (_time().Minute >= 45)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 46)
                    {
                        MMTimetest();
                        if (_check("M15", "Nowys", "1"))
                        { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "M15Nowys", "M15"); }
                    }
                    if (_time().Minute >= 47)
                    {
                    }
                    if (_time().Minute >= 48)
                    {
                    }
                    if (_time().Minute >= 49)
                    {
                    }
                    if (_time().Minute >= 50)
                    {
                    }
                    if (_time().Minute >= 51)
                    {
                    }
                    if (_time().Minute >= 52)
                    {
                    }
                    if (_time().Minute >= 53)
                    {
                    }
                    if (_time().Minute >= 54)
                    {
                    }
                    if (_time().Minute >= 55)
                    {
                    }
                    if (_time().Minute >= 56)
                    {
                    }
                    if (_time().Minute >= 57)
                    {
                    }
                    if (_time().Minute >= 58)
                    {
                    }
                    if (_time().Minute >= 59)
                    {
                    }
                }
            }

        }  //on tick close


        void MMTimetest()
        {

            if (_check("M15", "BUY", "2")
             && _value("M15", "", "C", "1") < _value("M30", "", "H", "3"))

            {
                _set("M15", "Only15");

            }

            if (_check("M15", "Only15", "1")) { _plot_pnt("M15 ", _value("M15", "", "C", "1"), "Only15", "M15"); }


            if (_check("M15", "BUY", "2")
                && _value("M15", "", "C", "1") > _value("M30", "", "H", "3"))

            {
                _set("M15", "Now30");

            }

            if (_check("M15", "Now30", "1")) { _plot_pnt("M15 ", _value("M15", "", "C", "1"), "Now30", "M15"); }









            if (_check("M1", "BUY", "1")
           && _value("M1", "", "C", "1") < _value("M2", "", "H", "3"))

            { _set("M2", "One"); }

            if (_check("M2", "One", "1")) { _plot_pnt("M2 ", _value("M2", "", "C", "1"), "One", "M2"); }


            if (_check("M1", "BUY", "1")
                && _value("M1", "", "C", "1") > _value("M2", "", "H", "3"))

            { _set("M2", "Two"); }

            if (_check("M2", "Two", "1")) { _plot_pnt("M2", _value("M2", "", "C", "1"), "two", "M2"); }





            if (_check("M2", "BUY", "1") && _check("M4", "BUY", "1"))


            { _set("M15", "Nowys"); }

            //if (_check("M15", "Nowys", "1")) { _plot_pnt("M15Nowys", _value("M15", "", "C", "1"), "Nowys", "M15"); }


            if (_check("M2", "BUY", "1")
                && _value("M2", "", "C", "1") > _value("M4", "", "H", "3"))

            { _set("M4", "Four"); }

            if (_check("M4", "Four", "1")) { _plot_pnt("M4", _value("M4", "", "C", "1"), "Four", "M4"); }




            if (_check("M4", "BUY", "1")
             && _value("M4", "", "C", "1") < _value("M8", "", "H", "3"))

            { _set("M8", "Four"); }

            if (_check("M8", "Four", "1")) { _plot_pnt("M8", _value("M8", "", "C", "1"), "Four", "M8"); }


            if (_check("M4", "BUY", "1")
                && _value("M4", "", "C", "1") > _value("M8", "", "H", "3"))

            { _set("M8", "Eight"); }

            if (_check("M8", "Eight", "1")) { _plot_pnt("M8", _value("M8", "", "C", "1"), "Eight", "M8"); }




            if (_check("M8", "BUY", "1")
          && _value("M8", "", "C", "1") < _value("M16", "", "H", "3"))

            { _set("M16", "Eight"); }

            if (_check("M16", "Eight", "1")) { _plot_pnt("M16", _value("M16", "", "C", "1"), "Eight", "M8"); }


            if (_check("M8", "BUY", "1")
                && _value("M8", "", "C", "1") > _value("M16", "", "H", "3"))

            { _set("M16", "Sixteen"); }

            if (_check("M16", "Sixteen", "1")) { _plot_pnt("M16", _value("M16", "", "C", "1"), "Sixteen", "M16"); }




            if (_check("M8", "BUY", "1")
          && _value("M8", "", "C", "1") < _value("M32", "", "H", "3"))

            { _set("M32", "Sixteen"); }

            if (_check("M32", "Sixteen", "1")) { _plot_pnt("M32", _value("M32", "", "C", "1"), "Sixteen", "M16"); }


            if (_check("M8", "BUY", "1")
                && _value("M8", "", "C", "1") > _value("M32", "", "H", "3"))

            { _set("M32", "Thirty2"); }

            if (_check("M32", "Thirty2", "1")) { _plot_pnt("M32", _value("M32", "", "C", "1"), "Thirty2", "M32"); }


            if (_check("M5", "BUY", "1")
                && _value("M5", "", "C", "1") > _value("M15", "", "L", "2"))
            {
                _set("M5", "Buyzone");
            }

            if (_check("M15", "BUY", "1")
                && _value("M15", "", "C", "1") > _value("M60", "", "L", "2"))
            {
                _set("M15", "Buyzone");
            }

            if (_check("M15", "Buyzone", "1")) { _plot_pnt("M15Buyzone", _value("M15", "", "C", "1"), "Buyzone", "M15"); }

            if (_check("M5", "Buyzone", "1")) { _plot_pnt("M5Buyzone", _value("M5", "", "C", "1"), "Buyzone", "M5"); }



            if (_check("M4", "Bull", "2"))

            {
                _set("Trend1", "Gup");

            }
            if (_check("Trend1", "Gup", "1")) { _plot_pnt("M4Gup", _value("M4", "", "C", "1"), "Tren1Gup", "M4"); }





            if (_check("M4", "BULL", "2"))


            {
                _set("Trend1", "Gup");

            }
            if (_check("Trend1", "Gup", "1")) { _plot_pnt("M4Gup", _value("M4", "", "C", "1"), "Trend1Gup", "M4"); }



        }
        /////////////////////////////////////



        //functions here in between Ontick close and public void close




    }//Public void
} //Namespace

