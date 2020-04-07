using Printery.Data.Contexts;
using Printery.Data.DataInit;
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
    public interface IOrderRespository
    {
        Task<List<OrderViewModel>> GetAllOrder();
    }
    public class OrderRespository:IOrderRespository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public OrderRespository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
            
        }

        public async Task<List<OrderViewModel>> GetAllOrder()
        {
            //PrinteryContext db1 = new PrinteryContext();
            //DataSeedinit.SeedDatabaseOrder(db1);
            var orderList = new List<OrderViewModel>();
            var orderlist = new List<Order>();
            orderlist = await _db.Order.ToListAsync();
            foreach (var item in orderlist)
            {
                OrderViewModel order = new OrderViewModel();
                order.OrderId = item.OrderId;
                order.Deadline = item.Deadline;
                order.CustomerName = item.CustomerName;
                order.Phone = item.Phone;
                order.Status = item.Status;
                order.Phone = item.Phone;
                order.Price = item.Price;
                order.OrderCreate = item.OrderCreate;
                order.OrderProcess = item.OrderProcess;
                order.CreatePersonId = item.CreatePersonId;
                orderList.Add(order);
            }
            return orderList;
        }
    }
}
