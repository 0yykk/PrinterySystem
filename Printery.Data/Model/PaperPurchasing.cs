using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Printery.Data.Model
{
    [Table("PaperPurchasing")]
    public class PaperPurchasing
    {
        /// <summary>
        /// 纸品采购单编号
        /// </summary>
        [Key]
        public string PurchasingID { get; set; }
        /// <summary>
        /// 纸品ID
        /// </summary>
        [ForeignKey("Paper")]
        public string PaperId { get; set; }
        public Paper Paper { get; set; }
        /// <summary>
        /// 纸品名称
        /// </summary>
        public string PaperName { get; set; }
        /// <summary>
        /// 纸品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public decimal Price { get; set; }
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
