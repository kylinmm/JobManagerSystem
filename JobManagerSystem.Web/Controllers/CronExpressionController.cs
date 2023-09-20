using JobManagerSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;

namespace JobManagerSystem.Controllers
{
    public class CronExpressionController : Controller
    {
        //
        // GET: /CronExpression/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalcRunTime(string CronExpression)
        {
            var result = new ResponseResult();
            try
            {
                result.data = GetTaskeFireTime(CronExpression, 5);
                result.success = true;
            }
            catch
            {
                result.data = "[]";
                result.success = false;
            }
            return Json(result);
        }

        /// <summary>
        /// 获取任务在未来周期内哪些时间会运行
        /// </summary>
        /// <param name="CronExpressionString">Cron表达式</param>
        /// <param name="numTimes">运行次数</param>
        /// <returns>运行时间段</returns>
        public static List<string> GetTaskeFireTime(string CronExpressionString, int numTimes)
        {
            if (numTimes < 0)
            {
                throw new Exception("参数numTimes值大于等于0");
            }
            //时间表达式
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(CronExpressionString).Build();
            IList<DateTimeOffset> dates = (IList<DateTimeOffset>)TriggerUtils.ComputeFireTimes(trigger as IOperableTrigger, null, numTimes);
            List<string> list = new List<string>();
            foreach (DateTimeOffset dtf in dates)
            {
                list.Add(TimeZoneInfo.ConvertTimeFromUtc(dtf.DateTime, TimeZoneInfo.Local).ToString());
            }
            return list;
        }
    }
}