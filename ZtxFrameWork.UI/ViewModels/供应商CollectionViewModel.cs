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
    public class 供应商CollectionViewModel : CollectionViewModel<供应商, ZtxDB, long>
    {
        public static 供应商CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 供应商CollectionViewModel());
        }
        protected 供应商CollectionViewModel() : base(DbFactory.Instance, x => x.供应商s, query => query.OrderBy(x=>x.编号), x => x.ID, t => InitEntity(t), permissionTitle: "供应商")
        {

        }
        static public void InitEntity(供应商 NewEntity)
        { }
    }
}