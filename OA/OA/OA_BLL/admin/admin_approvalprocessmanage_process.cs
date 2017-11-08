using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_approvalprocessmanage_process : BaseAdminService, Iadmin_approvalprocessmanage_process
    {
        /// <summary>
        /// 新增流程
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public returnresult AddProcess(oa_approvalprocess_process pc)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_approvalprocess_process>(a => a.companyid == pc.companyid && a.processname == pc.processname))
            {
                rr.status = false;
                rr.msg = "流程已存在";
            }
            else
            {
                rr.status = Add<oa_approvalprocess_process>(pc);
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
        /// 修改流程
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public returnresult UpdateProcess(oa_approvalprocess_process pc)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_approvalprocess_process>(a => a.companyid == pc.companyid && a.processname == pc.processname && a.id!=pc.id))
            {
                rr.status = false;
                rr.msg = "流程已存在";
            }
            else
            {
                rr.status = Update<oa_approvalprocess_process>(pc);
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
        /// 删除流程
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public bool DeleteProcess(oa_approvalprocess_process pc)
        {
            //将状态改为删除状态
            pc.status = -1;
            return Update<oa_approvalprocess_process>(pc); ;
        }
        /// <summary>
        /// 根据公司id查询审批流程列表
        /// </summary>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public List<oa_approvalprocess_process> QueryProcessList(long companyid)
        {
            return QueryList<oa_approvalprocess_process>(a=>a.companyid==companyid).Where(a=>a.status!=-1).ToList();
        }

        /// <summary>
        /// 根据id和公司id查询审批流程信息
        /// </summary>
        /// <param name="id">流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_approvalprocess_process QueryProcessByIdCompanyid(long id, long companyid)
        {
            return Query<oa_approvalprocess_process>(a => a.id == id && a.companyid == companyid);
        }

        /// <summary>
        /// 新增流程审批人
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public returnresult AddApprovalperson(oa_approvalprocess_approvalpersons ap)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_approvalprocess_approvalpersons>(a => a.processid==ap.processid && a.staffid == ap.staffid))
            {
                rr.status = false;
                rr.msg = "该审批人已存在";
            }
            else
            {
                rr.status = Add<oa_approvalprocess_approvalpersons>(ap);
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
        /// 修改流程审批人
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public returnresult UpdateApprovalperson(oa_approvalprocess_approvalpersons ap)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_approvalprocess_approvalpersons>(a => a.processid == ap.processid && a.staffid == ap.staffid && a.id!=ap.id))
            {
                rr.status = false;
                rr.msg = "该审批人已存在";
            }
            else
            {
                rr.status = Update<oa_approvalprocess_approvalpersons>(ap);
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
        /// 删除流程审批人
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public bool DeleteApprovalperson(oa_approvalprocess_approvalpersons ap,long companyid)
        {
            //将状态改为删除状态
            ap.status = -1;
            bool bool1 = Update<oa_approvalprocess_approvalpersons>(ap);
            var lstpp = QueryProcessPersonById(ap.processid).Where(a=>a.sort>ap.sort).ToList();
            foreach (var item in lstpp)
            {
                item.sort -= 1;
            }
            if (bool1)
            {
                var bool2 = UpdateIQueryable<oa_approvalprocess_approvalpersons>(lstpp);
                if (bool2)
                {
                    return true;
                }
                else
                {
                    ap.status = 0;
                    Update<oa_approvalprocess_approvalpersons>(ap);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据流程id和公司id查询审批人列表
        /// </summary>
        /// <param name="processid">流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_approvalprocess_processperson> QueryProcessPersonList(long processid, long companyid)
        {
            return QueryList<view_oa_approvalprocess_processperson>(a=>a.processid==processid && a.companyid==companyid).OrderBy(a=>a.sort).ToList();
        }
        /// <summary>
        /// 根据审批人id和公司id查询审批人信息
        /// </summary>
        /// <param name="id">审批人id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_approvalprocess_approvalpersons QueryProcessPerson(long personid,long companyid)
        {
            var view = Query<view_oa_approvalprocess_processperson>(a=>a.id==personid && a.companyid==companyid);
            if (view == null)
            {
                return null;
            }
            else
            {
                return Query<oa_approvalprocess_approvalpersons>(a => a.id == view.id);
            }
        }
        /// <summary>
        /// 根据员工id和公司id和流程id查询审批人信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <param name="companyid">公司id</param>
        /// <param name="processid">流程id</param>
        /// <returns></returns>
        public oa_approvalprocess_approvalpersons QueryProcessPersonByStaffid(long staffid, long companyid,long processid)
        {
            var view = Query<view_oa_approvalprocess_processperson>(a => a.staffid == staffid && a.companyid == companyid && a.processid == processid);
            if (view == null)
            {
                return null;
            }
            else
            {
                return Query<oa_approvalprocess_approvalpersons>(a => a.id == view.id);
            }
        }

        /// <summary>
        /// 根据排序和流程id和公司id查询审批人信息
        /// </summary>
        /// <param name="sort">排序</param>
        /// <param name="processid">流程id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_approvalprocess_approvalpersons QueryProcessPerson(int sort, long processid, long companyid)
        {
           var view = Query<view_oa_approvalprocess_processperson>(a=>a.processid==processid && a.companyid==companyid && a.sort==sort);
           if (view == null)
           {
               return null;
           }
           else
           {
               return Query<oa_approvalprocess_approvalpersons>(a => a.id == view.id);
           }
        }

        /// <summary>
        /// 根据流程id查询流程审批人列表
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        public List<oa_approvalprocess_approvalpersons> QueryProcessPersonById(long processid)
        {
            return QueryList<oa_approvalprocess_approvalpersons>(a=>a.processid==processid);
        }
    }
}
