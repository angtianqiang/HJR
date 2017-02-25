using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;
using System.Linq;


namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 饰品CollectionViewModel : CollectionViewModel<饰品, ZtxDB, long>
    {
        public static 饰品CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品CollectionViewModel());
        }
        protected 饰品CollectionViewModel() : base(DbFactory.Instance, x => x.饰品s, query => query.OrderBy(x=>x.品名), x => x.ID)
        {

        }
    }
}