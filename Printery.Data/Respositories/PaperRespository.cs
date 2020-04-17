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
    public interface IPaperRespository
    {
        Task<List<PaperCViewModel>> GetAllPaper();       
        Task<List<PurchasingPaperViewModel>> GetAllPaperPurchasing();
        List<PurchasingPaperViewModel> GetPurchasingById(string id);
        List<PurchasingPaperViewModel> GetPurchasingByName(string name);
        List<PaperCViewModel> GetPaperByPapername(string name);
        void EditPaper(string paperid, string papername);
        void DeletePaper(string paperid);
        void PushStockInPaper(decimal Price, string PurchaseId, string paperid, int PaperCount, string ProcessPersonId);
        void DeletePaperPurchase(string purchaseid);
        void CreatePurchaseOrder4Paper(PurchasingPaperViewModel Paper);
        void UpdatePaper(PaperCViewModel paper);
    }
    public class PaperRespository:IPaperRespository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public PaperRespository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = db.GetDbContext();
        }

        public async Task<List<PaperCViewModel>> GetAllPaper()
        {
            var paperList = new List<PaperCViewModel>();
            var paperlist = new List<Paper>();
            paperlist = await _db.Paper.ToListAsync();
            foreach (var item in paperlist)
            {
                PaperCViewModel paper = new PaperCViewModel();
                paper.PaperId = item.PaperId;
                paper.PaperName = item.PaperName;
                paper.Ccount = item.Ccount;
                paperList.Add(paper);
            }
            return paperList;
        }
        public List<PaperCViewModel> GetPaperByPapername(string name)
        {
            var paperlist = from u in _db.Paper
                            where (u.PaperName.Contains(name))
                            select u;
            var list = new List<PaperCViewModel>();
            foreach(var item in paperlist)
            {
                var paper = new PaperCViewModel();
                paper.PaperId = item.PaperId;
                paper.PaperName = item.PaperName;
                paper.Ccount = item.Ccount;
                list.Add(paper);
            }
            return list;
        }
        public async Task<List<PurchasingPaperViewModel>> GetAllPaperPurchasing()
        {
            var PaperList = new List<PurchasingPaperViewModel>();
            var Paperlist = new List<PaperPurchasing>();
            Paperlist = await _db.PaperPurchasing.ToListAsync();
            foreach (var item in Paperlist)
            {
                PurchasingPaperViewModel par = new PurchasingPaperViewModel();
                par.PurchasingID = item.PurchasingID;
                par.PaperId = item.PaperId;
                par.PaperName = item.PaperName;
                par.Count = item.Count;
                par.Price = item.Price;
                par.CreatePersonId = item.CreatePersonId;
                par.ProcessPersonId = item.ProcessPersonId;
                par.Status = item.Status;
                par.CreateDate = item.CreateDate;
                par.ProcessDate = item.ProcessDate;
                PaperList.Add(par);
            }
            return PaperList;
        }
        public List<PurchasingPaperViewModel> GetPurchasingById(string id)
        {
            var par = from u in _db.PaperPurchasing
                      where u.PurchasingID == id
                      select u;
            var parc = new PurchasingPaperViewModel();
            var parList = new List<PurchasingPaperViewModel>();
            foreach(var i in par)
            {
                parc.PurchasingID = i.PurchasingID;
                parc.PaperId = i.PaperId;
                parc.PaperName = i.PaperName;
                parc.Price = i.Price;
                parc.ProcessDate = i.ProcessDate;
                parc.ProcessPersonId = i.ProcessPersonId;
                parc.Count = i.Count;
                parc.CreateDate = i.CreateDate;
                parc.CreatePersonId = i.CreatePersonId;
                parc.Status = i.Status;
                parList.Add(parc);
            }
            return parList;
        }
        public List<PurchasingPaperViewModel> GetPurchasingByName(string name)
        {
            var par = from u in _db.PaperPurchasing
                      where (u.PaperName.Contains(name))
                      select u;
            var parList = new List<PurchasingPaperViewModel>();
            foreach (var i in par)
            {
                var parc = new PurchasingPaperViewModel();
                parc.PurchasingID = i.PurchasingID;
                parc.PaperId = i.PaperId;
                parc.PaperName = i.PaperName;
                parc.Price = i.Price;
                parc.ProcessDate = i.ProcessDate;
                parc.ProcessPersonId = i.ProcessPersonId;
                parc.Count = i.Count;
                parc.CreateDate = i.CreateDate;
                parc.CreatePersonId = i.CreatePersonId;
                parc.Status = i.Status;
                parList.Add(parc);
            }
            return parList;
        }
        public void DeletePaper(string paperid)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.Paper.Remove(pr.Paper.Where(p => p.PaperId==paperid).FirstOrDefault());
            pr.SaveChanges();
        }
        public void EditPaper(string paperid, string papername)
        {
            var db = new PrinteryContext();
            var exitPaper = db.Paper.FirstOrDefault(s => s.PaperId == paperid);
            if (exitPaper != null)
            {
                db.Set<Paper>().Attach(exitPaper);
                db.Entry(exitPaper).State = EntityState.Modified;
                exitPaper.PaperName = papername;
                db.SaveChanges();
            }
        }
        public void PushStockInPaper(decimal Price, string PurchaseId, string paperid, int PaperCount, string ProcessPersonId)
        {
            var storeProcedureName = "[dbo].[Update_PaperPurchase]";
            var Result = _dbContext.Database.SqlQuery<PurchasingInkViewModel>(
                $"{storeProcedureName} @PurChaseId,@PaperId,@Ccount,@Price,@ProcessPersonId",
                new SqlParameter("@PurchaseId",PurchaseId),
                new SqlParameter("@PaperId",paperid),
                new SqlParameter("@Ccount", PaperCount),
                new SqlParameter("@Price",Price),
                new SqlParameter("@ProcessPersonId",ProcessPersonId)
                ).SingleOrDefault();
        }
        public void DeletePaperPurchase(string purchaseid)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.PaperPurchasing.Remove(pr.PaperPurchasing.Where(p => p.PurchasingID == purchaseid).FirstOrDefault());
            pr.SaveChanges();
        }
        public void UpdatePaper(PaperCViewModel paper)
        {
            var exitPaper = _db.Paper.FirstOrDefault(s => s.PaperName == paper.PaperName);
            if (exitPaper == null)
            {
                var storeProcedureName = "[dbo].[Add_Paper]";
                var Result = _dbContext.Database.SqlQuery<PaperCViewModel>(
                    $"{storeProcedureName} @PaperId,@PaperName,@Ccount",
                    new SqlParameter("@PaperId",paper.PaperId),
                    new SqlParameter("@PaperName",paper.PaperName),
                    new SqlParameter("@Ccount",paper.Ccount)
                    ).SingleOrDefault();
            }
            else
            {
                var storeProcedureName = "[dbo].[Update_Paper]";
                var Result = _dbContext.Database.SqlQuery<PaperCViewModel>(
                    $"{storeProcedureName} @PaperId,@PaperName,@Ccount",
                    new SqlParameter("@PaperId", paper.PaperId),
                    new SqlParameter("@PaperName", paper.PaperName),
                    new SqlParameter("@Ccount", paper.Ccount)
                    ).SingleOrDefault();
            }
        }
        public void CreatePurchaseOrder4Paper(PurchasingPaperViewModel Paper)
        {
            var storeProcedureName = "[dbo].[Create_Paper_PurchaseOrder]";
            var Result = _dbContext.Database.SqlQuery<PurchasingInkViewModel>(
                    $"{storeProcedureName} @PurchasingID,@PaperName,@Count,@Price,@CreatePersonId,@ProcessPersonId,@Status,@CreateDate,@ProcessDate",
                    new SqlParameter("@PurchasingID", Paper.PurchasingID),
                    new SqlParameter("@PaperName", Paper.PaperName),
                    new SqlParameter("@Count", Paper.Count),
                    new SqlParameter("@Price", Paper.Price),
                    new SqlParameter("@CreatePersonId", Paper.CreatePersonId),
                    new SqlParameter("@ProcessPersonId", Paper.ProcessPersonId),
                    new SqlParameter("@Status", Paper.Status),
                    new SqlParameter("@CreateDate", Paper.CreateDate),
                    new SqlParameter("@ProcessDate", Paper.ProcessDate)
                ).SingleOrDefault();
        }
    }
}
