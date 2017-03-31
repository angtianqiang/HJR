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
    public class 销售退货单CollectionViewModel : CollectionViewModel<销售退货单, ZtxDB, long>
    {
        public static 销售退货单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售退货单CollectionViewModel());
        }
        protected 销售退货单CollectionViewModel() : base(DbFactory.Instance, x => x.销售退货单s, query => query.Include(t=>t.会员).Include(t=>t.分店).Include(t=>t.操作员).OrderBy(x=>x.编号), x =>x.ID,t=>InitEntity(t), permissionTitle: "销售退货单")
        {

        }
        private Action<销售退货单> b = InitEntity;
        static public void InitEntity(销售退货单 NewEntity)
        {
            NewEntity.编号 = GetNewCode("TH", DbFactory.Instance, x => x.销售退货单s, t => t.编号);
          NewEntity.日期 = DateTime.Now;
            NewEntity.操作员ID = App.CurrentUser.ID;
            NewEntity.状态 = "N";
        }

        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 销售退货单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.销售退货单明细s.RemoveRange(entity.销售退货单明细s);
        }
        #endregion
        //20170321退货单由销售单生成 ，用户不能自己添加
        //public override bool CanNew()
        //{
        //    if (this.IsInDesignMode()) return true;
        //    return false;
        //}

    }
}