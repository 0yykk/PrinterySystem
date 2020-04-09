using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("EmpGroup")]
    public class EmpGroup
    {
        /// <summary>
        /// 用户组Id
        /// </summary>
        [Key]
        public string GroupId { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Tip { get; set; }
    }
}
