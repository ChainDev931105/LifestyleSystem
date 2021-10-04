using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace LifestyleCommon
{
    public class MySQL
    {
        private MySqlWrapper _sqlWrapper = null;

        public MySQL(string sDBSever, int nPort, string sUser, string sPwd)
        {
            _sqlWrapper = new MySqlWrapper();
            _sqlWrapper.connect(sDBSever, sUser, sPwd, "price_m1", nPort);
        }

        public void CreateTable(string sSymbol)
        {
            // create tables
            string sql = $"CREATE TABLE IF NOT EXISTS {sSymbol.ToString()} (" +
                $"`id` BIGINT(30) NOT NULL AUTO_INCREMENT," +
                $"`time` DATETIME NULL," +
                $"`symbol` VARCHAR(50) NOT NULL," +
                $"`open` DECIMAL(11,6) NULL DEFAULT NULL," +
                $"`high` DECIMAL(11,6) NULL DEFAULT NULL," +
                $"`low` DECIMAL(11,6) NULL DEFAULT NULL," +
                $"`close` DECIMAL(11,6) NULL DEFAULT NULL," +
                $"PRIMARY KEY (`id`)," +
                $"INDEX `time` (`time` ASC)" +
                $")";

            lock (_sqlWrapper)
            {
                _sqlWrapper.ExecuteSqlQuery(sql);
            }
        }

        public List<Ohlc> Load(string sSymbol, DateTime dtStart, DateTime dtEnd)
        {
            if (_sqlWrapper == null || !_sqlWrapper.IsConnected)
            {
                Global.OnLog("MySQL is not connected");
                return new List<Ohlc>();
            }
            List<Ohlc> lstRate = new List<Ohlc>();
            try
            {
                lock (_sqlWrapper)
                {
                    var res = _sqlWrapper.Table(sSymbol.ToLower()).Between("time", dtStart.ToString("yyyy/MM/dd HH:mm:ss"),
                        dtEnd.ToString("yyyy/MM/dd HH:mm:ss")).Get();
                    foreach (var item in res.Values)
                    {
                        lstRate.Add(new Ohlc()
                        {
                            time = Global.UnixDateTimeToSeconds(DateTime.Parse(item["time"])),
                            open = double.Parse(item["open"]),
                            high = double.Parse(item["high"]),
                            low = double.Parse(item["low"]),
                            close = double.Parse(item["close"])
                        });
                    }
                    lstRate.Sort(delegate(Ohlc x, Ohlc y)
                    {
                        if (x.time < y.time) return -1;
                        if (x.time > y.time) return 1;
                        return 0;
                    });
                }
            }
            catch (Exception e)
            {
                Global.OnLog("MySQL Get Exception : " + e.Message);
            }
            return lstRate;
        }

        public void Save(string sSymbol, List<Ohlc> lstRate)
        {
            if (_sqlWrapper == null || !_sqlWrapper.IsConnected)
            {
                Global.OnLog("MySQL is not connected");
                return;
            }
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            foreach (var ohlc in lstRate)
            {
                data.Add(new Dictionary<string, string>()
                {
                    { "time", Global.UnixSecondsToDateTime(ohlc.time).ToString("yyyy/MM/dd-HH:mm:ss.fff") },
                    { "symbol", sSymbol },
                    { "open", ohlc.open.ToString() },
                    { "high", ohlc.high.ToString() },
                    { "low", ohlc.low.ToString() },
                    { "close", ohlc.close.ToString() }
                });
            }
            try
            {
                lock (_sqlWrapper)
                {
                    _sqlWrapper.Table(sSymbol.ToLower()).InsertRows(data);
                }
            }
            catch (Exception e)
            {
                Global.OnLog("MySQL Save Exception : " + e.Message);
            }
        }
    }

    class MySqlWrapper
    {
        /*
        * Hold active MySql connection.
        */
        private MySqlConnection connection;

        /*
        * If connected.
        */
        private bool _isConnted = false;

        public bool IsConnected => _isConnted;

        /*
         * Hold MySql execution query.
         */
        private string query;

        /*
         * Hold database table.
         */
        private string tableName;

        /*
         * Hold query where conditions.
         */
        private string where;

        /*
         * MySql query debugging.
         */
        private readonly bool debug = false;

        /*
         * Connect to database.
        */

        public bool connect(string hostname, string username, string password, string database, int port = 3306)
        {
            try
            {
                String connectionQuery = "Server=" + hostname + ";User ID=" + username + ";Password=" + password + ";Database=" + database + ";Port=" + port;

                connection = new MySqlConnection(connectionQuery);
                connection.Open();
                Global.OnLog("Connected to mysql database");
                _isConnted = true;
                return true;
            }
            catch (Exception ex)
            {
                Global.OnLog("Failed to connect to mysql database : " + ex.Message);
                _isConnted = false;
                return false;
            }
        }

        /*
         * Disconnect to database.
         */
        public void disconnect()
        {
            connection.Dispose();
            _isConnted = false;
        }

        /*
         * Select database table.
         */
        public MySqlWrapper Table(string name)
        {
            tableName = name;

            return this;
        }

        public void ExecuteSqlQuery(string sql)
        {
            try
            {
                var cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
                Global.OnLog("executed sql query - " + sql);
            }
            catch (Exception ex)
            {
                Global.OnLog(ex.Message);
            }
        }


        /**
         * Set query where condition.
         */
        public MySqlWrapper Where(Dictionary<string, Dictionary<string, string>> condition)
        {
            where = "WHERE " + string.Join(" AND ", condition.Select(
                x => string.Join(" OR ", x.Value.Select(y => "`" + x.Key + "` " + y.Key + " '" + y.Value + "'").ToArray())
            ).ToArray());

            return this;
        }


        /**
         * Set query between condition.
         */
        public MySqlWrapper Between(string field, string st, string ed)
        {
            where = "WHERE `" + field + "` BETWEEN '" + st + "' AND '" + ed + "'";

            return this;
        }

        /*
         * Insert database record.
         */
        public void Insert(Dictionary<string, string> data)
        {
            string columns = string.Join(", ", data.Select(x => x.Key).ToArray());
            string values = string.Join(", ", data.Select(x => "'" + x.Value + "'").ToArray());

            query = "INSERT INTO `" + tableName + "`(" + columns + ") VALUES (" + values + ")";

            ExecuteQuery();
        }

        /*
         * Insert database record.
         */
        public void InsertRows(List<Dictionary<string, string>> rows)
        {
            string columns = string.Join(", ", rows[0].Select(x => x.Key).ToArray());

            string values = "";
            bool isFirst = true;
            foreach (Dictionary<string, string> row in rows)
            {
                values += (isFirst ? "(" : ",(") + string.Join(", ", row.Select(x => "'" + x.Value + "'").ToArray()) + ")";
                isFirst = false;
            }

            query = "INSERT INTO `" + tableName + "`(" + columns + ") VALUES " + values + ";";

            ExecuteQuery();
        }

        /*
         * Get Column names of table
         */
        public List<string> GetColumnNames(string table)
        {
            string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + table + "'";

            var cmd = new MySqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            var result = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string column = reader.GetValue(0).ToString();
                    if (column != "id" && column != "time" && column != "symbol")
                        result.Add(column);
                }
            }

            reader.Close();
            return result;
        }

        /*
         *  Get all table names.
         */
        public List<string> GetTableNames(string db = "price_data")
        {
            string sql = string.Format("SELECT table_name FROM information_schema.tables WHERE table_schema = '{0}';", db);
            var cmd = new MySqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            var result = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string column = reader.GetValue(0).ToString();
                    result.Add(column);
                }
            }

            reader.Close();
            return result;
        }


        /*
         * Get database record.
         */
        public Dictionary<int, Dictionary<string, string>> Get(string fields = "*", int rowLimit = 0)
        {
            string limit = rowLimit > 0 ? " LIMIT " + rowLimit : "";
            query = "SELECT " + fields + " FROM `" + tableName + "` " + where + limit;
            return ExecuteQuery();
        }

        /*
         * Update database record.
         */
        public void Update(Dictionary<string, string> data)
        {
            string update = string.Join(", ", data.Select(x => "`" + x.Key + "` = '" + x.Value + "'").ToArray());
            query = "UPDATE `" + tableName + "` SET " + update + " " + where;

            ExecuteQuery();
        }

        /*
         * Delete database record.
         */
        public void Delete()
        {
            query = "DELETE FROM `" + tableName + "` " + where;
            ExecuteQuery();
        }

        /*
         * Reset query and variables.
         */
        private void ResetQuery()
        {
            query = "";
            where = "";
            tableName = "";
        }

        /*
         * Execute query and return results.
         */
        private Dictionary<int, Dictionary<string, string>> ExecuteQuery()
        {
            if (debug)
            {
                Global.OnLog(query);
            }

            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();

            var result = new Dictionary<int, Dictionary<string, string>>();

            if (reader.HasRows)
            {
                int rowCol = 0;
                while (reader.Read())
                {
                    var fieldValue = new Dictionary<string, string>();

                    for (int col = 0; col < reader.FieldCount; col++)
                    {
                        fieldValue.Add(reader.GetName(col).ToString(), reader.GetValue(col).ToString());
                    }

                    result.Add(rowCol, fieldValue);
                    rowCol++;
                }

            }

            reader.Close();
            ResetQuery();

            return result;
        }
    }
}
