using JobManagerSystem.Core.Business.Info;
using JobManagerSystem.Core.Common;
using JobManagerSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;

namespace JobManagerSystem.Controllers
{
    public class BaseController : Controller
    {
        #region JsonResult

        public new JsonResult Json(object data)
        {
            return new JsonResult(data, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
        }

        public new JsonResult Json(object data, string DateFormatString)
        {
            return new JsonResult(data, new JsonSerializerSettings() { DateFormatString = DateFormatString });
        }

        #endregion


        /// <summary>
        /// 获取页面参数
        /// </summary>
        /// <returns></returns>
        public PageParameter GetPageParameter()
        {
            PageParameter parameter = new PageParameter();
            parameter.rows = WebHelper.GetRequestInt("rows", 1);
            parameter.currentPageIndex = WebHelper.GetRequestInt("page", 1);
            string Param = WebHelper.GetRequestString("Param");
            if (!string.IsNullOrEmpty(Param))
            {
                try
                {
                    if (Param.Contains(",}"))
                    {
                        Param = Param.Replace(",}", "}");
                    }
                    parameter.SetDictionary(JsonConvert.DeserializeObject(Param, typeof(ConcurrentDictionary<string, string>)) as ConcurrentDictionary<string, string>);
                }
                catch (Exception)
                { }
            }

            foreach (string key in Request.Query.Keys)
            {
                if (!string.IsNullOrWhiteSpace(key) && !key.Equals("rows") && !key.Equals("page") && !key.Equals("Param"))
                {
                    parameter.AddParameter(key, Request.Query[key]);
                }
            }
            return parameter;
        }
    }
}