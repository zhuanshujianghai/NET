using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;
using OA_DAL;
using OA_IDAL;
using System.Data.SqlClient;

namespace OA_IBLL
{
    public interface IBaseService
    {
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns>16位加密结果</returns>
        string MD5_16(string str);

        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns>32位加密结果</returns>
        string MD5_32(string str);

        /// <summary>
        /// 获取当前IP
        /// </summary>
        /// <returns>ip</returns>
        string getip();
    }
}
