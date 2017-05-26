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
    public class 销售单CollectionViewModel : CollectionViewModel<销售单, ZtxDB, long>
    {
        public static 销售单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售单CollectionViewModel());
        }
        protected 销售单CollectionViewModel() : base(DbFactory.Instance, x => x.销售单s, query => query.Include(t => t.会员).Include(t => t.分店).Include(t => t.操作员).OrderByDescending(x => x.编号).Take(App.ViewTopCount), x =>x.ID,t=>InitEntity(t), permissionTitle: "销售单")
        {

        }
        private Action<销售单> b = InitEntity;
        static public async void InitEntity(销售单 NewEntity)
        {
            var t1 = GetNewCode("XS", DbFactory.Instance, x => x.销售单s, t => t.编号);
            NewEntity.日期 = DateTime.Now;
            NewEntity.操作员ID = App.CurrentUser.ID;
            NewEntity.状态 = "N";
            NewEntity.分店ID = App.Current分店.ID;
            NewEntity.编号 = await t1;
        }

        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 销售单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            if (!dbContext.Entry(entity).Collection(t => t.销售单明细s).IsLoaded)
            {
                dbContext.Entry(entity).Collection(t => t.销售单明细s).Load();
            }
            dbContext.销售单明细s.RemoveRange(entity.销售单明细s);
        }
        #endregion


    }
}