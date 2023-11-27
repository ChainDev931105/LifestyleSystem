using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleStrategy
{
    public class LogicLearn : Logic
    {
        public override void OnTick()
        {
            {// --------------------------------- EXAMPLE 1 --------------------------------- //
                int nIDLast2G2 = 100; // ignore this line

                // way 1
                double x = _value("M5", "", "H", nIDLast2G2.ToString());

                // way 2
                string s = nIDLast2G2.ToString();
                double y = _value("M5", "", "H", s);
            }


            {// --------------------------------- EXAMPLE 2 --------------------------------- //
                // original way
                if (_time().Hour == 4)
                {
                    if (_time().Minute == 2)
                    {
                        ZZZ1();
                    }
                    if (_time().Minute == 3)
                    {
                        ZZZ1();
                    }
                }

                // shorten way
                if (_time().Hour == 4)
                {
                    if (_time().Minute == 2 || _time().Minute == 3)
                    {
                        ZZZ1();
                    }
                }
            }

            {// --------------------------------- EXAMPLE 3 --------------------------------- //
                // this is for every minutes of Hour 4
                if (_time().Hour == 4)
                {
                    ZZZ1();
                }
            }
        }

        public void ZZZ1()
        {
        }
    }
}
