using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Printery.Data.Model
{
    [Table("ProductGoods")]
    public class ProductGoods
    {
        /// <summary>
        /// 生产单单号
        /// </summary>
        [Key]
        public string PurchasingID { get; set; }
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
        public string Count { get; set; }
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
