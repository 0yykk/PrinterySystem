using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrinterySystem.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult OrderManagement()
        {
            return View();
        }
        public ActionResult StockManagement()
        {
            return View();
        }
        public ActionResult PaperStockManagement()
        {
            return View();
        }

        public ActionResult PrintingInkStockManagement()
        {
            return View();
        }
        public ActionResult ProductStockManagement()
        {
            return View();
        }
        public ActionResult WorkflowManagement()
        {
            return View();
        }
        
        public ActionResult CustomerManagement()
        {
            return View();
        }
        public ActionResult EmployeeManagement()
        {
            return View();
        }
        public ActionResult OrderProcessing()
        {
            return View();
        }
        public ActionResult ProduceManagement()
        {
            return View();
        }
        public ActionResult PurchasingManagement()
        {
            return View();
        }
    }
}