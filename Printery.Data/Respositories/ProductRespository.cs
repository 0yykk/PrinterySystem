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
    public interface IProductRespository
    {
        Task<List<ProductsViewModel>> GetAllProduct();
        void UpdateProduct(ProductsViewModel product);
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
        public void UpdateProduct(ProductsViewModel product)
        {
            var exitPaper = _db.Product.FirstOrDefault(s => s.ProductName == product.ProductName);
            if (exitPaper == null)
            {
                var storeProcedureName = "[dbo].[Add_Product]";
                var Result = _dbContext.Database.SqlQuery<ProductsViewModel>(
                    $"{storeProcedureName} @ProductID,@ProductName,@eachPrice,@Ccount",
                    new SqlParameter("@ProductID", product.ProductID),
                    new SqlParameter("@ProductName", product.ProductName),
                    new SqlParameter("@eachPrice", product.eachPrice),
                    new SqlParameter("@CCount", product.CCOunt)
                    ).SingleOrDefault();
            }
            else
            {
                var storeProcedureName = "[dbo].[Update_Product]";
                var Result = _dbContext.Database.SqlQuery<ProductsViewModel>(
                    $"{storeProcedureName} @ProductID,@ProductName,@eachPrice,@Ccount",
                    new SqlParameter("@ProductID", product.ProductID),
                    new SqlParameter("@ProductName", product.ProductName),
                    new SqlParameter("@eachPrice", product.eachPrice),
                    new SqlParameter("@CCount", product.CCOunt)
                    ).SingleOrDefault();
            }
        }
    }
}
