using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("Paper")]
    public class Paper
    {
        /// <summary>
        /// 纸品ID
        /// </summary>
        [Key]
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
