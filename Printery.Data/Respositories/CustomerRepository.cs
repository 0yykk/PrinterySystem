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
        List<CustomerViewModel> GetCustomerByName(string name);
        void AddCustomer(CustomerViewModel cms);
        void EditCustomer(CustomerViewModel cms);
        void DeleteCustomer(string cmsid);
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
        public List<CustomerViewModel> GetCustomerByName(string name)
        {
            var cuslist = from u in _db.Customer
                          where (u.CustomerName.Contains(name))
                          select u;
            var list = new List<CustomerViewModel>();
            foreach (var item in cuslist)
            {
                var Cust = new CustomerViewModel();
                Cust.CustomerId = item.CustomerId;
                Cust.CustomerName = item.CustomerName;
                Cust.Contact = item.Contact;
                Cust.Phone = item.Phone;
                Cust.MobilePhone = item.MobilePhone;
                Cust.CAddress = item.CAddress;
                list.Add(Cust);
            }
            return list;
        }
        public void AddCustomer(CustomerViewModel cms)
        {
            var db = new PrinteryContext();
            var neWCus = new Customer()
            {
                CustomerId = Guid.NewGuid().ToString(),
                CustomerName = cms.CustomerName,
                Contact = cms.Contact,
                Phone = cms.Phone,
                MobilePhone = cms.MobilePhone,
                CAddress = cms.CAddress
            };
            db.Customer.Add(neWCus);
            db.SaveChanges();
        }
        public void EditCustomer(CustomerViewModel cms)
        {
            var db = new PrinteryContext();
            var exitCms = db.Customer.FirstOrDefault(s => s.CustomerId == cms.CustomerId);
            if (exitCms != null)
            {
                db.Set<Customer>().Attach(exitCms);
                db.Entry(exitCms).State = EntityState.Modified;
                exitCms.CustomerName = cms.CustomerName;
                exitCms.Contact = cms.Contact;
                exitCms.Phone = cms.Phone;
                exitCms.MobilePhone = cms.MobilePhone;
                exitCms.CAddress = cms.CAddress;
                db.SaveChanges();
            }
        }
        public void DeleteCustomer(string cmsid)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.Customer.Remove(pr.Customer.Where(p => p.CustomerId == cmsid).FirstOrDefault());
            pr.SaveChanges();
        }

    }
}
