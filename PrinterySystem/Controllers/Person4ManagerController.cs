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