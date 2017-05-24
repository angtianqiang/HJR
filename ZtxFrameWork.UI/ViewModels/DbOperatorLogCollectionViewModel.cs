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
    public class DbOperatorLogCollectionViewModel : CollectionViewModel<DbOperatorLog, ZtxDB, long>
    {
        public static DbOperatorLogCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new DbOperatorLogCollectionViewModel());
        }
        protected DbOperatorLogCollectionViewModel() : base(DbFactory.Instance, x => x.DbOperatorLogs, query => query.OrderByDescending(x=>x.OperatorOn).Take(App.ViewTopCount), x => x.ID,permissionTitle: "单据状态更改日志")
        {

        }
        static public void InitEntity(DbOperatorLog NewEntity)
        { }

        public override bool CanDelete(DbOperatorLog projectionEntity)
        {
            return base.CanDelete(projectionEntity);
        }

    }
}