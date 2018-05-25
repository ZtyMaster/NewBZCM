using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IBumenInfoSetService BumenInfoSetService { get; set; }

        // GET: /Home/
        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.PerSonName;
            }
            return View();
        }
        public ActionResult master()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.PerSonName;
                ViewBag.id = LoginUser.ID;
                ViewBag.qx = LoginUser.QuXian;
            }
            return View();
        }
        
        public ActionResult HomePage()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.UName;
            }
            return View();
        }

        #region 校验用户权限 完成登陆用户菜单权限的过滤
        public ActionResult GetMenus() {

            var returnActionlist = GetMenum();
            //序列化 权限
            return Json(returnActionlist,JsonRequestBehavior.AllowGet);

        }
        public object GetMenum()
        {
            //1：根据用户 ——角色——权限 将登陆用户具有的菜单权限查询出来放在一个集合中
            var loginUserInfo = UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();

            var loginUserRoleInfo = loginUserInfo.RoleInfoes;//获取登陆用户的角色信息
            short actionTypeEnum = (short)ActionInfoTypeEnum.MenuActionTypeEnum;//表示菜单权限
            //查询出角色对应的菜单权限
            var loginUserActionInfo = (from r in loginUserRoleInfo
                                       from a in r.ActionInfoes
                                       where a.ActionTypeEnum == actionTypeEnum
                                       select a).ToList();
            //2：根据用户——权限

            //根据登陆用户查询o.R_UserInfo_ActionInfo中间表，然后在用导航属性查询权限表
            var r_userInfo_actionInfo = from r in loginUserInfo.R_UserInfo_ActionInfo select r.ActionInfo;

            //判断是否是菜单权限
            var loginUserMenuAction = (from r in r_userInfo_actionInfo
                                       where r.ActionTypeEnum == actionTypeEnum
                                       select r).ToList();
            //将存储登陆用户权限的两个集合合并
            loginUserActionInfo.AddRange(loginUserMenuAction);
            //查询出所有登陆用户禁止的权限的编号
            var loginForbActionInfo = (from r in loginUserInfo.R_UserInfo_ActionInfo
                                       where r.IsPass == false
                                       select r.ActionInfoID).ToList();
            //将禁止的权限从集合中过滤掉
            var loginUserAllowActionlist = loginUserActionInfo.Where(a => !loginForbActionInfo.Contains(a.ID));
            //去除重复的
            var loginUserAllowActionlists = loginUserAllowActionlist.Distinct(new EqualityComparer());
            loginUserAllowActionlists= loginUserAllowActionlists.OrderBy(x => x.Sort);
            var returnActionlist = from a in loginUserAllowActionlists
                                   select new { icon = a.MenuIcon, title = a.ActionInfoName, url = a.Url };
            
            return returnActionlist;
        }
        #endregion


        public ActionResult logindex() {
            return View();
        }
     


        #region 微信用权限验证菜单
       
        ////获取所有菜单信息
        //public ActionResult getAllMenuInfo()
        //{
        //    var temp = WXXMenuInfoService.LoadEntities(x => x.ID > 0).DefaultIfEmpty().ToList();
        //    if (temp[0] != null)
        //    {
        //        return Json(new { ret = "ok", rows = temp }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { ret = "no" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        ////获取菜单列表
        //public ActionResult getWXXMenuList()
        //{
        //    var temp = WXXMenuInfoService.LoadEntities(x => x.ID > 0).DefaultIfEmpty().ToList();
        //    if (temp[0] != null)
        //    {
        //        return Json(new { ret = "ok", rows = temp }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { ret = "no", msg = "数据表中无数据！" }, JsonRequestBehavior.AllowGet);
        //}
        ////添加菜单数据
        //public ActionResult addMenuInfo()
        //{
        //    WXXMenuInfo menu = new WXXMenuInfo();
        //    menu.Name = Request["CHNname"];
        //    menu.EngName = Request["ENGname"];
        //    WXXMenuInfoService.AddEntity(menu);
        //    return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
        //}
        ////修改菜单信息
        //public ActionResult editMenuInfo()
        //{
        //    WXXMenuInfo menu = new WXXMenuInfo();
        //    menu.ID= Convert.ToInt32(Request["id"]);
        //    menu.Name = Request["CHNname"];
        //    menu.EngName = Request["ENGname"];
        //    if (WXXMenuInfoService.EditEntity(menu))
        //    {
        //        return Json(new { ret = "ok" }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { ret = "no" ,msg="修改失败，联系管理员"}, JsonRequestBehavior.AllowGet);
        //}
       
    
        
       
    
        //获取所有部门名称
        public ActionResult GetAllBuMen()
        {
            var temp = BumenInfoSetService.LoadEntities(x => x.ID > 0).DefaultIfEmpty().ToList();
            List<STUBuMen> list = new List<STUBuMen>();
            foreach (var a in temp)
            {
                if (a == null)
                {
                    continue;
                }
                STUBuMen stubm = new STUBuMen();
                stubm.ID = a.ID;
                stubm.Name = a.Name;
                list.Add(stubm);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //获取部门用户名字
        public ActionResult GetBuMenAllUser()
        {
            if (Request["BMID"].Length <= 0)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var bmid = Convert.ToInt32(Request["BMID"]);
            var temp = UserInfoService.LoadEntities(x => x.BuMenID == bmid).DefaultIfEmpty().ToList();
            if (temp[0] != null)
            {
                List<BMUser> list = new List<BMUser>();
                foreach (var a in temp)
                {
                    if (a == null)
                    {
                        continue;
                    }
                    BMUser bmu = new BMUser();
                    bmu.ID = a.ID;
                    bmu.Name = a.PerSonName;
                    list.Add(bmu);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //获取公司所有下属员工信息
        public ActionResult GetCompanyAllUser()
        {
            var temp = UserInfoService.LoadEntities(x => x.ID > 0).DefaultIfEmpty().ToList();
            if(temp[0] != null)
            {
                List<WXXQXmany> listwqx = new List<WXXQXmany>();
                foreach(var a in temp)
                {
                    WXXQXmany wqx = new WXXQXmany();
                    wqx.ID = a.ID;
                    wqx.Name = a.PerSonName + "【" + a.BumenInfoSet.Name + "】";
                    listwqx.Add(wqx);
                }
                return Json(new { ret = "ok", rows = listwqx }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { ret = "no", msg = "数据库中无数据" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
    public class STUBuMen
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class BMUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class WXXQX
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public List<string> CanSee { get; set; }
    }
    public class WXXQXmany
    {
        public int ID { get; set; }
        public string Name{ get; set; }
    }
}
