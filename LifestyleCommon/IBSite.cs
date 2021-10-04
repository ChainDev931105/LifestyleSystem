using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IBApi;
using IBTradingSystem.Broker.IB.messages;
using IBTradingSystem.Broker.IB;

namespace LifestyleCommon
{
    public class IBSite
    {
        private int m_nReqID = (new Random(DateTime.Now.Millisecond).Next() % 100) * 1000;
        private IBClient m_ibClient;
        private EReaderMonitorSignal signal = new EReaderMonitorSignal();
        private Dictionary<int, string> m_reqedSymbolDic = new Dictionary<int, string>();
        private int m_nextOrderId = 100;
        private Dictionary<int, List<OrderStatusMessage>> m_dicOrderResponse = new Dictionary<int, List<OrderStatusMessage>>();
        private Dictionary<int, Tick> m_dicRates = new Dictionary<int, Tick>();
        private Dictionary<Symbol, int> m_dicPriceReqID = new Dictionary<Symbol, int>();
        private int m_nPriceReqIDCnt = 0;

        public void Init()
        {
            m_ibClient = new IBClient(signal);
            // m_ibClient.ConnectionClosed += IBClient_ConnectionClosed;

            // m_ibClient.Error += IBClient_Error; // get error message
            // m_ibClient.NextValidId += ConnectionEventCallBack;    // get connection event
            m_ibClient.tickByTickBidAsk += TickMsg; // get tick bid ask values.
                                                    // m_ibClient.OrderStatus += OnOrderUpdate;
            m_ibClient.OrderStatus += onOrderStatusMessage;
        }

        public bool Connect(string sHost, int nPort, int nID)
        {
            try
            {
                m_ibClient.ClientId = nID;
                m_ibClient.ClientSocket.eConnect(sHost, nPort, nID);
                var reader = new EReader(m_ibClient.ClientSocket, signal);
                reader.Start();

                new Thread(() => { while (m_ibClient.ClientSocket.IsConnected()) { signal.waitForSignal(); reader.processMsgs(); } }) { IsBackground = true }.Start();
                return true;
            }
            catch (Exception e)
            {
                Global.OnLog("IB Connect Exception : " + e.Message);
                return false;
            }
        }

        public void SetHisUpdateAction(Action<string, string, double, double, double, double> onUpdate)
        {
            Action<HistoricalDataMessage> _onUpdate = (HistoricalDataMessage msg) =>
            {
                if (m_reqedSymbolDic.ContainsKey(msg.RequestId))
                {
                    onUpdate(m_reqedSymbolDic[msg.RequestId], msg.Date, msg.Open, msg.High, msg.Low, msg.Close);
                }
            };
            m_ibClient.HistoricalData += _onUpdate;
            //m_ibClient.HistoricalDataEnd += _onUpdate;
            m_ibClient.HistoricalDataUpdate += _onUpdate;
        }

        public void Disconnect()
        {
            m_ibClient.ClientSocket.eDisconnect();
        }

        public void Subscribe(List<Symbol> lstSymbol)
        {
            int id = 0;
            foreach (var symbol in lstSymbol)
            {
                m_ibClient.ClientSocket.reqTickByTickData(id, getContract(symbol), "BidAsk", 0, true);
            }
        }

        public void GetHistoricalData(Symbol symbol, DateTime dtStart)
        {
            int reqId = m_nReqID++;
            m_reqedSymbolDic[reqId] = symbol.m_sSymbol;
            
            m_ibClient.ClientSocket.reqHistoricalData(
                reqId, 
                getContract(symbol),
                dtStart.AddHours(2).ToString("yyyyMMdd HH:mm:ss") + " GMT", 
                "7200 S",
                "1 min", 
                "BID_ASK", 
                1, 
                1, 
                false, 
                null);
        }

        public bool RequestOrder(Symbol symbol, ORDER_COMMAND cmd, ref double dLots, ref double dPrice)
        {
            Global.OnLog(string.Format("RequestOrder({0},{1},{2},{3})",
                symbol.m_sSymbol, cmd, dLots, dPrice));
            int nOrderID = ++m_nextOrderId;
            Order order = new Order();
            order.Action = (cmd == ORDER_COMMAND.BUY || cmd == ORDER_COMMAND.SELLCLOSE) ? "BUY" : "SELL";

            order.OrderType = "MKT";
            order.TotalQuantity = Math.Round(dLots, 0);
            m_ibClient.ClientSocket.placeOrder(nOrderID, getContract(symbol), order);

            double dTotLots = 0;
            double dTotPrice = 0;
            DateTime dtWaitStart = DateTime.Now;
            while (DateTime.Now <= dtWaitStart.AddSeconds(5000) && Math.Abs(dTotLots - dLots) < 1e-5)
            {
                Thread.Sleep(100);
                lock (m_dicOrderResponse)
                {
                    if (!m_dicOrderResponse.ContainsKey(nOrderID) || m_dicOrderResponse[nOrderID].Count < 1) continue;
                    foreach (var orderStatusMessage in m_dicOrderResponse[nOrderID])
                    {
                        if (orderStatusMessage.Status == "Filled")
                        {
                            dTotLots += orderStatusMessage.Filled;
                            dTotPrice += orderStatusMessage.Filled * orderStatusMessage.AvgFillPrice;
                        }
                    }
                    m_dicOrderResponse[nOrderID].Clear();
                }
            }
            bool bFullFill = Math.Abs(dTotLots - dLots) < 1e-5;
            dLots = dTotLots;
            dPrice = dTotPrice / Math.Max(dTotLots, 1e-5);
            Global.OnLog(string.Format("   OrderResult = ({0},{1})", dLots, dPrice));
            return bFullFill;
        }

        private void onOrderStatusMessage(OrderStatusMessage orderStatusMessage)
        {
            Global.OnLog(string.Format("onOrderStatusMessage({0},{1},{2},{3})",
                orderStatusMessage.OrderId, orderStatusMessage.Status, orderStatusMessage.Filled, orderStatusMessage.AvgFillPrice));
            lock (m_dicOrderResponse)
            {
                if (!m_dicOrderResponse.ContainsKey(orderStatusMessage.OrderId))
                    m_dicOrderResponse[orderStatusMessage.OrderId] = new List<OrderStatusMessage>();
                m_dicOrderResponse[orderStatusMessage.OrderId].Add(orderStatusMessage);
            }
        }

        public double GetLots(Symbol symbol)
        {
            return 0;
        }

        public Tick GetRate(Symbol symbol)
        {
            if (!m_dicPriceReqID.ContainsKey(symbol))
            {
                m_dicPriceReqID[symbol] = ++m_nPriceReqIDCnt;
                m_ibClient.ClientSocket.reqTickByTickData(m_nPriceReqIDCnt, getContract(symbol), "BidAsk", 0, true);
                m_dicRates[m_dicPriceReqID[symbol]] = new Tick();
            }
            return m_dicRates[m_dicPriceReqID[symbol]];
        }

        void TickMsg(TickByTickBidAskMessage tick)
        {
            if (m_dicRates.ContainsKey(tick.ReqId))
            {
                m_dicRates[tick.ReqId] = new Tick()
                {
                    time = tick.Time,
                    ask = tick.AskPrice,
                    bid = tick.BidPrice
                };
            }
        }

        private Contract getContract(Symbol symbol)
        {
            return new Contract()
            {
                SecType = symbol.m_sSecurity,
                Symbol = symbol.m_sSymbol.Substring(0, 3),
                Exchange = symbol.m_sExchange,
                Currency = symbol.m_sSymbol.Substring(3)
            };
        }
    }
}
