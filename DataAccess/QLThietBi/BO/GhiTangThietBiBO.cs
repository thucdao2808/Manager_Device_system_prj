using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.QLThietBi.BO
{
    public class GhiTangThietBiBO 
    {
        public GhiTangThietBiBO() { }

        public List<GhiTangThietBi> GetGhiTang()
        {
            var cacheGhiTang = new DefaultCacheProvider();
            string CacheKeyGhiTang = cacheGhiTang.BuildCachedKey("GhiTangThietBi", "GetGhiTang");
             List<GhiTangThietBi> listGhiTang = new List<GhiTangThietBi>();
            if (!cacheGhiTang.IsSet(CacheKeyGhiTang))
            {
              using( var db = new  QuanLyThietBiEntities())
               {
                  var data= db.GhiTangThietBis.Select(gt => new
                  {
                      gt.ID,
                      gt.SoPhieu,
                      gt.NgayLapPhieu,
                      gt.GhiChu,
                  }
                  ).ToList();
                 foreach(var item in data)
                    {
                        GhiTangThietBi tb = new GhiTangThietBi();
                        tb.ID=item.ID;
                        tb.SoPhieu= item.SoPhieu;
                        tb.NgayLapPhieu=item.NgayLapPhieu;
                        tb.GhiChu= item.GhiChu;
                        listGhiTang.Add(tb);
                    }

               }
                cacheGhiTang.Set(CacheKeyGhiTang, listGhiTang, DefaultCacheProvider.CACHING_TIME_DEFAULT_IN_1_MINUTES);
            }else
            {
                listGhiTang=cacheGhiTang.Get(CacheKeyGhiTang) as List<GhiTangThietBi>;
            }
            return listGhiTang;

        }

    }
}
