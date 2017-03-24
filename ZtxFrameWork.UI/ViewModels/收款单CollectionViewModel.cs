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

        public override bool CanNew()
        {
            if (this.IsInDesignMode()) return true;
            return false;
        }
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 收款单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            //    dbContext.收款单明细s.RemoveRange(entity.付款单明细s);
            if (entity.销售单ID>0)//销售单
            {
                var temp = dbContext.销售单s.Where(t => t.ID == entity.销售单ID).Single();
                temp.已收金额 -= entity.金额;
                temp.未收金额 = temp.总金额 - temp.已收金额;
            }
            else//退货单
            {
                var temp = dbContext.销售退货单s.Where(t => t.ID == entity.销售退货单ID).Single();
                temp.已付金额+= entity.金额;
                temp.未付金额 = temp.总金额 - temp.已付金额;
            }
        }
        #endregion
    }
}