using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 用户信息页今日提醒视图
    /// </summary>
    public class TodayFocusViewModel
    {
        public int TodayAddOrder { get; set; }
        public int TodayOrderWait { get; set; }
        public int TodayHaveDone { get; set; }
        public int YouHaveDone { get; set; }
    }
}
