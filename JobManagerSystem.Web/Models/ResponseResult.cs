﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobManagerSystem.Models
{
    /// <summary>
    /// 前端异步调用时返回的json对象模型
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 结果状态,默认值false
        /// </summary>
        public bool success { set; get; }

        /// <summary>
        /// 消息 默认值空字符串
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 详细消息 默认值空字符串
        /// </summary>
        public string message_detail { get; set; }

        /// <summary>
        /// 代码 默认值0
        /// </summary>
        public int code { set; get; }

        /// <summary>
        /// 返回结果对象,默认值Null
        /// </summary>
        public object data { set; get; }


        public ResponseResult()
        {

        }
        public ResponseResult(bool _success)
        {
            success = _success;
        }
        public ResponseResult(bool _success, string _message)
        {
            success = _success;
            message = _message;
        }
        public ResponseResult(bool _success, string _message, object _data)
        {
            success = _success;
            message = _message;
            data = _data;
        }

        public ResponseResult(bool _success, string _message, int _code, object _data)
        {
            success = _success;
            message = _message;
            code = _code;
            data = _data;
        }
    }
}