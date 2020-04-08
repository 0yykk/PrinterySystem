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
        private readonly IPaperProvider _paperProvider;
        private readonly IInkProvider _inkProvider;

        public ManagerController(IOrderProvider orderProvider,IProductProvider productProvider,IPaperProvider paperProvider,
            IInkProvider inkProvider
            )
        {
            _orderProvider = orderProvider;
            _productProvider = productProvider;
            _paperProvider = paperProvider;
            _inkProvider = inkProvider;
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
        public async Task<ActionResult> PaperStockManagement()
        {
            var PaperList = new List<PaperCViewModel>();
            PaperList = await _paperProvider.GetAllPaper();
            int pageindex = 1;
            var recordCount = PaperList.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 15;
            ViewBag.PaperList = PaperList.OrderByDescending(art => art.PaperId)
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
        #region 添加库存api（新品种纸张、油墨、产品）
        public JsonResult AddPaper(PaperCViewModel Paper)
        {
            PaperCViewModel i = new PaperCViewModel();
            string Id = alLIDInit.PaperIDInit();
            Paper.PaperId = Id;
            i = Paper;
            _paperProvider.UpdatePaper(i);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        public JsonResult AddInk(InkCViewModel ink)
        {
            InkCViewModel i = new InkCViewModel();
            string Id = alLIDInit.InkIDInit();
            ink.InkId = Id;
            i = ink;
            _inkProvider.UpdateInk(ink);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        public JsonResult AddProduct(ProductsViewModel product)
        {
            ProductsViewModel pro = new ProductsViewModel();
            string Id = alLIDInit.ProductIDInit();
            product.ProductID = Id;
            pro = product;
            _productProvider.UpdateProduct(pro);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 库存采购api
        public JsonResult PurchasingPaper(PaperCViewModel paper)
        {
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PurchasingInk(InkCViewModel ink)
        {
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductGoods(ProductsViewModel product)
        {
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public async Task<ActionResult> PrintingInkStockManagement()
        {
            var InkList = new List<InkCViewModel>();
            InkList = await _inkProvider.GetAllInk();
            int pageindex = 1;
            var recordCount = InkList.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 15;
            ViewBag.InkList = InkList.OrderByDescending(art => art.InkId)
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