﻿using OA_BLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA_Web.Controllers.admin
{
    public class Admin_BaseController : MyBaseController
    {
        /// <summary>
        /// action拦截器
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            string url = ("/admin/" + controllerName + "/" + actionName).ToLower();
            string power = getuserpower();
            if (!url.Contains("/login"))
            {
                if (power == "-1")
                {
                    filterContext.Result = new RedirectResult("/admin_power/login");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 获取当前用户权限
        /// </summary>
        /// <returns></returns>
        public string getuserpower()
        {
            var me = getauser();
            if (me == null)
            {
                return "-1";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前管理员信息
        /// </summary>
        /// <returns></returns>
        public oa_power_ausers getauser()
        {
            oa_power_ausers auser = new oa_power_ausers();
            HttpCookie hcookie = Request.Cookies["adminlogincookie"];
            if (hcookie == null)
                auser = null;
            else
            {
                admin_powermanage_auser pa = new admin_powermanage_auser();
                long staffid = pa.checkadminlogincookie(hcookie.Value);
                if (staffid < 1)
                    auser = null;
                else
                {
                    auser = pa.QueryAuserById(staffid);
                    pa.refreshadminlogintime(staffid);
                }
            }
            return auser;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">文件存放目录</param>
        /// <param name="pathfile">文件</param>
        /// <returns></returns>
        public bool UploadFile(string path, HttpPostedFileBase pathfile)
        {
            #region 上传文件
            try
            {
                //获取上传目录 转换为物理路径
                string uploadPath = Server.MapPath(path);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                //保存文件的物理路径
                string saveFile = uploadPath + pathfile.FileName;
                //保存图片到服务器
                pathfile.SaveAs(saveFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// 获取左侧菜单列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string getleftdiv(string url)
        {
            var me = getauser();
            if (me == null)
            {
                return "请先登录";
            }
            using (oadbEntities db = new oadbEntities())
            {
                List<oa_power_pagemenu> lstap = db.oa_power_pagemenu.Where(a => a.status == 0 && a.ispage == 0).ToList();
                var lstap2 = db.oa_power_pagemenu.Where(a => a.status == 0).ToList();
                string html = "";
                for (var i = 0; i < lstap.Count; i++)
                {
                    html += "<li class=''><a href='javascript:;'><span class='title'>" + lstap[i].name + "</span><span class='arrow'></span></a>";
                    html += "<ul class='sub-menu'>";
                    for (var j = 0; j < lstap2.Count; j++)
                    {
                        if (lstap2[j].parentid == lstap[i].id)
                        {
                            if (lstap2[j].url.ToLower().Replace("admin/", "adminnew/") == url.ToLower())
                            {
                                html += "<li class='active'><a href='" + lstap2[j].url.ToLower().Replace("admin/", "adminnew/") + "'>" + lstap2[j].name + "</a></li>";
                                html = html.Replace("<li class=''><a href='javascript:;'><span class='title'>" + lstap[i].name + "</span>", "<li class='active'><a href='javascript:;'><span class='title'>" + lstap[i].name + "</span>");
                            }
                            else
                            {
                                html += "<li><a href='" + lstap2[j].url.ToLower().Replace("admin/", "adminnew/") + "'>" + lstap2[j].name + "</a></li>";
                            }
                        }
                    }
                    html += "</ul></li>";
                }
                return html;
            }
        }
        /// <summary>
        /// 获取顶部通用
        /// </summary>
        /// <returns></returns>
        public string gettopdiv()
        {
            var me = getauser();
            string html = string.Format("<div class='navbar-inner'><div class='container-fluid'><a class='brand' href='/adminnew'><img src='/Content/style/img/adminlogo.png' style='margin-left:20px;'></a><a href='javascript:;' class='btn-navbar collapsed' data-toggle='collapse' data-target='.nav-collapse'>菜单</a><ul class='nav pull-right'><li class='dropdown user'><a href='javascript:;' class='dropdown-toggle' data-toggle='dropdown'><span class='username'>{0}</span><i class='icon-angle-down'></i></a><ul class='dropdown-menu'><li><a href='/adminnew/common/updateauserpassword'><i class='icon-lock'></i>修改密码</a></li><li><a href='/adminnew/commonnoview/adminoutlogin'><i class='icon-key'></i>退出登录</a></li></ul></li></ul></div></div>", me.username);
            return html;
        }

        /// <summary>
        /// datatables接收参数实体
        /// </summary>
        public class DataTableParameter
        {
            /// <summary>
            /// DataTable请求服务器端次数
            /// </summary> 
            public string sEcho { get; set; }

            /// <summary>
            /// 过滤文本
            /// </summary>
            public string sSearch { get; set; }

            /// <summary>
            /// 每页显示的数量
            /// </summary>
            public int iDisplayLength { get; set; }

            /// <summary>
            /// 分页时每页跨度数量
            /// </summary>
            public int iDisplayStart { get; set; }

            /// <summary>
            /// 列数
            /// </summary>
            public int iColumns { get; set; }

            /// <summary>
            /// 排序列的数量
            /// </summary>
            public int iSortingCols { get; set; }

            /// <summary>
            /// 逗号分割所有的列
            /// </summary>
            public string sColumns { get; set; }
        }
	}
}