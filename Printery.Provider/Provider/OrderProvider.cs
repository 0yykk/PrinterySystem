using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface IOrderProvider
    {
        Task<List<OrderViewModel>> GetAllOrder();
    }
    public class OrderProvider:IOrderProvider
    {
        private readonly IOrderRespository _orderRespository;
        public OrderProvider(IOrderRespository orderRespository)
        {
            _orderRespository = orderRespository;
        }
        public async Task<List<OrderViewModel>> GetAllOrder()
        {
            var list = new List<OrderViewModel>();
            list = await _orderRespository.GetAllOrder();
            return (list != null) ? list : new List<OrderViewModel>();
        }
    }
}
