using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    public interface Iadmin_approvalprocessmanage_process
    {
        /// <summary>
        /// 新增流程
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        returnresult AddProcess(oa_approvalprocess_process pc);

        /// <summary>
        /// 修改流程
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        returnresult UpdateProcess(oa_approvalprocess_process pc);

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        bool DeleteProcess(oa_approvalprocess_process pc);

        /// <summary>
        /// 根据公司id查询审批流程列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        List<oa_approvalprocess_process> QueryProcessList(long companyid);

        /// <summary>
        /// 根据id和公司id查询审批流程信息
        /// </summary>
        /// <param name="id">流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_process QueryProcessByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 新增流程审批人
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        returnresult AddApprovalperson(oa_approvalprocess_approvalpersons ap);

        /// <summary>
        /// 修改流程审批人
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        returnresult UpdateApprovalperson(oa_approvalprocess_approvalpersons ap);

        /// <summary>
        /// 删除流程审批人
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        bool DeleteApprovalperson(oa_approvalprocess_approvalpersons ap,long companyid);

        /// <summary>
        /// 根据流程id和公司id查询审批人列表
        /// </summary>
        /// <param name="processid">流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_approvalprocess_processperson> QueryProcessPersonList(long processid, long companyid);

        /// <summary>
        /// 根据员工id和公司id查询审批人信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_approvalpersons QueryProcessPersonByStaffid(long staffid, long companyid, long processid);

        /// <summary>
        /// 根据审批人id和公司id查询审批人信息
        /// </summary>
        /// <param name="id">审批人id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_approvalpersons QueryProcessPerson(long personid, long companyid);

        /// <summary>
        /// 根据流程id查询流程审批人列表
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        List<oa_approvalprocess_approvalpersons> QueryProcessPersonById(long processid);

        /// <summary>
        /// 根据排序和流程id和公司id查询审批人信息
        /// </summary>
        /// <param name="sort">排序</param>
        /// <param name="processid">流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_approvalprocess_approvalpersons QueryProcessPerson(int sort, long processid, long companyid);
    }
}
