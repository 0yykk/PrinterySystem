using Printery.Data.Contexts;
using Printery.Data.Model;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Respositories
{
    public interface IEmpRepository
    {
        Task<EmployeeViewModel> GetEmployeeByUsernameAsync(string username);
        Task<EmployeeViewModel> GetEmployeeByUserIdAsync(string userid);
        Task<List<EmpGroupViewModel>> GetAllEmpGroup();
        Task<List<PowerListViewModel>> GetAllPowerList();
        void UpdatePowerList(List<PowerControlListViewModel> powerlist);

    }
    public class EmpRepository:IEmpRepository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public EmpRepository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = db.GetDbContext();
        }
        public async  Task<EmployeeViewModel> GetEmployeeByUsernameAsync(string username)
        {
            var emp = new EmployeeViewModel();
            var storeProcedureName = "[dbo].[Select_User]";
            try
            {
                emp = await _dbContext.Database.SqlQuery<EmployeeViewModel>(
                        $"{storeProcedureName} @username",
                        new SqlParameter("@username", username)
                    ).FirstOrDefaultAsync();
                return emp == null ? null : emp;
            }
            catch(Exception ex)
            {
                
                return new EmployeeViewModel();
            }
        }
        public async Task<EmployeeViewModel> GetEmployeeByUserIdAsync(string userid)
        {
            var emp = new EmployeeViewModel();
            //var order = from u in _db.Order
            //            where u.OrderId == orderid
            //            select u;
            var Iemp = await _db.Employee.Where(p => p.EmpId == userid).FirstOrDefaultAsync();
            if (Iemp != null)
            {
                emp.EmpId = Iemp.EmpId;
                emp.Username = Iemp.Username;
                emp.Sex = Iemp.Sex;
                emp.Nation = Iemp.Nation;
                emp.UserGroup = Iemp.UserGroup;
                emp.Department = Iemp.Department;
                emp.QQ = Iemp.QQ;
                emp.Ename = Iemp.Ename;
                emp.Phone = Iemp.Phone;
            }

            return emp;
        }
        public async Task<List<EmpGroupViewModel>> GetAllEmpGroup()
        {
            var empg = new List<EmpGroupViewModel>();
            var Empg = new List<EmpGroup>();
            Empg = await _db.EmpGroup.ToListAsync();
            foreach(var item in Empg)
            {
                EmpGroupViewModel EM = new EmpGroupViewModel();
                EM.GroupId = item.GroupId;
                EM.GroupName = item.GroupName;
                EM.Tip = item.Tip;
                empg.Add(EM);
            }
            return empg;
        }
        public async Task<List<PowerListViewModel>> GetAllPowerList()
        {
            var powlist = new List<PowerListViewModel>();
            var powList = new List<PowerList>();
            powList = await _db.PowerList.ToListAsync();
            foreach(var item in powList)
            {
                PowerListViewModel p = new PowerListViewModel();
                p.PowerId = item.PowerId;
                p.PowerName = item.PowerName;
                p.Tip = item.Tip;
                powlist.Add(p);
            }
            return powlist;
        }
        public void UpdatePowerList(List<PowerControlListViewModel> powerlist)
        {
            PrinteryContext db = new PrinteryContext();
            db.Database.ExecuteSqlCommand("DELETE FROM PowerControlList WHERE GroupId={0}", powerlist[0].GroupId);
            foreach(var item in powerlist)
            {
                var newItem = new PowerControlList()
                {
                    ControlId = Guid.NewGuid().ToString(),
                    PowerId=item.PowerId,
                    GroupId=item.GroupId
                };
                db.PowerControlList.Add(newItem);
                db.SaveChanges();
            }
        }
    }
}
