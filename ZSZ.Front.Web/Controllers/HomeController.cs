using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Common;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Front.Web.Controllers
{
    public class HomeController : Controller
    {
        public ICityService CityService { get; set; } //属性依赖注入

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CityDTO fc)
        {
            //调用接口服务
            long id = CityService.AddNew(fc.Name);

            CityDTO city = new CityDTO();
            city.Name = fc.Name;
            city.CreateDateTime = DateTime.Now;
            city.Id = id;

            LogHelper.Info(string.Format("新增城市{0}", fc.Name));

            return Json(city);
        }
    }
}