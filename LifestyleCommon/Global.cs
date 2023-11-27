using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LifestyleCommon
{
    public class Global
    {
        public static Action<string> OnLog = new Action<string>((x) => { });

        // file path
        public static string LOG_TRADER_DIR = Directory.GetCurrentDirectory() + "\\logs\\log_trader\\";
        public static string LOG_MANAGER_DIR = Directory.GetCurrentDirectory() + "\\logs\\log_manager\\";
        public static string MAIN_CONFIG = Directory.GetCurrentDirectory() + "\\config\\main.json";
        public static string SYMBOL_CONFIG = Directory.GetCurrentDirectory() + "\\config\\symbols.json";
        public static string STRATEGY_CONFIG = Directory.GetCurrentDirectory() + "\\config\\strategy.json";

        // constants
        public const double EPS = 1e-5;
        public const string MQ_TOPIC_CHART2TRADER = "CHART_TO_TRADER";
        public const string MQ_TOPIC_TRADER2CHART = "TRADER_TO_CHART";
        public const int PERSIST_STATE_CACHE_MAX_SIZE = 30;

        // Util functions
        public static DateTime UnixSecondsToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        public static long UnixDateTimeToSeconds(DateTime dt)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = dt - origin;
            return (long)diff.TotalSeconds;
        }

        public static DateTime ParseDate(string str)
        {
            string x = "";
            foreach (var c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    x += c;
                }
            }
            return new DateTime(
                int.Parse(x.Substring(0, 4)),
                int.Parse(x.Substring(4, 2)),
                int.Parse(x.Substring(6, 2)),
                int.Parse(x.Substring(8, 2)),
                int.Parse(x.Substring(10, 2)),
                int.Parse(x.Substring(12, 2)));
        }
    }

    public class Ohlc
    {
        public long time;
        public double open;
        public double high;
        public double low;
        public double close;
        public double open_ask;
        public double high_ask;
        public double low_ask;
        public double close_ask;
    }

    public class Tick
    {
        public long time;
        public double ask;
        public double bid;
    }

    public enum ORDER_COMMAND
    {
        NONE = 0,
        BUY = 1,
        SELL = 2,
        BUYCLOSE = 3,
        SELLCLOSE = 4
    }

    public enum CHART_ITEM_TYPE
    {
        rate = 0,
        pnt = 1,
        ind = 2
    }
}
