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
    }
}
