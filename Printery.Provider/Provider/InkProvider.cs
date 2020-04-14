using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface IInkProvider
    {
        Task<List<InkCViewModel>> GetAllInk();
        Task<List<PurchasingInkViewModel>> GetAllInkPurchasing();
        List<PurchasingInkViewModel> GetPurchasingById(string id);
        void EditInk(string inkid, string inkname);
        void DeleteInk(string inkid);
        void PushInStockInk(string PurchaseId, decimal Price, string InkId, int InkCout, string ProcessPersonId);
        void DeleteInkPurchase(string purchaseid);
        void UpdateInk(InkCViewModel ink);
        void CreatePurchaseOrder4Ink(PurchasingInkViewModel ink);
    }
    public class InkProvider:IInkProvider
    {
        private readonly IInkRespository _inkRespository;
        public InkProvider(IInkRespository inkRespository)
        {
            _inkRespository = inkRespository;
        }

        public async Task<List<InkCViewModel>> GetAllInk()
        {
            var list = new List<InkCViewModel>();
            list = await _inkRespository.GetAllInk();
            return (list != null) ? list : new List<InkCViewModel>();
        }
        public async Task<List<PurchasingInkViewModel>> GetAllInkPurchasing()
        {
            var list = new List<PurchasingInkViewModel>();
            list = await _inkRespository.GetAllInkPurchasing();
            return (list != null) ? list : new List<PurchasingInkViewModel>();
        }
        public List<PurchasingInkViewModel> GetPurchasingById(string id)
        {
            var list = new List<PurchasingInkViewModel>();
            list = _inkRespository.GetPurchasingById(id);
            return list;
        }
        public void EditInk(string inkid, string inkname)
        {
            _inkRespository.EditInk(inkid, inkname);
        }
        public void DeleteInk(string inkid)
        {
            _inkRespository.DeleteInk(inkid);
        }
        public void PushInStockInk(string PurchaseId, decimal Price, string InkId, int InkCout, string ProcessPersonId)
        {
            _inkRespository.PushInStockInk(PurchaseId, Price, InkId, InkCout, ProcessPersonId);
        }
        public void DeleteInkPurchase(string purchaseid)
        {
            _inkRespository.DeleteInkPurchase(purchaseid);
        }
        public void UpdateInk(InkCViewModel ink)
        {
            _inkRespository.UpdateInk(ink);
        }
        public void CreatePurchaseOrder4Ink(PurchasingInkViewModel ink)
        {
            _inkRespository.CreatePurchaseOrder4Ink(ink);
        }
    }
}

