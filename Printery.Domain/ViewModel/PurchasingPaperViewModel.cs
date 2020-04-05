using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 纸品采购单视图
    /// </summary>
    public class PurchasingPaperViewModel
    {
        /// <summary>
        /// 纸品采购单编号
        /// </summary>
        public string PurchasingID { get; set; }
        /// <summary>
        /// 纸品ID
        /// </summary>
        public string PaperId { get; set; }
        /// <summary>
        /// 纸品名称
        /// </summary>
        public string PaperName { get; set; }
        /// <summary>
        /// 纸品数量
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// 总价格
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
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime ProcessDate { get; set; }
    }
}
