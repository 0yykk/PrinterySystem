using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface ICustomerProvider
    {
        Task<List<CustomerViewModel>> GetAllCustomer();
        void EditCustomer(CustomerViewModel cms);
        void AddCustomer(CustomerViewModel cms);
        void DeleteCustomer(string cmsid);
    }
    public class CustomerProvider:ICustomerProvider
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerProvider(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerViewModel>> GetAllCustomer()
        {
            var list = new List<CustomerViewModel>();
            list = await _customerRepository.GetAllCustomer();
            return list;
        }
        public void EditCustomer(CustomerViewModel cms)
        {
            _customerRepository.EditCustomer(cms);
        }
        public void AddCustomer(CustomerViewModel cms)
        {
            _customerRepository.AddCustomer(cms);
        }
        public void DeleteCustomer(string cmsid)
        {
            _customerRepository.DeleteCustomer(cmsid);
        }
    }
}
