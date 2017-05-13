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
    public class 销售退货单明细表ViewModel : DynamicQueryCollectionViewModel<销售退货单明细>
    {
        public static 销售退货单明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售退货单明细表ViewModel());
        }

        protected 销售退货单明细表ViewModel() : base("销售退货单明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 销售退货单明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<销售退货单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().销售退货单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.销售退货单.编号,
                    供应商 = t.销售退货单.会员.姓名,
                    日期 = t.销售退货单.日期,
                    饰品编号 = t.销售单明细.饰品.编号,
                    饰品类型 = t.销售单明细.饰品.饰品类型.名称,
                    数量 = t.数量,
                    重量 = t.重量,
                    销售价 = t.销售价,
                    金额 = t.金额,                   
                    状态 = t.销售退货单.状态,
                    备注 = t.销售退货单.备注
                })
                .ToList<dynamic>();
        }
    }
}