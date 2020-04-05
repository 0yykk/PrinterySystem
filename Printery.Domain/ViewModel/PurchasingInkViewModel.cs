using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 油墨采购单视图
    /// </summary>
    public class PurchasingInkViewModel
    {
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchasingID { get; set; }
        /// <summary>
        /// 油墨ID
        /// </summary>
        public string InkId { get; set; }
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
