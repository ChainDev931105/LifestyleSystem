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
            // step 1: declare a list
            var list = new List<double>() { };

            // step 2: add values to the list, you can add as many values as you want
            list.Add(_value("M5", "UBU", "C", "0"));
            list.Add(_value("M5", "UBU", "C", "1"));
            list.Add(_value("M5", "UBU", "C", "2"));
            list.Add(_value("M5", "UBU", "C", "3"));

            // step 3: sort list, then the array is sorted from smallest to biggest
            list.Sort();

            // Check if the current M1 close value is below than minimum
            if (_value("M1", "", "C", "0") < list[0])
            {
                // You can write some code
            }

            // Check if the current M1 high value is bigger than maximum
            if (_value("M1", "", "H", "0") < list[list.Count - 1])
            {
                // You can write some code
            }


        }
    }//Public void
} //Namespace

