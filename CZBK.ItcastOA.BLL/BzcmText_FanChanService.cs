using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CZBK.ItcastOA.BLL
{
    public partial class BzcmText_FanChanService : BaseService<BzcmText_FanChan>, IBzcmText_FanChanService
    {
        public object LoadSearchEntities(UserInfoParam astr)
        {
            /// <summary>
            /// 多条件搜索用户信息
            /// </summary>
            /// <param name="userInfoSearchParam"></param>
            /// <returns></returns>

            short delFlag = (short)DelFlagEnum.Normarl;
            
            var temp = this.GetCurrentDbSession.BzcmText_FanChanDal.LoadEntities(c => c.DEL == (astr.IsMaster?1: 0));
           
            if (!string.IsNullOrEmpty(astr.Items))
            {
                int id = Convert.ToInt32(astr.Items);
                temp = temp.Where<BzcmText_FanChan>(u => u.IsFristItemsID==id);
            }
            astr.TotalCount = temp.Count();
            var temps= temp.OrderBy<BzcmText_FanChan, long>(u => u.ID).Skip<BzcmText_FanChan>((astr.PageIndex - 1) * astr.PageSize).Take<BzcmText_FanChan>(astr.PageSize);

            var ret = from a in temps
                       select new
                       {
                           a.ID,
                           a.Addtime,
                           AddUser = a.UserInfo.PerSonName,
                           a.DEL,
                           a.FYXX_Name,
                           a.FYXX_ONE,
                           a.FYXX_Photo,
                           a.FYXX_SHRER,
                           a.FYXX_TWO,
                           IsFristItemsID = a.IsFristItem.Str,
                           itemsid = a.IsFristItemsID,
                           a.IsTop,
                           a.IsTopStartTime,
                           a.IsTopStopTime,
                           a.IsTop_shor,
                           a.News_Addess,
                           a.News_Danwei,
                           a.News_Item,
                           a.News_KaiFaShang,
                           a.News_Money,
                           a.News_Name,
                           a.News_Photo,
                           a.News_Text,
                           a.News_YouHui,
                           a.Str_Image,
                           a.Str_Name,
                           a.Str_Photo
                       };
            return ret;
        }
    }
       
    
}
