//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CZBK.ItcastOA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WXXUserInfo
    {
        public WXXUserInfo()
        {
            this.Wx__BzcmText = new HashSet<Wx__BzcmText>();
            this.OrderHistories = new HashSet<OrderHistory>();
        }
    
        public long ID { get; set; }
        public Nullable<int> UID { get; set; }
        public string WXID { get; set; }
        public string PersonName { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public string Photo { get; set; }
        public string bak { get; set; }
        public int DEL { get; set; }
        public Nullable<decimal> ScoreNum { get; set; }
        public Nullable<System.DateTime> EditScoreTime { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Wx__BzcmText> Wx__BzcmText { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
