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
        public Person4ManagerController(IEmpProvider empProvider)
        {
            _empProvider = empProvider;
        }
        // GET: Person4Manager
        public ActionResult CustomerInfo()
        {
            return View();
        }
        public ActionResult EmployeeInfo()
        {
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
        public ActionResult MyInfoEdit()
        {
            return View();
        }
    }
}