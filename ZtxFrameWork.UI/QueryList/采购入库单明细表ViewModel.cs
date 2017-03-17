using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data.Model;
using System.Linq.Expressions;
using ZtxFrameWork.UI.Comm.DataModel;
using System.Linq;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 采购入库单明细表ViewModel : DynamicQueryCollectionViewModel<入库单>
    {
        public static 采购入库单明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 采购入库单明细表ViewModel());
        }

        protected 采购入库单明细表ViewModel():base("采购入库单明细表")
        {
         
            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 入库单().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 采购入库单明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<入库单, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().入库单s
                .Where(AdvancedExpression)
                .Select(t=>new {统号=t.编号,供应商=t.供应商.简称,日期=t.日期 })
                .ToList<dynamic>();
        }
    }


}