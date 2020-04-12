using Printery.Data.Contexts;
using Printery.Data.Model;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Respositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerViewModel>> GetAllCustomer();
    }
    public class CustomerRepository:ICustomerRepository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public CustomerRepository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = db.GetDbContext();
        }
        public async Task<List<CustomerViewModel>> GetAllCustomer()
        {
            var CusList = new List<CustomerViewModel>();
            var Cuslist = new List<Customer>();
            Cuslist = await _db.Customer.ToListAsync();
            foreach(var item in Cuslist)
            {
                CustomerViewModel Cust = new CustomerViewModel();
                Cust.CustomerId = item.CustomerId;
                Cust.CustomerName = item.CustomerName;
                Cust.Contact = item.Contact;
                Cust.Phone = item.Phone;
                Cust.MobilePhone = item.MobilePhone;
                Cust.CAddress = item.CAddress;
                CusList.Add(Cust);
            }
            return CusList;
        }

    }
}
