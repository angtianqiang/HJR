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
    public class 盘点表CollectionViewModel : CollectionViewModel<盘点表, ZtxDB, long>
    {
        public static 盘点表CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 盘点表CollectionViewModel());
        }
        protected 盘点表CollectionViewModel() : base(DbFactory.Instance, x => x.盘点表s, query => query.OrderBy(x=>x.编号), x =>x.ID,t=>InitEntity(t), permissionTitle: "盘点表")
        {

        }
        private Action<盘点表> b = InitEntity;
        static public void InitEntity(盘点表 NewEntity)
        {
            NewEntity.编号 = GetNewCode("RK", DbFactory.Instance, x => x.盘点表s, t => t.编号);
         //   NewEntity.日期 = DateTime.Now;
            NewEntity.操作员ID = App.CurrentUser.ID;
            NewEntity.状态 = "N";
        }




    }
}