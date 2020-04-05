using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 订单详细内容视图（购物车）
    /// </summary>
    public class CartListViewModel
    {
        /// <summary>
        /// 详单号
        /// </summary>
        public string CartID { get; set; }
        /// <summary>
        /// 对应订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal eachPrice { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Price { get; set; }
    }
}
