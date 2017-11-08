using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    public interface Iadmin_projectmanage_bug
    {
        /// <summary>
        /// 新增项目bug
        /// </summary>
        /// <param name="bug">项目bug实体</param>
        /// <returns></returns>
        returnresult AddProjectBug(oa_project_projectsbugs bug);

        /// <summary>
        /// 修改项目bug
        /// </summary>
        /// <param name="bug">项目bug实体</param>
        /// <returns></returns>
        returnresult UpdateProjectBug(oa_project_projectsbugs bug);

        /// <summary>
        /// 项目bug列表
        /// </summary>
        /// <returns></returns>
        List<view_oa_project_projectbuglist> QueryViewProjectBugList(long pid,long companyid);

        /// <summary>
        /// 根据bugid和公司id查询bug信息
        /// </summary>
        /// <param name="id">bugid</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        view_oa_project_projectbug QueryViewProjectBug(long id,long companyid);
    }
}
