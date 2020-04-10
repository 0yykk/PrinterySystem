using Printery.Data.Contexts;
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
    public interface IInkRespository
    {
        Task<List<InkCViewModel>> GetAllInk();
        Task<List<PurchasingInkViewModel>> GetAllInkPurchasing();
        List<PurchasingInkViewModel> GetPurchasingById(string id);
        void PushInStockInk(string PurchaseId, decimal Price, string InkId, int InkCout, string ProcessPersonId);
        void DeleteInkPurchase(string purchaseid);
        void UpdateInk(InkCViewModel ink);
        void CreatePurchaseOrder4Ink(PurchasingInkViewModel ink);
    }
    public class InkRespository : IInkRespository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public InkRespository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = db.GetDbContext();
        }

        public async Task<List<InkCViewModel>> GetAllInk()
        {
            var InkList = new List<InkCViewModel>();
            var Inklist = new List<InkStock>();
            Inklist = await _db.InkStock.ToListAsync();
            foreach (var item in Inklist)
            {
                InkCViewModel Ink = new InkCViewModel();
                Ink.InkId = item.InkId;
                Ink.InkName = item.InkName;
                Ink.Ccount = item.Ccount;
                InkList.Add(Ink);
            }
            return InkList;
        }
        public async Task<List<PurchasingInkViewModel>> GetAllInkPurchasing()
        {
            var InkList = new List<PurchasingInkViewModel>();
            var Inklist = new List<InkPurchasing>();
            Inklist = await _db.InkPurchasing.ToListAsync();
            foreach(var item in Inklist)
            {
                PurchasingInkViewModel ink = new PurchasingInkViewModel();
                ink.PurchasingID = item.PurchasingID;
                ink.InkId = item.InkId;
                ink.InkName = item.InkName;
                ink.Count = item.Count;
                ink.Price = item.Price;
                ink.CreatePersonId = item.CreatePersonId;
                ink.ProcessPersonId = item.ProcessPersonId;
                ink.Status = item.Status;
                ink.CreateDate = item.CreateDate;
                ink.ProcessDate = item.ProcessDate;
                InkList.Add(ink);
            }
            return InkList;
        }
        public List<PurchasingInkViewModel> GetPurchasingById(string id)
        {
            var pur = from u in _db.InkPurchasing
                      where u.PurchasingID == id
                      select u;
            var purc = new PurchasingInkViewModel();
            var purList = new List<PurchasingInkViewModel>();
            foreach(var i in pur)
            {
                purc.PurchasingID = i.PurchasingID;
                purc.InkId = i.InkId;
                purc.InkName = i.InkName;
                purc.Price = i.Price;
                purc.ProcessDate = i.ProcessDate;
                purc.ProcessPersonId = i.ProcessPersonId;
                purc.Count = i.Count;
                purc.CreateDate = i.CreateDate;
                purc.CreatePersonId = i.CreatePersonId;
                purc.Status = i.Status;
                purList.Add(purc);
            }
            return purList;
        }
        public void PushInStockInk(string PurchaseId, decimal Price, string InkId, int InkCout, string ProcessPersonId)
        {
            var storeProcedureName = "[dbo].[Update_InkPurchase]";
            var Result = _dbContext.Database.SqlQuery<PurchasingInkViewModel>(
                $"{storeProcedureName} @PurChaseId,@InkId,@Ccount,@Price,@ProcessPersonId",
                new SqlParameter("@PurchaseId", PurchaseId),
                new SqlParameter("@InkId", InkId),
                new SqlParameter("@Ccount", InkCout),
                new SqlParameter("@Price", Price),
                new SqlParameter("@ProcessPersonId", ProcessPersonId)
                ).SingleOrDefault();
        }
        public void DeleteInkPurchase(string purchaseid)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.InkPurchasing.Remove(pr.InkPurchasing.Where(p => p.PurchasingID == purchaseid).FirstOrDefault());
            pr.SaveChanges();
        }
        public void UpdateInk(InkCViewModel ink)
        {
            var exitink = _db.InkStock.FirstOrDefault(s => s.InkName == ink.InkName);
            if (exitink == null)
            {
                var storeProcedureName = "[dbo].[Add_Ink]";
                var Result = _dbContext.Database.SqlQuery<PaperCViewModel>(
                    $"{storeProcedureName} @InkId,@InkName,@Ccount",
                    new SqlParameter("@InkId", ink.InkId),
                    new SqlParameter("@InkName", ink.InkName),
                    new SqlParameter("@Ccount", ink.Ccount)
                    ).SingleOrDefault();
            }
            else
            {
                var storeProcedureName = "[dbo].[Update_Ink]";
                var Result = _dbContext.Database.SqlQuery<PaperCViewModel>(
                    $"{storeProcedureName} @InkId,@InkName,@Ccount",
                    new SqlParameter("@InkId", ink.InkId),
                    new SqlParameter("@InkName", ink.InkName),
                    new SqlParameter("@Ccount", ink.Ccount)
                    ).SingleOrDefault();
            }
        }
        public void CreatePurchaseOrder4Ink(PurchasingInkViewModel ink)
        {
            var storeProcedureName = "[dbo].[Create_Ink_PurchaseOrder]";
            var Result = _dbContext.Database.SqlQuery<PurchasingInkViewModel>(
                    $"{storeProcedureName} @PurchasingID,@InkName,@Count,@Price,@CreatePersonId,@ProcessPersonId,@Status,@CreateDate,@ProcessDate",
                    new SqlParameter("@PurchasingID", ink.PurchasingID),
                    new SqlParameter("@InkName", ink.InkName),
                    new SqlParameter("@Count", ink.Count),
                    new SqlParameter("@Price",ink.Price),
                    new SqlParameter("@CreatePersonId",ink.CreatePersonId),
                    new SqlParameter("@ProcessPersonId",ink.ProcessPersonId),
                    new SqlParameter("@Status",ink.Status),
                    new SqlParameter("@CreateDate",ink.CreateDate),
                    new SqlParameter("@ProcessDate",ink.ProcessDate)
                ).SingleOrDefault();
        }
    }
}
