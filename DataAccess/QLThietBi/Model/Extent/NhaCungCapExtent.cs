using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace DataAccess.QLThietBi.Model.Extent
{
    public class NhaCungCap
    {
       
        public NhaCungCap() { }
        [NotMapped]
        public string Mota { get; set; }
        [NotMapped]
        public string GhiChu { get; set; }
    }
}
