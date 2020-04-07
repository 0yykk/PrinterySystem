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
        Task<List<ProductsViewModel>> GetAllProduct();
    }
    public class ProductProvider:IProductProvider
    {
        private readonly IProductRespository _productRespository;
        public ProductProvider(IProductRespository productRespository)
        {
            _productRespository = productRespository;
        }
        public async Task<List<ProductsViewModel>> GetAllProduct()
        {
            var list = new List<ProductsViewModel>();
            list = await _productRespository.GetAllProduct();
            return (list != null) ? list : new List<ProductsViewModel>();
        }
    }
}
