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
    public class BZCMController : BaseController
    {
        //
        // GET: /BZCM/
        IBLL.IBzcmText_FanChanService BzcmText_FanChanService { get; set; }
        IBLL.IIsFristItemService IsFristItemService { get; set; }
        IBLL.IWx__BzcmTextService Wx__BzcmTextService { get; set; }
        IBLL.IT_BoolItemService T_BoolItemService { get; set; }
        IBLL.IBZCMLouPanJianJieService BZCMLouPanJianJieService { get; set; }
        IBLL.IWXXTsMessageService WXXTsMessageService { get; set; }
        IBLL.IWXXUserInfoService WXXUserInfoService { get; set; }
        IBLL.IWXX_FormIDService WXX_FormIDService { get; set; }
        public ActionResult Index()
        {
            var slider = T_BoolItemService.LoadEntities(x => x.ItemsID == 0).DefaultIfEmpty();
            ViewBag.slider0 = slider.Where(x=>x.ID==1).FirstOrDefault().@int;
            ViewBag.slider1 = slider.Where(x => x.ID == 2).FirstOrDefault().@int;
            ViewBag.slider2 = slider.Where(x => x.ID == 3).FirstOrDefault().@int;
            ViewBag.slider3 = slider.Where(x => x.ID == 4).FirstOrDefault().@int;
            ViewBag.slider4 = slider.Where(x => x.ID == 5).FirstOrDefault().@int;
            var radio = T_BoolItemService.LoadEntities(x => x.ThisItem == 9).DefaultIfEmpty();
            ViewBag.radio1 = radio.Where(x => x.@int == 1).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio2 = radio.Where(x => x.@int == 2).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio3 = radio.Where(x => x.@int == 3).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio4 = radio.Where(x => x.@int == 4).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio5 = radio.Where(x => x.@int == 5).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio6 = radio.Where(x => x.@int == 6).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio7 = radio.Where(x => x.@int == 7).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.radio8 = radio.Where(x => x.@int == 8).FirstOrDefault().BOLL_ == true ? 0 : 1;
            ViewBag.Jifen = T_BoolItemService.LoadEntities(x => x.ItemsID ==98).FirstOrDefault().@int;
            return View();
        }
        #region 获取上传图片
        public ActionResult FileUpload()
        {
            HttpPostedFileBase file = Request.Files["fileIconUp"];
            if (Request["item"] != null) {
                file= Request["item"]== "Image_banner"? Request.Files["img_banner"] : Request.Files["img_name"];
            }
            
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);//获取上传的文件名
                string fileExt = Path.GetExtension(filename);//获取扩展名
                if (fileExt == ".jpg" || fileExt == ".png")
                {
                    string dir = "/BZCMimage/topbanner/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                    string filenewName = Guid.NewGuid().ToString();
                    string fulldir = dir + filenewName + fileExt;
                    file.SaveAs(Request.MapPath(fulldir));
                    return Content("yes:" + fulldir);
                }
                else
                {
                    return Content("no:文件类型错误，文件扩展名错误！");
                }
            }
            else
            {
                return Content("no:请上传图片文件");
            }
        }

        //删除图片
        public ActionResult DelImage() {
            string str = Request["str"];
            try
            {
                NewDeletFile(str);
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                return Json(new { ret = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
            
        }

        private void NewDeletFile(string str)
        {
            FileInfo file = new FileInfo(Request.MapPath(str));
            file.Delete();
        }
        #endregion
        //控制中心
        #region 房产信息
        //获取信息列表
        public ActionResult Bzkx() {
            var p =Convert.ToInt32(Request["items"]);
            //p==0时 返回广告位信息  ==0时返回历史记录 
            var temps = IsFristItemService.LoadEntities(x=>x.Items==p).DefaultIfEmpty();
            var temp = from a in temps
                       select new {
                           a.ID,
                           a.Str,
                           a.Del,
                           a.Str_int
                       };
            return Json( temp , JsonRequestBehavior.AllowGet);
        }
        //新增信息表
        public ActionResult AddBzcmImage(BzcmText_FanChan btfc) {
            btfc.Addtime = MvcApplication.GetT_time();
            btfc.DEL = 0;
            btfc.AddUser = LoginUser.ID;
            btfc.IsFristItemsID = Convert.ToInt32(Request["IsFristItemsID"]);
            string str = "ok";
            if (btfc.ID > 0)
            {                
                BzcmText_FanChanService.EditEntity(btfc);
            }
            else {
                try { BzcmText_FanChanService.AddEntity(btfc); }
                catch (Exception e)
                {
                    if (btfc.Str_Image.Length > 0) {
                        NewDeletFile(btfc.Str_Image);
                    }
                    
                    str = e.ToString();
                }
            }
            

            return Json(new { ret = str }, JsonRequestBehavior.AllowGet);
        }
        //删除信息或撤销
        public ActionResult deldata() {
            long id = Convert.ToInt64(Request["id"]);
            var fcdata= BzcmText_FanChanService.LoadEntities(x => x.ID == id).FirstOrDefault();
            string ret = "ok";
            if (fcdata != null) {
                fcdata.DEL = fcdata.DEL==0?1:0;
                if (!BzcmText_FanChanService.EditEntity(fcdata)) {
                    ret = "未修改成功！";
                }
            }
            else {
                ret = "未找到要删除的信息！";    
            }            
            return Json(new { ret = ret }, JsonRequestBehavior.AllowGet);
        }
        //获取信息
        public ActionResult GetFanChan() {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            
            //构建搜索条件
            int totalCount = 0;
            UserInfoParam userInfoParam = new UserInfoParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = Request["item"] == null ? "2" : Request["item"],
                IsMaster = Request["IsHSZ"] == null ? false : Convert.ToBoolean(Request["IsHSZ"]),
                IsTop = false
            };

            var temp = BzcmText_FanChanService.LoadSearchEntities(userInfoParam);
          
            return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        //修改微信首页显示行数
        public ActionResult editslider() {

            var val =Convert.ToInt32( Request["val"]);
            long id = Convert.ToInt64(Request["id"]);

            var tbl= T_BoolItemService.LoadEntities(x => x.ID == id).FirstOrDefault();
            if (tbl != null)
            {
                tbl.@int = val;
                if (T_BoolItemService.EditEntity(tbl))
                {
                    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { ret = "修改失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                return Json(new { ret = "未找到要修改的数据！" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        //新增二级页面
        public ActionResult AddShere(BZCMLouPanJianJie bzcm) {
            bzcm.AddTime = DateTime.Now;
            bzcm.AddUserID = LoginUser.ID;
            string str = "ok";
            if (bzcm.ID > 0)
            {
                BZCMLouPanJianJieService.EditEntity(bzcm);
            }
            else
            {
                try { BZCMLouPanJianJieService.AddEntity(bzcm); }
                catch (Exception e)
                {
                    if (bzcm.Image_banner != null) {
                        NewDeletFile(bzcm.Image_banner);
                    }
                    if (bzcm.Image_Name != null)
                    {
                        NewDeletFile(bzcm.Image_Name);
                    }
                    str = e.ToString();
                }
            }


            return Json(new { ret = str }, JsonRequestBehavior.AllowGet);

            return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
        }
        //获取新增二级页面数据
        public ActionResult GetShtere() {
            long id = Convert.ToInt64(Request["id"]);
            var temp= BZCMLouPanJianJieService.LoadEntities(x => x.BzcmTextID == id).FirstOrDefault();
            
            if (temp != null)
            {
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.None
                };
                var ret = JsonConvert.SerializeObject(temp, setting);
                return Json(new { ret ="ok",temp= ret }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { ret = "未找到数据信息" }, JsonRequestBehavior.AllowGet);
            }
        }

        //修改登陆积分
        public ActionResult EditJiFen() {
            int jifen = Convert.ToInt32(Request["jifen"]);
            var temp_jifen = T_BoolItemService.LoadEntities(x => x.ItemsID == 98).FirstOrDefault();
            temp_jifen.@int = jifen;
            if (T_BoolItemService.EditEntity(temp_jifen))
            {
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { ret = "修改积分出现错误！" }, JsonRequestBehavior.AllowGet);
            }
        }
        //修改菜单显示
        public ActionResult EditMenuYorN()
        {
            bool value = Convert.ToInt32(Request["value"])==0?true:false;
            int type = Convert.ToInt32(Request["type"]);
            var temp = T_BoolItemService.LoadEntities(x => x.ThisItem == 9 && x.@int == type).FirstOrDefault();
            temp.BOLL_ = value;
            if (T_BoolItemService.EditEntity(temp))
            {
                return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new { ret = "修改数据出现错误！" }, JsonRequestBehavior.AllowGet);
            }
        }
        //获取推送消息记录信息
        public ActionResult GetTsMessageList()
        {
            var temp = WXXTsMessageService.LoadEntities(x => x.Del == true).DefaultIfEmpty().ToList();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
        //获取微信用户列表
        public ActionResult GetWxUserList()
        {
            var temp = WXXUserInfoService.LoadEntities(x => x.ID > 0).DefaultIfEmpty();
            var rtmp = from a in temp
                       select new
                       {
                           ID = a.ID,
                           Name = a.PersonName
                       };
            return Json(rtmp, JsonRequestBehavior.AllowGet);
        }
        //获取消息并推送
        public ActionResult ToTsMessage()
        {
            var uid = Request["BeiTsPerson"]==null?0:Convert.ToInt64(Request["BeiTsPerson"]);
            var text = Request["TsText"];
            string Message = "";
            if (uid == 0)
            {
                var temp = WXXUserInfoService.LoadEntities(x => x.ID > 0).DefaultIfEmpty().ToList();
                if (temp != null && temp[0] != null)
                {
                    foreach(var a in temp)
                    {
                        Message=SendTempletMessge(a.ID, text);
                    }
                    if (Message == "推送成功")
                    {
                        WXXTsMessage wtm = new WXXTsMessage();
                        wtm.Message = text;
                        wtm.ToAllUser = true;
                        wtm.AddTime = DateTime.Now;
                        WXXTsMessageService.AddEntity(wtm);
                        return Json(new { ret = "ok", msg = Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { ret = "no" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Message = SendTempletMessge(uid, text);
                if (Message == "推送成功")
                {
                    WXXTsMessage wtm = new WXXTsMessage();
                    wtm.Message = text;
                    wtm.ToAllUser = false;
                    wtm.ToOneUser = WXXUserInfoService.LoadEntities(x => x.ID == uid).FirstOrDefault().WXID;
                    wtm.AddTime = DateTime.Now;
                    WXXTsMessageService.AddEntity(wtm);
                    return Json(new { ret = "ok", msg = Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { ret = "no" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 微信通知用
        public string SendTempletMessge(long uid, string TsText)
        {
            //var user = UserInfoService.LoadEntities(x => x.ID == uid).FirstOrDefault();
            var rtmp = WXXUserInfoService.LoadEntities(x => x.UID == uid).FirstOrDefault();
            if (rtmp == null)
            {
                return null;
            }
            string strReturn = string.Empty;
            try
            {
                #region 获取access_token
                string apiurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxc67a3c17709458e5&secret=34a9647b2c1120e443cdf14b1a0d6b46";
                WebRequest request = WebRequest.Create(@apiurl);
                request.Method = "POST";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.UTF8;
                StreamReader reader = new StreamReader(stream, encode);
                string detail = reader.ReadToEnd();
                var jd = JsonConvert.DeserializeObject<WXApi>(detail);
                string token = (String)jd.access_token;
                DateTime dtime = MvcApplication.GetT_time();
                var wxx = WXX_FormIDService.LoadEntities(x => x.AddUserID == uid && x.StopTime > dtime).DefaultIfEmpty();
                WXX_FormID Minwxx = new WXX_FormID();
                if (wxx.ToList()[0] != null)
                {
                    Minwxx = wxx.OrderBy(x => x.StopTime).FirstOrDefault();
                }
                else
                {
                    return null;
                }
                #endregion
                #region 组装信息推送，并返回结果（其它模版消息于此类似）
                string url = "https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=" + token;
                string temp = "{\"touser\": \"" + rtmp.WXID + "\"," +
                       "\"template_id\": \"3zN541eDUYsMVVZnqf6GEuZr7KDdOC1jamBsgEKHXY0\", " +
                       "\"topcolor\": \"#FF0000\", " +
                       "\"form_id\": \"" + Minwxx.FormID + "\"," +
                       "\"data\": " +
                       "{\"first\": {\"value\": \"您好，您有一条咨询通知信息\"}," +
                       "\"keyword1\": { \"value\": \"" + TsText + "\"}," +
                       "\"remark\": {\"value\": \"\" }}}";
                #endregion
                //核心代码
                GetResponseData(temp, @url);
                strReturn = "推送成功";
                //删除使用过的formid
                WXX_FormIDService.DeleteEntity(Minwxx);
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }
        public string GetResponseData(string JSONData, string Url)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            //声明一个HttpWebRequest请求
            request.Timeout = 90000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            streamReceive.Dispose();
            streamReader.Dispose();
            return strResult;
        }
        #endregion
    }
    //微信通知用实体类
    public class WXApi
    {
        public string access_token { set; get; }
    }
}
