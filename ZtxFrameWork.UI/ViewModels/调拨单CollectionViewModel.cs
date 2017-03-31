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
        protected 调拨单CollectionViewModel() : base(DbFactory.Instance, x => x.调拨单s, query => query.Include(t => t.源分店).Include(t => t.目标分店).Include(t => t.调拨员).Include(t=>t.签收员).OrderBy(x=>x.编号), x =>x.ID,t=>InitEntity(t), permissionTitle: "调拨单")
        {

        }
        private Action<调拨单> b = InitEntity;
        static public void InitEntity(调拨单 NewEntity)
        {
            NewEntity.编号 = GetNewCode("DB", DbFactory.Instance, x => x.调拨单s, t => t.编号);
             NewEntity.日期 = DateTime.Now;
            NewEntity.调拨员ID = App.CurrentUser.ID;
       
            NewEntity.状态 = "N";
        }


        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 调拨单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.调拨单明细s.RemoveRange(entity.调拨单明细s);
        }
        #endregion

    }
}