using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data.Model;
using System.Linq.Expressions;
using ZtxFrameWork.UI.Comm.DataModel;
using System.Linq;
using System.Data.Entity;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 盈亏单明细表ViewModel : DynamicQueryCollectionViewModel<盈亏单明细>
    {
        public static 盈亏单明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 盈亏单明细表ViewModel());
        }

        protected 盈亏单明细表ViewModel() : base("盈亏单明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 盈亏单明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<盈亏单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().盈亏单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.盈亏单.编号,                 
                    日期 = t.盈亏单.日期,
                    饰品编号 = t.饰品.编号,
                    饰品类型 = t.饰品.饰品类型.名称,
                    盈亏数量 = t.盈亏数量,
                    盈亏重量 = t.盈亏重量,
                    盈亏金额 = t.盈亏金额,                   
                    状态 = t.盈亏单.状态,
                    备注 = t.盈亏单.备注
                })
                .ToList<dynamic>();
        }
    }
}