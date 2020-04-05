using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Domain.ViewModel
{
    /// <summary>
    /// 用户信息视图
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public virtual string UserGuid { get; set; }
        /// <summary>
        /// 用户代号
        /// </summary>
        public virtual string UserAccount { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public virtual string Nation { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public virtual string Politic { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime Birthday { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        public virtual string LevelEducation { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string Phonenum { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string Idnum { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public virtual string Married { get; set; }
    }
}
