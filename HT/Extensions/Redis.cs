using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HT
{
    public class Redis
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }
        /// <summary>
        /// 默认数据库
        /// </summary>
        public int DefaultDB { get; set; }
    }
}
