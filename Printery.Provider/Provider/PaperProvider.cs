using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface IPaperProvider
    {
        Task<List<PaperCViewModel>> GetAllPaper();
        Task<List<PurchasingPaperViewModel>> GetAllPaperPurchasing();
        void CreatePurchaseOrder4Paper(PurchasingPaperViewModel Paper);
        void UpdatePaper(PaperCViewModel paper);
    }
    public class PaperProvider:IPaperProvider
    {
        private readonly IPaperRespository _paperRespository;
        public PaperProvider(IPaperRespository paperRespository)
        {
            _paperRespository = paperRespository;
        }
        public async Task<List<PaperCViewModel>> GetAllPaper()
        {
            var list = new List<PaperCViewModel>();
            list = await _paperRespository.GetAllPaper();
            return (list != null) ? list : new List<PaperCViewModel>();
        }
        public async Task<List<PurchasingPaperViewModel>> GetAllPaperPurchasing()
        {
            var list = new List<PurchasingPaperViewModel>();
            list = await _paperRespository.GetAllPaperPurchasing();
            return (list != null) ? list : new List<PurchasingPaperViewModel>();
        }
        public void UpdatePaper(PaperCViewModel paper)
        {
            _paperRespository.UpdatePaper(paper);
        }
        public void CreatePurchaseOrder4Paper(PurchasingPaperViewModel Paper)
        {
            _paperRespository.CreatePurchaseOrder4Paper(Paper);
        }
    }
}
