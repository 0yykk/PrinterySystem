using Printery.Core.Utility;
using Printery.Domain.ViewModel;
using Printery.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrinterySystem.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        private readonly IOrderProvider _orderProvider;
        private readonly IProductProvider _productProvider;

        public ManagerController(IOrderProvider orderProvider,IProductProvider productProvider)
        {
            _orderProvider = orderProvider;
            _productProvider = productProvider;
        }
        public async Task<ActionResult> OrderManagement()
        {

            var Orderlist = new List<OrderViewModel>();
            Orderlist = await _orderProvider.GetAllOrder();
            int pageindex = 1;
            var recordCount = Orderlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 15;
            ViewBag.OrderList = Orderlist.OrderByDescending(art => art.OrderId)
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
        public async Task<ActionResult> ProductStockManagement()
        {

            var productlist = new List<ProductsViewModel>();
            productlist = await _productProvider.GetAllProduct();
            int pageindex = 1;
            var recordCount = productlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 15;
            ViewBag.ProductList = productlist.OrderByDescending(art => art.ProductID)
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