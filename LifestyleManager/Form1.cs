using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using LifestyleCommon;

namespace LifestyleManager
{
    public partial class Form1 : Form
    {
        private StreamWriter m_fileLog = null;
        private MainConfig m_mainConfig = null;
        private SymbolConfig m_symbolConfig = null;
        private MySQL m_database = null;
        private IBSite m_broker = new IBSite();
        private List<Ohlc> m_cache = new List<Ohlc>();
        private Dictionary<long, bool> m_exist = new Dictionary<long, bool>();
        private int m_nDownedRate = 0;

        public Form1()
        {
            InitializeComponent();
            init();
            Global.OnLog = PutLog;
        }

        ~Form1()
        {
            try
            {
                string sSymbol = cmb_symbol.Text;
                lock (m_cache)
                {
                    m_database.Save(sSymbol, m_cache);
                    m_cache.Clear();
                }
            }
            catch { }
        }

        private void btn_connectDB_Click(object sender, EventArgs e)
        {
            m_database = new MySQL(m_mainConfig.m_sDB_Server, m_mainConfig.m_nDB_Port,
                m_mainConfig.m_sDB_User, m_mainConfig.m_sDB_Pwd);
        }

        private void btn_connectIB_Click(object sender, EventArgs e)
        {
            m_broker.Init();
            m_broker.Connect(m_mainConfig.m_sIB_Host, m_mainConfig.m_nIB_Port, m_mainConfig.m_nIB_ID);
            m_broker.SetHisUpdateAction(new Action<string, string, double, double, double, double>((s, t, o, h, l, c) =>
            {
                lock (m_cache)
                {
                    long lTime = Global.UnixDateTimeToSeconds(Global.ParseDate(t));
                    lock (m_exist)
                    {
                        if (m_exist.ContainsKey(lTime)) return;
                        m_exist[lTime] = true;
                    }

                    m_cache.Add(new Ohlc()
                    {
                        time = lTime,
                        open = o,
                        high = h,
                        low = l,
                        close = c
                    });
                    if (m_cache.Count >= 60)
                    {
                        m_nDownedRate += m_cache.Count;
                        m_database.Save(s, m_cache);
                        m_cache.Clear();
                        setDebugString("Downed Rates = " + m_nDownedRate);
                    }
                }
            }));
        }


        private void btn_downRate_Click(object sender, EventArgs e)
        {
            m_nDownedRate = 0;
            if (m_database == null || m_broker == null)
            {
                PutLog((m_database == null ? "DB" : "IB") + " is not connected");
                return;
            }
            string sSymbol = cmb_symbol.Text;
            m_database.CreateTable(sSymbol);
            DateTime dtStart = dateTimePicker_st.Value.Date;
            DateTime dtEnd = dateTimePicker_en.Value.Date.AddDays(1);
            List<Ohlc> lstOriginalData = m_database.Load(sSymbol, dtStart, dtEnd);
            PutLog(string.Format("DownRate({0},{1},{2})", sSymbol, dtStart, dtEnd));
            lock (m_exist)
            {
                foreach (var ohlc in lstOriginalData) m_exist[ohlc.time] = true;
            }
            new Thread(() =>
            {
                for (DateTime dtDay = dtStart; dtDay < dtEnd; dtDay = dtDay.AddHours(2))
                {
                    PutLog(string.Format("Get Historical Data {0},{1}", sSymbol, dtDay));
                    m_broker.GetHistoricalData(m_symbolConfig.FindSymbol(sSymbol),
                        dtDay);
                    Thread.Sleep(3000);
                }
            }).Start();
        }

        private void btn_loadRate_Click(object sender, EventArgs e)
        {
            btn_loadRate.Enabled = false;
            new Thread(() =>
            {
                try
                {
                    if (m_database == null)
                    {
                        PutLog(" DB is not connected");
                        return;
                    }
                    listView1.Invoke((MethodInvoker)delegate
                    {
                        string sSymbol = cmb_symbol.Text;
                        DateTime dtStart = dateTimePicker_st.Value.Date;
                        DateTime dtEnd = dateTimePicker_en.Value.Date.AddDays(1);
                        List<Ohlc> lstData = m_database.Load(sSymbol, dtStart, dtEnd);
                        lstData.Sort((x, y) => (int)(x.time - y.time));
                        listView1.BeginUpdate();
                        listView1.Items.Clear();
                        foreach (var ohlc in lstData)
                        {
                            ListViewItem item = new ListViewItem()
                            {
                                Text = Global.UnixSecondsToDateTime(ohlc.time).ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            item.SubItems.Add(ohlc.open.ToString());
                            item.SubItems.Add(ohlc.high.ToString());
                            item.SubItems.Add(ohlc.low.ToString());
                            item.SubItems.Add(ohlc.close.ToString());
                            listView1.Items.Add(item);
                        }
                        listView1.EndUpdate();
                    });
                }
                catch (Exception ex)
                {
                    PutLog("loadRate Exception : " + ex.Message);
                }
                btn_loadRate.Invoke((MethodInvoker)delegate
                {
                    btn_loadRate.Enabled = true;
                });
            }).Start();
        }

        private void btn_cur_Click(object sender, EventArgs e)
        {
            dateTimePicker_en.Value = DateTime.Now;
        }

        private void setDebugString(string sDebugStr)
        {
            lock (txt_debug)
            {
                txt_debug.Invoke((MethodInvoker)delegate
                {
                    txt_debug.Text = sDebugStr;
                });
            }
        }

        public void PutLog(string sLog)
        {
            if (m_fileLog == null)
            {
                string sLogDir = Global.LOG_MANAGER_DIR;
                if (!Directory.Exists(sLogDir))
                {
                    Directory.CreateDirectory(sLogDir);
                }
                m_fileLog = new StreamWriter(sLogDir + DateTime.Now.ToString("yyyyMMddHH") + ".txt", true);
                m_fileLog.AutoFlush = true;
            }
            lock (m_fileLog)
            {
                string sLogEx = string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), sLog);
                m_fileLog.WriteLine(sLogEx);
            }
        }

        private void init()
        {
            m_mainConfig = new MainConfig(Global.MAIN_CONFIG);
            m_symbolConfig = new SymbolConfig(Global.SYMBOL_CONFIG);
            cmb_symbol.Items.Clear();
            foreach (var sSymbol in m_symbolConfig.SymbolNameList())
            {
                cmb_symbol.Items.Add(sSymbol);
            }
            PutLog("Inited");
        }
    }
}
