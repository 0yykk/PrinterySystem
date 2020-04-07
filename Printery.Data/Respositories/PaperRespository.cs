using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Respositories
{
    public interface IPaperRespository
    {
        Task<List<PaperCViewModel>> GetAllPaper();
    }
    public class PaperRespository
    {
    }
}
