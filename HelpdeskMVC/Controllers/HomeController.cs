using HelpdeskMVC.Component;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpdeskMVC.Controllers
{
    public class HomeController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        UserComplaintModel userComplaint = new UserComplaintModel();
        readonly ApplContext Dbcontext;
        readonly UserComplaintComponent userComplaintComponent;
        public HomeController(ApplContext context, UserComplaintComponent usrComplaintComponent)
        {
            this.Dbcontext = context;
            this.userComplaintComponent = usrComplaintComponent;
        }

        //public ActionResult CustomError()
        //{
        //    ViewBag.Message = TempData["ApplicationError"];
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Getting the application Names
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult UserComplaint()
        {
            userComplaint.ApplicationName = userComplaintComponent.GetApplicationName();
            return View(userComplaint);
        }
       
        /// <summary>
        /// Save The user Cmplaint
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult UserComplaint(UserComplaintModel userComplaint)
        {
            if (ModelState.IsValid)
            {
                userComplaintComponent.SaveUserComplaint(userComplaint);
            }
            return View();
        }

        /// <summary>
        /// Getting the modules list
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>

        [HttpGet]
        public JsonResult GetModuleList(int ApplicationId)
        {
            log.Info(">>> GetModules Entered");
            userComplaint.ModuleNames = userComplaintComponent.GetModuleName(ApplicationId);
            return Json(userComplaint.ModuleNames, JsonRequestBehavior.AllowGet);
        }
    }
}