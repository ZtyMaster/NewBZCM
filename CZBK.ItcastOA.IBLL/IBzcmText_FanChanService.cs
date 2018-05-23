using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZBK.ItcastOA.Model;
namespace CZBK.ItcastOA.IBLL
{
    
    public partial interface IBzcmText_FanChanService : IBaseService<BzcmText_FanChan>
    {
        IQueryable<BzcmText_FanChan> LoadSearchEntities(UserInfoParam upm);
    }
}
