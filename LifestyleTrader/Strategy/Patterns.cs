using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using LifestyleCommon;

namespace LifestyleTrader
{
    enum Pattern
    {
        None = 0,
        Up = 1,
        Down = 2,
        Dojo = 3,
        InRange = 4, InBody = 5,
        A1 = 6, A2 = 7,
        B1 = 8, B2 = 9,
        C1 = 10, C2 = 11,
        D1 = 12, D2 = 13,
        E1 = 14, E2 = 15,
        F1 = 16, F2 = 17, F3 = 18, F4 = 19,
        G1 = 20, G2 = 21, G3 = 22,
        H1 = 23, H2 = 24, H3 = 25
    }

    class Patterns
    {
    }

    class PersistentOhlc
    {
        List<Ohlc> m_lstOhlc = new List<Ohlc>();

        public bool Append(Ohlc ohlc)
        {
            if (m_lstOhlc.Count > 0 && m_lstOhlc[m_lstOhlc.Count - 1].time == ohlc.time) return false;
            m_lstOhlc.Add(ohlc);
            return true;
        }

        public int Count()
        {
            return m_lstOhlc.Count;
        }

        public Ohlc GetShift(int nShift)
        {
            return m_lstOhlc[m_lstOhlc.Count - nShift];
        }
    }

    class Indicator
    {
        public string m_sStrategyID;
        public string m_sName;
        public TimeFrame m_TF;
        public JArray m_jValue;
        public double m_dValue;
        public long m_time;

        public void UpdateValue(double dValue, long time)
        {
            time = m_TF.GetStartMoment(time);
            if (dValue == m_dValue && m_time == time) return;
            m_time = time;
            m_dValue = dValue;
            Manager.g_chart.Send(new List<string>()
            {
                m_sStrategyID,
                "ind",
                m_sName,
                m_time.ToString(),
                dValue.ToString(),
                m_TF.m_sName
            });
        }
    }
}
