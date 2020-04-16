using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    public class ProExViewModel
    {
        public string ProExId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string InkName1 { get; set; }
        public int InkId1Count { get; set; }
        public string PaperName1 { get; set; }
        public int PaperId1Count { get; set; }
        public string InkName2 { get; set; }
        public int InkId2Count { get; set; }
        public string PaperName2 { get; set; }
        public int PaperId2Count { get; set; }
    }
}
