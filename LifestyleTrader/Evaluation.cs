using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LifestyleCommon;

namespace LifestyleTrader
{
    class Evaluation
    {
        bool m_bUpdated = true;
        double m_dBalance;
        double m_dAsset;
        double m_dLots;
        List<Position> m_lstPos = new List<Position>();
        double m_dMaxProfit;
        double m_dMDD;
        ListView m_listView_pos = null;
        ListView m_listView_eval = null;
        ListView m_listView_his = null;

        public Evaluation()
        {
            Manager.g_mainForm.GetListViews(ref m_listView_pos, ref m_listView_eval, ref m_listView_his);
            Init();
        }

        public void Init()
        {
            m_dBalance = 0;
            m_dAsset = 0;
            m_dLots = 0;
            m_lstPos.Clear();
            m_dMaxProfit = 0;
            m_dMDD = 0;
        }

        public void RequestOrder(string sSymbol, ORDER_COMMAND cmd, double dLots, double dPrice)
        {
            m_bUpdated = true;
            if (cmd == ORDER_COMMAND.BUY || cmd == ORDER_COMMAND.SELLCLOSE)
            {
                m_dLots += dLots;
                m_dAsset -= dLots * dPrice;
            }
            if (cmd == ORDER_COMMAND.SELL || cmd == ORDER_COMMAND.BUYCLOSE)
            {
                m_dLots -= dLots;
                m_dAsset += dLots * dPrice;
            }
            bool bClosed = false;
            if (Math.Abs(m_dLots) < Global.EPS)
            {
                m_dLots = 0;
                m_dBalance = m_dAsset;
                m_dMaxProfit = Math.Max(m_dBalance, m_dMaxProfit);
                m_dMDD = Math.Max(m_dMDD, m_dMaxProfit - m_dBalance);
                bClosed = true;
            }
            Position pos = new Position()
            {
                sSymbol = sSymbol,
                eCmd = cmd,
                dLots = dLots,
                dPrice = dPrice,
                dtTime = Manager.g_dtCurTime
            };
            m_lstPos.Add(pos);

            Manager.g_mainForm.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                addToListView(m_listView_his, pos);
                if (bClosed) removeFromListView(m_listView_pos);
                else
                {
                    addToListView(m_listView_pos, pos);
                }
                if (IsUpdated())
                {
                    Display();
                }
            });
        }

        public double Lots()
        {
            return m_dLots;
        }

        public bool IsUpdated(bool bCheck = true)
        {
            return m_bUpdated;
        }

        public void Display()
        {
            m_bUpdated = false;
            m_listView_eval.BeginUpdate();
            m_listView_eval.Items.Clear();
            foreach (var p in new Dictionary<string, string>()
            {
                { "Profit", string.Format("{0:N6}", m_dBalance) },
                { "TradeCnt", m_lstPos.Count.ToString() },
                { "MDD", string.Format("{0:N6}", m_dMDD) }
            })
            {
                ListViewItem item = new ListViewItem() { Text = p.Key };
                item.SubItems.Add(p.Value);
                m_listView_eval.Items.Add(item);
            }
            m_listView_eval.EndUpdate();
        }

        private ListViewItem posToItem(Position pos)
        {
            ListViewItem item = new ListViewItem() { Text = pos.sSymbol };
            item.SubItems.Add(pos.eCmd.ToString());
            item.SubItems.Add(pos.dLots.ToString());
            item.SubItems.Add(pos.dPrice.ToString());
            item.SubItems.Add(pos.dtTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return item;
        }

        private void addToListView(ListView listView, Position pos)
        {
            listView.BeginUpdate();
            listView.Items.Add(posToItem(pos));
            listView.EndUpdate();
        }

        private void removeFromListView(ListView listView)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            listView.EndUpdate();
        }

        private struct Position
        {
            public string sSymbol;
            public ORDER_COMMAND eCmd;
            public double dLots;
            public double dPrice;
            public DateTime dtTime;
        }
    }
}
