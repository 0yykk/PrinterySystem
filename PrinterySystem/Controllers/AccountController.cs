using Printery.Core.Utility;
using Printery.Domain.ViewModel;
using Printery.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrinterySystem.Controllers
{
    public class AccountController:Controller
    {
        private readonly IEmpProvider _empProvider;
        public AccountController(IEmpProvider empProvider)
        {
            _empProvider = empProvider;
        }
        public ActionResult Login()
        {
            HttpCookie httpCookie = Request.Cookies.Get("Login");
            if (httpCookie != null)
            {
                ViewBag.LoginUser = httpCookie.Values["LoginUser"].ToString();
                ViewBag.LoginPwd = httpCookie.Values["LoginPwd"].ToString();
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string user)
        {
            user = Request.Form["login"];
            var model = await _empProvider.GetEmployeeByUsernameAsync(user);
            if (model.Username != null)
            {
                if (int.Parse(model.Password) == int.Parse(Request.Form["password"]))
                {
                    //var model = await _informationProvider.GetUser(user);
                    var ck = Request.Form["checkbox"];
                    if (ck == "on")
                    {
                        HttpCookie cookie = new HttpCookie("Login");
                        cookie["LoginUser"] = model.Username;
                        cookie["LoginPwd"] = model.Password;
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie);
                    }
                    Session["LoginId"] = model.EmpId; 
                    Session["Password"] = model.Password;
                    Session["LoginUserName"] = model.Username;
                    Session["Power"] = model.UserGroup;
                    if (model.UserGroup == "SA")
                    {
                        return RedirectToAction("Dashborad4Manager", "SystemOp");
                    }
                    else
                    {
                        return RedirectToAction("Dashborad4employee", "SysntemOp");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "密码错误";
                }
            }
            else
            {
                ViewBag.Error = "用户编号错误";
            }
            return View();
        }
        public async Task<ActionResult> UserGroupManager()
        {
            var empg = new List<EmpGroupViewModel>();
            var powlist = new List<PowerListViewModel>();
            powlist = await _empProvider.GetAllPowerList();
            empg = await _empProvider.GetAllEmpGroup();
            int pageindex = 1;
            var recordCount = empg.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
            ViewBag.EmpGroupList = empg.OrderByDescending(art => art.GroupId)
            .Skip((pageindex - 1) * PAGE_SZ)
            .Take(PAGE_SZ).ToList();
            ViewBag.PowerList = powlist;
            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount,
            };
            return View();
        }
        # region 用户组处理api
        public JsonResult PowerListUpdate(List<PowerControlListViewModel> powerlist)
        {
            List<PowerControlListViewModel> i = new List<PowerControlListViewModel>();
            i = powerlist;
            //decimal totalPrice = 0;
            _empProvider.UpdatePowerList(i);
            return Json("Sucessful", JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult RecoverPassword()
        {
            return View();
        }
    }
}