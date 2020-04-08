using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 产品生产单
    /// </summary>
    public class ProductGoodViewModel
    {
        /// <summary>
        /// 生产单单号
        /// </summary>
        public string PurchasingID { get; set; }
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
        /// 总价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal eachPrice { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreatePersonId { get; set; }
        /// <summary>
        /// 处理人ID
        /// </summary>
        public string ProcessPersonId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime ProcessDate { get; set; }
    }
}
