using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Printery.Data.Model;
using Printery.Data.Contexts;
using System.Data.SqlClient;

namespace Printery.Data.DataInit
{
    public class DataSeedinit
    {
        public static void SeedDatabaseOrder(PrinteryContext db)
        {
            var _Order = new Order()
            {
                OrderId = "12345",
                OrderCreate = DateTime.Now,
                OrderProcess = DateTime.Now,
                CreatePersonId = "123345",
                CustomerId = "12345",
                Deadline = DateTime.Now,
                CustomerName = "12345",
                Status = "已完成",
                Price = 123,
                ProcessPersonId = "123",
                Phone = "1234",
                Email = "1234",
                Tips = "123",
                Addressed="123456"
            };
            db.Order.Add(_Order);
            db.SaveChanges();
        }
    }
}
