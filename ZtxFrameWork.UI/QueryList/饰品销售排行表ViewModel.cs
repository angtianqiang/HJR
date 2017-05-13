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
    public class 饰品销售排行表ViewModel : DynamicQueryCollectionViewModel<销售单明细>
    {
        public static 饰品销售排行表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品销售排行表ViewModel());
        }

        protected 饰品销售排行表ViewModel() : base("饰品销售排行表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 饰品销售排行表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<销售单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().销售单明细s
                .Where(AdvancedExpression)
                 .Where(t => t.销售单.备注 != "N")
                .GroupBy(x => new
                {
                    x.饰品.编号,
                    x.饰品.品名,
                    x.饰品.饰品类型.名称
                })
                .Select(t => new
                {
                    饰品编号 = t.Key.编号,
                    饰品品名 = t.Key.品名,
                    饰品类型 = t.Key.名称,
                    数量 = t.Sum(a => a.数量),
                    重量 = t.Sum(a => a.重量),
                    金额 = t.Sum(a => a.金额)
                })
                .OrderByDescending(x=>x.金额)
                .ToList<dynamic>();
        }
    }
}