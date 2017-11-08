using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.user
{
    /// <summary>
    /// 绩效明细接口
    /// </summary>
    public interface Iachievementmanage_achievement
    {
        /// <summary>
        /// 新增绩效明细
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        bool AddAchievementsinfo(oa_achievements_achievementsinfo ai);

        /// <summary>
        /// 新增绩效明细_批量
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        bool AddAchievementsinfo(List<oa_achievements_achievementsinfo> lstai);

        /// <summary>
        /// 根据公司id查询员工绩效列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_achievements_staffachievementlist> QueryStaffAchievementList(long companyid);

        /// <summary>
        /// 根据公司id查询绩效明细列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_achievements_achievementinfolist> QueryAchievementInfoList(long companyid);

        /// <summary>
        /// 新增绩效制度
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        returnresult AddInstitution(oa_achievements_institution it);

        /// <summary>
        /// 修改绩效制度
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        returnresult UpdateInstitution(oa_achievements_institution it);

        /// <summary>
        /// 删除绩效制度
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        bool DeleteInstitution(oa_achievements_institution it);

        /// <summary>
        /// 根据制度id和公司id查询制度信息
        /// </summary>
        /// <param name="id">制度id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_achievements_institution QueryInstitution(long id, long companyid);

        /// <summary>
        /// 根据公司id查询绩效制度列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_achievements_institution> QueryInstitutionList(long companyid);

        /// <summary>
        /// 根据员工id和公司id查询员工绩效信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_achievements_achievementsinfo QueryAchievementInfoByStaffid(long staffid, long companyid);
    }
}
