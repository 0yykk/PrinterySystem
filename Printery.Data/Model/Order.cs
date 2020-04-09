using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("Order")]
    public class Order
    {
        [Key]
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime OrderCreate { get; set; }
        /// <summary>
        /// 订单处理时间
        /// </summary>
        public DateTime OrderProcess { get; set; }
        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 交付日期
        /// </summary>
        public DateTime Deadline { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Tips { get; set; }
        /// <summary>
        /// 订单创建人ID
        /// </summary>
        public string CreatePersonId { get; set; }
        /// <summary>
        /// 订单处理人ID
        /// </summary>
        public string ProcessPersonId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 订单客户ID
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 订单客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 订单送货地址
        /// </summary>
        public string Addressed { get; set; }
        /// <summary>
        /// 订单联系人电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
    }
}
