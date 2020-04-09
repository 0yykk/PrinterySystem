using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrinterySystem.Controllers
{
    public class Person4ManagerController : Controller
    {
        // GET: Person4Manager
        public ActionResult CustomerInfo()
        {
            return View();
        }
        public ActionResult EmployeeInfo()
        {
            return View();
        }
        public ActionResult MyInfo()
        {
            return View();
        }
        public ActionResult MyInfoEdit()
        {
            return View();
        }
    }
}