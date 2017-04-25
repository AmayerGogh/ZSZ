using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Common;
using ZSZ.IService;

namespace ZSZ.Admin.Web.Controllers
{
    public class MainController : Controller
    {
        public ICityService CityService { get; set; } //属性依赖注入

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}