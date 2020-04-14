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
        public Person4ManagerController(IEmpProvider empProvider,ICustomerProvider customerProvider)
        {
            _empProvider = empProvider;
            _customerProvider = customerProvider;
        }
        // GET: Person4Manager
        public async Task<ActionResult> CustomerInfo()
        {
            var Culist = new List<CustomerViewModel>();
            Culist = await _customerProvider.GetAllCustomer();
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
            var Emlist = new List<EmployeeViewModel>();
            var Department = new List<DepartmentViewModel>();
            var Usergroup = new List<EmpGroupViewModel>();
            Department = await _empProvider.GetDepartment();
            Usergroup = await _empProvider.GetAllEmpGroup();
            Emlist = await _empProvider.GetAllEmployee();
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
            var emp = new EmployeeViewModel();
            string id = Session["LoginId"].ToString();
            emp = await _empProvider.GetEmployeeByUserIdAsync(id);
            emp.EmpId = id;
            ViewBag.Employee = emp;
            return View();
        }
        #region 雇员管理 api
        public JsonResult AddEmployee(EmployeeViewModel emp)
        {
            _empProvider.AddEmployee(emp);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditEmployee(EmployeeViewModel emp)
        {
            _empProvider.EditEmployee(emp);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEmployee(string empguid)
        {
            _empProvider.DeleteEnployee(empguid.Trim());
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 客户管理api
        public JsonResult EditCustomer(CustomerViewModel cms)
        {
            _customerProvider.EditCustomer(cms);
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCustomer(string Cusguid)
        {
            _customerProvider.DeleteCustomer(Cusguid.Trim());
            string a = "Sucessful";
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddCustomer(CustomerViewModel cms)
        {
            _customerProvider.AddCustomer(cms);
            string a = "Sucessful";
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