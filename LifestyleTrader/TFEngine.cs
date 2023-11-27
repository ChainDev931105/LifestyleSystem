using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleCommon;

namespace LifestyleTrader
{
    class TFEngine
    {
        Symbol m_symbol = null;
        string m_sStrategyID = "";
        public Dictionary<string, TimeFrame> m_dicTF = new Dictionary<string, TimeFrame>();
        double m_dAsk;
        double m_dBid;
        public long m_time;
        Dictionary<string, List<Ohlc>> m_dicOHLC = new Dictionary<string, List<Ohlc>>();

        public TFEngine(Symbol symbol, List<TimeFrame> lstTF, string sStrategyID)
        {
            m_symbol = symbol;
            m_dicTF.Clear();
            m_dicOHLC.Clear();
            foreach (var tf in lstTF)
            {
                m_dicTF.Add(tf.m_sName, tf);
                m_dicOHLC.Add(tf.m_sName, new List<Ohlc>());
            }
            m_sStrategyID = sStrategyID;
        }

        public TimeFrame GetTimeFrame(string sTF)
        {
            return m_dicTF.ContainsKey(sTF) ? m_dicTF[sTF] : null;
        }

        public void PushTick(Tick tick)
        {
            if (m_dAsk == tick.ask && m_dBid == tick.bid && (m_time / 60 == tick.time / 60)) return;
            m_dAsk = tick.ask;
            m_dBid = tick.bid;
            m_time = tick.time;
            foreach (var tf in m_dicTF)
            {
                Ohlc ohlc = pushTick(tf.Value, m_dicOHLC[tf.Key], tick);
                Manager.g_chart.Send(new List<string>()
                {
                    m_sStrategyID,
                    "rate",
                    tf.Key,
                    ohlc.time.ToString(),
                    ohlc.open.ToString(),
                    ohlc.high.ToString(),
                    ohlc.low.ToString(),
                    ohlc.close.ToString()
                });
            }
        }

        public void PushOhlc(Ohlc ohlc)
        {
            PushTick(new Tick() { ask = ohlc.open_ask == 0 ? ohlc.open : ohlc.open_ask, bid = ohlc.open, time = ohlc.time });
            PushTick(new Tick() { ask = ohlc.high_ask == 0 ? ohlc.high : ohlc.high_ask, bid = ohlc.high, time = ohlc.time });
            PushTick(new Tick() { ask = ohlc.low_ask == 0 ? ohlc.low : ohlc.low_ask, bid = ohlc.low, time = ohlc.time });
            PushTick(new Tick() { ask = ohlc.close_ask == 0 ? ohlc.close : ohlc.close_ask, bid = ohlc.close, time = ohlc.time });
        }

        public Ohlc GetOhlc(string sTF, int nShift = 0)
        {
            if (!m_dicOHLC.ContainsKey(sTF)) return null;
            if (m_dicOHLC[sTF].Count < nShift + 1 || nShift < 0) return null;
            return m_dicOHLC[sTF][m_dicOHLC[sTF].Count - nShift - 1];
        }

        public int FindIndexOf(string sTF, Ohlc rate)
        {
            for (int i = 0; i < 100; i++)
            {
                if (GetOhlc(sTF, i).time == rate.time) return i + 1;
            }
            return -1;
        }

        public List<string> TFList()
        {
            return m_dicTF.Keys.ToList();
        }

        public int OhlcCount(string sTF)
        {
            return m_dicOHLC.ContainsKey(sTF) ? m_dicOHLC[sTF].Count : 0;
        }

        public DateTime GetTime()
        {
            return Global.UnixSecondsToDateTime(m_time);
        }

        public double Ask()
        {
            return m_dAsk;
        }

        public double Bid()
        {
            return m_dBid;
        }

        private Ohlc pushTick(TimeFrame tf, List<Ohlc> lstOhlc, Tick tick)
        {
            Ohlc lastOhlc = lstOhlc.Count > 0 ? lstOhlc[lstOhlc.Count - 1] : new Ohlc();
            long stMoment = tf.GetStartMoment(tick.time);
            if (stMoment == lastOhlc.time)
            {
                lastOhlc.close = tick.bid;
                lastOhlc.close_ask = tick.ask;
                lastOhlc.low = Math.Min(lastOhlc.low, tick.bid);
                lastOhlc.low_ask = Math.Min(lastOhlc.low_ask, tick.ask);
                lastOhlc.high = Math.Max(lastOhlc.high, tick.bid);
                lastOhlc.high_ask = Math.Max(lastOhlc.high_ask, tick.ask);
            }
            else
            {
                lstOhlc.Add(lastOhlc = new Ohlc()
                {
                    time = stMoment,
                    open = tick.bid,
                    high = tick.bid,
                    low = tick.bid,
                    close = tick.bid,
                    open_ask = tick.ask,
                    high_ask = tick.ask,
                    low_ask = tick.ask,
                    close_ask = tick.ask
                });
            }
            return lastOhlc;
        }
    }
}
