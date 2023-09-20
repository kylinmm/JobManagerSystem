using JobManagerSystem.Core.Business.Info;
using JobManagerSystem.Core.Business.Manager;
using System;
using System.Collections.Generic;

namespace JobManagerSystem.Core.Models
{
    [Serializable]
    public class RecordPackage
    {
        public RecordPackage()
        {
        }

        private string _requestTime = DateTime.Now.ToString();
        private string _requestUrl = "RestfulApi";
        private string _requestToken = "";
        private string _subData = "[]";
        private string _resultData = "[]";

        /// <summary>
        /// 必填，请求时间
        /// </summary>
        public string RequestTime
        {
            get { return _requestTime; }
            set { _requestTime = value; }
        }

        /// <summary>
        /// 必填，请求Url
        /// </summary>
        public string RequestUrl
        {
            get { return _requestUrl; }
            set { _requestUrl = value; }
        }

        /// <summary>
        /// 必填，请求Url
        /// </summary>
        public string RequestToken
        {
            get { return _requestToken; }
            set { _requestToken = value; }
        }

        /// <summary>
        /// 必填，提交Data
        /// </summary>
        public string SubData
        {
            get { return _subData; }
            set { _subData = value; }
        }

        /// <summary>
        /// 必填，返回Data
        /// </summary>
        public string ResultData
        {
            set { _resultData = value; }
            get { return _resultData; }
        }
    }
}