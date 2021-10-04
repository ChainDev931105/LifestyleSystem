using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using System.Threading;

namespace LifestyleCommon
{
    public class RabbitMQ
    {
        private string m_sPrefix;
        public IBus m_bus;
        List<Action<string>> m_actions = new List<Action<string>>();
        private bool m_bIsClient = true;

        public void connectToMQ(string sHost, string sUser, string sPwd, Action<string> onReceive = null, bool bIsClient = true)
        {
            if (onReceive == null) onReceive = new Action<string>((x) => { });
            m_bIsClient = bIsClient;
            m_bus = RabbitHutch.CreateBus(string.Format("host={0};username={1};password={2}", sHost, sUser, sPwd));
            m_bus.Subscribe<string>("Upload_lifestyle_" + m_bIsClient, onReceive, config_logicParam_down);
        }

        public void disConnect()
        {
            try
            {
                m_bus.Dispose();
            }
            catch
            {

            }
        }

        public void publish_msg(string sMsg)
        {
            try
            {
                m_bus.Publish(sMsg, x => x.WithTopic("topic_" + m_bIsClient));
            }
            catch { }
        }

        public void config_logicParam_down(EasyNetQ.FluentConfiguration.ISubscriptionConfiguration subConfig)
        {
            subConfig.WithTopic("topic_" + !m_bIsClient);
            subConfig.WithExpires(1000 * 60 * 10);
            subConfig.WithDurable(false);
        }
    }
}
