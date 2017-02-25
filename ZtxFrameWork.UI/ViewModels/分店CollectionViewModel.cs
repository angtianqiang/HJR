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
    public class 分店CollectionViewModel : CollectionViewModel<分店, ZtxDB, long>
    {
        public static 分店CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 分店CollectionViewModel());
        }
        protected 分店CollectionViewModel() : base(DbFactory.Instance, x => x.分店s, query => query.OrderBy(x=>x.编号), x => x.ID)
        {

        }
    }
}