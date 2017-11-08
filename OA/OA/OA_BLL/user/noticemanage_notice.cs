using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    /// <summary>
    /// 公告类
    /// </summary>
    public class noticemanage_notice : BaseUserService, Inoticemanage_notice
    {
        /// <summary>
        /// 新增公告类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public returnresult AddNoticetype(oa_notice_noticetype type)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_notice_noticetype>(a => a.name == type.name && a.companyid == type.companyid))
            {
                rr.status = false;
                rr.msg = "公告类型已存在";
            }
            else
            {
                rr.status = Add<oa_notice_noticetype>(type);
                if (rr.status)
                {
                    rr.msg = "公告类型新增成功";
                }
                else
                {
                    rr.msg = "公告类型新增失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改公告类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public returnresult UpdateNoticetype(oa_notice_noticetype type)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_notice_noticetype>(a => a.name == type.name && a.companyid == type.companyid && a.id != type.id))
            {
                rr.status = false;
                rr.msg = "公告类型已存在";
            }
            else
            {
                rr.status = Update<oa_notice_noticetype>(type);
                if (rr.status)
                {
                    rr.msg = "公告类型修改成功";
                }
                else
                {
                    rr.msg = "公告类型修改失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除公告类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool DeleteNoticetype(oa_notice_noticetype type)
        {
            type.status = -1;
            return Update<oa_notice_noticetype>(type);
        }

        /// <summary>
        /// 根据类型id和公司id查询公告类型
        /// </summary>
        /// <param name="typeid">类型id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_notice_noticetype QueryNoticetype(long typeid, long companyid)
        {
            return Query<oa_notice_noticetype>(a=>a.id==typeid && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询公告类型列表
        /// </summary>
        /// <param name="companyid">公告类型id</param>
        /// <returns></returns>
        public List<oa_notice_noticetype> QueryNoticetypeList(long companyid)
        {
            return QueryList<oa_notice_noticetype>(a=>a.companyid==companyid).Where(a=>a.status!=-1).ToList();
        }

        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public bool AddNoticecontent(oa_notice_notice nc)
        {
            return Add<oa_notice_notice>(nc);
        }

        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public bool UpdateNoticecontent(oa_notice_notice nc)
        {
            return Update<oa_notice_notice>(nc);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public bool DeleteNoticecontent(oa_notice_notice nc)
        {
            nc.status = -1;
            return Update<oa_notice_notice>(nc);
        }

        /// <summary>
        /// 根据公告id和公司id查询公告信息
        /// </summary>
        /// <param name="id">公告id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_notice_notice QueryNoticeByIdCompanyid(long id, long companyid)
        {
            return Query<oa_notice_notice>(a=>a.id==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据类型id和公司id查询公告列表
        /// </summary>
        /// <param name="typeid">类型id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_notice_notice> QueryNoticeListByTypeidCompanyid(long typeid, long companyid)
        { 
            List<view_oa_notice_notice> lstview = new List<view_oa_notice_notice>();
            //查询所有
            if (typeid == 0)
            {
                lstview = QueryList<view_oa_notice_notice>(a => a.companyid == companyid); 
            }
            else//查询该类型下公告
            {
                lstview = QueryList<view_oa_notice_notice>(a => a.typeid == typeid && a.companyid == companyid);
            }
            return lstview;
        }

        /// <summary>
        /// 新增公告评论
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public bool AddNoticecomment(oa_notice_noticecomment nc)
        {
            return Add<oa_notice_noticecomment>(nc);
        }

        /// <summary>
        /// 修改公告评论
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public bool UpdateNoticecomment(oa_notice_noticecomment nc)
        {
            return Update<oa_notice_noticecomment>(nc);
        }

        /// <summary>
        /// 删除公告评论
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public bool DeleteNoticecomment(oa_notice_noticecomment nc)
        {
            nc.status = -1;
            return Update<oa_notice_noticecomment>(nc);
        }

        /// <summary>
        /// 根据公告评论id和公司id查询公告评论信息
        /// </summary>
        /// <param name="id">评论id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_notice_noticecomment QueryNoticeCommentByIdCompanyid(long id, long companyid)
        {
            return Query<oa_notice_noticecomment>(a=>a.id==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公告id和公司id查询公告评论列表
        /// </summary>
        /// <param name="noticeid">公告id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_notice_noticecomment> QueryNoticeCommentListByIdCompanyid(long noticeid, long companyid)
        {
            return QueryList<view_oa_notice_noticecomment>(a=>a.noticeid==noticeid && a.companyid ==companyid);
        }
    }
}
