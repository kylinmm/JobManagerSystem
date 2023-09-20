using log4net;
using JobManagerSystem.Core.Business.Info;
using JobManagerSystem.Core.Services;
using SqlSugar;
using System;
using System.Linq;

namespace JobManagerSystem.Core.Common
{
    public class Log
    {
        public static readonly ILog _logger = LogManager.GetLogger(typeof(Log));

        /// <summary>
        /// 日志事件
        /// </summary>
        /// <param name="db">SqlSugarClient</param>
        /// <param name="IsEnableLogEvent">是否启用</param>
        public static void _LogEvent(SqlSugarClient db, bool IsEnableLogEvent)
        {
            db.Ado.IsEnableLogEvent = IsEnableLogEvent;//启用日志事件
            if (IsEnableLogEvent)
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    Console.WriteLine();
                };
            }
        }

        /// <summary>
        /// 日志记录数据库
        /// </summary>
        /// <param name="Job"></param>
        /// <param name="Log"></param>
        public static void _LogToDb(BackgroundJobInfo Job, string Log)
        {
            new BackgroundJobService().WriteBackgroundJoLog(
                Job.BackgroundJobId,
                Job.Name,
                DateTime.Now, "<" + DateTime.Now + ">,Log:" + Log);
        }
    }
}