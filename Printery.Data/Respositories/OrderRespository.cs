using Printery.Data.Contexts;
using Printery.Data.DataInit;
using Printery.Data.Model;
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
    public interface IOrderRespository
    {
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
        public async Task<List<OrderViewModel>> GetOrderByDate(string empid)
        {
            var storeProcedureName = "[dbo].[Get_Dashborad_Order]";
            var result = await _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProcedureName} @empid",
                new SqlParameter("@empid", empid)
                ).ToListAsync();
            var list = new List<OrderViewModel>();
            foreach(var i in result)
            {
                var ord = new OrderViewModel();
                ord.OrderId = i.OrderId;
                ord.CustomerName = i.CustomerName;
                ord.ProductName = i.ProductName;
                ord.Price = i.Price;
                list.Add(ord);
            }
            return list;
        }
        public async Task<TodayFocusViewModel> GetAllOrderDisplayByempid(string empid)
        {
            var storeProcedureName = "[dbo].[Get_TodayFocus]";
            var result = await _dbContext.Database.SqlQuery<TodayFocusViewModel>(
                $"{storeProcedureName} @empid",
                new SqlParameter("@empid",empid)
                ).FirstOrDefaultAsync();
            var foc = new TodayFocusViewModel();
            if (result != null)
            {
                foc.TodayAddOrder = result.TodayAddOrder;
                foc.TodayHaveDone = result.TodayHaveDone;
                foc.TodayOrderWait = result.TodayOrderWait;
                foc.YouHaveDone = result.YouHaveDone;
            }
            return foc;
        }
        public async Task<DashboradViewModel> GetDashboradDisplay(string empid)
        {
            var storeProcedureName = "[dbo].[Get_Dashborad_nav]";
            var result = await _dbContext.Database.SqlQuery<DashboradViewModel>(
                $"{storeProcedureName} @empid",
                new SqlParameter("@empid", empid)
                ).SingleOrDefaultAsync();
            var dash = new DashboradViewModel();
            if (result != null)
            {
                dash.OrderCount = result.OrderCount;
                dash.OrderWaitCount = result.OrderWaitCount;
                dash.TodayPrice = result.TodayPrice;
                dash.CustomerCount = result.CustomerCount;
            }
            return dash;
        }
        public async Task <List<DashboradBottomViewModel>> GetDashboradBottomDisplay(string empid)
        {
            var storeProcedureName = "[dbo].[Get_Dashborad_Bottom]";
            var result = await _dbContext.Database.SqlQuery<DashboradBottomViewModel>(
                $"{storeProcedureName} @empid",
                new SqlParameter("@empid", empid)
                ).ToListAsync();
            var list = new List<DashboradBottomViewModel>();
            foreach (var i in result)
            {
                var ord = new DashboradBottomViewModel();
                ord.Month = i.Month;
                ord.OrderCount = i.OrderCount;
                ord.OrderWait = i.OrderWait;
                ord.OrderDone = i.OrderDone;
                ord.Business = i.Business;
                ord.Profit = i.Profit;
                ord.BackOrder = i.BackOrder;
                list.Add(ord);
            }
            return list;
        }
        public void CreateOrder(OrderViewModel order)
        {
            var storeProcedureName = "[dbo].[Create_Order]";
            var result = _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProcedureName} @OrderId,@OrderCreate,@OrderProcess,@Price,@Deadline,@Tips,@CreatePersonId,@Status,@CustomerName,@Contact,@Addressed,@Phone,@Email,@ProductName",
                new SqlParameter("@OrderId", order.OrderId),
                new SqlParameter("@OrderCreate", order.OrderCreate),
                new SqlParameter("@OrderProcess", order.OrderProcess),
                new SqlParameter("@Price", order.Price),
                new SqlParameter("@Deadline", order.Deadline),
                new SqlParameter("@Tips", order.tips),
                new SqlParameter("@CreatePersonId", order.CreatePersonId),
                new SqlParameter("@Status", order.Status),
                new SqlParameter("@CustomerName", order.CustomerName),
                new SqlParameter("@Contact", order.Contact),
                new SqlParameter("@Addressed", order.Addressed),
                new SqlParameter("@Phone", order.Phone),
                new SqlParameter("@Email", order.Email),
                new SqlParameter("@ProductName",order.ProductName)
                ).SingleOrDefault();
        }
        public List<OrderViewModel> GetOrderByOrderId(string orderid)
        {
            var order = from u in _db.Order
                        where u.OrderId == orderid
                        select u;
            var ord = new OrderViewModel();
            var ordlist = new List<OrderViewModel>();
            foreach(var i in order)
            {
                ord.CustomerName = i.CustomerName;
                ord.Addressed = i.Addressed;
                ord.Phone = i.Phone;
                ord.Email = i.Email;
                ord.Contact = i.Contact;
                ord.ProductName = i.ProductName;
                ordlist.Add(ord);
            }
            return ordlist;
        }
        public void ProcessOrder(string orderid, string processpersonid)
        {
            var storeProcedureName = "[dbo].[Process_Order]";
            var result = _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProcedureName} @Orderid,@ProcessPersonId",
                new SqlParameter("@Orderid", orderid),
                new SqlParameter("ProcessPersonId", processpersonid)
                ).SingleOrDefault();
        }
        public void EditOrder(OrderViewModel order)
        {
            var storeProcedureName = "[dbo].[Edit_Order]";
            var result = _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProcedureName} @Orderid,@Tips,@Addressed,@Phone,@Email,@Contact",
                new SqlParameter("@Orderid", ModelItemIsNow(order.OrderId)),
                new SqlParameter("@Tips", ModelItemIsNow(order.tips)),
                new SqlParameter("@Addressed", ModelItemIsNow(order.Addressed)),
                new SqlParameter("@Phone", ModelItemIsNow(order.Phone)),
                new SqlParameter("@Email", ModelItemIsNow(order.Email)),
                new SqlParameter("@Contact", ModelItemIsNow(order.Contact))
                ).SingleOrDefault();
        }
        #region 传Null工具
        public object ModelItemIsNow(object str)
        {
            if (str == null || str.ToString().Trim().Length <= 0)
            {
                return DBNull.Value;
            }
            else
            {
                return str;
            }
        }
        #endregion
        public void DeleteOrder(string orderid)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.Order.Remove(pr.Order.Where(p => p.OrderId == orderid).FirstOrDefault());
            pr.SaveChanges();
            //_db.Order.Remove(_db.Order.Where(p => p.OrderId == orderid).FirstOrDefault());
            
        }
    }
}
