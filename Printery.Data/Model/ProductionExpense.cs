using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("ProductionExpense")]
    public class ProductionExpense
    {
        [Key]
        public string ProExId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string InkId1 { get; set; }
        public int? InkId1Count { get; set; }
        public string InkId2 { get; set; }
        public int? InkId2Count { get; set; }
        public string PaperId1 { get; set; }
        public int? PaperId1Count { get; set; }
        public string PaperId2 { get; set; }
        public int? PaperId2Count { get; set; }
    }
}
