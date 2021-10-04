using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleCommon;

namespace LifestyleChart
{
    class RateData
    {
        Dictionary<long, Ohlc> m_dicPrice = new Dictionary<long, Ohlc>();
        Dictionary<long, bool> m_dicUpdate = new Dictionary<long, bool>();

        public void Update(Ohlc ohlc)
        {
            lock (this)
            {
                if (!m_dicPrice.ContainsKey(ohlc.time))
                {
                    m_dicPrice[ohlc.time] = ohlc;
                }
                else
                {
                    m_dicPrice[ohlc.time].open = ohlc.open;
                    m_dicPrice[ohlc.time].high = ohlc.high;
                    m_dicPrice[ohlc.time].low = ohlc.low;
                    m_dicPrice[ohlc.time].close = ohlc.close;
                    m_dicPrice[ohlc.time].open_ask = ohlc.open_ask;
                    m_dicPrice[ohlc.time].high_ask = ohlc.high_ask;
                    m_dicPrice[ohlc.time].low_ask = ohlc.low_ask;
                    m_dicPrice[ohlc.time].close_ask = ohlc.close_ask;
                }
                m_dicUpdate[ohlc.time] = true;
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

        public List<Ohlc> GetUpdateList()
        {
            lock (this)
            {
                List<Ohlc> lstRlt = new List<Ohlc>();
                foreach (var time in m_dicUpdate.Keys)
                {
                    lstRlt.Add(m_dicPrice[time]);
                }
                m_dicUpdate.Clear();
                return lstRlt;
            }
        }
    }
}
