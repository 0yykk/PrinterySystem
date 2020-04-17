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
        private readonly IPowerCheckProvider _powerCheckProvider;

        public ManagerController(IOrderProvider orderProvider,IProductProvider productProvider,IPaperProvider paperProvider,
            IInkProvider inkProvider, IPowerCheckProvider powerCheckProvider
            )
        {
            _orderProvider = orderProvider;
            _productProvider = productProvider;
            _paperProvider = paperProvider;
            _inkProvider = inkProvider;
            _powerCheckProvider = powerCheckProvider;
        }
        public async Task<ActionResult> OrderManagement(int? page)
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var productlist = new List<ProductGoodsViewModel>();
            var Orderlist = new List<OrderViewModel>();
            if (sa.EmpId != null)
            {
                productlist = await _productProvider.GetAllProduct();
                Orderlist = await _orderProvider.GetAllOrder();
            }
            else
            {
                if (userpower.Contains("101"))
                {
                    productlist = await _productProvider.GetAllProduct();
                    Orderlist = await _orderProvider.GetAllOrder();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> OrderManagement(OrderViewModel order)
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var productlist = new List<ProductGoodsViewModel>();
            var Orderlist = new List<OrderViewModel>();
            string cusname = Request.Form["OrCustomer"];
            DateTime? CreateDate = null;
            if (Request.Form["CreateDate"] != null)
            {
                CreateDate = Convert.ToDateTime(Request.Form["CreateDate"]);
            } 
            if (sa.EmpId != null)
            {
                if (Orderlist != null&&CreateDate==null)
                {
                    productlist = await _productProvider.GetAllProduct();
                    Orderlist = _orderProvider.GetOrderByCustomer(cusname);
                }
                if (CreateDate != null)
                {
                    productlist = await _productProvider.GetAllProduct();
                    Orderlist = await _orderProvider.SearchOrderByDate(CreateDate);
                }
            }
            else
            {
                if (userpower.Contains("101"))
                {
                    if (Orderlist != null&& CreateDate == null)
                    {
                        productlist = await _productProvider.GetAllProduct();
                        Orderlist = _orderProvider.GetOrderByCustomer(cusname);
                    }
                    if (CreateDate != null)
                    {
                        productlist = await _productProvider.GetAllProduct();
                        Orderlist = await _orderProvider.SearchOrderByDate(CreateDate);
                    }
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                return View();
            }
            else
            {
                if (userpower.Contains("201"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> PaperStockManagement(string papername)
        {
            var PaperList = new List<PaperCViewModel>();
            string name = Request.Form["PostPaperName"];
            if (name == null)
            {
                PaperList = await _paperProvider.GetAllPaper();
            }
            else
            {
                PaperList = _paperProvider.GetPaperByPapername(name);
            }
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
        #region 添加修改库存api（新品种纸张、油墨、产品）
        public JsonResult AddPaper(PaperCViewModel Paper)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            PaperCViewModel i = new PaperCViewModel();
            string Id = alLIDInit.PaperIDInit();
            Paper.PaperId = Id;
            i = Paper;
            if (sa.EmpId != null)
            {
                _paperProvider.UpdatePaper(i);
                a = "添加成功";
            }
            else
            {
                if (userpower.Contains("202"))
                {
                    _paperProvider.UpdatePaper(i);
                    a = "添加成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        public JsonResult editPaper(string paperguid,string papername)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _paperProvider.EditPaper(paperguid.Trim(), papername.Trim());
                a = "修改成功";
            }
            else
            {
                if (userpower.Contains("203"))
                {
                    _paperProvider.EditPaper(paperguid.Trim(), papername.Trim());
                    a = "修改成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deletePaper(string paperid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _paperProvider.DeletePaper(paperid.Trim());
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("204"))
                {
                    _paperProvider.DeletePaper(paperid.Trim());
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddInk(InkCViewModel ink)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            InkCViewModel i = new InkCViewModel();
            string Id = alLIDInit.InkIDInit();
            ink.InkId = Id;
            i = ink;
            if (sa.EmpId != null)
            {
                _inkProvider.UpdateInk(ink);
                a = "添加成功";
            }
            else
            {
                if (userpower.Contains("202"))
                {
                    _inkProvider.UpdateInk(ink);
                    a = "添加成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        public JsonResult editInk(string inkguid,string inkname)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _inkProvider.EditInk(inkguid.Trim(), inkname.Trim());
                a = "修改成功";
            }
            else
            {
                if (userpower.Contains("203"))
                {
                    _inkProvider.EditInk(inkguid.Trim(), inkname.Trim());
                    a = "修改成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteInk(string inkid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _inkProvider.DeleteInk(inkid.Trim());
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("204"))
                {
                    _inkProvider.DeleteInk(inkid.Trim());
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddProduct(ProductGoodsViewModel product)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            ProductGoodsViewModel pro = new ProductGoodsViewModel();
            string Id = alLIDInit.ProductIDInit();
            product.ProductID = Id;
            pro = product;
            if (sa.EmpId != null)
            {
                _productProvider.UpdateProduct(pro);
                a = "添加成功";
            }
            else
            {
                if (userpower.Contains("202"))
                {
                    _productProvider.UpdateProduct(pro);
                    a = "添加成功";
                }
                else
                {
                    a = "权限不足"; 
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);

        }
        public JsonResult editProduct(ProductGoodsViewModel product)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _productProvider.EditProduct(product);
                a = "修改成功";
            }
            else
            {
                if (userpower.Contains("203"))
                {
                    _productProvider.EditProduct(product);
                    a = "修改成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteProduct(string productid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId!=null)
            {
                _productProvider.DeleteProduct(productid.Trim());
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("204"))
                {
                    _productProvider.DeleteProduct(productid.Trim());
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 库存采购生产api
        public JsonResult PurchasingPaper(PurchasingPaperViewModel paper)
        {
            //PurchasingPaperViewModel par = new PurchasingPaperViewModel();
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            paper.PurchasingID = Guid.NewGuid().ToString();
            paper.CreateDate = DateTime.Now;
            paper.ProcessDate = DateTime.Now;
            if (sa.EmpId != null)
            {
                _paperProvider.CreatePurchaseOrder4Paper(paper);
                a = "提交成功";
            }
            else
            {
                if (userpower.Contains("202"))
                {
                    _paperProvider.CreatePurchaseOrder4Paper(paper);
                    a = "提交成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PurchasingInk(PurchasingInkViewModel ink)
        {
            //PurchasingInkViewModel pur = new PurchasingInkViewModel();
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            ink.PurchasingID = Guid.NewGuid().ToString();
            ink.CreateDate = DateTime.Now;
            ink.ProcessDate = DateTime.Now;
            if (sa.EmpId != null)
            {
                _inkProvider.CreatePurchaseOrder4Ink(ink);
               a = "提交成功";
            }
            else
            {
                if (userpower.Contains("202"))
                {
                    _inkProvider.CreatePurchaseOrder4Ink(ink);
                    a = "提交成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PurchasingProduct(ProductGoodViewModel product)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            product.PurchasingID = Guid.NewGuid().ToString();
            product.CreateDate = DateTime.Now;
            product.ProcessDate = DateTime.Now;
            product.Price = 0;
            product.eachPrice = 10;
            if (sa.EmpId != null)
            {
                _productProvider.CreatePurchaseOrder4Produt(product);
                a = "提交成功";
            }
            else
            {
                if (userpower.Contains("202"))
                {
                    _productProvider.CreatePurchaseOrder4Produt(product);
                    a = "提交成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
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
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if(sa.EmpId != null){
                _paperProvider.PushStockInPaper(Price, PurchaseId, paperid, PaperCount, ProcessPersonId);
                a = "入库处理成功提交";
            }
            else
            {
                if (userpower.Contains("205"))
                {
                    _paperProvider.PushStockInPaper(Price, PurchaseId, paperid, PaperCount, ProcessPersonId);
                    a = "入库处理成功提交";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PushStockInInk(string PurchaseId, decimal Price, string InkId, int InkCout, string ProcessPersonId)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _inkProvider.PushInStockInk(PurchaseId, Price, InkId, InkCout, ProcessPersonId);
                a = "入库请求提交成功";
            }
            else
            {
                if (userpower.Contains("205"))
                {
                    _inkProvider.PushInStockInk(PurchaseId, Price, InkId, InkCout, ProcessPersonId);
                    a = "入库请求提交成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PushProcessIn(string PurchasingID,string Processpersonid)
        {
            string b = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            string i = PurchasingID.Trim();
            string a = Processpersonid.Trim();
            if (sa.EmpId != null)
            {
                _productProvider.ProcessProductGood(i, a);
                b = "请求处理成功提交";
            }
            else
            {
                if (userpower.Contains("205"))
                {
                    _productProvider.ProcessProductGood(i, a);
                    b = "请求处理成功提交";
                }
                else
                {
                    b = "权限不足";
                }
            }
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePaperPurchase(string purchaseguid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null) 
            {
                _paperProvider.DeletePaperPurchase(purchaseguid);
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("206"))
                {
                    _paperProvider.DeletePaperPurchase(purchaseguid);
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteInkPurchase(string purchaseguid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _inkProvider.DeleteInkPurchase(purchaseguid);
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("206"))
                {
                    _inkProvider.DeleteInkPurchase(purchaseguid);
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
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
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId!= null)
            {
                _orderProvider.CreateOrder(order);
                a = "提交成功";
            }
            else
            {
                if (userpower.Contains("102"))
                {
                    _orderProvider.CreateOrder(order);
                    a = "提交成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
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
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _orderProvider.EditOrder(order);
                a = "成功修改";
            }
            else
            {
                if (userpower.Contains("104"))
                {
                    _orderProvider.EditOrder(order);
                    a = "成功修改";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProcessOrder(string orderid,string processpersonid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            string id = orderid.Trim();
            string pid = processpersonid.Trim();
            if (sa.EmpId!=null)
            {
                _orderProvider.ProcessOrder(id, pid);
                a = "处理成功";
            }
            else
            {
                if (userpower.Contains("103"))
                {
                    _orderProvider.ProcessOrder(id, pid);
                    a = "处理成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteOrder(string orderguid)
        {
            string a = "";
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                _orderProvider.DeleteOrder(orderguid);
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("105"))
                {
                    _orderProvider.DeleteOrder(orderguid);
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> PrintingInkStockManagement(string na)
        {
            var InkList = new List<InkCViewModel>();
            string name = Request.Form["PostInkName"];
            if (name == null)
            {
                InkList = await _inkProvider.GetAllInk();
            }
            else
            {
                InkList = _inkProvider.GetInkByName(name);
            }
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
        [HttpPost]
        public async Task<ActionResult> ProductStockManagement(string na)
        {
            string name = Request.Form["PostProdcutName"];
            var productlist = new List<ProductGoodsViewModel>();
            if (name == null)
            {
                productlist = await _productProvider.GetAllProduct();
            }
            else
            {
                productlist = _productProvider.GetProductByProductName(name);
            }
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
        
        public async Task<ActionResult> OrderProcessing(int? page)
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var productlist = new List<ProductGoodsViewModel>();
            var Orderlist = new List<OrderViewModel>();
            if (sa.EmpId != null)
            {
                productlist = await _productProvider.GetAllProduct();
                Orderlist = await _orderProvider.GetAllOrder();
            }
            else
            {
                if (userpower.Contains("101"))
                {
                    productlist = await _productProvider.GetAllProduct();
                    Orderlist = await _orderProvider.GetAllOrder();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> OrderProcessing(OrderViewModel order)
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var productlist = new List<ProductGoodsViewModel>();
            var Orderlist = new List<OrderViewModel>();
            string cusname = Request.Form["OrCustomer"];
            DateTime? CreateDate = null;
            if (Request.Form["CreateDate"] != null)
            {
                CreateDate = Convert.ToDateTime(Request.Form["CreateDate"]);
            }
            if (sa.EmpId != null)
            {
                if (Orderlist != null && CreateDate == null)
                {
                    productlist = await _productProvider.GetAllProduct();
                    Orderlist = _orderProvider.GetOrderByCustomer(cusname);
                }
                if (CreateDate != null)
                {
                    productlist = await _productProvider.GetAllProduct();
                    Orderlist = await _orderProvider.SearchOrderByDate(CreateDate);
                }
            }
            else
            {
                if (userpower.Contains("101"))
                {
                    if (Orderlist != null && CreateDate == null)
                    {
                        productlist = await _productProvider.GetAllProduct();
                        Orderlist = _orderProvider.GetOrderByCustomer(cusname);
                    }
                    if (CreateDate != null)
                    {
                        productlist = await _productProvider.GetAllProduct();
                        Orderlist = await _orderProvider.SearchOrderByDate(CreateDate);
                    }
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        public async Task<ActionResult> ProduceManagement()
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var purchaselist = new List<ProductGoodViewModel>();
            if (sa.EmpId != null)
            {
                purchaselist = await _productProvider.GetAllProductPurchase(); 
            }
            else
            {
                if (userpower.Contains("201"))
                {
                    purchaselist = await _productProvider.GetAllProductPurchase();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> ProduceManagement(string na)
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            string name = Request.Form["PostProductName"];
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var purchaselist = new List<ProductGoodViewModel>();
            if (sa.EmpId != null)
            {
                if (name == null)
                {
                    purchaselist = await _productProvider.GetAllProductPurchase();
                }
                else
                {
                    purchaselist = _productProvider.GetPurchaseByName(name);
                }
            }
            else
            {
                if (userpower.Contains("201"))
                {
                    if (name == null)
                    {
                        purchaselist = await _productProvider.GetAllProductPurchase();
                    }
                    else
                    {
                        purchaselist = _productProvider.GetPurchaseByName(name);
                    }
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var inklist = new List<PurchasingInkViewModel>();
            var paperlist = new List<PurchasingPaperViewModel>();
            if (sa.EmpId != null)
            {
                inklist = await _inkProvider.GetAllInkPurchasing();
                paperlist = await _paperProvider.GetAllPaperPurchasing();
            }
            else
            {
                if (userpower.Contains("201"))
                {
                    inklist = await _inkProvider.GetAllInkPurchasing();
                    paperlist = await _paperProvider.GetAllPaperPurchasing();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> PurchasingManagement(string na)
        {
            string papername = Request.Form["PostPaperName"];
            string inkname = Request.Form["PostInkName"];
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var inklist = new List<PurchasingInkViewModel>();
            var paperlist = new List<PurchasingPaperViewModel>();
            if (sa.EmpId != null)
            {
                if (papername == null && inkname == null)
                {
                    inklist = await _inkProvider.GetAllInkPurchasing();
                    paperlist = await _paperProvider.GetAllPaperPurchasing();
                }
                else if (papername != null && inkname == null)
                {
                    inklist = await _inkProvider.GetAllInkPurchasing();
                    paperlist = _paperProvider.GetPurchasingByName(papername);
                }
                else if (papername == null && inkname != null)
                {
                    paperlist = await _paperProvider.GetAllPaperPurchasing();
                    inklist = _inkProvider.GetPurchasingByName(inkname);
                }
                else
                {
                    paperlist = _paperProvider.GetPurchasingByName(papername);
                    inklist = _inkProvider.GetPurchasingByName(inkname);
                }
            }
            else
            {
                if (userpower.Contains("201"))
                {
                    if (papername == null && inkname == null)
                    {
                        inklist = await _inkProvider.GetAllInkPurchasing();
                        paperlist = await _paperProvider.GetAllPaperPurchasing();
                    }
                    else if (papername != null && inkname == null)
                    {
                        inklist = await _inkProvider.GetAllInkPurchasing();
                        paperlist = _paperProvider.GetPurchasingByName(papername);
                    }
                    else if (papername == null && inkname != null)
                    {
                        paperlist = await _paperProvider.GetAllPaperPurchasing();
                        inklist = _inkProvider.GetPurchasingByName(inkname);
                    }
                    else
                    {
                        paperlist = _paperProvider.GetPurchasingByName(papername);
                        inklist = _inkProvider.GetPurchasingByName(inkname);
                    }
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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