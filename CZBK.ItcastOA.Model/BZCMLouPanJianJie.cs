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
    
    public partial class BZCMLouPanJianJie
    {
        public long ID { get; set; }
        public int DEL { get; set; }
        public long BzcmTextID { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public Nullable<int> AddUserID { get; set; }
        public string Texts { get; set; }
        public string ShuoSuQuYu { get; set; }
        public string Addess { get; set; }
        public string Photo { get; set; }
        public Nullable<int> XiaoShouZT { get; set; }
        public string XiangMuTeShe { get; set; }
        public string KaiFaShang { get; set; }
        public Nullable<System.DateTime> KaiPanTime { get; set; }
        public Nullable<System.DateTime> RuzuTime { get; set; }
        public Nullable<decimal> Money { get; set; }
        public string ShouLouAddess { get; set; }
        public Nullable<int> ChanQuanYear { get; set; }
        public string HuXing { get; set; }
        public Nullable<decimal> JianZhuMianJi { get; set; }
        public Nullable<decimal> ZhanDiMianJi { get; set; }
        public string JianZhuLeiBie { get; set; }
        public string ZhuangXiuQingKuang { get; set; }
        public string LouCheZhuanKuang { get; set; }
        public string ChengJianShang { get; set; }
        public string DaiLiShang { get; set; }
        public string WuYeLeiBie { get; set; }
        public Nullable<decimal> RongJiLv { get; set; }
        public Nullable<decimal> LvHuaLv { get; set; }
        public string GongShui { get; set; }
        public string GongQi { get; set; }
        public string GongNuan { get; set; }
        public string KuanDai { get; set; }
        public string WuYeGongShi { get; set; }
        public string WuYeFei { get; set; }
        public string StopCar { get; set; }
        public string GongJiaoXianLu { get; set; }
        public string Shool { get; set; }
        public string Shop { get; set; }
        public string YiYuan { get; set; }
        public string Sty { get; set; }
        public string Game { get; set; }
        public string EAC { get; set; }
        public string Image_banner { get; set; }
        public string Image_Name { get; set; }
    
        public virtual BzcmText_FanChan BzcmText_FanChan { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
