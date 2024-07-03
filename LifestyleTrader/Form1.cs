using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifestyleTrader
{
    public partial class Form1 : Form
    {
        private DateTime g_dtLastPerfUpdate = new DateTime();
        private object m_oLock = null;
        private DetailForm m_detailForm = new DetailForm();


        public Form1()
        {
            InitializeComponent();
            foreach (var eMode in (RUN_MODE[])Enum.GetValues(typeof(RUN_MODE)))
            {
                if (eMode == RUN_MODE.NONE) continue;
                cmb_mode.Items.Add(eMode.ToString());
            }
            btn_stop.Enabled = false;
            this.Width -= panel_eval.Width;
            Manager.Init(this);
            m_oLock = new object();
        }

        ~Form1()
        {
            try
            {
                Manager.Stop();
            }
            catch { }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            btn_start.Enabled = false;
            string sRunMode = cmb_mode.Text;
            string sSymbol = cmb_symbol.Text;
            if (sRunMode.Length < 1) DisplayState("Please select MODE");
            else if (sSymbol.Length < 1) DisplayState("Please select symbol");
            else Manager.Start((RUN_MODE)Enum.Parse(typeof(RUN_MODE), sRunMode, true),
                sSymbol, dtpicker_st.Value, dtpicker_en.Value);
            btn_stop.Enabled = true;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            btn_stop.Enabled = false;
            Manager.Stop();
            btn_start.Enabled = true;
        }

        private void btn_chart_Click(object sender, EventArgs e)
        {
            string sConnect = "Connect Chart";
            string sDisconnect = "Disconnect Chart";
            btn_chart.Enabled = false;
            if (btn_chart.Text == sConnect)
            {
                bool bRlt = Manager.CH_Connect();
                Manager.PutLog("Connect Chart " + (bRlt ? "Success" : "Failed"));
                if (bRlt) btn_chart.Text = sDisconnect;
            }
            else if (btn_chart.Text == sDisconnect)
            {
                btn_chart.Text = sConnect;
                bool bRlt = Manager.CH_Disconnect();
                Manager.PutLog("Disconnect Chart " + (bRlt ? "Success" : "Failed"));
                if (bRlt) btn_chart.Text = sConnect;
            }
            btn_chart.Enabled = true;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            lock (txt_log)
            {
                txt_log.Invoke((MethodInvoker)delegate
                {
                    txt_log.Text = "";
                });
            }
        }

        public void DisplayLog(string sLog)
        {
            if (m_oLock == null) return;
            lock (m_oLock)
            {
                txt_log.Invoke((MethodInvoker)delegate
                {
                    txt_log.Text += sLog + "\r\n";
                    if (txt_log.Text.Length > TraderGlobal.MAX_LOG_DISPLAY_LENGTH)
                    {
                        txt_log.Text = txt_log.Text.Substring(TraderGlobal.MAX_LOG_DISPLAY_LENGTH / 2);
                    }
                    txt_log.SelectionStart = txt_log.Text.Length;
                    txt_log.ScrollToCaret();
                });
            }
        }

        public void DisplayState(string sState)
        {
            lock (txt_state)
            {
                txt_state.Invoke((MethodInvoker)delegate
                {
                    txt_state.Text = sState;
                });
            }
        }

        public void DisplayPerformance(string sPerf, bool bMust = false)
        {
            if (!bMust && DateTime.Now < g_dtLastPerfUpdate.AddMilliseconds(500)) return;
            g_dtLastPerfUpdate = DateTime.Now;
            lock (txt_perf)
            {
                txt_perf.Invoke((MethodInvoker)delegate
                {
                    txt_perf.Text = sPerf;
                });
            }
            g_dtLastPerfUpdate = DateTime.Now;
        }

        public void SetSymbolList(List<string> lstSymbol)
        {
            cmb_symbol.Items.Clear();
            foreach (var sSymbol in lstSymbol)
            {
                cmb_symbol.Items.Add(sSymbol);
            }
        }

        public void OnStop()
        {
            try
            {
                btn_stop.Invoke((MethodInvoker)delegate
                {
                    btn_stop_Click(null, null);
                });
            }
            catch { }
        }

        private void btn_eval_Click(object sender, EventArgs e)
        {
            if (btn_eval.Text.Contains(">>"))
            {
                btn_eval.Text = btn_eval.Text.Replace(">>", "<<");
                this.Width += panel_eval.Width;
            }
            else if (btn_eval.Text.Contains("<<"))
            {
                btn_eval.Text = btn_eval.Text.Replace("<<", ">>");
                this.Width -= panel_eval.Width;
            }
        }

        public void GetListViews(ref ListView listView_pos, ref ListView listView_eval, ref ListView listView_his, ref ListView listView_tf)
        {
            listView_pos = this.listView_pos;
            listView_eval = this.listView_eval;
            listView_his = this.listView_his;
            listView_tf = this.listView_tf;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void btn_detailForm_Click(object sender, EventArgs e)
        {
            m_detailForm.Show();
        }
    }
}
