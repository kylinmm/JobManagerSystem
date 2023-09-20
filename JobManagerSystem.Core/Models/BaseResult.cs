using System;

namespace JobManagerSystem.Core.Models
{
    public class BaseResult
    {
        public string status { get; set; }
        public string message { get; set; }
        public object stackInfo { get; set; }
    }

    public class ApiResult : BaseResult
    {
        public object data { get; set; }
    }
}