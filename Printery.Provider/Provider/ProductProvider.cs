using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    /// <summary>
    /// 样品库存资料Provider
    /// </summary>
    public interface IProductProvider
    {
        Task<List<ProductGoodViewModel>> GetAllProductPurchase();
        Task<List<ProductGoodsViewModel>> GetAllProduct();
        Task<List<ProExViewModel>> GetAllProExi(string empid);
        List<ProductGoodViewModel> GetPurchaseById(string purchaseid);
        List<ProductGoodsViewModel> GetProductByProductName(string ProductName);
        ProExViewModel GetProExiByProId(string proexid);
        void EditProduct(ProductGoodsViewModel product);
        void DeleteProduct(string id);
        void ProcessProductGood(string purchaseid, string processpersonid);
        void CreatePurchaseOrder4Produt(ProductGoodViewModel propurchase);
        void UpdateProduct(ProductGoodsViewModel product);
        void AddProExis(addProExiViewModel pro);
        void EditProExi(addProExiViewModel pro);
        void DeleteProExi(string proid);
    }
    public class ProductProvider:IProductProvider
    {
        private readonly IProductRespository _productRespository;
        public ProductProvider(IProductRespository productRespository)
        {
            _productRespository = productRespository;
        }
        public async Task<List<ProductGoodViewModel>> GetAllProductPurchase()
        {
            var list = new List<ProductGoodViewModel>();
            list = await _productRespository.GetAllProductPurchase();
            return list;
        }
        public async Task<List<ProductGoodsViewModel>> GetAllProduct()
        {
            var list = new List<ProductGoodsViewModel>();
            list = await _productRespository.GetAllProduct();
            return (list != null) ? list : new List<ProductGoodsViewModel>();
        }
        public async Task<List<ProExViewModel>> GetAllProExi(string empid)
        {
            var list = new List<ProExViewModel>();
            list = await _productRespository.GetAllProExi(empid);
            return list;
        }
        public List<ProductGoodViewModel> GetPurchaseById(string purchaseid)
        {
            var list = new List<ProductGoodViewModel>();
            list = _productRespository.GetPurchaseById(purchaseid);
            return list;
        }
        public List<ProductGoodsViewModel> GetProductByProductName(string ProductName)
        {
            var list = new List<ProductGoodsViewModel>();
            list = _productRespository.GetProductByProductName(ProductName);
            return list;
        }
        public ProExViewModel GetProExiByProId(string proexid)
        {
            var pro = new ProExViewModel();
            pro = _productRespository.GetProExiByProId(proexid);
            return pro;
        }
        public void EditProduct(ProductGoodsViewModel product)
        {
            _productRespository.EditProduct(product);
        }
        public void DeleteProduct(string id)
        {
            _productRespository.DeleteProduct(id);
        }
        public void ProcessProductGood(string purchaseid, string processpersonid)
        {
            _productRespository.ProcessProductGood(purchaseid, processpersonid);
        }
        public void CreatePurchaseOrder4Produt(ProductGoodViewModel propurchase)
        {
            _productRespository.CreatePurchaseOrder4Produt(propurchase);
        }
        public void UpdateProduct(ProductGoodsViewModel product)
        {
            _productRespository.UpdateProduct(product);
        }
        public void AddProExis(addProExiViewModel pro)
        {
            _productRespository.AddProExis(pro);
        }
        public void EditProExi(addProExiViewModel pro)
        {
            _productRespository.EditProExi(pro);
        }
        public void DeleteProExi(string proid)
        {
            _productRespository.DeleteProExi(proid);
        }
    }
}
