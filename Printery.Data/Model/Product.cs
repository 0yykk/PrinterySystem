using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("Products")]
    public class Product
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [Key]
        public string ProductID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal eachPrice { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int CCOunt { get; set; }
    }
}
