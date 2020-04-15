using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 控制台视图
    /// </summary>
    public class DashboradViewModel
    {
        public decimal TodayPrice { get; set; }
        public int CustomerCount { get; set; }
        public int OrderCount { get; set; }
        public int OrderWaitCount { get; set; }
    }
}
