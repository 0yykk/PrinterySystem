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
        void UpdatePowerList(List<PowerControlListViewModel> powerlist);
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
        public void UpdatePowerList(List<PowerControlListViewModel> powerlist)
        {
            _empRespository.UpdatePowerList(powerlist);
        }
    }
}
