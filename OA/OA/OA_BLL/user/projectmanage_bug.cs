using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    /// <summary>
    /// 项目bug模块
    /// </summary>
    public class projectmanage_bug : BaseUserService,Iprojectmanage_bug
    {
        /// <summary>
        /// 新增项目bug
        /// </summary>
        /// <param name="bug">项目bug实体</param>
        /// <returns></returns>
        public returnresult AddProjectBug(oa_project_projectsbugs bug)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_project_projectsbugs>(a => a.title == bug.title))
            {
                rr.status = false;
                rr.msg = "该BUG已存在";
            }
            else
            {
                rr.status = Add<oa_project_projectsbugs>(bug);
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
        /// 修改项目bug
        /// </summary>
        /// <param name="bug">项目bug实体</param>
        /// <returns></returns>
        public returnresult UpdateProjectBug(oa_project_projectsbugs bug)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_project_projectsbugs>(a => a.title == bug.title && a.id!=bug.id))
            {
                rr.status = false;
                rr.msg = "该BUG已存在";
            }
            else
            {
                rr.status = Update<oa_project_projectsbugs>(bug);
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
        /// 根据项目id和公司id查询项目信息
        /// </summary>
        /// <param name="pid">项目id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_project_projects QueryProject(long pid, long companyid)
        {
            return Query<oa_project_projects>(a=>a.id==pid && a.companyid==companyid);
        }

        /// <summary>
        /// 根据bugid和公司id查询bug信息
        /// </summary>
        /// <param name="id">bugid</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_project_projectsbugs QueryProjectBug(long id,long companyid)
        {
            var pb = Query<oa_project_projectsbugs>(a=>a.id==id);
            if (pb == null)
            {
                return null;
            }
            else
            {
                var pj = Query<oa_project_projects>(a => a.id == pb.projectid);
                if (pj == null)
                {
                    return null;
                }
                else
                {
                    if (pj.companyid == companyid)
                    {
                        return pb;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 项目bug列表
        /// </summary>
        /// <returns></returns>
        public List<view_oa_project_projectbuglist> QueryViewProjectBugList(long pid,long companyid)
        {
            var sss = QueryList<view_oa_project_projectbuglist>(a => a.id>0);
            var ss = QueryList<view_oa_project_projectbuglist>(a => a.companyid == companyid && a.projectid == pid);
            return QueryList<view_oa_project_projectbuglist>(a=>a.companyid==companyid && a.projectid==pid);
        }

        /// <summary>
        /// 根据bugid和公司id查询bug信息
        /// </summary>
        /// <param name="id">bugid</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public view_oa_project_projectbug QueryViewProjectBug(long id, long companyid)
        {
            return Query<view_oa_project_projectbug>(a=>a.id==id && a.companyid==companyid);
        }
    }
}
