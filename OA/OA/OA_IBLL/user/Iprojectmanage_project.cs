using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.user
{
    /// <summary>
    /// 项目接口
    /// </summary>
    public interface Iprojectmanage_project
    {
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="pj">项目表实体</param>
        /// <returns></returns>
        returnresult AddProject(oa_project_projects pj);

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="pj">项目表实体</param>
        /// <returns></returns>
        returnresult UpdateProject(oa_project_projects pj);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="pj">项目表实体</param>
        /// <returns></returns>
        returnresult DeleteProject(oa_project_projects pj);

        /// <summary>
        /// 根据项目id和公司id查询项目信息
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        oa_project_projects QueryProjectById(long pid, long companyid);

        /// <summary>
        /// 根据公司id查询项目列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_project_projects> QueryProjectList(long companyid);

        /// <summary>
        /// 新增项目人员
        /// </summary>
        /// <param name="ps">项目人员表实体</param>
        /// <returns></returns>
        bool AddProjectstaff(oa_project_projectsstaffs ps);

        /// <summary>
        /// 修改项目人员
        /// </summary>
        /// <param name="ps">项目人员表实体</param>
        /// <returns></returns>
        bool UpdateProjectstaff(oa_project_projectsstaffs ps);

        /// <summary>
        /// 删除项目人员
        /// </summary>
        /// <param name="ps">项目人员表实体</param>
        /// <returns></returns>
        bool DeleteProjectstaff(oa_project_projectsstaffs ps);

        /// <summary>
        /// 根据项目人员id和公司id查询项目人员信息
        /// </summary>
        /// <param name="id">项目人员id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_project_projectsstaffs QueryProjectStaffByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据项目id和公司id查询项目员工列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_project_projectstafflist> QueryViewProjectStaffList(long pid, long companyid);
    }
}
