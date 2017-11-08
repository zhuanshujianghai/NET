using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA_Web.Controllers
{
    /// <summary>
    /// 前后端通用控制器
    /// </summary>
    public class MyCommonController : Controller
    {
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult Captcha()
        {
            //验证码字符长度
            int codelength = 4;
            //验证码图片宽度
            int imgwidth = 100;
            if (codelength == 6)
            {
                imgwidth = 150;
            }
            string verificationCode = JVerify.Verification.CreateVerificationText(codelength, 1);
            Bitmap _img = JVerify.Verification.CreateVerificationImage(verificationCode, imgwidth, 30);
            _img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            TempData["captcha"] = verificationCode.ToUpper();
            return null;
        }
	}
}