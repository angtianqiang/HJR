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
    public class 饰品销售明细ViewModel : ISupportParameter
    {

        public static 饰品销售明细ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品销售明细ViewModel());
        }

        protected 饰品销售明细ViewModel()
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

                Entities = await DbFactory.Instance.CreateDbContext().销售单明细s.Where(t => t.饰品ID == id).Select(t => new {

                    销售日期=   t.销售单.日期,
                   销售单号 =t.销售单.编号,
                    操作员=  t.销售单.操作员.DispalyName,
                    会员=  t.销售单.会员.姓名,
                    分店=t.销售单.分店.名称,
                    数量= t.数量,
                    重量= t.重量,
                    金额= t.金额

                
                }).ToListAsync<dynamic>();


            

             

                //    Mouse.OverrideCursor = null;

            }
        }
    }


}
