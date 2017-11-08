using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    public class achievementmanage_achievement : BaseUserService, Iachievementmanage_achievement
    {
        /// <summary>
        /// 新增绩效明细
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        public bool AddAchievementsinfo(oa_achievements_achievementsinfo ai)
        {
            return Add<oa_achievements_achievementsinfo>(ai);
        }

        /// <summary>
        /// 新增绩效明细_批量
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        public bool AddAchievementsinfo(List<oa_achievements_achievementsinfo> lstai)
        {
            return AddIQueryable<oa_achievements_achievementsinfo>(lstai);
        }

        /// <summary>
        /// 根据公司id查询员工绩效列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_achievements_staffachievementlist> QueryStaffAchievementList(long companyid)
        {
            return QueryList<view_oa_achievements_staffachievementlist>(a=>a.companyid==companyid).OrderByDescending(a=>a.score).ToList();
        }

        /// <summary>
        /// 根据公司id查询绩效明细列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_achievements_achievementinfolist> QueryAchievementInfoList(long companyid)
        {
            return QueryList<view_oa_achievements_achievementinfolist>(a=>a.companyid==companyid).OrderByDescending(a=>a.addtime).ToList();
        }

        /// <summary>
        /// 新增绩效制度
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public returnresult AddInstitution(oa_achievements_institution it)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_achievements_institution>(a => a.companyid == it.companyid && a.title == it.title))
            {
                rr.status = false;
                rr.msg = "绩效制度已存在";
            }
            else
            {
                rr.status = Add<oa_achievements_institution>(it);
                if (rr.status)
                {
                    rr.msg = "新增绩效制度成功";
                }
                else
                {
                    rr.msg = "新增绩效制度失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改绩效制度
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public returnresult UpdateInstitution(oa_achievements_institution it)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_achievements_institution>(a => a.companyid == it.companyid && a.title == it.title && a.id != it.id))
            {
                rr.status = false;
                rr.msg = "绩效制度已存在";
            }
            else
            {
                rr.status = Update<oa_achievements_institution>(it);
                if (rr.status)
                {
                    rr.msg = "修改绩效制度成功";
                }
                else
                {
                    rr.msg = "修改绩效制度失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除绩效制度
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public bool DeleteInstitution(oa_achievements_institution it)
        {
            it.status = -1;
            return Update<oa_achievements_institution>(it);
        }

        /// <summary>
        /// 根据制度id和公司id查询制度信息
        /// </summary>
        /// <param name="id">制度id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_achievements_institution QueryInstitution(long id, long companyid)
        {
            return Query<oa_achievements_institution>(a => a.id == id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询绩效制度列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_achievements_institution> QueryInstitutionList(long companyid)
        {
            return QueryList<oa_achievements_institution>(a=>a.companyid==companyid);
        }

        /// <summary>
        /// 根据员工id和公司id查询员工绩效信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_achievements_achievementsinfo QueryAchievementInfoByStaffid(long staffid, long companyid)
        {
            membermanage_staff ms = new membermanage_staff();
            var sf = ms.QueryStaffByIdCompanyid(staffid, companyid);
            if (sf == null)
            {
                return null;
            }
            else
            {
                return QueryList<oa_achievements_achievementsinfo>(a => a.staffid == staffid).OrderByDescending(a=>a.addtime).ToList().FirstOrDefault();
            }
        }
    }
}
