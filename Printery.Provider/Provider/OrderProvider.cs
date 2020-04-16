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
        /// <summary>
        /// 取得所有订单
        /// </summary>
        /// <returns>订单列表</returns>
        Task<List<OrderViewModel>> GetAllOrder();
        Task<List<OrderViewModel>> GetOrderByDate(string empid);
        Task<TodayFocusViewModel> GetAllOrderDisplayByempid(string empid);
        Task<DashboradViewModel> GetDashboradDisplay(string empid);
        Task<List<DashboradBottomViewModel>> GetDashboradBottomDisplay(string empid);
        List<OrderViewModel> GetOrderByOrderId(string orderid);
        void ProcessOrder(string orderid, string processpersonid);
        void CreateOrder(OrderViewModel order);
        void EditOrder(OrderViewModel order);
        void DeleteOrder(string orderid);
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
        public async Task<List<OrderViewModel>> GetOrderByDate(string empid)
        {
            var list = new List<OrderViewModel>();
            list = await _orderRespository.GetOrderByDate(empid);
            return list;
        }
        public async Task<TodayFocusViewModel> GetAllOrderDisplayByempid(string empid)
        {
            var foc = new TodayFocusViewModel();
            foc = await _orderRespository.GetAllOrderDisplayByempid(empid);
            return foc;
        }
        public async Task<DashboradViewModel> GetDashboradDisplay(string empid)
        {
            var dash = new DashboradViewModel();
            dash = await _orderRespository.GetDashboradDisplay(empid);
            return dash;
        }
        public async Task<List<DashboradBottomViewModel>> GetDashboradBottomDisplay(string empid)
        {
            var list=new List<DashboradBottomViewModel>();
            list = await _orderRespository.GetDashboradBottomDisplay(empid);
            return list;
        }
        public List<OrderViewModel> GetOrderByOrderId(string orderid)
        {
            var list = new List<OrderViewModel>();
            list = _orderRespository.GetOrderByOrderId(orderid);
            return list;
        }
        public void ProcessOrder(string orderid, string processpersonid)
        {
            _orderRespository.ProcessOrder(orderid, processpersonid);
        }
        public void CreateOrder(OrderViewModel order)
        {
            _orderRespository.CreateOrder(order);
        }
        public void EditOrder(OrderViewModel order)
        {
            _orderRespository.EditOrder(order);
        }
        public void DeleteOrder(string orderid)
        {
            _orderRespository.DeleteOrder(orderid);
        }
    }
}
