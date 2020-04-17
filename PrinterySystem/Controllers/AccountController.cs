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
        private readonly IPowerCheckProvider _powerCheckProvider;
        public AccountController(IEmpProvider empProvider, IPowerCheckProvider powerCheckProvider)
        {
            _empProvider = empProvider;
            _powerCheckProvider = powerCheckProvider;
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
                        Session["IsSA"] = "Yes";
                        return RedirectToAction("Dashborad4Manager", "SystemOp");
                    }
                    else
                    {
                        return RedirectToAction("Dashborad4Manager", "SystemOp");
                    }
                }
                else
                {
                    return Content("<script>alert('密码错误！');history.go(-1);</script> ");
                }
            }
            else
            {
                return Content("<script>alert('用户名错误！');history.go(-1);</script> ");
            }
            //return View();
        }
        public async Task<ActionResult> UserGroupManager()
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            var empg = new List<EmpGroupViewModel>();
            var powlist = new List<PowerListViewModel>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa=_powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                
                powlist = await _empProvider.GetAllPowerList();
                empg = await _empProvider.GetAllEmpGroup();
                
            }
            else
            {
                if (userpower.Contains("1"))
                {
                    powlist = await _empProvider.GetAllPowerList();
                    empg = await _empProvider.GetAllEmpGroup();
                }       
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        [HttpPost]
        public async Task<ActionResult> UserGroupManager(string na)
        {
            string name = Request.Form["PostUserGroup"];
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            var empg = new List<EmpGroupViewModel>();
            var powlist = new List<PowerListViewModel>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            if (sa.EmpId != null)
            {
                if (name == null)
                {
                    powlist = await _empProvider.GetAllPowerList();
                    empg = await _empProvider.GetAllEmpGroup();
                }
                else
                {
                    powlist = await _empProvider.GetAllPowerList();
                    empg = _empProvider.GetUserGroupByName(name);
                }
            }
            else
            {
                if (userpower.Contains("1"))
                {
                    if (name == null)
                    {
                        powlist = await _empProvider.GetAllPowerList();
                        empg = await _empProvider.GetAllEmpGroup();
                    }
                    else
                    {
                        powlist = await _empProvider.GetAllPowerList();
                        empg = _empProvider.GetUserGroupByName(name);
                    }
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
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
        public JsonResult DisplayPowerList(string GroupId)
        {
            string i = GroupId.Trim();
            List<PowerControlListViewModel> list = new List<PowerControlListViewModel>();
            list = _empProvider.GetPowerContrlListById(i);
            //var a= new List<String>;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddGroup(string GroupName)
        {
            string GroupN = GroupName.Trim();
            _empProvider.AddUserGroupByGroupName(GroupN);
            return Json("123", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteGroup(string GroupId)
        {
            string Groupid = GroupId.Trim();
            _empProvider.DeleteUserGroup(Groupid);
            return Json("123", JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RecoverPassword(string user)
        {
            string empusername = Session["LoginUserName"].ToString();
            string confirmpwd = Request.Form["confirmpwd"];
            string newpwd = Request.Form["newpwd"];
            var emp=await _empProvider.GetEmployeeByUsernameAsync(empusername);
            if (emp.EmpId != null)
            {
                if (emp.Password != confirmpwd)
                {
                    return Content("<script>alert('密码错误！');history.go(-1);</script> ");
                }
                else
                {
                    _empProvider.UpdatePassword(newpwd, emp.EmpId);
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("PowerError", "Account");
            }
            
        }
        public ActionResult PowerError()
        {
            return View();
        }
    }
}