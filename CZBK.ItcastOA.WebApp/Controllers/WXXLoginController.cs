using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class WXXLoginController : Controller
    {
        //
        // GET: /WXXLogin/
        IBLL.IWXXUserInfoService WXXUserInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckLogin()
        {
            string wxid = Request["WXID"];
            var temp = WXXUserInfoService.LoadEntities(x => x.WXID == wxid).FirstOrDefault();
            if (temp != null)
            {
                return Json(new { ret = "ok", msg = "登陆成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                WXXUserInfo wui = new WXXUserInfo();
                wui.WXID = wxid;
                wui.PersonName = Request["nickName"];
                wui.Gender = Convert.ToInt32(Request["gender"]);
                wui.Photo = Request["avatarUrl"];
                wui.DEL = 0;
                WXXUserInfoService.AddEntity(wui);
                return Json(new { ret = "ok", msg = "登陆成功!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #region 微信获取openid
        public ActionResult getOpenid()
        {
            //获取openid
            // //// 链接地址
            //int id = Convert.ToInt32(Request.QueryString["ID"]);
            string code = Request["js_code"].ToString();
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + Request["appid"] + "&secret=" + Request["secret"] + "&js_code=" + code + "&grant_type=" + Request["grant_type"];
            WebRequest request = WebRequest.Create(@url);
            request.Method = "GET";
            request.ContentType = "text/html:charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return Json(new { ret = "ok", openInfo = retString }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
