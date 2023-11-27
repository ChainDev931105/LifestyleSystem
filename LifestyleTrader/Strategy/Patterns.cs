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
        A1 = 11, A2 = 12,
        B1 = 21, B2 = 22,
        C1 = 31, C2 = 32,
        D1 = 41, D2 = 42, D3 = 43, D4 = 44, D5 = 45, D6 = 46,
        E1 = 51, E2 = 52, E3 = 53, E4 = 54, E5 = 55, E6 = 56,
        F1 = 61, F2 = 62, F3 = 63, F4 = 64,
        G1 = 71, G2 = 72, G3 = 73,
        H1 = 81, H2 = 82, H3 = 83
    }

    class Patterns
    {
    }

    class PersistentOhlc
    {
        List<Ohlc> m_lstOhlc = new List<Ohlc>();
        public int nMaxSize = 100000;

        public bool Append(Ohlc ohlc)
        {
            if (m_lstOhlc.Count > 0 && m_lstOhlc[m_lstOhlc.Count - 1].time == ohlc.time) return false;
            m_lstOhlc.Add(ohlc);
            if (nMaxSize < m_lstOhlc.Count)
            {
                m_lstOhlc.RemoveAt(0);
            }
            return true;
        }

        public int Count()
        {
            return m_lstOhlc.Count;
        }

        public Ohlc GetShift(int nShift)
        {
            return m_lstOhlc[m_lstOhlc.Count - nShift - 1];
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
