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
        public IQueryable<BzcmClass> LoadSearchEntities(UserInfoParam astr)
        {
            /// <summary>
            /// 多条件搜索用户信息
            /// </summary>
            /// <param name="userInfoSearchParam"></param>
            /// <returns></returns>

            short delFlag = (short)DelFlagEnum.Normarl;
            
            var temp = this.GetCurrentDbSession.BzcmText_FanChanDal.LoadEntities(c => c.DEL == (astr.IsMaster?1: 0));
            
            if (astr.IsTop) {
                temp = temp.Where(x => x.IsTop == 1&&x.IsTopStartTime<=DateTime.Now&&x.IsTopStopTime>=DateTime.Now);
            }
           
            if (!string.IsNullOrEmpty(astr.Items))
            {
                int id = Convert.ToInt32(astr.Items);
                temp = temp.Where<BzcmText_FanChan>(u => u.IsFristItemsID==id);
            }
            astr.TotalCount = temp.Count();
            var temps= temp.OrderBy<BzcmText_FanChan, long>(u => u.ID).Skip<BzcmText_FanChan>((astr.PageIndex - 1) * astr.PageSize).Take<BzcmText_FanChan>(astr.PageSize);
            var temp_bzcm = this.GetCurrentDbSession.BZCMLouPanJianJieDal.LoadEntities(x => x.DEL == 0).DefaultIfEmpty();
            var ret = from a in temps 
                      from b in temp_bzcm 
                      select  new BzcmClass 
                      {
                          ID = a.ID,
                          Addtime = a.Addtime,
                          PerSonName = a.UserInfo.PerSonName,
                          DEL = a.DEL,
                          FYXX_Name = a.FYXX_Name,
                          FYXX_ONE = a.FYXX_ONE,
                          FYXX_Photo = a.FYXX_Photo,
                          FYXX_SHRER = a.FYXX_SHRER,
                          FYXX_TWO = a.FYXX_TWO,
                          Str = a.IsFristItem.Str,
                          itemsid = a.IsFristItemsID,
                          IsTop = a.IsTop,
                          IsTopStartTime = a.IsTopStartTime,
                          IsTopStopTime = a.IsTopStopTime,
                          IsTop_shor = a.IsTop_shor,
                          News_Addess = a.News_Addess,
                          News_Danwei = a.News_Danwei,
                          News_Item = a.News_Item,
                          News_KaiFaShang = a.News_KaiFaShang,
                          News_Money = a.News_Money,
                          News_Name = a.News_Name,
                          News_Photo = a.News_Photo,
                          News_Text = a.News_Text,
                          News_YouHui = a.News_YouHui,
                          Str_Image = a.Str_Image,
                          Str_Name = a.Str_Name,
                          Str_Photo = a.Str_Photo,
                          Shtere = b.BzcmTextID == a.ID ? true : false
                       } ;
            

            return ret;

        }
    }
       
    
}
