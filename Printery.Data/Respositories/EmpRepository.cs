using Printery.Data.Contexts;
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
    }
}
