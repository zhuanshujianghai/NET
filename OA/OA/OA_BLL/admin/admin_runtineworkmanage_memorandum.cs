using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_runtineworkmanage_memorandum : BaseAdminService, Iadmin_runtineworkmanage_memorandum
    {
        /// <summary>
        /// 新增备忘录
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        public bool Addmemoreandum(oa_runtinework_memorandums md)
        {
            return Add<oa_runtinework_memorandums>(md);
        }

        /// <summary>
        /// 修改备忘录
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        public bool Updatememoreandum(oa_runtinework_memorandums md)
        {
            return Update<oa_runtinework_memorandums>(md);
        }

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        public bool Deletememoreandum(oa_runtinework_memorandums md)
        {
            return Delete<oa_runtinework_memorandums>(md);
        }

        /// <summary>
        /// 根据备忘录id和公司id查询备忘录信息
        /// </summary>
        /// <param name="memorandumid">备忘录id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_runtinework_memorandums QueryMemorandum(long memorandumid, long companyid)
        {
            return Query<oa_runtinework_memorandums>(a=>a.id==memorandumid && a.companyid==companyid);
        }

        /// <summary>
        /// 根据员工id和公司id查询备忘录列表
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <param name="compnayid">公司id</param>
        /// <returns></returns>
        public List<oa_runtinework_memorandums> QueryMemorandumList(long staffid, long compnayid)
        {
            return QueryList<oa_runtinework_memorandums>(a=>a.staffid==staffid && a.companyid == compnayid);
        }
    }
}
