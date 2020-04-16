using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Printery.Domain.ViewModel;
using System.Web.Mvc;
using Printery.Provider.Provider;
using Printery.Core.Utility;

namespace PrinterySystem.Controllers
{
    public class SystemOpController : Controller
    {
        private readonly IOrderProvider _orderProvider;
        private readonly IProductProvider _productProvider;
        private readonly IPowerCheckProvider _powerCheckProvider;
        public SystemOpController(IOrderProvider orderProvider, IProductProvider productProvider, IPowerCheckProvider powerCheckProvider)
        {
            _orderProvider = orderProvider;
            _productProvider = productProvider;
            _powerCheckProvider = powerCheckProvider;
        }
        // GET: SystemOp
        public ActionResult Dashborad4employee()
        {
            return View();
        }
        public async Task<ActionResult> ProductSet()
        {
            var list = new List<ProExViewModel>();
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                list = await _productProvider.GetAllProExi(empid);
            }
            else
            {
                if (userpower.Contains("2"))
                {
                    list = await _productProvider.GetAllProExi(empid);
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
            int pageindex = 1;
            var recordCount = list.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
            ViewBag.ProExi = list.OrderByDescending(art => art.ProExId)
                .Skip((pageindex - 1) * PAGE_SZ)
                .Take(PAGE_SZ).ToList();

            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount,
            };
            return View();
        }
        public async Task<ActionResult> Dashborad4Manager()
        {
            string empid = Session["LoginId"].ToString();
            var Dash = new DashboradViewModel();
            Dash = await _orderProvider.GetDashboradDisplay(empid);
            var Orderlist = await _orderProvider.GetOrderByDate(empid);
            var OrdercountList = new List<DashboradBottomViewModel>();
            OrdercountList = await _orderProvider.GetDashboradBottomDisplay(empid);
            ViewBag.DashNav = Dash;
            ViewBag.OrderList = Orderlist;
            ViewBag.OrderCount = OrdercountList;
            return View();
        }
        #region
        public JsonResult GetProExiById(string proexiId)
        {
            var pro = new ProExViewModel();
            pro = _productProvider.GetProExiByProId(proexiId.Trim());
            string a = "su";
            return Json(pro, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}