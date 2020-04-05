using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 产品视图
    /// </summary>
    public class ProductsVIewModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal eachPrice { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int CCOunt { get; set; }
    }
}
