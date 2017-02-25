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
    public class 重量单位CollectionViewModel : CollectionViewModel<重量单位, ZtxDB, long>
    {
        public static 重量单位CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 重量单位CollectionViewModel());
        }
        protected 重量单位CollectionViewModel() : base(DbFactory.Instance, x => x.重量单位s, query => query.OrderBy(x=>x.排序号), x => x.ID)
        {

        }
    }
}