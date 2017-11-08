using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    /// <summary>
    /// 公告接口
    /// </summary>
    public interface Iadmin_noticemanage_notice
    {
        /// <summary>
        /// 新增公告类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        returnresult AddNoticetype(oa_notice_noticetype type);

        /// <summary>
        /// 修改公告类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        returnresult UpdateNoticetype(oa_notice_noticetype type);

        /// <summary>
        /// 删除公告类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool DeleteNoticetype(oa_notice_noticetype type);

        /// <summary>
        /// 根据类型id和公司id查询公告类型
        /// </summary>
        /// <param name="typeid">类型id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_notice_noticetype QueryNoticetype(long typeid,long companyid);

        /// <summary>
        /// 根据公司id查询公告类型列表
        /// </summary>
        /// <param name="companyid">公告类型id</param>
        /// <returns></returns>
        List<oa_notice_noticetype> QueryNoticetypeList(long companyid);

        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        bool AddNoticecontent(oa_notice_notice nc);

        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        bool UpdateNoticecontent(oa_notice_notice nc);

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        bool DeleteNoticecontent(oa_notice_notice nc);

        /// <summary>
        /// 根据公告id和公司id查询公告信息
        /// </summary>
        /// <param name="id">公告id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_notice_notice QueryNoticeByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据类型id和公司id查询公告列表
        /// </summary>
        /// <param name="typeid">类型id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_notice_notice> QueryNoticeListByTypeidCompanyid(long typeid, long companyid);

        /// <summary>
        /// 新增公告评论
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        bool AddNoticecomment(oa_notice_noticecomment nc);

        /// <summary>
        /// 修改公告评论
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        bool UpdateNoticecomment(oa_notice_noticecomment nc);

        /// <summary>
        /// 删除公告评论
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        bool DeleteNoticecomment(oa_notice_noticecomment nc);

        /// <summary>
        /// 根据公告评论id和公司id查询公告评论信息
        /// </summary>
        /// <param name="id">评论id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_notice_noticecomment QueryNoticeCommentByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据公告id和公司id查询公告评论列表
        /// </summary>
        /// <param name="noticeid">公告id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_notice_noticecomment> QueryNoticeCommentListByIdCompanyid(long noticeid,long companyid);
    }
}
