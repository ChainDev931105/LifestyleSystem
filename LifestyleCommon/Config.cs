using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LifestyleCommon
{
    public class MainConfig
    {
        private JObject m_jConfig = null;
        public string m_sDB_Server = "localhost";
        public int m_nDB_Port = 3306;
        public string m_sDB_User = "root";
        public string m_sDB_Pwd = "root";
        public string m_sIB_Host = "";
        public int m_nIB_Port = 4002;
        public int m_nIB_ID = 1;
        public string m_sChart_Host = "";
        public string m_sChart_User = "";
        public string m_sChart_Pwd = "";

        public MainConfig(string sFile)
        {
            m_jConfig = JObject.Parse(File.ReadAllText(sFile));
            var jDB = m_jConfig["database"];
            m_sDB_Server = (string)jDB["db_server"];
            m_nDB_Port = (int)jDB["port"];
            m_sDB_User = (string)jDB["user"];
            m_sDB_Pwd = (string)jDB["password"];
            var jIB = m_jConfig["ib"];
            m_sIB_Host = (string)jIB["host"];
            m_nIB_Port = (int)jIB["port"];
            m_nIB_ID = (int)jIB["client_id"];
            var jChart = m_jConfig["chart"];
            m_sChart_Host = (string)jChart["host"];
            m_sChart_User = (string)jChart["user"];
            m_sChart_Pwd = (string)jChart["pwd"];
        }
    }

    public class SymbolConfig
    {
        private JObject m_jConfig = null;
        public List<Symbol> m_lstSymbol = new List<Symbol>();
        public List<TimeFrame> m_lstTF = new List<TimeFrame>();

        public SymbolConfig(string sFile)
        {
            m_jConfig = JObject.Parse(File.ReadAllText(sFile));
            JArray jSymbols = (JArray)m_jConfig["symbols"];
            m_lstSymbol.Clear();
            foreach (var jSymbol in jSymbols)
            {
                Symbol symbol = new Symbol();
                symbol.m_sSymbol = (string)jSymbol["symbol"];
                symbol.m_sExchange = (string)jSymbol["exchange"];
                symbol.m_sSecurity = (string)jSymbol["security"];
                m_lstSymbol.Add(symbol);
            }
            JArray jTimeFrames = (JArray)m_jConfig["timeframes"];
            m_lstTF.Clear();
            foreach (var jTimeFrame in jTimeFrames)
            {
                m_lstTF.Add(new TimeFrame()
                {
                    m_sName = (string)jTimeFrame["name"],
                    m_eType = (TF_TYPE)Enum.Parse(typeof(TF_TYPE), (string)jTimeFrame["type"], true),
                    m_nStart = int.Parse((string)jTimeFrame["start"]),
                    m_nSize = int.Parse((string)jTimeFrame["size"])
                });
            }
        }

        public Symbol FindSymbol(string sSymbol)
        {
            foreach (var symbol in m_lstSymbol)
            {
                if (symbol.m_sSymbol == sSymbol) return symbol;
            }
            return null;
        }

        public List<string> SymbolNameList()
        {
            List<string> lstSymbolName = new List<string>();
            foreach (var symbol in m_lstSymbol)
            {
                lstSymbolName.Add(symbol.m_sSymbol);
            }
            return lstSymbolName;
        }
    }

    public class Symbol
    {
        public string m_sSymbol;
        public string m_sExchange;
        public string m_sSecurity;
    }

    public class TimeFrame
    {
        public string m_sName;
        public long m_nStart;
        public TF_TYPE m_eType;
        public int m_nSize;

        public long GetStartMoment(long time)
        {
            time /= 60;
            if (time == 0) return 0;
            if (m_eType == TF_TYPE.small)
            {
                return 60 * (time - (((time % 10080) - (m_nStart % m_nSize) + m_nSize) % m_nSize));
            }
            else if (m_eType == TF_TYPE.medium)
            {
                return 60 * (time - ((((time - m_nStart) % m_nSize) + m_nSize) % m_nSize));
            }
            else if (m_eType == TF_TYPE.large)
            { // not completed yet
                return 60 * time;
            }
            return 0;
        }
    }

    public enum TF_TYPE
    {
        small = 0,
        medium = 1,
        large = 2
    }
}
