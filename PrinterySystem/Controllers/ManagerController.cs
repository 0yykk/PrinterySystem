using Printery.Core.Utility;
using Printery.Domain.ViewModel;
using Printery.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public async Task<ActionResult> OrderManagement(int? page)
        {
            var productlist = new List<ProductGoodsViewModel>();
            productlist = await _productProvider.GetAllProduct();
            var Orderlist = new List<OrderViewModel>();
            Orderlist = await _orderProvider.GetAllOrder();
            ViewBag.ProductList = productlist;
            int pageindex = 1;
            var recordCount = Orderlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
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
            const int PAGE_SZ = 5;
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
        public JsonResult AddProduct(ProductGoodsViewModel product)
        {
            ProductGoodsViewModel pro = new ProductGoodsViewModel();
            string Id = alLIDInit.ProductIDInit();
            product.ProductID = Id;
            pro = product;
            _productProvider.UpdateProduct(pro);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 库存采购生产api
        public JsonResult PurchasingPaper(PurchasingPaperViewModel paper)
        {
            //PurchasingPaperViewModel par = new PurchasingPaperViewModel();
            paper.PurchasingID = Guid.NewGuid().ToString();
            paper.CreateDate = DateTime.Now;
            paper.ProcessDate = DateTime.Now;
            _paperProvider.CreatePurchaseOrder4Paper(paper);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PurchasingInk(PurchasingInkViewModel ink)
        {
            //PurchasingInkViewModel pur = new PurchasingInkViewModel();
            ink.PurchasingID = Guid.NewGuid().ToString();
            ink.CreateDate = DateTime.Now;
            ink.ProcessDate = DateTime.Now;
            _inkProvider.CreatePurchaseOrder4Ink(ink);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PurchasingProduct(ProductGoodViewModel product)
        {
            product.PurchasingID = Guid.NewGuid().ToString();
            product.CreateDate = DateTime.Now;
            product.ProcessDate = DateTime.Now;
            product.Price = 0;
            product.eachPrice = 10;
            _productProvider.CreatePurchaseOrder4Produt(product);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 物料采购生产工单处理相关api
        public JsonResult GetInkPurchase(string inkpurchaseId)
        {
            string id = inkpurchaseId.Trim();
            var list = new List<PurchasingInkViewModel>();
            list = _inkProvider.GetPurchasingById(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPaperPurchase(string paperpurchaseid)
        {
            string id = paperpurchaseid.Trim();
            var list = new List<PurchasingPaperViewModel>();
            list = _paperProvider.GetPurchasingById(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PushStockIn(decimal Price,string PurchaseId, string paperid, int PaperCount,string ProcessPersonId)
        {
            _paperProvider.PushStockInPaper(Price, PurchaseId, paperid, PaperCount, ProcessPersonId);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PushStockInInk(string PurchaseId, decimal Price, string InkId, int InkCout, string ProcessPersonId)
        {
            _inkProvider.PushInStockInk(PurchaseId, Price, InkId, InkCout, ProcessPersonId);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PushProcessIn(string PurchasingID,string Processpersonid)
        {
            string i = PurchasingID.Trim();
            string a = Processpersonid.Trim();
            _productProvider.ProcessProductGood(i, a);
            string b = "Sucessful";
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePaperPurchase(string purchaseguid)
        {
            _paperProvider.DeletePaperPurchase(purchaseguid);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteInkPurchase(string purchaseguid)
        {
            _inkProvider.DeleteInkPurchase(purchaseguid);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取得产品生产单
        /// </summary>
        /// <param name="purchaseid"></param>
        /// <returns>产品生产工单单</returns>
        public JsonResult GetProductPurchase(string purchaseid)
        {
            var pro = new List<ProductGoodViewModel>();
            pro = _productProvider.GetPurchaseById(purchaseid.Trim());
            return Json(pro, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取商品单价
        public JsonResult GetPriceList(string pri)
        {
            string pro = pri;
            List<ProductGoodsViewModel> proList = _productProvider.GetProductByProductName(pro);
            return Json(proList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 订单处理Api
        public JsonResult AddOrder(OrderViewModel order)
        {
            string Id = Guid.NewGuid().ToString();
            order.OrderId= Id;
            order.OrderCreate = DateTime.Now;
            order.OrderProcess = DateTime.Now;
            order.Status = "待处理";
            _orderProvider.CreateOrder(order);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetOrder(string orderguid)
        {
            string id = orderguid.Trim();
            var list = new List<OrderViewModel>();
            list=_orderProvider.GetOrderByOrderId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditOrder(OrderViewModel order)
        {
            _orderProvider.EditOrder(order);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProcessOrder(string orderid,string processpersonid)
        {
            string id = orderid.Trim();
            string pid = processpersonid.Trim();
            _orderProvider.ProcessOrder(id, pid);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteOrder(string orderguid)
        {
            _orderProvider.DeleteOrder(orderguid);
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
            const int PAGE_SZ = 5;
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

            var productlist = new List<ProductGoodsViewModel>();
            productlist = await _productProvider.GetAllProduct();
            int pageindex = 1;
            var recordCount = productlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
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
        public async Task<ActionResult> OrderProcessing(int? page)
        {
            var productlist = new List<ProductGoodsViewModel>();
            productlist = await _productProvider.GetAllProduct();
            var Orderlist = new List<OrderViewModel>();
            Orderlist = await _orderProvider.GetAllOrder();
            //int pageNumber = page ?? 1;
            ////每页显示多少条
            //int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //Orderlist = Orderlist.OrderBy(x => x.OrderId).ToList();
            ////通过ToPagedList扩展方法进行分页
            //IPagedList<OrderViewModel> pagedList = Orderlist.ToPagedList(pageNumber, pageSize);
            ViewBag.ProductList = productlist;
            int pageindex = 1;
            var recordCount = Orderlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
            ViewBag.OrderList = Orderlist.OrderByDescending(art => art.OrderCreate)
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
        public async Task<ActionResult> ProduceManagement()
        {
            var purchaselist = new List<ProductGoodViewModel>();
            purchaselist = await _productProvider.GetAllProductPurchase();
            int pageindex = 1;
            var recordCount = purchaselist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
            ViewBag.PurchaseList = purchaselist.OrderByDescending(art => art.CreateDate)
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
        public async Task<ActionResult> PurchasingManagement()
        {
            var inklist = new List<PurchasingInkViewModel>();
            var paperlist = new List<PurchasingPaperViewModel>();
            inklist = await _inkProvider.GetAllInkPurchasing();
            paperlist = await _paperProvider.GetAllPaperPurchasing();
            int pageindex = 1;
            int pageindex1 = 1;
            var recordCount = inklist.Count();
            var recordCount1 = paperlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            if (Request.QueryString["page1"] != null)
                pageindex1 = Convert.ToInt32(Request.QueryString["page1"]);
            const int PAGE_SZ = 5;
            ViewBag.InkPurchase = inklist.OrderByDescending(art => art.CreateDate)
                .Skip((pageindex - 1) * PAGE_SZ)
                .Take(PAGE_SZ).ToList();
            ViewBag.PaperPurchase = paperlist.OrderByDescending(art => art.CreateDate)
                .Skip((pageindex1 - 1) * PAGE_SZ)
                .Take(PAGE_SZ).ToList();
            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount,
            };
            ViewBag.Pager1 = new PagerHelper()
            {
                PageIndex = pageindex1,
                PageSize = PAGE_SZ,
                RecordCount = recordCount1,
            };
            return View();
        }
    }
}