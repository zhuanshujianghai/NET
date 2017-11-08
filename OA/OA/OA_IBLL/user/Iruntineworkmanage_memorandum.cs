using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.user
{
    public interface Iruntineworkmanage_memorandum
    {
        /// <summary>
        /// 新增备忘录
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        bool Addmemoreandum(oa_runtinework_memorandums md);

        /// <summary>
        /// 修改备忘录
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        bool Updatememoreandum(oa_runtinework_memorandums md);

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        bool Deletememoreandum(oa_runtinework_memorandums md);

        /// <summary>
        /// 根据备忘录id和公司id查询备忘录信息
        /// </summary>
        /// <param name="memorandumid">备忘录id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_runtinework_memorandums QueryMemorandum(long memorandumid, long companyid);

        /// <summary>
        /// 根据员工id和公司id查询备忘录列表
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <param name="compnayid">公司id</param>
        /// <returns></returns>
        List<oa_runtinework_memorandums> QueryMemorandumList(long staffid, long compnayid);
    }
}
