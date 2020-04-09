﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Model
{
    [Table("Customer")]
    public class Customer
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        [Key]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string CAddress { get; set; }
    }
}
