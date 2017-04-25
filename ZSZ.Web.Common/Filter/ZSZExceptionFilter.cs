
using System;
using System.Web.Hosting;
/**
*@项目名称: 停车场岗亭管理软件系统
*@文件名称: ZSZExceptionFilter
*@Date: 2017/3/27 17:58:00
*@Copyright: 2015 悦畅科技有限公司. All rights reserved.
*注意：本内容仅限于悦畅科技有限公司内部传阅，禁止外泄以及用于其他的商业目的
*/
using System.Web.Mvc;
using ZSZ.Common;

namespace ZSZ.Web.Common.Filter
{
    public class ZSZExceptionFilter : IExceptionFilter
    {
        public ZSZExceptionFilter()
        {
            string l4net = HostingEnvironment.MapPath("~/bin/log4net.config");
            LogHelper.SetPath(l4net);
        }
        public void OnException(ExceptionContext filterContext)
        {
            LogHelper.Error("异常：", filterContext.Exception);
        }
    }
}
