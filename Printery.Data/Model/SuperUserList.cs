using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("SuperUserList")]
    public class SuperUserList
    {
        [Key]
        [ForeignKey("Employee")]
        public string EmpId { get; set; }
        public Employee Employee { get; set; }
        public string PowerGUIDName { get; set; }
    }
}
