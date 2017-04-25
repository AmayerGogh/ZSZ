using System;
using System.Web.Mvc;

namespace ZSZ.Web.Common.Filter
{
    public class ZSZJsonNetActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is JsonResult && !(filterContext.Result is ZSZJsonNetResult))
            {
                JsonResult oldResult = (JsonResult)filterContext.Result;
                ZSZJsonNetResult newResult = new ZSZJsonNetResult();

                //偷天换日
                newResult.JsonRequestBehavior = oldResult.JsonRequestBehavior;
                newResult.MaxJsonLength = oldResult.MaxJsonLength;
                newResult.RecursionLimit = oldResult.RecursionLimit;
                newResult.ContentEncoding = oldResult.ContentEncoding;
                newResult.ContentType = oldResult.ContentType;
                newResult.Data = oldResult.Data;

                filterContext.Result = newResult;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //
        }
    }
}
