using JobManagerSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace JobManagerSystem.Core.Common
{
    /// <summary>
    /// Web帮助类
    /// </summary>
    public class WebHelper
    {

        public static IHttpContextAccessor Accessor;
        public static HttpContext GetContext()
        {
            return Accessor.HttpContext;
        }

        /// <summary>
        /// 是否是get请求
        /// </summary>
        /// <returns></returns>
        public static bool IsGet()
        {
            return GetContext().Request.Method == "GET";
        }

        /// <summary>
        /// 是否是post请求
        /// </summary>
        /// <returns></returns>
        public static bool IsPost()
        {
            return GetContext().Request.Method == "POST";
        }

        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAjax()
        {
            if (GetContext() != null && GetContext().Request != null)
            {
                return GetContext().Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            }
            return false;
        }

        /// <summary>
        /// 获得查询字符串中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetQueryString(string key, string defaultValue)
        {
            string value = GetContext().Request.Query[key].ToString();
            if (!string.IsNullOrWhiteSpace(value))
                return value;
            else
                return defaultValue;
        }

        /// <summary>
        /// 获得查询字符串中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetQueryString(string key)
        {
            return GetQueryString(key, "");
        }

        /// <summary>
        /// 获得查询字符串中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetQueryInt(string key, int defaultValue)
        {
            string val = GetContext().Request.Query[key].ToString();
            if (!string.IsNullOrWhiteSpace(val))
            {
                int result;
                if (int.TryParse(val, out result))
                    return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 获得查询字符串中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static int GetQueryInt(string key)
        {
            return GetQueryInt(key, 0);
        }

        /// <summary>
        /// 获得表单中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetFormString(string key, string defaultValue)
        {
            string value = GetContext().Request.Form[key];
            if (!string.IsNullOrWhiteSpace(value))
                return value;
            else
                return defaultValue;
        }

        /// <summary>
        /// 获得表单中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetFormString(string key)
        {
            return GetFormString(key, "");
        }

        /// <summary>
        /// 获得表单中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetFormInt(string key, int defaultValue)
        {
            string val = GetContext().Request.Form[key];
            if (!string.IsNullOrWhiteSpace(val))
            {
                int result;
                if (int.TryParse(val, out result))
                    return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 获得表单中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static int GetFormInt(string key)
        {
            return GetFormInt(key, 0);
        }

        /// <summary>
        /// 获得请求中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetRequestString(string key, string defaultValue)
        {
            if (!string.IsNullOrEmpty(GetContext().Request.Form[key]))
                return GetFormString(key, defaultValue);
            else
                return GetQueryString(key, defaultValue);
        }

        /// <summary>
        /// 获得请求中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetRequestString(string key)
        {
            if (!string.IsNullOrEmpty(GetContext().Request.Form[key]))
                return GetFormString(key);
            else
                return GetQueryString(key);
        }

        /// <summary>
        /// 获得请求中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetRequestInt(string key, int defaultValue)
        {
            if (!string.IsNullOrEmpty(GetContext().Request.Form[key]))
                return GetFormInt(key, defaultValue);
            else
                return GetQueryInt(key, defaultValue);
        }

        /// <summary>
        /// 获得请求中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static int GetRequestInt(string key)
        {
            if (!string.IsNullOrEmpty(GetContext().Request.Form[key]))
                return GetFormInt(key);
            else
                return GetQueryInt(key);
        }

        /// <summary>
        /// 请求提交数据包
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="BakDirPath">备份路径</param>
        /// <returns></returns>
        public static string RequestData(string url, string token, string dataJson, string BakDirPath)
        {
            #region 实例化实体

            ApiResult apiResult = new ApiResult();
            RecordPackage recordPackage = new RecordPackage();

            #endregion 实例化实体

            try
            {
                
                if (token != "")
                {
                    apiResult = JsonConvert.DeserializeObject<ApiResult>(RequestHttp(url, token, dataJson));
                }
                else
                {
                    apiResult = JsonConvert.DeserializeObject<ApiResult>(RequestHttpGet(url, token));//20221107
                }
                #region 记录请求数据及返回结果
                Log._logger.Info(BakDirPath + ";" + JsonConvert.SerializeObject(apiResult));
                recordPackage.RequestTime = DateTime.Now.ToString();
                recordPackage.RequestUrl = url;
                recordPackage.RequestToken = token;
                recordPackage.SubData = dataJson;
                recordPackage.ResultData = JsonConvert.SerializeObject(apiResult);
                BakDirPath = BakDirPath + GetDatePathStamp();
                if (IsDirPathExist(BakDirPath))
                {
                    if (!XmlSerialize(BakDirPath + @"\RecordPackage-" + GetTimeStamp() + ".xml", recordPackage))
                    {
                        Log._logger.Error("写入XML文件失败!");
                    }
                }

                #endregion 记录请求数据及返回结果
            }
            catch (Exception ex)
            {
                Log._logger.Error("NG:调用Api:" + url + "失败,\r\n" + " 执行过程中发生异常:" + ex.Message);
                apiResult.status = "error";
                apiResult.stackInfo = ex.Message;
            }
            return JsonConvert.SerializeObject(apiResult);
        }
        /// <summary>
        /// 自动获取Token
        /// </summary>
        /// <param name="apiTokenKeyUrl"></param>
        /// <param name="BakDirPath"></param>
        /// <returns></returns>
        public static string GetToken(string apiTokenKeyUrl, string BakDirPath)
        {
            string apiToken = "";
            try
            {

                string userinfo_dataJson = "";
                var TokendataRes = WebHelper.RequestData(apiTokenKeyUrl, apiToken, userinfo_dataJson, BakDirPath);
                ApiResult ApiResult = JsonConvert.DeserializeObject<ApiResult>(TokendataRes);
                apiToken = ApiResult.data.ToString();
            }
            catch (Exception ex)
            {
                Log._logger.Error("NG:GetToken失败" + " 执行过程中发生异常:" + ex.Message);
                apiToken = "{'status':'error','message':'" + apiTokenKeyUrl + "接口出错','stackInfo':'" + ex.Message + "','data':''}";
            }
            return apiToken;
        }


        /// <summary>
        /// Request请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="token">token</param>
        /// <param name="data">传递的参数</param>
        /// <param name="contentType">application/x-www-form-urlencoded</param>
        /// <param name="Method">POST,GET等</param>
        /// <param name="Timeout">请求超时</param>
        /// <returns></returns>
        private static string RequestHttp(string url, string token, string data, string contentType = "application/json;charset=utf-8", string Method = "POST", int Timeout = 15000)
        {
            string responseContent = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                if (token!="")
                {
                    httpWebRequest.Headers.Add("Authorization", $"Bearer {token}");
                }
                else
                {
                    Method = "GET";
                }    

                httpWebRequest.ContentType = contentType;
                httpWebRequest.Method = Method;
                httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
                httpWebRequest.Timeout = Timeout;

                byte[] btBodys = Encoding.UTF8.GetBytes(data);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                responseContent = streamReader.ReadToEnd();

                httpWebResponse.Close();
                streamReader.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                Log._logger.Error("NG:RequestHttp失败" + " 执行过程中发生异常:" + ex.Message);
                responseContent = "{'status':'error','message':'" + url + "接口出错','stackInfo':'" + ex.Message + "','data':''}";
            }
            return responseContent;
        }
        private static string RequestHttpGet(string url, string token, string contentType = "application/json;charset=utf-8", string Method = "POST", int Timeout = 15000)
        {
            string responseContent = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                Method = "GET";
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Method = Method;
                httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
                httpWebRequest.Timeout = Timeout;
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                responseContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
                Log._logger.Error("NG:RequestHttpGet失败" + " 执行根据TokenKey获取Token过程中发生异常:" + ex.Message);
                responseContent = "{'status':'error','message':'" + url + "接口出错','stackInfo':'" + ex.Message + "','data':''}";
            }
            return responseContent;
        }
        /// <summary>
        /// 将实体写入XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="XmlPath"></param>
        /// <param name="obj"></param>
        private static bool XmlSerialize<T>(string XmlPath, T obj)
        {
            try
            {
                StreamWriter writer = new StreamWriter(XmlPath);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(writer, obj);
                writer.Close();
            }
            catch (Exception ex)
            {
                if (File.Exists(XmlPath))
                {
                    File.Delete(XmlPath);
                }
                Log._logger.Error("NG:创建XML失败" + " 执行过程中发生异常:" + ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool IsDirPathExist(string filePath)
        {
            bool flag = false;
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                flag = true;
            }
            catch (Exception ex)
            {
                Log._logger.Error("NG:文件夹创建失败" + " 执行过程中发生异常:" + ex.Message);
            }
            return flag;
        }

        /// <summary>
        /// 获取时间标识
        /// </summary>
        /// <returns></returns>
        private static string GetTimeStamp()
        {
            DateTime NowTime = DateTime.Now;
            string year = NowTime.Year.ToString().PadLeft(4, '0').Substring(0);
            string month = NowTime.Month.ToString().PadLeft(2, '0');
            string day = NowTime.Day.ToString().PadLeft(2, '0');
            string time = NowTime.Hour.ToString().PadLeft(2, '0');
            string minute = NowTime.Minute.ToString().PadLeft(2, '0');
            string Second = NowTime.Second.ToString().PadLeft(2, '0');
            string MSecond = NowTime.Millisecond.ToString().PadLeft(3, '0');
            return year + month + day + time + minute + Second + MSecond;
        }

        /// <summary>
        /// 获取日期标识
        /// </summary>
        /// <returns></returns>
        private static string GetDatePathStamp()
        {
            DateTime NowTime = DateTime.Now;
            string year = NowTime.Year.ToString().PadLeft(4, '0').Substring(0);
            string month = NowTime.Month.ToString().PadLeft(2, '0');
            string day = NowTime.Day.ToString().PadLeft(2, '0');
            string time = NowTime.Hour.ToString().PadLeft(2, '0');
            return year + "\\" + month + "\\" + day + "\\" + time;
        }
    }
}