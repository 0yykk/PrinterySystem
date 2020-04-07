using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        /// <summary>
        /// 详单号
        /// </summary>
        [Key]
        public string CartID { get; set; }
        /// <summary>
        /// 对应订单号
        /// </summary>
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
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
