using SqlSugar;
using System;
using System.Configuration;

namespace JobManagerSystem.Core.Business.Manager
{
    public class DbManager
    {
        private string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["connectionStringLog"].ConnectionString;
            }
        }

        private string DbType
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DbType"];
            }
        }

        public SqlSugarClient db
        {
            get
            {
                SqlSugarClient _db = null;

                if (DbType?.ToLower() == "mysql")
                {
                    _db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = SqlSugar.DbType.MySql, IsAutoCloseConnection = true });
                }
                else if (DbType?.ToLower() == "sqlserver")
                {
                    _db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = SqlSugar.DbType.SqlServer, IsAutoCloseConnection = true });
                }
                else if (DbType?.ToLower() == "oracle")
                {
                    _db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = SqlSugar.DbType.Oracle, IsAutoCloseConnection = true });
                }
                else
                {
                    throw new Exception("DbType:" + DbType + " 未知");
                }
                //Common.Log._LogEvent(db, true);

                if (_db != null)
                {
                    string BackgroundJobMappingDbTable = GetDbTableNameSetting("BackgroundJobMappingDbTable");
                    BackgroundJobMappingDbTable = string.IsNullOrWhiteSpace(BackgroundJobMappingDbTable) ? "BackgroundJob" : BackgroundJobMappingDbTable;
                    _db.MappingTables.Add("BackgroundJobInfo", BackgroundJobMappingDbTable);

                    string BackgroundJobLogMappingDbTable = GetDbTableNameSetting("BackgroundJobLogMappingDbTable");
                    BackgroundJobLogMappingDbTable = string.IsNullOrWhiteSpace(BackgroundJobLogMappingDbTable) ? "BackgroundJobLog" : BackgroundJobLogMappingDbTable;
                    _db.MappingTables.Add("BackgroundJobLogInfo", BackgroundJobLogMappingDbTable);
                }
                return _db;
            }
        }

        private string GetDbTableNameSetting(string dbName)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(dbName);
        }
    }
}