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
        List<ProductGoodViewModel> GetPurchaseById(string purchaseid);
        List<ProductGoodsViewModel> GetProductByProductName(string ProductName);
        void CreatePurchaseOrder4Produt(ProductGoodViewModel propurchase);
        void UpdateProduct(ProductGoodsViewModel product);
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
        public void CreatePurchaseOrder4Produt(ProductGoodViewModel propurchase)
        {
            _productRespository.CreatePurchaseOrder4Produt(propurchase);
        }
        public void UpdateProduct(ProductGoodsViewModel product)
        {
            _productRespository.UpdateProduct(product);
        }
    }
}
