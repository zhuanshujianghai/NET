using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_approvalprocessmanage_project : BaseAdminService, Iadmin_approvalprocessmanage_project
    {
        /// <summary>
        /// 新增审批项目
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public returnresult AddProject(oa_approvalprocess_projects ap)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_approvalprocess_projects>(a => a.projectname == ap.projectname && a.companyid == ap.companyid))
            {
                rr.status = false;
                rr.msg = "审批项目已存在";
            }
            else
            {
                rr.status = Add<oa_approvalprocess_projects>(ap);
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
        /// 修改审批项目
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public returnresult UpdateProject(oa_approvalprocess_projects ap)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_approvalprocess_projects>(a => a.projectname == ap.projectname && a.companyid == ap.companyid && a.id != ap.id))
            {
                rr.status = false;
                rr.msg = "审批项目已存在";
            }
            else
            {
                rr.status = Update<oa_approvalprocess_projects>(ap);
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
        /// 删除审批项目
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public bool DeleteProject(oa_approvalprocess_projects ap)
        {
            //将状态修改为删除状态
            ap.status = -1;
            return Update<oa_approvalprocess_projects>(ap);
        }

        /// <summary>
        /// 根据公司id查询审批项目列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public List<oa_approvalprocess_projects> QueryProjectList(long companyid)
        {
            return QueryList<oa_approvalprocess_projects>(a => a.companyid == companyid).Where(a => a.status != -1).ToList(); ;
        }

        /// <summary>
        /// 根据审批项目id和公司id查询审批项目信息
        /// </summary>
        /// <param name="id">审批项目id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_approvalprocess_projects QueryProjectByIdCompanyid(long id, long companyid)
        {
            return Query<oa_approvalprocess_projects>(a=>a.id==id && a.companyid == companyid);
        }

        /// <summary>
        /// 新增项目流程
        /// </summary>
        /// <param name="pp"></param>
        /// <returns></returns>
        public bool AddProjectProcess(oa_approvalprocess_projectprocess pp)
        {
            return Add<oa_approvalprocess_projectprocess>(pp);
        }
        /// <summary>
        /// 修改项目流程
        /// </summary>
        /// <param name="pp"></param>
        /// <returns></returns>
        public bool UpdateProjectProcess(oa_approvalprocess_projectprocess pp)
        {
            return Update<oa_approvalprocess_projectprocess>(pp);
        }

        /// <summary>
        /// 删除项目流程
        /// </summary>
        /// <param name="pp"></param>
        /// <returns></returns>
        public bool DeleteProjectProcess(oa_approvalprocess_projectprocess pp)
        {
            pp.status = -1;
            return Update<oa_approvalprocess_projectprocess>(pp);
        }

        /// <summary>
        /// 根据项目流程id和公司id查询项目流程信息
        /// </summary>
        /// <param name="id">项目流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_approvalprocess_projectprocess QueryProjectProcessByIdCompanyid(long id, long companyid)
        {
            return Query<oa_approvalprocess_projectprocess>(a=>a.id ==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询项目流程列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public List<view_oa_approvalprocess_projectprocess> QueryProjectProcessList(long companyid)
        {
            return QueryList<view_oa_approvalprocess_projectprocess>(a => a.companyid == companyid).Where(a => a.status != -1).ToList();
        }
        /// <summary>
        /// 根据公司id查询项目流程列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_approvalprocess_projectprocess> QueryProjectProcessList_Select(long companyid)
        {
            return QueryList<oa_approvalprocess_projectprocess>(a => a.companyid == companyid).Where(a => a.status != -1).ToList();
        }

        /// <summary>
        /// 新增员工申请
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        public bool AddStaffapply(oa_approvalprocess_staffapplys sa)
        {
            return Add<oa_approvalprocess_staffapplys>(sa);
        }

        /// <summary>
        /// 修改员工申请
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        public bool UpdateStaffapply(oa_approvalprocess_staffapplys sa)
        {
            return Update<oa_approvalprocess_staffapplys>(sa);
        }

        /// <summary>
        /// 删除员工申请
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        public bool DeleteStaffapply(oa_approvalprocess_staffapplys sa)
        {
            sa.status = -1;
            return Update<oa_approvalprocess_staffapplys>(sa);
        }
        /// <summary>
        /// 根据公司id查询员工申请项目流程列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_approvalprocess_staffapplylist> QueryStaffApplyList(long companyid)
        {
            var lstsa = QueryList<view_oa_approvalprocess_staffapplylist>(a => a.companyid == companyid && a.approvalstatus==0).OrderByDescending(a=>a.addtime).ToList();
            var lstsa1 = QueryList<view_oa_approvalprocess_staffapplylist>(a => a.companyid == companyid && a.approvalstatus != 0).OrderByDescending(a => a.approvaltime).ToList();
            lstsa.AddRange(lstsa1);
            return lstsa;
        }
        /// <summary>
        /// 根据公司id和员工申请id查询员工申请信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="id">员工申请id</param>
        /// <returns></returns>
        public oa_approvalprocess_staffapplys QueryStaffApply(long companyid,long id)
        {
            return Query<oa_approvalprocess_staffapplys>(a=>a.companyid==companyid && a.id==id);
        }

        /// <summary>
        /// 新增员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public bool AddStaffApplyApproval(oa_approvalprocess_staffapplyapprovals saa)
        {
            return Add<oa_approvalprocess_staffapplyapprovals>(saa);
        }

        /// <summary>
        /// 新增员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public bool AddStaffApplyApproval(List<oa_approvalprocess_staffapplyapprovals> saa)
        {
            return AddIQueryable<oa_approvalprocess_staffapplyapprovals>(saa);
        }

        /// <summary>
        /// 修改员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public bool UpdateStaffApplyApproval(oa_approvalprocess_staffapplyapprovals saa)
        {
            return Update<oa_approvalprocess_staffapplyapprovals>(saa);
        }

        /// <summary>
        /// 删除员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public bool DeleteStaffApplyApproval(oa_approvalprocess_staffapplyapprovals saa)
        {
            saa.status = -1;
            return Update<oa_approvalprocess_staffapplyapprovals>(saa);
        }

        /// <summary>
        /// 删除员工申请审批_列表
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public bool DeleteStaffApplyApproval(List<oa_approvalprocess_staffapplyapprovals> saa)
        {
            return DeleteIQueryable<oa_approvalprocess_staffapplyapprovals>(saa);
        }

        /// <summary>
        /// 根据公司id和员工申请审批id查询员工申请审批信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="id">员工申请审批id</param>
        /// <returns></returns>
        public oa_approvalprocess_staffapplyapprovals QueryStaffApplyApproval(long companyid, long id)
        {
            return Query<oa_approvalprocess_staffapplyapprovals>(a=>a.companyid==companyid && a.id==id);
        }

        /// <summary>
        /// 根据公司id和员工申请id查询申请审批列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="staffapplyid">申请id</param>
        /// <returns></returns>
        public List<view_oa_approvalprocess_staffapplyapprovallist> QueryStaffApplyApprovalList(long companyid, long staffapplyid)
        {
            return QueryList<view_oa_approvalprocess_staffapplyapprovallist>(a=>a.companyid==companyid && a.staffapplyid==staffapplyid).OrderBy(a=>a.sort).ToList();
        }

        /// <summary>
        /// 根据公司id和员工申请id查询员工申请审批信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="id">员工申请id</param>
        /// <returns></returns>
        public List<oa_approvalprocess_staffapplyapprovals> QueryStaffApplyApprovalByStaffApplyid(long companyid, long id)
        {
            return QueryList<oa_approvalprocess_staffapplyapprovals>(a => a.companyid == companyid && a.staffapplyid == id);
        }

        /// <summary>
        /// 根据审批人id和申请id和公司id查询申请审批信息
        /// </summary>
        /// <param name="approvalpersonid">审批人id</param>
        /// <param name="staffapplyid">申请id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_approvalprocess_staffapplyapprovals QueryStaffApplyApprovalByApprovalpersonid(long approvalpersonid, long staffapplyid, long companyid)
        {
            return Query<oa_approvalprocess_staffapplyapprovals>(a => a.companyid == companyid && a.staffapplyid == staffapplyid && a.approvalpersonid == approvalpersonid);
        }

        /// <summary>
        /// 根据员工申请id和公司id查询员工申请信息及关联信息
        /// </summary>
        /// <param name="staffapplyid">员工申请id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public view_oa_approvalprocess_staffapplyall QueryStaffApplyAll(long staffapplyid, long companyid)
        {
            return Query<view_oa_approvalprocess_staffapplyall>(a=>a.id==staffapplyid && a.companyid == companyid);
        }

        /// <summary>
        /// 根据员工申请id和公司id和审批人id查询员工申请信息及关联信息
        /// </summary>
        /// <param name="staffapplyid">员工申请id</param>
        /// <param name="companyid">公司id</param>
        /// <param name="approvalstaffid">审批人id</param>
        /// <returns></returns>
        public view_oa_approvalprocess_staffapplyall QueryStaffApplyAll(long staffapplyid, long companyid,long approvalstaffid)
        {
            return Query<view_oa_approvalprocess_staffapplyall>(a => a.id == staffapplyid && a.companyid == companyid && a.approvalstaffid==approvalstaffid);
        }
    }
}
