using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.admin;
using OA_Models;

namespace OA_Web.Controllers.admin
{
    public class Admin_AchievementNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 绩效制度列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryInstitutionList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var lstap = aa.QueryInstitutionList(companyid);
            int total = lstap.Count;
            lstap = lstap.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstap
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询绩效列表-下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryInstitutionList_Select()
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var lstap = aa.QueryInstitutionList(companyid);
            return Json(lstap, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑绩效制度
        /// </summary>
        /// <returns></returns>
        public ActionResult EditInstitution()
        {
            returnresult rr = new returnresult();
            decimal impactsocre = Request.Form["impactscore"].ToDecimal();
            string title = Request.Form["title"];
            string content = Request.Form["content"];
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var it = aa.QueryInstitution(id, companyid);
            DateTime dt = DateTime.Now;
            if (it == null)
            {
                it = new oa_achievements_institution();
                it.addtime = dt;
                it.companyid = companyid;
                it.content = content;
                it.impactscore = impactsocre;
                it.sort = 0;
                it.staffid = 0;
                it.title = title;
                it.updatetime = dt;
                it.status = 0;
                rr = aa.AddInstitution(it);
            }
            else
            {
                it.updatetime = dt;
                it.impactscore = impactsocre;
                it.title = title;
                it.content = content;
                rr = aa.UpdateInstitution(it);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除绩效制度
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteInstitution()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var it = aa.QueryInstitution(id, companyid);
            if (it == null)
            {
                rr.status = false;
                rr.msg = "未找到该绩效制度";
            }
            else
            {
                rr.status = aa.DeleteInstitution(it);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 员工绩效列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStaffAchievementList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var lstap = aa.QueryStaffAchievementList(companyid);
            int total = lstap.Count;
            lstap = lstap.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstap
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 员工绩效明细
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAchievementInfoList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var lstap = aa.QueryAchievementInfoList(companyid);
            int total = lstap.Count;
            lstap = lstap.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstap
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增绩效明细
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAchievementInfo()
        {
            returnresult rr = new returnresult();
            long staffid = Request.Form["staffid"].ToLong();
            long institutionid = Request.Form["institutionid"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var sf = ms.QueryStaffByIdCompanyid(staffid, companyid);
            if (sf == null)
            {
                rr.status = false;
                rr.msg = "未找到该员工";
            }
            else
            {
                admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
                var it = aa.QueryInstitution(institutionid, companyid);
                if (it == null)
                {
                    rr.status = false;
                    rr.msg = "未找到该制度";
                }
                else
                {
                    oa_achievements_achievementsinfo ai = new oa_achievements_achievementsinfo();
                    ai.addtime = DateTime.Now;
                    ai.explain = "";
                    var aiai = aa.QueryAchievementInfoByStaffid(staffid, companyid);
                    ai.impactafterscore = aiai == null ? it.impactscore : aiai.impactafterscore + it.impactscore;
                    ai.impactscore = it.impactscore;
                    ai.institutionid = institutionid;
                    ai.staffid = staffid;
                    rr.status = aa.AddAchievementsinfo(ai);
                    if (rr.status)
                    {
                        rr.msg = "新增成功";
                    }
                    else
                    {
                        rr.msg = "新增失败";
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 初始化绩效
        /// </summary>
        /// <returns></returns>
        public ActionResult InitializeAchievement()
        {
            returnresult rr = new returnresult();
            int allscore = 100;
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var lsta = aa.QueryStaffAchievementList(companyid);
            List<oa_achievements_achievementsinfo> lstai = new List<oa_achievements_achievementsinfo>();
            foreach (var item in lsta)
            {
                oa_achievements_achievementsinfo ai = new oa_achievements_achievementsinfo();
                ai.addtime = DateTime.Now;
                ai.explain = "初始化绩效分数";
                ai.impactafterscore = allscore;
                var aiai = aa.QueryAchievementInfoByStaffid(item.id, companyid);
                ai.impactscore = aiai == null ? allscore : allscore - aiai.impactafterscore;
                ai.institutionid = 0;
                ai.staffid = item.id;
                lstai.Add(ai);
            }
            rr.status = aa.AddAchievementsinfo(lstai);
            if (rr.status)
            {
                rr.msg = "初始化成功";
            }
            else
            {
                rr.msg = "初始化失败";
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
	}
}