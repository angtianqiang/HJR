using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;
using System.Linq;
using System.Data.Entity;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 调拨单CollectionViewModel : CollectionViewModel<调拨单, ZtxDB, long>
    {
        public static 调拨单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 调拨单CollectionViewModel());
        }
        protected 调拨单CollectionViewModel() : base(DbFactory.Instance, x => x.调拨单s, query => query.OrderBy(x=>x.编号), x =>x.ID,t=>InitEntity(t), permissionTitle: "调拨单")
        {

        }
        private Action<调拨单> b = InitEntity;
        static public void InitEntity(调拨单 NewEntity)
        {
            NewEntity.编号 = GetNewCode("RK", DbFactory.Instance, x => x.调拨单s, t => t.编号);
            //NewEntity.日期 = DateTime.Now;
            //NewEntity.操作员ID = App.CurrentUser.ID;
            //NewEntity.状态 = "N";
        }




    }
}