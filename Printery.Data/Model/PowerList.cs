using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("PowerControl")]
    public class PowerList
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        [Key]
        public string PowerId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PowerName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Tip { get; set; }
    }
}
