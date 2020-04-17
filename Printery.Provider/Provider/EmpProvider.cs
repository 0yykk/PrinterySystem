using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface IEmpProvider
    {
        Task<EmployeeViewModel> GetEmployeeByUsernameAsync(string username);
        Task<EmployeeViewModel> GetEmployeeByUserIdAsync(string userid);
        Task<List<EmpGroupViewModel>> GetAllEmpGroup();
        Task<List<PowerListViewModel>> GetAllPowerList();
        Task<EmployeeViewModel> GetEmployeeById(string id);
        Task<List<EmployeeViewModel>> GetAllEmployee();
        Task<List<DepartmentViewModel>> GetDepartment();
        List<EmployeeViewModel> GetEmployeeByName(string name);
        List<PowerControlListViewModel> GetPowerContrlListById(string GroupId);
        List<EmpGroupViewModel> GetUserGroupByName(string name);
        void UpdatePassword(string pwd,string empid);
        void UpdatePowerList(List<PowerControlListViewModel> powerlist);
        void AddUserGroupByGroupName(string GroupName);
        void AddEmployee(EmployeeViewModel emp);
        void UpdateEmployeeInfo(EmployeeViewModel emp);
        void EditEmployee(EmployeeViewModel emp);
        void DeleteUserGroup(string GroupId);
        void DeleteEnployee(string empid);
    }
    public class EmpProvider:IEmpProvider
    {
        private readonly IEmpRepository _empRespository;
        public EmpProvider(IEmpRepository empRespository)
        {
            _empRespository = empRespository;
        }
        public async Task<EmployeeViewModel> GetEmployeeByUsernameAsync(string username)
        {
            var user = new EmployeeViewModel();
            user = await _empRespository.GetEmployeeByUsernameAsync(username);
            return user == null ? new EmployeeViewModel() : user;
        }
        public async Task<EmployeeViewModel> GetEmployeeByUserIdAsync(string userid)
        {
            var emp = new EmployeeViewModel();
            emp = await _empRespository.GetEmployeeByUserIdAsync(userid);
            return emp;
        }
        public async Task<List<EmpGroupViewModel>> GetAllEmpGroup()
        {
            var empg = new List<EmpGroupViewModel>();
            empg = await _empRespository.GetAllEmpGroup();
            return empg;
        }
        public async Task<List<PowerListViewModel>> GetAllPowerList()
        {
            var powlist = new List<PowerListViewModel>();
            powlist = await _empRespository.GetAllPowerList();
            return powlist;
        }
        public async Task<EmployeeViewModel> GetEmployeeById(string id)
        {
            var emp = new EmployeeViewModel();
            emp = await _empRespository.GetEmployeeById(id);
            return emp;
        }
        public async Task<List<EmployeeViewModel>> GetAllEmployee()
        {
            var emp = new List<EmployeeViewModel>();
            emp = await _empRespository.GetAllEmployee();
            return emp;
        }
        public async Task<List<DepartmentViewModel>> GetDepartment()
        {
            var dep = new List<DepartmentViewModel>();
            dep = await _empRespository.GetDepartment();
            return dep;
        }
        public List<EmployeeViewModel> GetEmployeeByName(string name)
        {
            var list = new List<EmployeeViewModel>();
            list = _empRespository.GetEmployeeByName(name);
            return list;
        }
        public List<PowerControlListViewModel> GetPowerContrlListById(string GroupId)
        {
            var list = new List<PowerControlListViewModel>();
            list = _empRespository.GetPowerContrlListById(GroupId);
            return list;
        }
        public List<EmpGroupViewModel> GetUserGroupByName(string name)
        {
            var list = new List<EmpGroupViewModel>();
            list = _empRespository.GetUserGroupByName(name);
            return list;
        }
        public void UpdatePassword(string pwd, string empid)
        {
            _empRespository.UpdatePassword(pwd, empid);
        }
        public void UpdatePowerList(List<PowerControlListViewModel> powerlist)
        {
            _empRespository.UpdatePowerList(powerlist);
        }
        public void AddUserGroupByGroupName(string GroupName)
        {
            _empRespository.AddUserGroupByGroupName(GroupName);
        }
        public void AddEmployee(EmployeeViewModel emp)
        {
            _empRespository.AddEmployee(emp);
        }
        public void UpdateEmployeeInfo(EmployeeViewModel emp)
        {
            _empRespository.UpdateEmployeeInfo(emp);
        }
        public void EditEmployee(EmployeeViewModel emp)
        {
            _empRespository.EditEmployee(emp);
        }
        public void DeleteUserGroup(string GroupId)
        {
            _empRespository.DeleteUserGroup(GroupId);
        }
        public void DeleteEnployee(string empid)
        {
            _empRespository.DeleteEnployee(empid);
        }
    }
}
