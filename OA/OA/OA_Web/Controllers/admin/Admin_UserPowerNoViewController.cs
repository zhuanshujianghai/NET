using OA_BLL.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_Models;

namespace OA_Web.Controllers.admin
{
    public class Admin_UserPowerNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryPageMenuList(DataTableParameter param)
        {
            var me = getauser();
            long companyid = Request.QueryString["companyid"].ToLong();
            string pagemenuname = Request.QueryString["pagemenuname"];
            admin_userpowermanage_pagemenu up = new admin_userpowermanage_pagemenu();
            var lstpm = up.QueryPagemenuList(companyid);
            if (pagemenuname != null)
            {
                lstpm = lstpm.Where(a=>a.name.Contains(pagemenuname)).ToList();
            }
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
        /// 查询菜单列表-下拉列表（顶级菜单，本系统只有两级菜单）
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPageMenuList_Select()
        {
            var me = getauser();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_userpowermanage_pagemenu up = new admin_userpowermanage_pagemenu();
            var lstpm = up.QueryPagemenuList(0).Where(a=>a.parentid==0).ToList();
            return Json(lstpm,JsonRequestBehavior.DenyGet);
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
            int ispage = Request.Form["ispage"].ToInt();
            long parentid = Request.Form["parentid"].ToLong();
            string url = Request.Form["url"];
            string remark = Request.Form["remark"];
            var me = getauser();
            admin_userpowermanage_pagemenu up = new admin_userpowermanage_pagemenu();
            var menu = up.QueryPagemenuById(menuid);
            //新增
            if (menu == null)
            {
                menu = new oa_userpower_pagemenu();
                menu.companyid = 0;
                menu.image = "";
                menu.ispage = ispage;
                menu.name = name;
                menu.parentid = parentid;
                menu.remark = remark;
                menu.url = url;
                rr = up.AddPagemenu(menu);
            }
            else//修改
            {
                menu.name = name;
                menu.remark = remark;
                menu.ispage = ispage;
                menu.parentid = parentid;
                menu.url = url;
                rr = up.UpdatePagemenu(menu);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletePageMenu()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            var me = getauser();
            admin_userpowermanage_pagemenu up = new admin_userpowermanage_pagemenu();
            var pm = up.QueryPagemenuById(id);
            if (pm == null)
            {
                rr.status = false;
                rr.msg = "未找到该菜单";
            }
            else
            {
                rr.status = up.DeletePagemenu(pm);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 用户权限操作日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryUserPowerLogList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_userpowermanage_powerlog up = new admin_userpowermanage_powerlog();
            var lstpl = up.QueryPowerlogList(companyid);
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
            long pid = Request.QueryString["pid"].ToLong();
            admin_userpowermanage_function up = new admin_userpowermanage_function();
            var lstft = up.QueryFunctionList().Where(a=>a.parentid==pid).ToList();
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
            long pid = Request.Form["pid"].ToLong();
            string functionname = Request.Form["functionname"];
            string functioninfo = Request.Form["functioninfo"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            var func = uf.QueryFunctionById(id);
            if (func == null)
            {
                func = new oa_userpower_functions();
                func.functioninfo = functioninfo;
                func.functionname = functionname;
                func.parentid = pid;
                rr = uf.AddFunction(func);
            }
            else
            {
                func.functionname = functionname;
                func.functioninfo = functioninfo;
                func.parentid = pid;
                rr = uf.UpdateFunction(func);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteFunction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            var ft = uf.QueryFunctionById(id);
            if (ft == null)
            {
                rr.status = false;
                rr.msg = "未找到该功能";
            }
            else
            {
                rr = uf.DeleteFunction(ft);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑职位的功能列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult EditFunctionList(DataTableParameter param)
        {
            long positionid = Request.QueryString["pid"].ToLong();
            string functionname = Request.QueryString["functionname"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_userpowermanage_function up = new admin_userpowermanage_function();
            var lstac = up.QueryFunctionListByPositionid(positionid);
            if (functionname != "" && functionname != null)
            {
                lstac = lstac.Where(a => a.functionname.Contains(functionname)).ToList();
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
        /// ACTION列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryActionList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            string actionname = Request.QueryString["actionname"];
            admin_userpowermanage_function up = new admin_userpowermanage_function();
            var lstac = up.QueryActionList();
            if (actionname != null)
            {
                lstac = lstac.Where(a=>a.actionname.Contains(actionname)).ToList();
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
        /// 编辑ACTION
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string actionname = Request.Form["actionname"];
            string actioninfo = Request.Form["actioninfo"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            var act = uf.QueryActionById(id);
            if (act == null)
            {
                act = new oa_userpower_actions();
                act.actionname = actionname;
                act.actioninfo = actioninfo;
                rr = uf.AddAction(act);
            }
            else
            {
                act.actionname = actionname;
                act.actioninfo = actioninfo;
                rr = uf.UpdateAction(act);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除ACTION
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteAction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            var act = uf.QueryActionById(id);
            if (act == null)
            {
                rr.status = false;
                rr.msg = "未找到该ACTION";
            }
            else
            {
                rr = uf.DeleteAction(act);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 重新读取程序集中的ACTION
        /// </summary>
        /// <returns></returns>
        public ActionResult ReadAllAction()
        {
            returnresult rr = new returnresult();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            List<oa_userpower_actions> lsta = new List<oa_userpower_actions>();
            int count = 0;
            //获取数据库action
            var action = uf.QueryActionList();
            //获取当前程序集的所有action
            List<string> lststr = Common.GetALLUserPageByReflection();
            //如果数据库的action条数为0，则进行第一次添加，直接添加全部，如果不是第一次添加，则添加两者之间的差距
            if (action.Count == 0)
            {
                foreach (var item in lststr)
                {
                    oa_userpower_actions ac = new oa_userpower_actions();
                    ac.actionname = item;
                    ac.actioninfo = "";
                    lsta.Add(ac);
                }
                rr = uf.AddActionList(lsta);
                count = rr.status ? lsta.Count : 0;
            }
            else
            {
                //不是第一次添加，先判断两个结合的数量是否不同
                if (action.Count != lststr.Count)
                {
                    //先求两个集合的差集
                    List<string> lstacstr = new List<string>();
                    List<oa_userpower_actions> lstac = uf.QueryActionList();
                    foreach (var item in lstac)
                    {
                        lstacstr.Add(item.actionname);
                    }
                    List<string> lstaction = lststr.Except(lstacstr).ToList();

                    //将差集添加进数据库
                    foreach (var item in lstaction)
                    {
                        oa_userpower_actions ac = new oa_userpower_actions();
                        ac.actionname = item;
                        ac.actioninfo = "";
                        lsta.Add(ac);
                    }
                    rr = uf.AddActionList(lsta);
                    count = rr.status ? lstaction.Count : 0;
                }
                else
                {
                    count = 0;
                }
            }
            try
            {
                return Json(count, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(0, JsonRequestBehavior.DenyGet);
            }
        }

        /// <summary>
        /// 编辑功能的ACTION列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult EditActionList(DataTableParameter param)
        {
            long functionid = Request.QueryString["fid"].ToLong();
            string actionname = Request.QueryString["actionname"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_userpowermanage_function up = new admin_userpowermanage_function();
            var lstac = up.QueryFunctionActionListByFunctionid(functionid);
            if (actionname != "" && actionname != null)
            {
                lstac = lstac.Where(a => a.actionname.Contains(actionname)).ToList();
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
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
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