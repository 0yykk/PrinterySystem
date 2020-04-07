using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("InkStock")]
    public class InkStock
    {

        /// <summary>
        /// 油墨ID
        /// </summary>
        [Key]
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
