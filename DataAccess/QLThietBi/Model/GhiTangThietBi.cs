//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.QLThietBi.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class GhiTangThietBi
    {
        public int ID { get; set; }
        public string SoPhieu { get; set; }
        public System.DateTime NgayLapPhieu { get; set; }
        public string GhiChu { get; set; }
        public long DonViID { get; set; }
        public Nullable<long> NguoiTaoID { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<long> NguoiSuaID { get; set; }
        public Nullable<System.DateTime> NgaySua { get; set; }
    }
}
