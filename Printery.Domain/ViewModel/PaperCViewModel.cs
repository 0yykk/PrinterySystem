using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 纸品库存
    /// </summary>
    public class PaperCViewModel
    {
        /// <summary>
        /// 纸品ID
        /// </summary>
        public string PaperId { get; set; }
        /// <summary>
        /// 纸品名称
        /// </summary>
        public string PaperName { get; set; }
        /// <summary>
        /// 纸品库存数
        /// </summary>
        public string Ccount { get; set; }
    }
}
