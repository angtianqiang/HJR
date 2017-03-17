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
    public class 收款单CollectionViewModel : CollectionViewModel<收款单, ZtxDB, long>
    {
        public static 收款单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 收款单CollectionViewModel());
        }
        protected 收款单CollectionViewModel() : base(DbFactory.Instance, x => x.收款单s, query => query.OrderBy(x=>x.编号), x => x.ID, t => InitEntity(t), permissionTitle: "收款单")
        {

        }
        static public void InitEntity(收款单 NewEntity)
        { }
    }
}