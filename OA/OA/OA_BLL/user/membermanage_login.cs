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
    /// 登陆类
    /// </summary>
    public class membermanage_login : BaseUserService,Imembermanage_login
    {
        #region 登陆
        /// <summary>
        /// 登陆（用户名、邮箱、手机三合一）
        /// </summary>
        /// <param name="lg">登陆实体</param>
        /// <returns>员工表实体</returns>
        public oa_member_staffs loginusername(logintype lg)
        {
            Int64 usernameint = 0;
            try
            {
                usernameint = Convert.ToInt64(lg.username);
            }
            catch (Exception)
            {
                usernameint = 0;
            }
            if (lg.username.IndexOf("@") > 0)
            {
                return Query<oa_member_staffs>(a => a.username == lg.username && a.password == lg.password);
            }
            else if (usernameint > 0)
            {
                return Query<oa_member_staffs>(a => a.username == lg.username && a.password == lg.password);
            }
            else
            {
                return Query<oa_member_staffs>(a => a.username == lg.username && a.password == lg.password);
            }
        }

        /// <summary>
        /// 登陆（有口令，用户名、邮箱、手机三合一）
        /// </summary>
        /// <param name="lg">登陆实体</param>
        /// <returns>员工表实体</returns>
        public oa_member_staffs loginusernameuserword(logintype lg)
        {
            Int64 usernameint = 0;
            try
            {
                usernameint = Convert.ToInt64(lg.username);
            }
            catch (Exception)
            {
                usernameint = 0;
            }
            if (lg.username.IndexOf("@") > 0)
            {
                return Query<oa_member_staffs>(a => a.username == lg.username && a.password == lg.password && a.userword == lg.userword);
            }
            else if (usernameint > 0)
            {
                return Query<oa_member_staffs>(a => a.username == lg.username && a.password == lg.password && a.userword == lg.userword);
            }
            else
            {
                return Query<oa_member_staffs>(a => a.username == lg.username && a.password == lg.password && a.userword == lg.userword);
            }
        }

        /// <summary>
        /// 登陆存入cookie
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>布尔值</returns>
        public returnresult logincookie(oa_member_staffs sf)
        {
            DateTime dt = DateTime.Now;
            returnresult rr = new returnresult();
            string key = "mykey";
            string cookie = MD5_32(sf.username+key+DateTime.Now.ToString("yyyyMMDDHHmmss"));
            oa_member_staffslogins sfs = new oa_member_staffslogins();
            sfs.addip = getip();
            sfs.addtime = dt;
            sfs.dotime = dt;
            sfs.cookie = cookie;
            sfs.expiretime = 30;
            sfs.staffid = sf.id;
            if (Exist<oa_member_staffslogins>(a => a.staffid == sf.id))
            {
                sfs = Query<oa_member_staffslogins>(a => a.staffid == sf.id);
                sfs.addtime = dt;
                sfs.dotime = dt;
                sfs.cookie = cookie;
                rr.status = Update<oa_member_staffslogins>(sfs);
            }
            else
            {
                rr.status = Add<oa_member_staffslogins>(sfs);
            }
            if (rr.status)
            {
                //写入操作日志
                rr.status = writedolog("进行登陆", "login", sf.id, dt);
                if (rr.status)
                {
                    rr.msg = cookie;
                }
                else
                {
                    rr.msg = "写入操作日志失败";
                    sfs.cookie += "写入操作日志失败";
                    Update<oa_member_staffslogins>(sfs);
                }
            }
            else
            {
                rr.msg = "COOKIE写入失败";
            }
            return rr;
        }

        /// <summary>
        /// 根据cookie字符串检查用户登录情况
        /// </summary>
        /// <param name="cookie">cookie字符串</param>
        /// <returns>登录人id</returns>
        public long checklogincookie(string cookie)
        {
            long result = 0;
            oa_member_staffslogins sl = Query<oa_member_staffslogins>(a => a.cookie == cookie);
            if (sl == null)
            {
                result = 0;
            }
            else
            {
                if (sl.dotime.AddMinutes(sl.expiretime) > DateTime.Now)
                {
                    result = sl.staffid;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// 更新登陆时间
        /// </summary>
        /// <param name="staffid"></param>
        public void refreshlogintime(long staffid)
        {
            var sl = Query<oa_member_staffslogins>(a=>a.staffid==staffid);
            sl.dotime = DateTime.Now;
            Update<oa_member_staffslogins>(sl);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public int adminoutlogin(string cookie)
        {
            int result = 0;
            oa_member_staffslogins sl = Query<oa_member_staffslogins>(a => a.cookie == cookie);
            if (sl == null)
            {
                result = 0;
            }
            else
            {
                if (sl.dotime.AddMinutes(sl.expiretime) > DateTime.Now)
                {
                    result = 2;
                    sl.cookie += "手动退出";
                    Update<oa_member_staffslogins>(sl);
                }
                else
                {
                    result = 1;
                }
            }
            return result;
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="rg">员工表实体</param>
        /// <returns>结果实体</returns>
        public returnresult register(oa_member_staffs sf)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_staffs>(a => a.username == sf.username))
            {
                rr.status = false;
                rr.msg = "用户名已存在";
            }
            else
            {
                if (Exist<oa_member_staffs>(a => a.phone == sf.phone))
                {
                    rr.status = false;
                    rr.msg = "手机号码已存在";
                }
                else
                {
                    if (Exist<oa_member_staffs>(a => a.email == sf.email))
                    {
                        rr.status = false;
                        rr.msg = "邮箱已存在";
                    }
                    else
                    {
                        bool re = Add<oa_member_staffs>(sf);
                        if (re)
                        {
                            rr.status = true;
                            rr.msg = "注册成功";
                        }
                        else
                        {
                            rr.status = false;
                            rr.msg = "注册失败";
                        }
                    }
                }
            }
            return rr;
        }
        #endregion
    }
}
