using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.IO;
using LifestyleCommon;

namespace LifestyleChart
{
    public partial class Form1 : Form
    {
        private static string CHART_CONFIG_FILE = Directory.GetCurrentDirectory() + "\\config\\chart.json";
        JObject m_jConfig = null;
        RabbitMQ m_rmq = null;
        Dictionary<Tuple<string, string>, RateData> m_dataRate = new Dictionary<Tuple<string, string>, RateData>();
        Dictionary<Tuple<string, string>, PntData> m_dataPnt = new Dictionary<Tuple<string, string>, PntData>();
        Dictionary<Tuple<string, string>, IndData> m_dataInd = new Dictionary<Tuple<string, string>, IndData>();

        Tuple<string, string> m_activeRateKey = Tuple.Create("", "");
        Tuple<string, string> m_activePntKey = Tuple.Create("", "");
        List<Tuple<string, string>> m_activeIndKeys = new List<Tuple<string, string>>() {
            Tuple.Create("", ""),
            Tuple.Create("", ""),
            Tuple.Create("", ""),
            Tuple.Create("", "")
        };

        public Form1()
        {
            InitializeComponent();
        }

        ~Form1()
        {
            try
            {
                m_rmq.disConnect();
            }
            catch { }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            btn_connect.Enabled = false;
            if (btn_connect.Text == "Connect")
            {
                m_jConfig = JObject.Parse(File.ReadAllText(CHART_CONFIG_FILE));
                try
                {
                    string sHost = (string)m_jConfig["host"];
                    string sUser = (string)m_jConfig["user"];
                    string sPwd = (string)m_jConfig["pwd"];
                    m_rmq = new RabbitMQ();
                    m_rmq.connectToMQ(sHost, sUser, sPwd, onReceiveGroup, false);
                    btn_connect.Text = "Disconnect";
                }
                catch (Exception ex)
                {
                    Global.OnLog("ChartCon Exception : " + ex.Message);
                }
            }
            else
            {
                try
                {
                    m_rmq.disConnect();
                }
                catch { }
                btn_connect.Text = "Connect";
            }
            btn_connect.Enabled = true;
        }

        private void btn_draw_price_Click(object sender, EventArgs e)
        {
            if (cmb_price.Text.Contains(':'))
            {
                string[] sWords = cmb_price.Text.Split(':');
                m_activeRateKey = Tuple.Create(sWords[0], sWords[1]);
                ucSciStockChart1.ClearRate();
                if (m_dataRate.ContainsKey(m_activeRateKey)) m_dataRate[m_activeRateKey].Prepare();
            }
        }

        private void btn_draw_pnt_Click(object sender, EventArgs e)
        {
            if (cmb_pnt.Text.Contains(':'))
            {
                string[] sWords = cmb_pnt.Text.Split(':');
                m_activePntKey = Tuple.Create(sWords[0], sWords[1]);
                ucSciStockChart1.ClearPnt();
                if (m_dataPnt.ContainsKey(m_activePntKey)) m_dataPnt[m_activePntKey].Prepare();
            }
        }
        private void btn_draw_ind_Click(int id)
        {
            if (cmb_ind.Text.Contains(':'))
            {
                string[] sWords = cmb_ind.Text.Split(':');
                m_activeIndKeys[id] = Tuple.Create(sWords[0], sWords[1]);
                ucSciStockChart1.ClearInd();
                if (m_dataInd.ContainsKey(m_activeIndKeys[id])) m_dataInd[m_activeIndKeys[id]].Prepare();
            }
        }

        private void btn_draw_ind0_Click(object sender, EventArgs e) { btn_draw_ind_Click(0); }
        private void btn_draw_ind1_Click(object sender, EventArgs e) { btn_draw_ind_Click(1); }
        private void btn_draw_ind2_Click(object sender, EventArgs e) { btn_draw_ind_Click(2); }
        private void btn_draw_ind3_Click(object sender, EventArgs e) { btn_draw_ind_Click(3); }


        private void btn_clear_Click(object sender, EventArgs e)
        {
            btn_clear.Enabled = false;
            timer1.Enabled = false;
            System.Threading.Thread.Sleep(2000);
            for (int nTry = 0; nTry < 3; nTry++)
            {
                try
                {
                    cmb_ind.Items.Clear();
                    m_dataRate.Clear();
                    System.Threading.Thread.Sleep(1000);
                }
                catch { }
            }
            timer1.Enabled = true;
            btn_clear.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateChart();
        }


        private void onReceiveGroup(string sGroupMsg)
        {
            try
            {
                string[] sWords = sGroupMsg.Split(';');
                int nCnt = int.Parse(sWords[0]);
                int nID = 1;
                for (int i = 0; i < nCnt; i++)
                {
                    CHART_ITEM_TYPE eType = (CHART_ITEM_TYPE)Enum.Parse(typeof (CHART_ITEM_TYPE), sWords[nID + 1], true);
                    int nLen = 8;
                    if (eType == CHART_ITEM_TYPE.rate) nLen = 8;
                    else if (eType == CHART_ITEM_TYPE.pnt) nLen = 6;
                    else if (eType == CHART_ITEM_TYPE.ind) nLen = 6;
                    List<string> tmp = new List<string>();
                    for (int j = nID + 2; j < nID + nLen; j++) tmp.Add(sWords[j]);
                    onReceive(sWords[nID], eType, tmp);
                    nID += nLen;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void onReceive(string sLogicID, CHART_ITEM_TYPE eType, List<string> lstWords)
        {
            if (eType == CHART_ITEM_TYPE.rate)
            {
                Tuple<string, string> key = Tuple.Create(sLogicID, lstWords[0]);
                if (!m_dataRate.ContainsKey(key))
                {
                    m_dataRate[key] = new RateData();
                    cmb_price.Invoke((MethodInvoker)delegate
                    {
                        lock (cmb_price)
                        {
                            cmb_price.Items.Add(key.Item1 + ":" + key.Item2);
                        }
                    });
                }
                m_dataRate[key].Update(new Ohlc()
                {
                    time = long.Parse(lstWords[1]),
                    open = double.Parse(lstWords[2]),
                    high = double.Parse(lstWords[3]),
                    low = double.Parse(lstWords[4]),
                    close = double.Parse(lstWords[5]),
                    open_ask = double.Parse(lstWords[2]),
                    high_ask = double.Parse(lstWords[3]),
                    low_ask = double.Parse(lstWords[4]),
                    close_ask = double.Parse(lstWords[5])
                });
            }
            else if (eType == CHART_ITEM_TYPE.ind)
            {
                Tuple<string, string> key = Tuple.Create(sLogicID, lstWords[0]);
                if (!m_dataInd.ContainsKey(key))
                {
                    m_dataInd[key] = new IndData();
                    cmb_ind.Invoke((MethodInvoker)delegate
                    {
                        lock (cmb_ind)
                        {
                            cmb_ind.Items.Add(key.Item1 + ":" + key.Item2);
                        }
                    });
                }
                m_dataInd[key].Update(long.Parse(lstWords[1]), double.Parse(lstWords[2]));
            }
            else if (eType == CHART_ITEM_TYPE.pnt)
            {
                Tuple<string, string> key = Tuple.Create(sLogicID, lstWords[0]);
                if (!m_dataPnt.ContainsKey(key))
                {
                    m_dataPnt[key] = new PntData();
                    cmb_pnt.Invoke((MethodInvoker)delegate
                    {
                        lock (cmb_pnt)
                        {
                            cmb_pnt.Items.Add(key.Item1 + ":" + key.Item2);
                        }
                    });
                }
                m_dataPnt[key].Update(long.Parse(lstWords[1]), double.Parse(lstWords[2]), lstWords[3]);
            }
        }

        private void updateChart()
        {
            if (m_dataRate.ContainsKey(m_activeRateKey))
            {
                try
                {
                    RateData dataRate = m_dataRate[m_activeRateKey];
                    List<Ohlc> lstUpdate = dataRate.GetUpdateList();
                    DateTime dtLastTime = ucSciStockChart1.getLastTime_price();
                    displayBug(dtLastTime.ToString());
                    foreach (var ohlc in lstUpdate)
                    {
                        DateTime dt = Global.UnixSecondsToDateTime(ohlc.time);
                        ucSciStockChart1.update_price(0, dt > dtLastTime, dt,
                            ohlc.open, ohlc.high, ohlc.low, ohlc.close);
                    }
                }
                catch { }
            }
            if (m_dataPnt.ContainsKey(m_activePntKey))
            {
                try
                {
                    PntData dataPnt = m_dataPnt[m_activePntKey];
                    List<Pnt> lstUpdate = dataPnt.GetUpdateList();
                    foreach (var pnt in lstUpdate)
                    {
                        DateTime dt = Global.UnixSecondsToDateTime(pnt.time);
                        if (m_activePntKey.Item2 == "SIGNAL")
                        {
                            if (pnt.sComment == "BUY" || pnt.sComment == "SELL_CLOSE") ucSciStockChart1.update_pnt(0, dt, pnt.dValue);
                            else if (pnt.sComment == "SELL" || pnt.sComment == "BUY_CLOSE") ucSciStockChart1.update_pnt(1, dt, pnt.dValue);
                        }
                        else
                        {
                            ucSciStockChart1.AddAnnotation(m_activePntKey.Item2, dt, pnt.dValue, pnt.sComment);
                        }
                    }
                }
                catch { }
            }
            for (int i = 0; i < m_activeIndKeys.Count; i++)
            {
                if (m_dataInd.ContainsKey(m_activeIndKeys[i]))
                {
                    try
                    {
                        IndData dataInd = m_dataInd[m_activeIndKeys[i]];
                        List<Tuple<long, double>> lstUpdate = dataInd.GetUpdateList();
                        foreach (var ind in lstUpdate)
                        {
                            DateTime dt = Global.UnixSecondsToDateTime(ind.Item1);
                            ucSciStockChart1.update_ind(i, dt, ind.Item2);
                        }
                    }
                    catch { }
                }
            }
        }

        private void displayBug(string sMsg)
        {
            txt_bug.Invoke((MethodInvoker)delegate
            {
                txt_bug.Text = sMsg;
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
