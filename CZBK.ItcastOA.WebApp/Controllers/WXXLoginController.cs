using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.SearchParam;
using Newtonsoft.Json;
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
        IBLL.IBzcmText_FanChanService BzcmText_FanChanService { get; set; }
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }
        IBLL.IBZCMLouPanJianJieService BZCMLouPanJianJieService { get; set; }


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
                int state = 0;
                if (temp.PersonName != Request["nickName"])
                {
                    temp.PersonName = Request["nickName"];
                    state = 1;
                }
                if (temp.Gender != Convert.ToInt32(Request["gender"]))
                {
                    temp.Gender = Convert.ToInt32(Request["gender"]);
                    state = 1;
                }
                if (temp.Photo != Request["avatarUrl"])
                {
                    temp.Photo = Request["avatarUrl"];
                    state = 1;
                }
                if (state == 1)
                {
                    WXXUserInfoService.EditEntity(temp);
                }
                return Json(new { ret = "ok", msg = "登陆成功!!", userInfo = temp }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                WXXUserInfo wui = new WXXUserInfo();
                wui.WXID = wxid;
                wui.PersonName = Request["nickName"];
                wui.Gender = Convert.ToInt32(Request["gender"]);
                wui.Photo = Request["avatarUrl"];
                wui.ScoreNum = 0;
                wui.DEL = 0;
                WXXUserInfo wxxui = WXXUserInfoService.AddEntity(wui);
                return Json(new { ret = "ok", msg = "登陆成功!",userInfo= wxxui }, JsonRequestBehavior.AllowGet);
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

        //获取信息
        public ActionResult GetFanChan()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;

            //构建搜索条件
            int totalCount = 0;
            UserInfoParam userInfoParam = new UserInfoParam()
            {
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                TotalCount = totalCount,
                Items = Request["item"] == null ? "2" : Request["item"],
                IsMaster = Request["IsHSZ"] == null ? false : Convert.ToBoolean(Request["IsHSZ"]),
                IsTop = Request["IsTop"] == null ? true : Convert.ToBoolean(Request["IsTop"])
            };
            var temp = BzcmText_FanChanService.LoadSearchEntities(userInfoParam).OrderBy(x => x.IsTop_shor);
            return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        //获取首页没栏显示数
        public ActionResult GetIndexPageSize()
        {
            var temp = T_BoolItemService.LoadEntities(x => x.ThisItem == 1 || x.ThisItem == 2 || x.ThisItem == 3 || x.ThisItem == 4 || x.ThisItem == 5 || x.ThisItem == 0).DefaultIfEmpty().ToList();
            List<getInfo> lgf = new List<getInfo>();
            foreach (var a in temp)
            {
                getInfo gi = new getInfo();
                gi.ID = a.ThisItem;
                gi.Num = a.@int;
                lgf.Add(gi);
            }
            return Json(lgf, JsonRequestBehavior.AllowGet);
        }
        //获取新增二级页面数据
        public ActionResult GetShtere()
        {
            long id = Convert.ToInt64(Request["id"]);
            var temp = BZCMLouPanJianJieService.LoadEntities(x => x.BzcmTextID == id).FirstOrDefault();

            if (temp != null)
            {
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.None
                };
                var ret = JsonConvert.SerializeObject(temp, setting);
                return Json(new { ret = "ok", temp = ret }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ret = "未找到数据信息" }, JsonRequestBehavior.AllowGet);
            }
        }
        //获取功能页菜单显示列表
        public ActionResult GetMenuList()
        {
            var temp = T_BoolItemService.LoadEntities(x => x.ThisItem == 9 && x.BOLL_ == true).DefaultIfEmpty().ToList();
            List<getInfo> lgf = new List<getInfo>();
            foreach (var a in temp)
            {
                getInfo gi = new getInfo();
                gi.ID = a.@int;
                gi.Name = a.str;
                lgf.Add(gi);
            }
            return Json(lgf, JsonRequestBehavior.AllowGet);
        }
        //微信小程序登录积分
        public ActionResult AddScoreNum()
        {
            string wxid = Request["WXID"];
            var temp = WXXUserInfoService.LoadEntities(x => x.WXID == wxid).FirstOrDefault();
            if (temp != null)
            {
                var dateDay = temp.EditScoreTime.GetValueOrDefault().ToLongDateString();
                if (dateDay == DateTime.Now.ToLongDateString())
                {
                    return Json(new { ret = "no", msg = "您今日已签过到了~" }, JsonRequestBehavior.AllowGet);
                }
                var score = T_BoolItemService.LoadEntities(x => x.ThisItem == 0).First().@int;
                temp.ScoreNum = temp.ScoreNum + score;
                temp.EditScoreTime = DateTime.Now;
                if (WXXUserInfoService.EditEntity(temp))
                {
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { ret = "no", msg = "签到失败！请联系客服" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { ret = "no", msg = "我此微信用户登录信息" }, JsonRequestBehavior.AllowGet);
            }
        }
        //获取微信人员信息
        public ActionResult GetWxUserInfo()
        {
            string wxid = Request["WXID"];
            var temp = WXXUserInfoService.LoadEntities(x => x.WXID == wxid).FirstOrDefault();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
            /*//获取小程序版本号
            public ActionResult GetVersionNumber()
            {
                var temp = T_BoolItemService.LoadEntities(x => x.ThisItem == 10).FirstOrDefault();
                getInfo gi = new getInfo();
                gi.Name = temp.str;
                return Json(gi, JsonRequestBehavior.AllowGet);
            }*/
        }
    public class getInfo
    {
        public int? ID { get; set; }
        public int? Num { get; set; } 
        public string Name { get; set; }
    }
}
