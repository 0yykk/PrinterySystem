using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrinterySystem.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult CustomerInfo()
        {
            return View();
        }
        public ActionResult EmployeeInfo()
        {
            return View();
        }
    }
}