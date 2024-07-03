using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using LifestyleCommon;

namespace LifestyleTrader
{
    class Manager
    {
        public static Form1 g_mainForm = null;
        private static StreamWriter g_fileLog = null;
        private static MainConfig g_mainConfig = null;
        public static SymbolConfig g_symbolConfig = null;
        private static Strategy g_strategy = null;
        public static ChartCon g_chart = new ChartCon();
        public static TradeHistory g_tradeHistory = new TradeHistory();
        public static RUN_MODE g_eMode = RUN_MODE.NONE;
        public static MySQL g_database = null;
        public static IBSite g_broker = new IBSite();
        private static Thread g_mainThread = null;
        private static bool g_bRunning = false;
        private static DateTime g_dtLastDisplayState = new DateTime();
        public static DateTime g_dtCurTime = new DateTime();
        private static Dictionary<long, bool> g_exist = new Dictionary<long, bool>();

        public static void Init(Form1 form)
        {
            g_mainForm = form;
            PutLog("Inited");
            g_mainConfig = new MainConfig(Global.MAIN_CONFIG);
            g_symbolConfig = new SymbolConfig(Global.SYMBOL_CONFIG);
            form.SetSymbolList(g_symbolConfig.SymbolNameList());
            Global.OnLog = PutLog;
            PutLog("Loading Config success");
            g_broker.Init();
        }

        public static void PutLog(string sLog)
        {
            if (g_fileLog == null)
            {
                string sLogDir = Global.LOG_TRADER_DIR;
                if (!Directory.Exists(sLogDir))
                {
                    Directory.CreateDirectory(sLogDir);
                }
                g_fileLog = new StreamWriter(sLogDir + DateTime.Now.ToString("yyyyMMddHH") + ".txt", true);
                g_fileLog.AutoFlush = true;
            }
            lock (g_fileLog)
            {
                string sLogEx = string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), sLog);
                g_fileLog.WriteLine(sLogEx);
                g_mainForm.DisplayLog(sLogEx);
            }
        }

        public static bool CH_Connect()
        {
            return g_chart.Connect(g_mainConfig.m_sChart_Host, g_mainConfig.m_sChart_User, g_mainConfig.m_sChart_Pwd);
        }

        public static bool CH_Disconnect()
        {
            return g_chart.Disconnect();
        }

        public static void Start(RUN_MODE eMode, string sSymbol, DateTime dtStart, DateTime dtEnd)
        {
            if (g_database == null)
            {// init database
                PutLog("Initializing Database ...");
                g_database = new MySQL(g_mainConfig.m_sDB_Server, g_mainConfig.m_nDB_Port,
                    g_mainConfig.m_sDB_User, g_mainConfig.m_sDB_Pwd);
                g_database.CreateTable(sSymbol);
                PutLog("Database init success");
            }
            PutLog(string.Format("Start({0},{1},{2},{3})", eMode, sSymbol, dtStart, dtEnd));
            Symbol symbol = g_symbolConfig.FindSymbol(sSymbol);
            if (symbol == null)
            {
                PutLog("Can't find such symbol");
                return;
            }

            g_strategy = new Strategy(symbol, Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(Global.STRATEGY_CONFIG)));

            (g_mainThread = new Thread(() =>
            {
                try
                {
                    g_bRunning = true;
                    if (eMode == RUN_MODE.BACKTEST)
                    {
                        runBacktest(dtStart, dtEnd);
                        g_mainForm.OnStop();
                    }
                    else if (eMode == RUN_MODE.REAL_TRADE)
                    {
                        g_broker.Connect(g_mainConfig.m_sIB_Host, g_mainConfig.m_nIB_Port, g_mainConfig.m_nIB_ID);
                        runRealTrade();
                    }
                    else if (eMode == RUN_MODE.MERGE_MODE)
                    {
                        g_broker.Connect(g_mainConfig.m_sIB_Host, g_mainConfig.m_nIB_Port, g_mainConfig.m_nIB_ID);
                        runMergedMode(dtStart);
                    }
                }
                catch (Exception e)
                {
                    PutLog(e.ToString());
                    if (e.InnerException != null)
                    {
                        PutLog(e.InnerException.ToString());
                    }
                    throw e;
                }
            })).Start();
        }

        public static void Stop()
        {
            g_eMode = RUN_MODE.NONE;
            if (g_mainThread != null)
            {
                g_bRunning = false;
                Thread.Sleep(1000);
                try
                {
                    g_mainThread.Abort();
                }
                catch { }
            }
        }

        private static void runBacktest(DateTime dtStart, DateTime dtEnd)
        {
            g_eMode = RUN_MODE.BACKTEST;
            var lstOhlc = g_database.Load(g_strategy.SymbolEx(), dtStart, dtEnd);
            int nTot = lstOhlc.Count;
            int nCur = 0;
            PutLog("Load rates finished, total ticks = " + nTot);
            foreach (var ohlc in lstOhlc)
            {
                if (!g_bRunning) break;
                g_dtCurTime = Global.UnixSecondsToDateTime(ohlc.time);
                g_strategy.PushOhlc(ohlc);
                g_strategy.OnTick();
                nCur++;
                double dPercent = 1.0 * nCur / nTot;
                g_mainForm.DisplayPerformance(string.Format("{0} %, {1}", 
                    ((int)(dPercent * 100 + 0.5)).ToString(), g_dtCurTime.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            g_mainForm.DisplayPerformance(string.Format("100 %, {0}", 
                g_dtCurTime.ToString("yyyy-MM-dd HH:mm:ss")), true);
            PutLog("Backtest finished");
        }

        private static void runRealTrade()
        {
            g_eMode = RUN_MODE.REAL_TRADE;

            long minuteLast = 0;
            while (g_bRunning)
            {
                Tick tick = g_broker.GetRate(g_strategy.symbol());
                if (tick.time == 0)
                {
                    Thread.Sleep(500);
                    continue;
                }
                g_mainForm.DisplayState(string.Format("{0},{1},{2}", tick.time, tick.ask, tick.bid));
                g_dtCurTime = DateTime.Now;
                g_strategy.PushTick(tick);
                long minuteCurrent = tick.time / 60 * 60;
                if (minuteCurrent > minuteLast)
                {
                    if (minuteLast != 0)
                    {
                        Ohlc ohlc = g_strategy.m_TFEngine.GetOhlc("M1", 1);
                        lock (g_database)
                        {
                            g_database.Save(g_strategy.SymbolEx(), new List<Ohlc>() { ohlc });
                        }
                    }
                    minuteLast = minuteCurrent;
                    g_strategy.OnTick();
                }
                Thread.Sleep(100);
            }
        }

        private static void runMergedMode(DateTime dtStart)
        {
            List<Ohlc> lstOriginalData = g_database.Load(g_strategy.SymbolEx(), dtStart, DateTime.Now);
            long rateExist = 0;
            lock (g_exist)
            {
                foreach (var ohlc in lstOriginalData)
                {
                    g_exist[ohlc.time] = true;
                    rateExist = Math.Max(rateExist, ohlc.time);
                }
            }

            List<Ohlc> lstOhlc = new List<Ohlc>();

            bool bStarted = false;

            g_broker.SetHisUpdateAction(new Action<string, string, double, double, double, double>((s, t, o, h, l, c) =>
            {
                long lTime = Global.UnixDateTimeToSeconds(Global.ParseDate(t));
                lock (g_exist)
                {
                    if (g_exist.ContainsKey(lTime)) return;
                    g_exist[lTime] = true;
                }
                lock (g_database)
                {
                    g_database.Save(s, new List<Ohlc>() {
                        new Ohlc()
                        {
                            time = lTime,
                            open = o,
                            high = h,
                            low = l,
                            close = c
                        }
                    });
                }
                if (bStarted)
                {
                    lock (lstOhlc)
                    {
                        for (int i = lstOhlc.Count - 1; i >= 0; i--)
                        {
                            if (lstOhlc[i].time == lTime) break;
                            if (lstOhlc[i].time < lTime)
                            {
                                lstOhlc.Insert(i + 1, new Ohlc()
                                {
                                    time = lTime,
                                    open = o,
                                    high = h,
                                    low = l,
                                    close = c
                                });
                                break;
                            }
                        }
                    }
                }
            }));

            for (DateTime dtDay = dtStart; dtDay < DateTime.Now.AddHours(2); dtDay = dtDay.AddHours(2))
            {
                long lTime = Global.UnixDateTimeToSeconds(dtDay);
                if (lTime < rateExist - 60 * 60 * 4)
                {
                    continue;
                }
                PutLog(string.Format("Get Historical Data {0},{1}", g_strategy.symbol(), dtDay));
                g_broker.GetHistoricalData(g_strategy.symbol(), dtDay);
                Thread.Sleep(2000);
            }

            Thread.Sleep(5000);

            g_eMode = RUN_MODE.BACKTEST;
            lstOhlc = g_database.Load(g_strategy.SymbolEx(), dtStart, DateTime.Now);
            PutLog("Load rates finished, total ticks = " + lstOhlc.Count);
            bStarted = true;
            int nCur = 0;
            for (int i = 0; ; i++)
            {
                Ohlc ohlc;
                int nTot = 0;
                lock (lstOhlc)
                {
                    nTot = lstOhlc.Count;
                    ohlc = lstOhlc[i];
                }
                if (!g_bRunning) break;
                g_dtCurTime = Global.UnixSecondsToDateTime(ohlc.time);
                g_strategy.PushOhlc(ohlc);
                g_strategy.OnTick();
                nCur++;
                double dPercent = 1.0 * nCur / nTot;
                g_mainForm.DisplayPerformance(string.Format("{0} %, {1}",
                    ((int)(dPercent * 100 + 0.5)).ToString(), g_dtCurTime.ToString("yyyy-MM-dd HH:mm:ss")));
                lock (lstOhlc)
                {
                    if (i >= lstOhlc.Count - 1) break;
                }
            }
            g_mainForm.DisplayPerformance(string.Format("100 %, {0}",
                g_dtCurTime.ToString("yyyy-MM-dd HH:mm:ss")), true);
            PutLog("Backtest finished, moving to live mode");

            runRealTrade();
        }
    }
}
