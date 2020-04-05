using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 油墨库存视图
    /// </summary>
    public class InkCViewModel
    {
        /// <summary>
        /// 油墨ID
        /// </summary>
        public string InkId { get; set; }
        /// <summary>
        /// 油墨名称
        /// </summary>
        public string InkName { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Ccount { get; set; }
    }
}
