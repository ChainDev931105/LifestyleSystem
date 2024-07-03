using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LifestyleStrategy
{
    public class Logic0 : Logic
    {
        List<double> m_list;

        public override void OnTick()
        {
            m_list.Add(1);

            _print_to_table("M5", "UBU", 1.23456, 14);
        }
    }
}

