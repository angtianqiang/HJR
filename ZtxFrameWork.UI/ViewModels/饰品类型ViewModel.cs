using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using System.Collections.Generic;
using ZtxFrameWork.UI.Comm.DataModel;
using DevExpress.Mvvm.POCO;
using System.Linq;
namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 饰品类型ViewModel : SingleObjectViewModel<饰品类型, ZtxDB, long>
    {
        public static 饰品类型ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品类型ViewModel());
        }
        
        protected 饰品类型ViewModel() : base(DbFactory.Instance, x => x.饰品类型s, x=>x.ID, x => x.名称, "饰品类型")
        {
           var db = DB;
            类别Source = db.饰品类别s.OrderBy(t=>t.排序号).ToList();


        }
        public virtual List<饰品类别> 类别Source { get; set; }
    }
}