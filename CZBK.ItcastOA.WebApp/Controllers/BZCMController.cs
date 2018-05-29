using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.SearchParam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public ActionResult Index()
        {
            var slider = T_BoolItemService.LoadEntities(x => x.ItemsID == 0).DefaultIfEmpty();
            ViewBag.slider0 = slider.Where(x=>x.ID==1).FirstOrDefault().@int;
            ViewBag.slider1 = slider.Where(x => x.ID == 2).FirstOrDefault().@int;
            ViewBag.slider2 = slider.Where(x => x.ID == 3).FirstOrDefault().@int;
            ViewBag.slider3 = slider.Where(x => x.ID == 4).FirstOrDefault().@int;
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
        #endregion

    }
}
