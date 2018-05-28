using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.Model.SearchParam
{
    public class BzcmClass:BzcmText_FanChan
    {
        public  int itemsid { get; set; }
        public string PerSonName { get; set; }
        public string Str { get; set; }
        public bool Shtere { get; set; }
    }
}
