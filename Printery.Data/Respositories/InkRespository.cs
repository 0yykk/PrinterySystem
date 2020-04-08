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
        void UpdateInk(InkCViewModel ink);
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
    }
}
