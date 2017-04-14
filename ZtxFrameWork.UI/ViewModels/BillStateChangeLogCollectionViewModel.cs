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
    public class BillStateChangeLogCollectionViewModel : CollectionViewModel<BillStateChangeLog, ZtxDB, long>
    {
        public static BillStateChangeLogCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new BillStateChangeLogCollectionViewModel());
        }
        protected BillStateChangeLogCollectionViewModel() : base(DbFactory.Instance, x => x.BillStateChangeLogs, query => query.OrderByDescending(x=>x.ChangeOn), x => x.ID,permissionTitle: "单据状态更改日志")
        {

        }
        static public void InitEntity(BillStateChangeLog NewEntity)
        { }

       

    }
}