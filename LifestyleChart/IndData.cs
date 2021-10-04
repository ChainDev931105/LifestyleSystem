using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleChart
{
    class IndData
    {
        Dictionary<long, double> m_dicPrice = new Dictionary<long, double>();
        Dictionary<long, bool> m_dicUpdate = new Dictionary<long, bool>();

        public void Update(long time, double dValue)
        {
            lock (this)
            {
                m_dicPrice[time] = dValue;
                m_dicUpdate[time] = true;
            }
        }

        public void Prepare()
        {
            lock (this)
            {
                m_dicUpdate.Clear();
                foreach (var time in m_dicPrice.Keys)
                {
                    m_dicUpdate[time] = true;
                }
            }
        }

        public List<Tuple<long, double>> GetUpdateList()
        {
            lock (this)
            {
                List<Tuple<long, double>> lstRlt = new List<Tuple<long, double>>();
                foreach (var time in m_dicUpdate.Keys)
                {
                    lstRlt.Add(Tuple.Create(time, m_dicPrice[time]));
                }
                m_dicUpdate.Clear();
                return lstRlt;
            }
        }
    }
}
