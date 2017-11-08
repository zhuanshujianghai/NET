using OA_BLL.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_Models;

namespace OA_Web.Controllers.user
{
    public class UserPowerNoViewController : BaseController
    {
        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryPageMenuList(DataTableParameter param)
        {
            var me = getme();
            userpowermanage_pagemenu up = new userpowermanage_pagemenu();
            var lstpm = up.QueryPagemenuList(me.companyid);
            int total = lstpm.Count;
            lstpm = lstpm.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstpm
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPageMenu()
        {
            returnresult rr = new returnresult();
            long menuid = Request.Form["menuid"].ToLong();
            string name = Request.Form["name"];
            string remark = Request.Form["remark"];
            var me = getme();
            userpowermanage_pagemenu up = new userpowermanage_pagemenu();
            var menu = up.QueryPageMenuByIdCompanyid(menuid, me.companyid);
            if (menu == null)
            {
                rr.status = false;
                rr.msg = "未找到该菜单";
            }
            else
            {
                menu.name = name;
                menu.remark = remark;
                rr = up.UpdatePagemenu(menu);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 用户权限操作日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryUserPowerLogList(DataTableParameter param)
        {
            var me = getme();
            userpowermanage_powerlog up = new userpowermanage_powerlog();
            var lstpl = up.QueryPowerlogList(me.companyid);
            int total = lstpl.Count;
            lstpl = lstpl.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstpl
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryFunctionList(DataTableParameter param)
        {
            var me = getme();
            userpowermanage_function up = new userpowermanage_function();
            var lstft = up.QueryFunctionList();
            int total = lstft.Count;
            lstft = lstft.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstft
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string functionname = Request.Form["functionname"];
            string functioninfo = Request.Form["functioninfo"];
            var me = getme();
            userpowermanage_function uf = new userpowermanage_function();
            var func = uf.QueryFunctionByIdCompanyid(id);
            if (func == null)
            {
                rr.status = false;
                rr.msg = "未找到该功能";
            }
            else
            {
                func.functionname = functionname;
                func.functioninfo = functioninfo;
                rr = uf.UpdateFunction(func);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 职位功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPositionFunctionList()
        {
            long positionid = Request.Form["positionid"].ToLong();
            userpowermanage_function uf = new userpowermanage_function();
            var lstpf = uf.QueryViewPositionFunctionListByPositionid(positionid);
            return Json(new
            {
                bbData = lstpf.Where(a => a.parentid != 0).ToList(),
                aaData = lstpf.Where(a => a.parentid == 0).ToList()
            }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 编辑职位功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPositionFunction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long positionid = Request.Form["positionid"].ToLong();
            userpowermanage_function uf = new userpowermanage_function();
            //全选和反选
            if (positionid > 0)
            {
                var lstrf = uf.QueryPositionFunctionListByPositionid(positionid);
                var temp = lstrf.Where(a => a.status == 0).ToList().Count > 0 ? 1 : 0;
                foreach (var item in lstrf)
                {
                    if (item.status != -1)
                    {
                        item.status = temp;
                    }
                }
                rr.status = uf.UpdatePositionFunctionList(lstrf);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            else
            {
                var rf = uf.QueryPositionFunction(id);
                if (rf == null)
                {
                    rr.status = false;
                    rr.msg = "未找到该选项";
                }
                else
                {
                    var fun = uf.QueryFunctionById(rf.functionid);
                    //勾选顶级功能（则修改其所属功能）
                    if (fun.parentid == 0)
                    {
                        List<oa_userpower_positionsfunctions> lstrf = new List<oa_userpower_positionsfunctions>();
                        var lstfun = uf.QueryFunctionByPid(id);
                        lstfun.Add(fun);
                        foreach (var item in lstfun)
                        {
                            rf = uf.QueryPositionFunctionByPositionIdFunctionId(rf.positionid, item.id);
                            lstrf.Add(rf);
                        }
                        var temp = lstrf.Where(a => a.status == 0).ToList().Count > 0 ? 1 : 0;
                        foreach (var item in lstrf)
                        {
                            if (item.status != -1)
                            {
                                item.status = temp;
                            }
                        }
                        rr.status = uf.UpdatePositionFunctionList(lstrf);
                        if (rr.status)
                        {
                            rr.msg = "修改成功";
                        }
                        else
                        {
                            rr.msg = "修改失败";
                        }
                    }
                    else//修改单个
                    {
                        List<oa_userpower_positionsfunctions> lstrf = new List<oa_userpower_positionsfunctions>();
                        if (rf.status == -1)
                        {
                            rr.status = false;
                            rr.msg = "修改失败，该功能未授权";
                        }
                        else
                        {
                            rf.status = rf.status == 0 ? 1 : 0;
                            lstrf.Add(rf);
                            if (rf.status == 0)
                            {
                                //获取父级功能，并修改其选中状态
                                var temprf = uf.QueryPositionFunctionByPositionIdFunctionId(rf.positionid, fun.parentid);
                                temprf.status = rf.status;
                                lstrf.Add(temprf);
                            }
                            else
                            {
                                List<oa_userpower_positionsfunctions> lstrf1 = new List<oa_userpower_positionsfunctions>();
                                var lstfun = uf.QueryFunctionByPid(fun.parentid);
                                foreach (var item in lstfun)
                                {
                                    rf = uf.QueryPositionFunctionByPositionIdFunctionId(rf.positionid, item.id);
                                    lstrf1.Add(rf);
                                }
                                var temp = lstrf1.Where(a => a.status == 0 && a.id != rf.id).ToList().Count > 0 ? 0 : 1;
                                //获取父级功能，并修改其选中状态
                                var temprf = uf.QueryPositionFunctionByPositionIdFunctionId(rf.positionid, fun.parentid);
                                temprf.status = temp;
                                lstrf.Add(temprf);
                            }
                            rr.status = uf.UpdatePositionFunctionList(lstrf);
                            if (rr.status)
                            {
                                rr.msg = "修改成功";
                            }
                            else
                            {
                                rr.msg = "修改失败";
                            }
                        }
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// ACTION列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryActionList(DataTableParameter param)
        {
            var me = getme();
            userpowermanage_function up = new userpowermanage_function();
            var lstac = up.QueryActionList(me.companyid);
            int total = lstac.Count;
            lstac = lstac.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstac
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑ACTION
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string actionname = Request.Form["actionname"];
            string actioninfo = Request.Form["actioninfo"];
            var me = getme();
            userpowermanage_function uf = new userpowermanage_function();
            var act = uf.QueryActionByIdCompanyid(id);
            if (act == null)
            {
                rr.status = false;
                rr.msg = "未找到该ACTION";
            }
            else
            {
                act.actionname = actionname;
                act.actioninfo = actioninfo;
                rr = uf.UpdateAction(act);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 功能ACTION列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult EditActionList(DataTableParameter param)
        {
            long functionid = Request.QueryString["fid"].ToLong();
            string actionname = Request.QueryString["actionname"];
            var me = getme();
            userpowermanage_function up = new userpowermanage_function();
            var lstac = up.QueryActionListByFunctionid(functionid);
            if (actionname != "" && actionname != null)
            {
                lstac = lstac.Where(a=>a.actionname.ToLower().Contains(actionname)).ToList();
            }
            int total = lstac.Count;
            lstac = lstac.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstac
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改功能action
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunctionAction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            userpowermanage_function uf = new userpowermanage_function();
            var fa = uf.QueryFunctionAction(id);
            if (fa == null)
            {
                rr.status = false;
                rr.msg = "未找到该功能action";
            }
            else
            {
                fa.status = fa.status == 0 ? 1 : 0;
                rr.status = uf.UpdateFunctionAction(fa);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
	}
}