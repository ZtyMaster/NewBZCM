﻿ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IBumenInfoSetDal BumenInfoSetDal{get;set;}
	
		IBZCMLouPanJianJieDal BZCMLouPanJianJieDal{get;set;}
	
		IBzcmText_FanChanDal BzcmText_FanChanDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IIsFristItemDal IsFristItemDal{get;set;}
	
		ILogin_listDal Login_listDal{get;set;}
	
		IOrderHistoryDal OrderHistoryDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		IT_BoolItemDal T_BoolItemDal{get;set;}
	
		IUserbakDal UserbakDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	
		IWx__BzcmTextDal Wx__BzcmTextDal{get;set;}
	
		IWXX_FormIDDal WXX_FormIDDal{get;set;}
	
		IWXXTsMessageDal WXXTsMessageDal{get;set;}
	
		IWXXUserInfoDal WXXUserInfoDal{get;set;}
	}	
}