using JobManagerSystem.Core.Common;
using JobManagerSystem.Core.Services;
using JobManagerSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobManagerSystem.Controllers
{
    public class BackgroundJobLogController : BaseController
    {
        //
        // GET: /BackgroundJobLog/

        [HttpGet]
        public ActionResult List()
        {
            BackgroundJobService _BackgroundJobService = new BackgroundJobService();
            var data = _BackgroundJobService.GetBackgroundJobLogInfoPagerList(GetPageParameter());
            var result = new ResponseResult() { success = true, message = "数据获取成功", data = data };
            return Json(result);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InfoData(System.Guid BackgroundJobLogId)
        {
            var result = new ResponseResult();
            BackgroundJobService _BackgroundJobService = new BackgroundJobService();
            result.data = _BackgroundJobService.GetBackgroundJobLogInfo(BackgroundJobLogId);
            result.success = true;
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(string idList)
        {
            var result = new ResponseResult();
            BackgroundJobService _BackgroundJobService = new BackgroundJobService();
            result.success = _BackgroundJobService.DeleteBackgroundJobLog(Utils.StringToGuidList(idList));
            result.message = result.success == true ? "操作成功" : "操作失败";
            return Json(result);
        }
    }
}
