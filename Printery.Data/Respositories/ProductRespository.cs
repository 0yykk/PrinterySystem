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
        Task<List<ProductGoodViewModel>> GetAllProductPurchase();
        Task<List<ProductGoodsViewModel>> GetAllProduct();
        List<ProductGoodViewModel> GetPurchaseById(string purchaseid);
        List<ProductGoodsViewModel> GetProductByProductName(string ProductName);
        void EditProduct(ProductGoodsViewModel product);
        void DeleteProduct(string id);
        void ProcessProductGood(string purchaseid, string processpersonid);
        void CreatePurchaseOrder4Produt(ProductGoodViewModel propurchase);
        void UpdateProduct(ProductGoodsViewModel product);
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
        public async Task<List<ProductGoodViewModel>> GetAllProductPurchase()
        {
            var purchaseList = new List<ProductGoodViewModel>();
            var purchaselist = new List<ProductGoods>();
            purchaselist = await _db.ProductGoods.ToListAsync();
            foreach(var item in purchaselist)
            {
                ProductGoodViewModel pro = new ProductGoodViewModel();
                pro.PurchasingID = item.PurchasingID;
                pro.ProductId = item.ProductId;
                pro.ProductName = item.ProductName;
                pro.Count = item.Count;
                pro.Status = item.Status;
                pro.eachPrice = item.eachPrice;
                pro.CreateDate = item.CreateDate;
                pro.ProcessDate = item.ProcessDate;
                pro.ProcessPersonId = item.ProcessPersonId;
                purchaseList.Add(pro);

            }
            return purchaseList;
        }
        public async Task<List<ProductGoodsViewModel>> GetAllProduct()
        {
            var productList = new List<ProductGoodsViewModel>();
            var productlist = new List<Product>();
            productlist = await _db.Product.ToListAsync();
            foreach (var item in productlist)
            {
                ProductGoodsViewModel product = new ProductGoodsViewModel();
                product.ProductID = item.ProductID;
                product.ProductName = item.ProductName;
                product.eachPrice = item.eachPrice;
                product.CCOunt = item.CCOunt;
                productList.Add(product);
            }
            return productList;
        }
        public List<ProductGoodViewModel> GetPurchaseById(string purchaseid)
        {
            var PurchaseList = new List<ProductGoodViewModel>();
            var purchaselist = from u in _db.ProductGoods
                               where u.PurchasingID == purchaseid
                               select u;
            foreach(var item in purchaselist)
            {
                ProductGoodViewModel pro = new ProductGoodViewModel();
                pro.ProductId = item.ProductId;
                pro.ProductName = item.ProductName;
                pro.eachPrice = item.eachPrice;
                pro.CreatePersonId = item.CreatePersonId;
                pro.ProcessPersonId = item.ProcessPersonId;
                pro.Count = item.Count;
                pro.Status = item.Status;
                PurchaseList.Add(pro);
            }
            return PurchaseList;
        }
        public List<ProductGoodsViewModel> GetProductByProductName(string ProductName)
        {
            var proList = new List<ProductGoodsViewModel>();
            var prolist = from u in _db.Product
                          where u.ProductName == ProductName
                          select u;
            foreach(var item in prolist)
            {
                ProductGoodsViewModel product = new ProductGoodsViewModel();
                product.ProductID = item.ProductID;
                product.ProductName = item.ProductName;
                product.eachPrice = item.eachPrice;
                product.CCOunt = item.CCOunt;
                proList.Add(product);
            }
            return proList;
        }
        public void EditProduct(ProductGoodsViewModel product)
        {
            var db = new PrinteryContext();
            var exitProduct = db.Product.FirstOrDefault(s => s.ProductID == product.ProductID);
            if (exitProduct != null)
            {
                db.Set<Product>().Attach(exitProduct);
                db.Entry(exitProduct).State = EntityState.Modified;
                exitProduct.ProductName = product.ProductName;
                exitProduct.eachPrice = product.eachPrice;
                db.SaveChanges();
            }
        }
        public void DeleteProduct(string id)
        {
            PrinteryContext pr = new PrinteryContext();
            pr.Product.Remove(pr.Product.Where(p => p.ProductID==id).FirstOrDefault());
            pr.SaveChanges();
        }
        public void ProcessProductGood(string purchaseid, string processpersonid)
        {
            var storeProcedureName = "[dbo].[Process_Product_PurchaseOrder]";
            var Result = _dbContext.Database.SqlQuery<ProductGoodViewModel>(
                $"{storeProcedureName} @PurchasingID,@ProcessPersonId",
                new SqlParameter("@PurchasingID",purchaseid),
                new SqlParameter("@ProcessPersonId",processpersonid)
                ).SingleOrDefault();
        }
        public void CreatePurchaseOrder4Produt(ProductGoodViewModel propurchase)
        {
            var storeProcedureName = "[dbo].[Create_Product_PurchaseOrder]";
            var Result = _dbContext.Database.SqlQuery<PurchasingInkViewModel>(
                    $"{storeProcedureName} @PurchasingID,@ProductName,@Count,@Price,@eachPrice,@CreatePersonId,@ProcessPersonId,@Status,@CreateDate,@ProcessDate",
                    new SqlParameter("@PurchasingID", ModelItemIsNow(propurchase.PurchasingID)),
                    new SqlParameter("@ProductName", ModelItemIsNow(propurchase.ProductName)),
                    new SqlParameter("@Count", ModelItemIsNow(propurchase.Count)),
                    new SqlParameter("@Price", ModelItemIsNow(propurchase.Price)),
                    new SqlParameter("@eachPrice",ModelItemIsNow(propurchase.eachPrice)),
                    new SqlParameter("@CreatePersonId", ModelItemIsNow(propurchase.CreatePersonId)),
                    new SqlParameter("@ProcessPersonId", ModelItemIsNow(propurchase.ProcessPersonId)),
                    new SqlParameter("@Status", ModelItemIsNow(propurchase.Status)),
                    new SqlParameter("@CreateDate", ModelItemIsNow(propurchase.CreateDate)),
                    new SqlParameter("@ProcessDate", ModelItemIsNow(propurchase.ProcessDate))
                ).SingleOrDefault();
        }
        public void UpdateProduct(ProductGoodsViewModel product)
        {
            var exitPaper = _db.Product.FirstOrDefault(s => s.ProductName == product.ProductName);
            if (exitPaper == null)
            {
                var storeProcedureName = "[dbo].[Add_Product]";
                var Result = _dbContext.Database.SqlQuery<ProductGoodsViewModel>(
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
                var Result = _dbContext.Database.SqlQuery<ProductGoodsViewModel>(
                    $"{storeProcedureName} @ProductID,@ProductName,@eachPrice,@Ccount",
                    new SqlParameter("@ProductID", product.ProductID),
                    new SqlParameter("@ProductName", product.ProductName),
                    new SqlParameter("@eachPrice", product.eachPrice),
                    new SqlParameter("@CCount", product.CCOunt)
                    ).SingleOrDefault();
            }
        }
        #region 传Null工具
        public object ModelItemIsNow(object str)
        {
            if (str == null || str.ToString().Trim().Length <= 0)
            {
                return DBNull.Value;
            }
            else
            {
                return str;
            }
        }
        #endregion
    }
}
