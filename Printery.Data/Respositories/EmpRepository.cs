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
        Task<EmployeeViewModel> GetEmployeeById(string id);
        List<PowerControlListViewModel> GetPowerContrlListById(string GroupId);
        void AddUserGroupByGroupName(string GroupName);
        void UpdatePowerList(List<PowerControlListViewModel> powerlist);
        void UpdateEmployeeInfo(EmployeeViewModel emp);
        void DeleteUserGroup(string GroupId);

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
        public async Task<EmployeeViewModel> GetEmployeeById(string id)
        {
            var emp = new EmployeeViewModel();
            var db = new PrinteryContext();
            var exitemp = await _db.Employee.FirstOrDefaultAsync(s => s.EmpId == id);
            if (exitemp != null)
            {
                emp.EmpId = exitemp.EmpId;
                emp.Ename = exitemp.Ename;
                emp.Department = exitemp.Department;
                emp.IDCardNum = exitemp.IDCardNum;
                emp.Nation = exitemp.Nation;
                emp.Phone = exitemp.Phone;
                emp.QQ = exitemp.QQ;
                emp.Sex = exitemp.Sex;
                emp.UserGroup = exitemp.UserGroup;
                emp.Username = exitemp.Username;
            }
            return emp;
        }
        public List<PowerControlListViewModel> GetPowerContrlListById(string GroupId)
        {
            var powerlist = from u in _db.PowerControlList
                            where u.GroupId == GroupId
                            select u;
            var powerList = new List<PowerControlListViewModel>();
            foreach(var i in powerlist)
            {
                var power = new PowerControlListViewModel();
                power.PowerId = i.PowerId;
                power.GroupId = i.GroupId;
                power.ControlId = i.ControlId;
                powerList.Add(power);

            }
            return powerList;
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
        public void AddUserGroupByGroupName(string GroupName)
        {
            PrinteryContext db = new PrinteryContext();
            var newGroup = new EmpGroup()
            {
                GroupId = Guid.NewGuid().ToString(),
                GroupName = GroupName,
                Tip = ""
            };
            db.EmpGroup.Add(newGroup);
            db.SaveChanges();

        }
        public void UpdateEmployeeInfo(EmployeeViewModel emp)
        {
            var db = new PrinteryContext();
            var exitEmp = db.Employee.FirstOrDefault(s => s.EmpId == emp.EmpId);
            if (exitEmp != null)
            {
                db.Set<Employee>().Attach(exitEmp);
                db.Entry(exitEmp).State= EntityState.Modified;
                exitEmp.Ename = emp.Ename;
                exitEmp.Department = emp.Department;
                exitEmp.Nation = emp.Nation;
                exitEmp.Sex = emp.Sex;
                exitEmp.QQ = emp.QQ;
                exitEmp.Phone = emp.Phone;
                db.SaveChanges();
            }
           
        }
        public void DeleteUserGroup(string GroupId)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.Database.ExecuteSqlCommand("DELETE FROM PowerControlList WHERE GroupId={0}", GroupId);
            pr.EmpGroup.Remove(pr.EmpGroup.Where(p => p.GroupId == GroupId).FirstOrDefault());
            pr.SaveChanges();
        }
    }
}
