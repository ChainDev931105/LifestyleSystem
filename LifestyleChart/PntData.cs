using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleChart
{
    public struct Pnt
    {
        public long time;
        public double dValue;
        public string sComment;
    }

    class PntData
    {
        Dictionary<long, Pnt> m_dicPnt = new Dictionary<long, Pnt>();
        Dictionary<long, bool> m_dicUpdate = new Dictionary<long, bool>();

        public void Update(long time, double dValue, string sComment)
        {
            lock (this)
            {
                m_dicPnt[time] = new Pnt()
                {
                    time = time,
                    dValue = dValue,
                    sComment = sComment
                };
                m_dicUpdate[time] = true;
            }
        }

        public void Prepare()
        {
            lock (this)
            {
                m_dicUpdate.Clear();
                foreach (var time in m_dicPnt.Keys)
                {
                    m_dicUpdate[time] = true;
                }
            }
        }

        public List<Pnt> GetUpdateList()
        {
            lock (this)
            {
                List<Pnt> lstRlt = new List<Pnt>();
                foreach (var time in m_dicUpdate.Keys)
                {
                    lstRlt.Add(m_dicPnt[time]);
                }
                m_dicUpdate.Clear();
                return lstRlt;
            }
        }
    }
}
