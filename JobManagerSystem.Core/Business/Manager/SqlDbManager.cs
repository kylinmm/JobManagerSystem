using SqlSugar;
using System;
using System.Linq;

namespace JobManagerSystem.Core.Business.Manager
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public class SqlDbManager
    {
        private static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            }
        }


        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = DbType.SqlServer, IsAutoCloseConnection = true, InitKeyType = InitKeyType.Attribute });
            Common.Log._LogEvent(db,false);
            return db;
        }
    }
}
