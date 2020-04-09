using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("PowerControlList")]
    public class PowerControlList
    {
        [Key]
        public string ControlId { get; set; }
        [ForeignKey("PowerList")]
        public string PowerId { get; set; }
        public PowerList PowerList { get; set; }
        [ForeignKey("EmpGroup")]
        public string GroupId { get; set; }
        public EmpGroup EmpGroup { get; set; }
    }
}
