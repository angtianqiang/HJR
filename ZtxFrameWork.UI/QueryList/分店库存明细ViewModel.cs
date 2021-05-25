using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data.Model;
using System.Linq.Expressions;
using ZtxFrameWork.UI.Comm.DataModel;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 分店库存明细ViewModel : ISupportParameter
    {


        public static 分店库存明细ViewModel Create()
        {
            return ViewModelSource.Create(() => new 分店库存明细ViewModel());
        }

        protected 分店库存明细ViewModel()
        { }
        public virtual List<dynamic> Entities { get; set; }

        public virtual object Parameter { get; set; }
        protected virtual async void OnParameterChanged()
        {
            if (int.TryParse(Parameter.ToString(), out var id))
            {
                //Entities = DbFactory.Instance.CreateDbContext().库存s.Include(t => t.分店).Where(t => t.饰品ID == id)
                //          .Select(t => new { 分店 = t.分店.名称, 数量 = t.数量, 重量 = t.重量, 在途重量出 = 0m, 在途数量出 = 0, 在途重量入 = 0m, 在途数量入 = 0 })
                //          .ToList<dynamic>();

            //    Mouse.OverrideCursor = Cursors.Wait;

                var db = DbFactory.Instance.CreateDbContext();

                var aa = from a1 in db.调拨单s
                         join a2 in db.调拨单明细s
                         on a1.ID equals a2.调拨单ID into a3
                         where a1.状态 == "N"
                         from a4 in a3
                         group a4 by new { a4.饰品ID, a4.调拨单.源分店ID } into a5
                         select new { id = a5.Key.饰品ID, 分店id = a5.Key.源分店ID, 在途重量出 = a5.Sum(t => t.重量), 在途数量出 = a5.Sum(t => t.数量) };
                var bb = from b1 in db.调拨单s
                         join b2 in db.调拨单明细s
                         on new { ID = b1.ID, 状态 = b1.状态 } equals new { ID = b2.调拨单ID, 状态 = "N" } into b3
                         from b4 in b3
                         group b4 by new { b4.饰品ID, b4.调拨单.目标分店ID } into b5
                         select new { id = b5.Key.饰品ID, 分店id = b5.Key.目标分店ID, 在途重量入 = b5.Sum(t => t.重量), 在途数量入 = b5.Sum(t => t.数量) };
                var cc = from c1 in db.库存s
                         join c2 in db.分店s on c1.分店ID equals c2.ID
                         select new
                         {
                             饰品ID = c1.饰品ID,
                             分店id = c1.分店ID,
                             分店 = c2.名称,
                             数量 = c1.数量,
                             重量 = c1.重量
                         };
                var dd = from c in cc
                         join b in bb
                          on new { 饰品ID = c.饰品ID, 分店id = c.分店id } equals new { 饰品ID = b.id, 分店id = b.分店id } into d1
                         from d2 in d1.DefaultIfEmpty()
                         join a in aa
                         on new { 饰品ID = c.饰品ID, 分店id = c.分店id } equals new { 饰品ID = a.id, 分店id = a.分店id } into d3
                         where c.饰品ID == id

                         from d4 in d3.DefaultIfEmpty()
                         

                         
                           
                         select new
                         {
                             分店 = c.分店,
                             数量 = c.数量,
                             重量 = c.重量,
                             在途重量出 = d4 == null ? 0m : d4.在途重量出,
                             在途数量出 = d4 == null ? 0: d4.在途数量出,
                             在途重量入 = d2 == null ? 0m : d2.在途重量入,
                             在途数量入 = d2 == null ? 0 : d2.在途数量入
                         };

                Entities =await dd.ToListAsync<dynamic>();

            //    Mouse.OverrideCursor = null;

            }
        }
    }


}
