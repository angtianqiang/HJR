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
    public class 付款单CollectionViewModel : CollectionViewModel<付款单, ZtxDB, long>
    {
        public static 付款单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 付款单CollectionViewModel());
        }
        protected 付款单CollectionViewModel() : base(DbFactory.Instance, x => x.付款单s, query => query.Include(t => t.分店).Include(t => t.操作员).Include(t=>t.供应商).OrderBy(x=>x.编号), x =>x.ID,t=>InitEntity(t), permissionTitle: "付款单")
        {

        }
        private Action<付款单> b = InitEntity;
        static public async void InitEntity(付款单 NewEntity)
        {
         var t1=   GetNewCode("FK", DbFactory.Instance, x => x.付款单s, t => t.编号);
            NewEntity.日期 = DateTime.Now;
           NewEntity.操作员ID = App.CurrentUser.ID;
           NewEntity.状态 = "N";
            NewEntity.编号 = await t1;
        }

        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 付款单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.付款单明细s.RemoveRange(entity.付款单明细s);
        }
        #endregion


    }
}