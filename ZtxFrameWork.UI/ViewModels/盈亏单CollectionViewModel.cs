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
    public class 盈亏单CollectionViewModel : CollectionViewModel< 盈亏单, ZtxDB, long>
    {
        public static  盈亏单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new  盈亏单CollectionViewModel());
        }
        protected  盈亏单CollectionViewModel() : base(DbFactory.Instance, x => x. 盈亏单s, query => query.Include(t=>t.分店).Include(t=>t.操作员).OrderByDescending(x => x.编号).Take(App.ViewTopCount), x =>x.ID,t=>InitEntity(t), permissionTitle: "盈亏单")
        {

        }
        private Action< 盈亏单> b = InitEntity;
        static public async void InitEntity( 盈亏单 NewEntity)
        {
            var t1 = GetNewCode("YK", DbFactory.Instance, x => x.盈亏单s, t => t.编号);
            NewEntity.日期 = DateTime.Now;
            NewEntity.操作员ID = App.CurrentUser.ID;
            NewEntity.状态 = "N";
            NewEntity.分店ID = App.Current分店.ID;
            NewEntity.编号 = await t1;

        }

        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 盈亏单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            if (!dbContext.Entry(entity).Collection(t => t.盈亏单明细s).IsLoaded)
            {
                dbContext.Entry(entity).Collection(t => t.盈亏单明细s).Load();
            }
            dbContext.盈亏单明细s.RemoveRange(entity.盈亏单明细s);
        }
        #endregion


    }
}