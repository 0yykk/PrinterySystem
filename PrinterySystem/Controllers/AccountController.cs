using Printery.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}