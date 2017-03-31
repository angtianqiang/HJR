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
    public class 饰品提成ViewModel : SingleObjectViewModel<饰品提成, ZtxDB, long>
    {
        public static 饰品提成ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品提成ViewModel());
        }
        protected 饰品提成ViewModel() : base(DbFactory.Instance, x => x.饰品提成s, x=>x.ID, x => x.饰品.品名, "饰品提成")
        {
           var db = DB;
            饰品Source = db.饰品s.OrderBy(t => t.编号).ToList();

        }
        public virtual List<饰品> 饰品Source { get; set; }
    }
}