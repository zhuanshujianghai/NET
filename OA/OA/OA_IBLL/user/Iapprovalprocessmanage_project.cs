using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.user
{
    public interface Iapprovalprocessmanage_project
    {
        /// <summary>
        /// 新增审批项目
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        returnresult AddProject(oa_approvalprocess_projects ap);

        /// <summary>
        /// 修改审批项目
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        returnresult UpdateProject(oa_approvalprocess_projects ap);

        /// <summary>
        /// 删除审批项目
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        bool DeleteProject(oa_approvalprocess_projects ap);

        /// <summary>
        /// 根据公司id查询审批项目列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        List<oa_approvalprocess_projects> QueryProjectList(long companyid);

        /// <summary>
        /// 根据审批项目id和公司id查询审批项目信息
        /// </summary>
        /// <param name="id">审批项目id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_projects QueryProjectByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据项目流程id和公司id查询项目流程信息
        /// </summary>
        /// <param name="id">项目流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_projectprocess QueryProjectProcessByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据公司id查询项目流程列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        List<view_oa_approvalprocess_projectprocess> QueryProjectProcessList(long companyid);

        /// <summary>
        /// 根据公司id查询项目流程列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_approvalprocess_projectprocess> QueryProjectProcessList_Select(long companyid);

        /// <summary>
        /// 新增员工申请
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        bool AddStaffapply(oa_approvalprocess_staffapplys sa);

        /// <summary>
        /// 修改员工申请
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        bool UpdateStaffapply(oa_approvalprocess_staffapplys sa);

        /// <summary>
        /// 删除员工申请
        /// </summary>
        /// <param name="sa"></param>
        /// <returns></returns>
        bool DeleteStaffapply(oa_approvalprocess_staffapplys sa);

        /// <summary>
        /// 根据公司id查询员工申请项目流程列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_approvalprocess_staffapplylist> QueryStaffApplyList(long companyid);

         /// <summary>
        /// 根据公司id和员工申请id查询员工申请信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="id">员工申请id</param>
        /// <returns></returns>
        oa_approvalprocess_staffapplys QueryStaffApply(long companyid, long id);

        /// <summary>
        /// 新增员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        bool AddStaffApplyApproval(oa_approvalprocess_staffapplyapprovals saa);

        /// <summary>
        /// 修改员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        bool UpdateStaffApplyApproval(oa_approvalprocess_staffapplyapprovals saa);

        /// <summary>
        /// 删除员工申请审批
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        bool DeleteStaffApplyApproval(oa_approvalprocess_staffapplyapprovals saa);

        /// <summary>
        /// 删除员工申请审批_列表
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        bool DeleteStaffApplyApproval(List<oa_approvalprocess_staffapplyapprovals> saa);

        /// <summary>
        /// 根据公司id和员工申请id查询申请审批列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="staffapplyid">申请id</param>
        /// <returns></returns>
        List<view_oa_approvalprocess_staffapplyapprovallist> QueryStaffApplyApprovalList(long companyid, long staffapplyid);

        /// <summary>
        /// 根据公司id和员工申请审批id查询员工申请审批信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="id">员工申请审批id</param>
        /// <returns></returns>
        oa_approvalprocess_staffapplyapprovals QueryStaffApplyApproval(long companyid, long id);

        /// <summary>
        /// 根据公司id和员工申请id查询员工申请审批信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <param name="id">员工申请id</param>
        /// <returns></returns>
        List<oa_approvalprocess_staffapplyapprovals> QueryStaffApplyApprovalByStaffApplyid(long companyid, long id);

        /// <summary>
        /// 根据审批人id和申请id和公司id查询申请审批信息
        /// </summary>
        /// <param name="approvalpersonid">审批人id</param>
        /// <param name="staffapplyid">申请id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_staffapplyapprovals QueryStaffApplyApprovalByApprovalpersonid(long approvalpersonid, long staffapplyid, long companyid);

        /// <summary>
        /// 根据员工申请id和公司id查询员工申请信息及关联信息
        /// </summary>
        /// <param name="staffapplyid">员工申请id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        view_oa_approvalprocess_staffapplyall QueryStaffApplyAll(long staffapplyid, long companyid);

        /// <summary>
        /// 根据员工申请id和公司id和审批人id查询员工申请信息及关联信息
        /// </summary>
        /// <param name="staffapplyid">员工申请id</param>
        /// <param name="companyid">公司id</param>
        /// <param name="approvalstaffid">审批人id</param>
        /// <returns></returns>
        view_oa_approvalprocess_staffapplyall QueryStaffApplyAll(long staffapplyid, long companyid, long approvalstaffid);
    }
}
