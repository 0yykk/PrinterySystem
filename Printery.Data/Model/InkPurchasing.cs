using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("InkPurchasing")]
    public class InkPurchasing
    {
        /// <summary>
        /// 采购单号
        /// </summary>
        [Key]
        public string PurchasingID { get; set; }
        /// <summary>
        /// 油墨ID
        /// </summary>
        [ForeignKey("InkStock")]
        public string InkId { get; set; }
        public InkStock InkStock { get; set; }
        /// <summary>
        /// 油墨名称
        /// </summary>
        public string InkName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public string Price { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime ProcessDate { get; set; }
    }
}
