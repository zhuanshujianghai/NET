using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    /// <summary>
    /// 项目模块
    /// </summary>
    public class admin_projectmanage_project : BaseAdminService, Iadmin_projectmanage_project
    {
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="pj">项目表实体</param>
        /// <returns></returns>
        public returnresult AddProject(oa_project_projects pj)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_project_projects>(a => a.projectname == pj.projectname))
            {
                rr.status = false;
                rr.msg = "项目已存在";
            }
            else
            {
                rr.status = Add<oa_project_projects>(pj);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="pj">项目表实体</param>
        /// <returns></returns>
        public returnresult UpdateProject(oa_project_projects pj)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_project_projects>(a => a.projectname == pj.projectname && a.id != pj.id))
            {
                rr.status = false;
                rr.msg = "项目已存在";
            }
            else
            {
                rr.status = Update<oa_project_projects>(pj);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="pj">项目表实体</param>
        /// <returns></returns>
        public returnresult DeleteProject(oa_project_projects pj)
        {
            returnresult rr = new returnresult();
            if (pj.status == 0)
            {
                rr.status = Delete<oa_project_projects>(pj);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            else
            {
                rr.status = false;
                rr.msg = "只能删除新建项目";
            }
            return rr;
        }

        /// <summary>
        /// 根据项目id和公司id查询项目信息
        /// </summary>
        /// <param name="pid">项目id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_project_projects QueryProjectById(long pid, long companyid)
        {
            return Query<oa_project_projects>(a=>a.id==pid && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询项目列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_project_projects> QueryProjectList(long companyid)
        {
            return QueryList<oa_project_projects>(a=>a.companyid==companyid && a.status!=-1);
        }

        /// <summary>
        /// 新增项目人员
        /// </summary>
        /// <param name="ps">项目人员表实体</param>
        /// <returns></returns>
        public bool AddProjectstaff(oa_project_projectsstaffs ps)
        {
            return Add<oa_project_projectsstaffs>(ps);
        }

        /// <summary>
        /// 修改项目人员
        /// </summary>
        /// <param name="ps">项目人员表实体</param>
        /// <returns></returns>
        public bool UpdateProjectstaff(oa_project_projectsstaffs ps)
        {
            return Update<oa_project_projectsstaffs>(ps);
        }

        /// <summary>
        /// 删除项目人员
        /// </summary>
        /// <param name="ps">项目人员表实体</param>
        /// <returns></returns>
        public bool DeleteProjectstaff(oa_project_projectsstaffs ps)
        {
            return Delete<oa_project_projectsstaffs>(ps);
        }

        /// <summary>
        /// 根据项目人员id和公司id查询项目人员信息
        /// </summary>
        /// <param name="id">项目人员id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_project_projectsstaffs QueryProjectStaffByIdCompanyid(long id, long companyid)
        {
            return Query<oa_project_projectsstaffs>(a=>a.id==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询项目员工列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_project_projectstafflist> QueryViewProjectStaffList(long pid,long companyid)
        {
            return QueryList<view_oa_project_projectstafflist>(a=>a.companyid==companyid && a.projectid ==pid && a.status!=-1);
        }
    }
}
