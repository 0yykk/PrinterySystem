using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Printery.Domain.ViewModel;
using System.Web.Mvc;
using Printery.Provider.Provider;

namespace PrinterySystem.Controllers
{
    public class SystemOpController : Controller
    {
        private readonly IOrderProvider _orderProvider;
        public SystemOpController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }
        // GET: SystemOp
        public ActionResult Dashborad4employee()
        {
            return View();
        }
        public async Task<ActionResult> Dashborad4Manager()
        {
            string empid = Session["LoginId"].ToString();
            var Dash = new DashboradViewModel();
            Dash = await _orderProvider.GetDashboradDisplay(empid);
            var Orderlist = await _orderProvider.GetOrderByDate(empid);
            ViewBag.DashNav = Dash;
            ViewBag.OrderList = Orderlist;
            return View();
        }
    }
}