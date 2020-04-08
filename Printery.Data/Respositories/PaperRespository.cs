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
    }
}
