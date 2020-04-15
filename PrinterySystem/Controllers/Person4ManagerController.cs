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
    public class Person4ManagerController : Controller
    {
        private readonly IEmpProvider _empProvider;
        private readonly ICustomerProvider _customerProvider;
        private readonly IPowerCheckProvider _powerCheckProvider;
        private readonly IOrderProvider _orderProvider;
        public Person4ManagerController(IEmpProvider empProvider,ICustomerProvider customerProvider, IPowerCheckProvider powerCheckProvider, IOrderProvider orderProvider)
        {
            _empProvider = empProvider;
            _customerProvider = customerProvider;
            _powerCheckProvider = powerCheckProvider;
            _orderProvider = orderProvider;
        }
        // GET: Person4Manager
        public async Task<ActionResult> CustomerInfo()
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var Culist = new List<CustomerViewModel>();
            if (sa.EmpId != null)
            {
                Culist = await _customerProvider.GetAllCustomer();
            }
            else
            {
                if (userpower.Contains("401"))
                {
                    Culist = await _customerProvider.GetAllCustomer();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
            int pageindex = 1;
            var recordCount = Culist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
            ViewBag.CustomerList = Culist.OrderByDescending(art => art.CustomerName)
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
        public async Task<ActionResult> EmployeeInfo()
        {
            string power = Session["Power"].ToString();
            string empid = Session["LoginId"].ToString();
            var sa = new SAViewModel();
            var userpower = new List<string>();
            userpower = _powerCheckProvider.CheckPower(power);
            sa = _powerCheckProvider.CheckSAuser(empid);
            var Emlist = new List<EmployeeViewModel>();
            var Department = new List<DepartmentViewModel>();
            var Usergroup = new List<EmpGroupViewModel>();
            if (sa.EmpId != null)
            {
                Department = await _empProvider.GetDepartment();
                Usergroup = await _empProvider.GetAllEmpGroup();
                Emlist = await _empProvider.GetAllEmployee();
            }
            else
            {
                if (userpower.Contains("301"))
                {
                    Department = await _empProvider.GetDepartment();
                    Usergroup = await _empProvider.GetAllEmpGroup();
                    Emlist = await _empProvider.GetAllEmployee();
                }
                else
                {
                    return RedirectToAction("PowerError", "Account");
                }
            }
            int pageindex = 1;
            var recordCount = Emlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 5;
            ViewBag.EmployeeList = Emlist.OrderByDescending(art => art.EmpId)
                .Skip((pageindex - 1) * PAGE_SZ)
                .Take(PAGE_SZ).ToList();
            ViewBag.Department = Department;
            ViewBag.EmpGroup = Usergroup;
            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount,
            };
            return View();
        }
        public async Task<ActionResult> MyInfo()
        {
            var foc = new TodayFocusViewModel();
            var emp = new EmployeeViewModel();
            string id = Session["LoginId"].ToString();
            emp = await _empProvider.GetEmployeeByUserIdAsync(id);
            foc = await _orderProvider.GetAllOrderDisplayByempid(id);
            emp.EmpId = id;
            ViewBag.TodayFocus = foc;
            ViewBag.Employee = emp;
            return View();
        }
        #region 雇员管理 api
        public JsonResult AddEmployee(EmployeeViewModel emp)
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
                _empProvider.AddEmployee(emp);
                a = "添加成功";
            }
            else
            {
                if (userpower.Contains("304"))
                {
                    _empProvider.AddEmployee(emp);
                    a = "添加成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditEmployee(EmployeeViewModel emp)
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
                _empProvider.EditEmployee(emp);
                a = "修改成功";
            }
            else
            {
                if (userpower.Contains("302"))
                {
                    _empProvider.EditEmployee(emp);
                    a = "修改成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEmployee(string empguid)
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
                _empProvider.DeleteEnployee(empguid.Trim());
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("303"))
                {
                    _empProvider.DeleteEnployee(empguid.Trim());
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
        #region 客户管理api
        public JsonResult EditCustomer(CustomerViewModel cms)
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
                _customerProvider.EditCustomer(cms);
                a = "编辑成功";
            }
            else
            {
                if (userpower.Contains("402"))
                {
                    _customerProvider.EditCustomer(cms);
                    a = "编辑成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCustomer(string Cusguid)
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
                _customerProvider.DeleteCustomer(Cusguid.Trim());
                a = "删除成功";
            }
            else
            {
                if (userpower.Contains("403"))
                {
                    _customerProvider.DeleteCustomer(Cusguid.Trim());
                    a = "删除成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddCustomer(CustomerViewModel cms)
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
                _customerProvider.AddCustomer(cms);
                a = "添加成功";
            }
            else
            {
                if (userpower.Contains("404"))
                {
                    _customerProvider.AddCustomer(cms);
                    a = "添加成功";
                }
                else
                {
                    a = "权限不足";
                }
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public async Task<ActionResult> MyInfoEdit(string id,string type)
        {
            var employee = new EmployeeViewModel();
            employee = await _empProvider.GetEmployeeById(id);
            employee.EmpId = id.Trim();
            return View(employee);
            
        }
        [HttpPost]
        public ActionResult MyInfoEdit(EmployeeViewModel emp)
        {
            _empProvider.UpdateEmployeeInfo(emp);
            return Content("<script>alert('更新成功!');history.go(-2);</script> ");

        }
    }
}