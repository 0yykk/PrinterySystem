using Printery.Data.Contexts;
using Printery.Data.Model;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Respositories
{
    public interface IProductRespository
    {
        Task<List<ProductsViewModel>> GetAllProduct();
    }
    public class ProductRespository:IProductRespository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public ProductRespository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }

        public async Task<List<ProductsViewModel>> GetAllProduct()
        {
            var productList = new List<ProductsViewModel>();
            var productlist = new List<Product>();
            productlist = await _db.Product.ToListAsync();
            foreach (var item in productlist)
            {
                ProductsViewModel product = new ProductsViewModel();
                product.ProductID = item.ProductID;
                product.ProductName = item.ProductName;
                product.eachPrice = item.eachPrice;
                product.CCOunt = item.CCOunt;
                productList.Add(product);
            }
            return productList;
        }
    }
}
