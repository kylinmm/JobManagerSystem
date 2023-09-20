using SqlSugar;
using System;
using System.Linq;

namespace JobManagerSystem.Core.Business.Manager
{
    public class OracleDbManager
    {
        private static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringOracle"].ConnectionString;
            }
        }
        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = DbType.Oracle, IsAutoCloseConnection = true });
            Common.Log._LogEvent(db,false);
            return db;
        }
    }
}
