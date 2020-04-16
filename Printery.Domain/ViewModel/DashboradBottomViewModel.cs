using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    public class DashboradBottomViewModel
    {
        public int Month { get; set; }
        public int OrderCount { get; set; }
        public int OrderWait { get; set; }
        public int OrderDone { get; set; }
        public decimal Business { get; set; }
        public decimal Profit { get; set; }
        public int BackOrder { get; set; }
    }
}
