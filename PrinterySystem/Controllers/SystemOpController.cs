﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrinterySystem.Controllers
{
    public class SystemOpController : Controller
    {
        // GET: SystemOp
        public ActionResult Dashborad4employee()
        {
            return View();
        }
        public ActionResult Dashborad4Manager()
        {
            return View();
        }
    }
}