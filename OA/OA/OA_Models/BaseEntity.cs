using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Models
{
    /// <summary>
    /// 登陆实体
    /// </summary>
    public class logintype
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 口令
        /// </summary>
        public string userword { get; set; }
    }

    /// <summary>
    /// 注册实体
    /// </summary>
    public class registertype
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string phone { get; set; }
    }

    /// <summary>
    /// 返回结果实体
    /// </summary>
    public class returnresult
    {
        /// <summary>
        /// 状态（true、false）
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }
}
