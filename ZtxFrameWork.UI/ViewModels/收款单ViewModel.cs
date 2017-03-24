using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using System.Collections.Generic;
using ZtxFrameWork.UI.Comm.DataModel;
using DevExpress.Mvvm.POCO;
using System.Linq;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 收款单ViewModel : SingleObjectViewModel<收款单, ZtxDB, long>
    {
        public static 收款单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 收款单ViewModel());
        }
        protected 收款单ViewModel() : base(DbFactory.Instance, x => x.收款单s, x=>x.ID, x => x.编号, "收款单")
        {
         

        }
        public override void UpdateIsReadOnly()
        {
            IsReadOnly = true;
        }
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 收款单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            //    dbContext.收款单明细s.RemoveRange(entity.付款单明细s);
            if (entity.销售单ID > 0)//销售单
            {
                var temp = dbContext.销售单s.Where(t => t.ID == entity.销售单ID).Single();
                temp.已收金额 -= entity.金额;
                temp.未收金额 = temp.总金额 - temp.已收金额;
            }
            else//退货单
            {
                var temp = dbContext.销售退货单s.Where(t => t.ID == entity.销售退货单ID).Single();
                temp.已付金额 += entity.金额*-1m;
                temp.未付金额 = temp.总金额 - temp.已付金额;
            }
        }
        #endregion

    }
}