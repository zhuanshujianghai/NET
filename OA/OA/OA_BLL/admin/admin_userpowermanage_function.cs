using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_userpowermanage_function : BaseAdminService, Iadmin_userpowermanage_function
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public returnresult AddFunction(oa_userpower_functions ft)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_functions>(a => a.functionname == ft.functionname))
            {
                rr.status = false;
                rr.msg = "功能名称已存在";
            }
            else
            {
                rr.status = Add<oa_userpower_functions>(ft);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                    /**************新增角色功能***************/
                    List<oa_userpower_positionsfunctions> lstrf = new List<oa_userpower_positionsfunctions>();
                    var lstrl = QueryList<oa_member_positions>(a => a.id > 0);
                    foreach (var item in lstrl)
                    {
                        oa_userpower_positionsfunctions rf = new oa_userpower_positionsfunctions();
                        rf.functionid = ft.id;
                        rf.positionid = item.id;
                        rf.status = 0;
                        lstrf.Add(rf);
                    }
                    rr.status = AddIQueryable<oa_userpower_positionsfunctions>(lstrf);
                    if (rr.status)
                    {
                        rr.msg += ",新增职位功能成功";
                    }
                    else
                    {
                        rr.msg += ",新增职位功能失败";
                    }
                    /**************新增角色功能***************/

                    /**************新增功能ACTION***************/
                    List<oa_userpower_functionsactions> lstfa = new List<oa_userpower_functionsactions>();
                    var lstat = QueryList<oa_userpower_actions>(a => a.id > 0);
                    foreach (var item in lstat)
                    {
                        oa_userpower_functionsactions fa = new oa_userpower_functionsactions();
                        fa.functionid = ft.id;
                        fa.actionid = item.id;
                        fa.status = 0;
                        lstfa.Add(fa);
                    }
                    rr.status = AddIQueryable<oa_userpower_functionsactions>(lstfa);
                    if (rr.status)
                    {
                        rr.msg += ",新增功能ACTION成功";
                    }
                    else
                    {
                        rr.msg += ",新增功能ACTION失败";
                    }
                    /**************新增功能ACTION***************/
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return rr;
        }
        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult UpdateFunction(oa_userpower_functions pm)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_functions>(a => a.functionname == pm.functionname && a.id!=pm.id))
            {
                rr.status = false;
                rr.msg = "功能名称已存在";
            }
            else
            {
                rr.status = Update<oa_userpower_functions>(pm);
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
        /// 删除功能
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public returnresult DeleteFunction(oa_userpower_functions pt)
        {
            returnresult rr = new returnresult();
            rr.status = Delete<oa_userpower_functions>(pt);
            if (rr.status)
            {
                rr.msg = "删除成功";
                var pf = QueryList<oa_userpower_positionsfunctions>(a => a.functionid == pt.id);
                rr.status = DeleteIQueryable<oa_userpower_positionsfunctions>(pf);
                if (rr.status)
                {
                    rr.msg += "删除职位功能成功";
                }
                else
                {
                    rr.msg += "删除职位功能失败";
                }
                

                var fa = QueryList<oa_userpower_functionsactions>(a => a.functionid == pt.id);
                rr.status = DeleteIQueryable<oa_userpower_functionsactions>(fa);
                if (rr.status)
                {
                    rr.msg += "删除功能ACTION成功";
                }
                else
                {
                    rr.msg += "删除功能ACTION失败";
                }
            }
            else
            {
                rr.msg = "删除失败";
            }
            return rr;
        }

        /// <summary>
        /// 根据功能id查询功能信息
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public oa_userpower_functions QueryFunctionById(long functionid)
        {
            return Query<oa_userpower_functions>(a=>a.id==functionid);
        }

        /// <summary>
        /// 根据公司id查询功能列表
        /// </summary>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionList()
        {
            return QueryList<oa_userpower_functions>(a=>a.id>0);
        }
        /// <summary>
        /// 新增ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult AddAction(oa_userpower_actions at)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_actions>(a => a.actionname == at.actionname && a.id != at.id))
            {
                rr.status = false;
                rr.msg = "ACTION名称已存在";
            }
            else
            {
                rr.status = Add<oa_userpower_actions>(at);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                    List<oa_userpower_functionsactions> lstfa = new List<oa_userpower_functionsactions>();
                    var lstfu = QueryList<oa_power_functions>(a => a.id > 0);
                    foreach (var item in lstfu)
                    {
                        oa_userpower_functionsactions fa = new oa_userpower_functionsactions();
                        fa.functionid = item.id;
                        fa.actionid = at.id;
                        fa.status = 0;
                        lstfa.Add(fa);
                    }
                    rr.status = AddIQueryable<oa_userpower_functionsactions>(lstfa);
                    if (rr.status)
                    {
                        rr.msg += ",新增功能ACTION成功";
                    }
                    else
                    {
                        rr.msg += ",新增角色ACTION失败";
                    }
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 批量新增action
        /// </summary>
        /// <param name="lstat"></param>
        /// <returns></returns>
        public returnresult AddActionList(List<oa_userpower_actions> lstat)
        {
            returnresult rr = new returnresult();
            rr.status = AddIQueryable<oa_userpower_actions>(lstat);
            if (rr.status)
            {
                List<oa_userpower_functionsactions> lstfa = new List<oa_userpower_functionsactions>();
                rr.msg = "批量新增ACTION成功";
                foreach (var at in lstat)
                {
                    var lstfu = QueryList<oa_userpower_functions>(a => a.id > 0);
                    foreach (var item in lstfu)
                    {
                        oa_userpower_functionsactions fa = new oa_userpower_functionsactions();
                        fa.functionid = item.id;
                        fa.actionid = at.id;
                        fa.status = 0;
                        lstfa.Add(fa);
                    }
                }
                rr.status = AddIQueryable<oa_userpower_functionsactions>(lstfa);
                if (rr.status)
                {
                    rr.msg += ",新增功能ACTION成功";
                }
                else
                {
                    rr.msg += ",新增角色ACTION失败";
                }
            }
            else
            {
                rr.msg = "批量新增ACTION失败";
            }
            return rr;
        }

        /// <summary>
        /// 修改ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult UpdateAction(oa_userpower_actions at)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_actions>(a => a.actionname == at.actionname && a.id != at.id))
            {
                rr.status = false;
                rr.msg = "ACTION名称已存在";
            }
            else
            {
                rr.status = Update<oa_userpower_actions>(at);
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

        public returnresult DeleteAction(oa_userpower_actions at)
        {
            returnresult rr = new returnresult();
            rr.status = Delete<oa_userpower_actions>(at);
            if (rr.status)
            {
                rr.msg = "删除ACTION成功";

                var fa = QueryList<oa_userpower_functionsactions>(a=>a.actionid==at.id);
                rr.status = DeleteIQueryable<oa_userpower_functionsactions>(fa);
                if (rr.status)
                {
                    rr.msg += "删除功能ACTION成功";
                }
                else
                {
                    rr.msg += "删除功能ACTION失败";
                }
            }
            else
            {
                rr.msg = "删除ACTION失败";
            }
            return rr;
        }

        /// <summary>
        /// 根据actionid查询action信息
        /// </summary>
        /// <param name="functionid">actionid</param>
        /// <returns></returns>
        public oa_userpower_actions QueryActionById(long actionid)
        {
            return Query<oa_userpower_actions>(a => a.id == actionid);
        }

        /// <summary>
        /// 查询action列表
        /// </summary>
        /// <returns></returns>
        public List<oa_userpower_actions> QueryActionList()
        {
            return QueryList<oa_userpower_actions>(a => a.id>0);
        }

        /// <summary>
        /// 新增职位功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool AddPositionFunction(oa_userpower_positionsfunctions pf)
        {
            return Add<oa_userpower_positionsfunctions>(pf);
        }

        /// <summary>
        /// 修改职位功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool UpdatePositionFunction(oa_userpower_positionsfunctions at)
        {
            return Update<oa_userpower_positionsfunctions>(at);
        }

        /// <summary>
        /// 根据id查询职位功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public oa_userpower_positionsfunctions QueryPositionFunction(long id)
        {
            return Query<oa_userpower_positionsfunctions>(a=>a.id==id);
        }

        /// <summary>
        /// 根据功能id查询功能action列表
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public List<view_oa_userpower_functionactionlist> QueryFunctionActionListByFunctionid(long functionid)
        {
            return QueryList<view_oa_userpower_functionactionlist>(a=>a.functionid==functionid);
        }

        /// <summary>
        /// 根据功能id查询拥有该功能的职位列表
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public List<oa_member_positions> QueryPositionListByFunctionid(long functionid)
        {
            var lstpf = QueryList<oa_userpower_positionsfunctions>(a=>a.functionid==functionid);
            List<oa_member_positions> lstpt = new List<oa_member_positions>();
            foreach (var item in lstpf)
            {
                lstpt.Add(Query<oa_member_positions>(a=>a.id==item.positionid));
            }
            return lstpt;
        }
        /// <summary>
        /// 根据职位id查询该职位的功能列表
        /// </summary>
        /// <param name="positionid"></param>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionListByPositionid(long positionid)
        {
            var lstpf = QueryList<oa_userpower_positionsfunctions>(a => a.positionid == positionid);
            List<oa_userpower_functions> lstft = new List<oa_userpower_functions>();
            foreach (var item in lstpf)
            {
                lstft.Add(Query<oa_userpower_functions>(a => a.id == item.functionid));
            }
            return lstft;
        }


        /// <summary>
        /// 新增功能ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool AddFunctionAction(oa_userpower_functionsactions fa)
        {
            return Add<oa_userpower_functionsactions>(fa);
        }

        /// <summary>
        /// 修改功能ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool UpdateFunctionAction(oa_userpower_functionsactions fa)
        {
            return Update<oa_userpower_functionsactions>(fa);
        }

        /// <summary>
        /// 根据id查询功能ACTION信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public oa_userpower_functionsactions QueryFunctionAction(long id)
        {
            return Query<oa_userpower_functionsactions>(a => a.id == id);
        }
        /// <summary>
        /// 根据功能id查询该功能的Action
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public List<oa_userpower_actions> QueryActionListByFunctionid(long functionid)
        {
            var lstfa = QueryList<oa_userpower_functionsactions>(a => a.functionid == functionid);
            List<oa_userpower_actions> lstpt = new List<oa_userpower_actions>();
            foreach (var item in lstfa)
            {
                lstpt.Add(Query<oa_userpower_actions>(a => a.id == item.actionid));
            }
            return lstpt;
        }
        /// <summary>
        /// 根据actionid查询拥有该action的功能列表
        /// </summary>
        /// <param name="positionid">actionid</param>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionListByActionid(long actionid)
        {
            var lstfa = QueryList<oa_userpower_functionsactions>(a => a.actionid == actionid);
            List<oa_userpower_functions> lstft = new List<oa_userpower_functions>();
            foreach (var item in lstfa)
            {
                lstft.Add(Query<oa_userpower_functions>(a => a.id == item.functionid));
            }
            return lstft;
        }
    }
}
