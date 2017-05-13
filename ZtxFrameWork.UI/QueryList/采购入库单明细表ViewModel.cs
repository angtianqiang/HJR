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
    public class 采购入库单明细表ViewModel : DynamicQueryCollectionViewModel<入库单明细>
    {
        public static 采购入库单明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 采购入库单明细表ViewModel());
        }
        protected 采购入库单明细表ViewModel() : base("采购入库单明细表")
        {
            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 入库单明细().ID)
            };
            AdditionalProperties = null;
            this.EntityExpressionViewModel = 采购入库单明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }
        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<入库单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().入库单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.入库单.编号,
                    供应商 = t.入库单.供应商.简称,
                    日期 = t.入库单.日期,
                    饰品编号 = t.饰品.编号,
                    饰品类型 = t.饰品.饰品类型.名称,
                    数量 = t.数量,
                    重量 = t.重量,
                    计价方式 = t.计价方式,
                    金额 = t.金额,
                    状态 = t.入库单.状态,
                    备注 = t.入库单.备注
                })
                .ToList<dynamic>();
        }
    }
}