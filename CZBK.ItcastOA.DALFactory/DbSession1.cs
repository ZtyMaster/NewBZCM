 

using CZBK.ItcastOA.DAL;
using CZBK.ItcastOA.IDAL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                   // _ActionInfoDal = new ActionInfoDal();
				    _ActionInfoDal =AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IBumenInfoSetDal _BumenInfoSetDal;
        public IBumenInfoSetDal BumenInfoSetDal
        {
            get
            {
                if(_BumenInfoSetDal == null)
                {
                   // _BumenInfoSetDal = new BumenInfoSetDal();
				    _BumenInfoSetDal =AbstractFactory.CreateBumenInfoSetDal();
                }
                return _BumenInfoSetDal;
            }
            set { _BumenInfoSetDal = value; }
        }
	
		private IBzcmText_FanChanDal _BzcmText_FanChanDal;
        public IBzcmText_FanChanDal BzcmText_FanChanDal
        {
            get
            {
                if(_BzcmText_FanChanDal == null)
                {
                   // _BzcmText_FanChanDal = new BzcmText_FanChanDal();
				    _BzcmText_FanChanDal =AbstractFactory.CreateBzcmText_FanChanDal();
                }
                return _BzcmText_FanChanDal;
            }
            set { _BzcmText_FanChanDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                   // _DepartmentDal = new DepartmentDal();
				    _DepartmentDal =AbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IIsFristItemDal _IsFristItemDal;
        public IIsFristItemDal IsFristItemDal
        {
            get
            {
                if(_IsFristItemDal == null)
                {
                   // _IsFristItemDal = new IsFristItemDal();
				    _IsFristItemDal =AbstractFactory.CreateIsFristItemDal();
                }
                return _IsFristItemDal;
            }
            set { _IsFristItemDal = value; }
        }
	
		private ILogin_listDal _Login_listDal;
        public ILogin_listDal Login_listDal
        {
            get
            {
                if(_Login_listDal == null)
                {
                   // _Login_listDal = new Login_listDal();
				    _Login_listDal =AbstractFactory.CreateLogin_listDal();
                }
                return _Login_listDal;
            }
            set { _Login_listDal = value; }
        }
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if(_R_UserInfo_ActionInfoDal == null)
                {
                   // _R_UserInfo_ActionInfoDal = new R_UserInfo_ActionInfoDal();
				    _R_UserInfo_ActionInfoDal =AbstractFactory.CreateR_UserInfo_ActionInfoDal();
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set { _R_UserInfo_ActionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                   // _RoleInfoDal = new RoleInfoDal();
				    _RoleInfoDal =AbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private IT_BoolItemDal _T_BoolItemDal;
        public IT_BoolItemDal T_BoolItemDal
        {
            get
            {
                if(_T_BoolItemDal == null)
                {
                   // _T_BoolItemDal = new T_BoolItemDal();
				    _T_BoolItemDal =AbstractFactory.CreateT_BoolItemDal();
                }
                return _T_BoolItemDal;
            }
            set { _T_BoolItemDal = value; }
        }
	
		private IUserbakDal _UserbakDal;
        public IUserbakDal UserbakDal
        {
            get
            {
                if(_UserbakDal == null)
                {
                   // _UserbakDal = new UserbakDal();
				    _UserbakDal =AbstractFactory.CreateUserbakDal();
                }
                return _UserbakDal;
            }
            set { _UserbakDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                   // _UserInfoDal = new UserInfoDal();
				    _UserInfoDal =AbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	
		private IWx__BzcmTextDal _Wx__BzcmTextDal;
        public IWx__BzcmTextDal Wx__BzcmTextDal
        {
            get
            {
                if(_Wx__BzcmTextDal == null)
                {
                   // _Wx__BzcmTextDal = new Wx__BzcmTextDal();
				    _Wx__BzcmTextDal =AbstractFactory.CreateWx__BzcmTextDal();
                }
                return _Wx__BzcmTextDal;
            }
            set { _Wx__BzcmTextDal = value; }
        }
	
		private IWXX_FormIDDal _WXX_FormIDDal;
        public IWXX_FormIDDal WXX_FormIDDal
        {
            get
            {
                if(_WXX_FormIDDal == null)
                {
                   // _WXX_FormIDDal = new WXX_FormIDDal();
				    _WXX_FormIDDal =AbstractFactory.CreateWXX_FormIDDal();
                }
                return _WXX_FormIDDal;
            }
            set { _WXX_FormIDDal = value; }
        }
	
		private IWXXLogin_bakDal _WXXLogin_bakDal;
        public IWXXLogin_bakDal WXXLogin_bakDal
        {
            get
            {
                if(_WXXLogin_bakDal == null)
                {
                   // _WXXLogin_bakDal = new WXXLogin_bakDal();
				    _WXXLogin_bakDal =AbstractFactory.CreateWXXLogin_bakDal();
                }
                return _WXXLogin_bakDal;
            }
            set { _WXXLogin_bakDal = value; }
        }
	
		private IWXXMenuInfoDal _WXXMenuInfoDal;
        public IWXXMenuInfoDal WXXMenuInfoDal
        {
            get
            {
                if(_WXXMenuInfoDal == null)
                {
                   // _WXXMenuInfoDal = new WXXMenuInfoDal();
				    _WXXMenuInfoDal =AbstractFactory.CreateWXXMenuInfoDal();
                }
                return _WXXMenuInfoDal;
            }
            set { _WXXMenuInfoDal = value; }
        }
	
		private IWXXUserInfoDal _WXXUserInfoDal;
        public IWXXUserInfoDal WXXUserInfoDal
        {
            get
            {
                if(_WXXUserInfoDal == null)
                {
                   // _WXXUserInfoDal = new WXXUserInfoDal();
				    _WXXUserInfoDal =AbstractFactory.CreateWXXUserInfoDal();
                }
                return _WXXUserInfoDal;
            }
            set { _WXXUserInfoDal = value; }
        }
	}	
}