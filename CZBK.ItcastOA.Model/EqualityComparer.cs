using CZBK.ItcastOA.Model.SearchParam;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.Model
{
    public class EqualityComparer : IEqualityComparer<ActionInfo>
    {
        public bool Equals(ActionInfo x, ActionInfo y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(ActionInfo obj)
        {
            return obj.GetHashCode();
        }
    }

    public class BzcmCompare : IEqualityComparer<BzcmClass>
    {
        public bool Equals(BzcmClass x, BzcmClass y)
        {
            return x.ID == y.ID;
        }


        public int GetHashCode(BzcmClass obj)
        {
            return obj.GetHashCode();
        }        
    }

}
