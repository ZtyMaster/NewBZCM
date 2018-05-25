﻿ 
using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.ActionInfoDal;
        }
    }   
	
	public partial class BumenInfoSetService :BaseService<BumenInfoSet>,IBumenInfoSetService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.BumenInfoSetDal;
        }
    }   
	
	public partial class BZCMLouPanJianJieService :BaseService<BZCMLouPanJianJie>,IBZCMLouPanJianJieService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.BZCMLouPanJianJieDal;
        }
    }   
	
	public partial class BzcmText_FanChanService :BaseService<BzcmText_FanChan>,IBzcmText_FanChanService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.BzcmText_FanChanDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.DepartmentDal;
        }
    }   
	
	public partial class IsFristItemsService :BaseService<IsFristItems>,IIsFristItemsService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.IsFristItemsDal;
        }
    }   
	
	public partial class Login_listService :BaseService<Login_list>,ILogin_listService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.Login_listDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.RoleInfoDal;
        }
    }   
	
	public partial class T_BoolItemService :BaseService<T_BoolItem>,IT_BoolItemService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.T_BoolItemDal;
        }
    }   
	
	public partial class UserbaksService :BaseService<Userbaks>,IUserbaksService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.UserbaksDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.UserInfoDal;
        }
    }   
	
	public partial class Wx__BzcmTextService :BaseService<Wx__BzcmText>,IWx__BzcmTextService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.Wx__BzcmTextDal;
        }
    }   
	
	public partial class WXX_FormIDService :BaseService<WXX_FormID>,IWXX_FormIDService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.WXX_FormIDDal;
        }
    }   
	
	public partial class WXXUserInfoService :BaseService<WXXUserInfo>,IWXXUserInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.WXXUserInfoDal;
        }
    }   
	
}