﻿using Printery.Data.Respositories;
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
        List<PurchasingPaperViewModel> GetPurchasingById(string id);
        List<PaperCViewModel> GetPaperByPapername(string name);
        List<PurchasingPaperViewModel> GetPurchasingByName(string name);
        void EditPaper(string paperid, string papername);
        void DeletePaper(string paperid);
        void PushStockInPaper(decimal Price, string PurchaseId, string paperid, int PaperCount, string ProcessPersonId);
        void DeletePaperPurchase(string purchaseid);
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
        public List<PurchasingPaperViewModel> GetPurchasingById(string id)
        {
            var list = new List<PurchasingPaperViewModel>();
            list = _paperRespository.GetPurchasingById(id);
            return list;
        }
        public List<PaperCViewModel> GetPaperByPapername(string name)
        {
            var list = new List<PaperCViewModel>();
            list = _paperRespository.GetPaperByPapername(name);
            return list;
        }
        public List<PurchasingPaperViewModel> GetPurchasingByName(string name)
        {
            var list = new List<PurchasingPaperViewModel>();
            list = _paperRespository.GetPurchasingByName(name);
            return list;
        }
        public void EditPaper(string paperid, string papername)
        {
            _paperRespository.EditPaper(paperid, papername);
        }
        public void DeletePaper(string paperid)
        {
            _paperRespository.DeletePaper(paperid);
        }
        public void PushStockInPaper(decimal Price, string PurchaseId, string paperid, int PaperCount, string ProcessPersonId)
        {
            _paperRespository.PushStockInPaper(Price,PurchaseId,paperid,PaperCount,ProcessPersonId);
        }
        public void DeletePaperPurchase(string purchaseid)
        {
            _paperRespository.DeletePaperPurchase(purchaseid);
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
