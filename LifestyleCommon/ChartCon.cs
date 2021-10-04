using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace LifestyleCommon
{
    public class ChartCon
    {
        private const int MULTI_SEND_UNIT = 16;
        private const int MAX_CACHE_SIZE = 1024;
        List<string> m_cache = new List<string>();
        private bool m_bConnect = false;
        RabbitMQ m_rabbitMQ = new RabbitMQ();

        public ChartCon()
        {// not completed yet
        }

        public bool Connect(string sHost, string sUser, string sPwd)
        {
            try
            {
                m_rabbitMQ.connectToMQ(sHost, sUser, sPwd);
            }
            catch (Exception e)
            {
                Global.OnLog("ChartCon Exception : " + e.Message);
                return false;
            }
            m_bConnect = true;
            return true;
        }

        public bool Disconnect()
        {
            m_bConnect = false;
            try
            {
                m_rabbitMQ.disConnect();
            }
            catch { }
            return true;
        }

        public void Send(List<string> lstWord, bool bMustPublish = false)
        {
            List<string> lstMsg = new List<string>();
            lock (m_cache)
            {
                m_cache.Add(joinWords(lstWord));
                while (!m_bConnect && m_cache.Count > MAX_CACHE_SIZE)
                {
                    m_cache.RemoveAt(0);
                }
                if (m_bConnect && (bMustPublish || m_cache.Count >= MULTI_SEND_UNIT))
                {
                    int nCnt = Math.Min(m_cache.Count, MULTI_SEND_UNIT);
                    for (int i = 0; i < nCnt; i++)
                    {
                        lstMsg.Add(m_cache[0]);
                        m_cache.RemoveAt(0);
                    }
                }
            }
            if (lstMsg.Count > 0) publish(lstMsg);
        }

        public void SendDirect(List<string> lstWord)
        {
            publish(new List<string>(){ joinWords(lstWord) });
        }

        public void Publish()
        {
            while (m_bConnect)
            {
                List<string> lstMsg = new List<string>();
                lock (m_cache)
                {
                    if (m_cache.Count > 0)
                    {
                        int nSz = Math.Min(MULTI_SEND_UNIT, m_cache.Count);
                        for (int i = 0; i < nSz; i++)
                        {
                            lstMsg.Add(m_cache[0]);
                            m_cache.RemoveAt(0);
                        }
                    }
                }
                if (lstMsg.Count > 0)
                {
                    publish(lstMsg);
                }
                else break;
                Thread.Sleep(10);
            }
        }

        private string joinWords(List<string> lstWord)
        {
            string sRlt = "";
            foreach (var sWord in lstWord)
                sRlt += sWord + ";";
            return sRlt;
        }

        private void publish(List<string> lstMsg)
        {
            string sTotMsg = lstMsg.Count + ";";
            foreach (var sMsg in lstMsg)
                sTotMsg += sMsg;
            m_rabbitMQ.publish_msg(sTotMsg);
        }
    }
}
