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
    public class 入库单CollectionViewModel : CollectionViewModel<入库单, ZtxDB, long>
    {
        public static 入库单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 入库单CollectionViewModel());
        }
        protected 入库单CollectionViewModel() : base(DbFactory.Instance, x => x.入库单s, query => query.Include(t => t.供应商).Include(t => t.分店).Include(t => t.操作员).OrderBy(x => x.编号), x => x.ID, t => InitEntity(t), permissionTitle: "采购入库单")
        {

        }
        private Action<入库单> b = InitEntity;
        static public void InitEntity(入库单 NewEntity)
        {
            NewEntity.编号 = GetNewCode("RK", DbFactory.Instance, x => x.入库单s, t => t.编号);
            NewEntity.日期 = DateTime.Now;
            NewEntity.操作员ID = App.CurrentUser.ID;
            NewEntity.状态 = "N";
        }

        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 入库单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.入库单明细s.RemoveRange(entity.入库单明细s);
        }
        #endregion


    }
}