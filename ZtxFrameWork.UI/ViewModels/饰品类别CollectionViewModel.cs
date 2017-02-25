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
    public class 饰品类别CollectionViewModel : CollectionViewModel<饰品类别, ZtxDB, long>
    {
        public static 饰品类别CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品类别CollectionViewModel());
        }
        protected 饰品类别CollectionViewModel() : base(DbFactory.Instance, x => x.饰品类别s, query => query.OrderBy(x=>x.排序号), x => x.ID)
        {

        }
    }
}