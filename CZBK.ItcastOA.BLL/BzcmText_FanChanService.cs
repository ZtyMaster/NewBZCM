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
        public IQueryable<BzcmText_FanChan> LoadSearchEntities(UserInfoParam userInfoSearchParam)
        {
            /// <summary>
            /// 多条件搜索用户信息
            /// </summary>
            /// <param name="userInfoSearchParam"></param>
            /// <returns></returns>

            short delFlag = (short)DelFlagEnum.Normarl;
            
            var temp = this.GetCurrentDbSession.BzcmText_FanChanDal.LoadEntities(c => c.DEL == 0);
           
            if (!string.IsNullOrEmpty(userInfoSearchParam.Remark))
            {
                temp = temp.Where<BzcmText_FanChan>(u => u.News_Text.Contains(userInfoSearchParam.Remark));
            }
            userInfoSearchParam.TotalCount = temp.Count();
            return temp.OrderBy<BzcmText_FanChan, long>(u => u.ID).Skip<BzcmText_FanChan>((userInfoSearchParam.PageIndex - 1) * userInfoSearchParam.PageSize).Take<BzcmText_FanChan>(userInfoSearchParam.PageSize);

        }
    }
       
    
}
