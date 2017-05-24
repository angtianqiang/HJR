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
                    ID = x.饰品.ID,
                    图片 = x.饰品.饰品图片.图片,
                    编号 =   x.饰品.编号,
                    品名=  x.饰品.品名.名称,
                    饰品类型 = x.饰品.饰品类型.名称,
                    材质 =   x.饰品.材质.名称,
                    电镀方式=  x.饰品.电镀方式.名称,
                    石头颜色=   x.饰品.石头颜色.名称,
                    单位=  x.饰品.单位.名称,
                    重量单位=     x.饰品.重量单位.名称,
                })
                .Select(t => new
                {
                    ID = t.Key.ID,
                    图片 = t.Key.图片,

                    编号 =t.Key.编号,
                    品名 = t.Key.品名,
                    饰品类型 = t.Key.饰品类型,
                    材质  = t.Key.材质,
                    电镀方式  = t.Key.电镀方式,
                    石头颜色 = t.Key.石头颜色,
                    单位 = t.Key.单位,
                    重量单位 = t.Key.重量单位,
                    数量 = t.Sum(a => a.数量),
                    重量 = t.Sum(a => a.重量),
                    金额 = t.Sum(a => a.金额)
                })
                .OrderByDescending(x=>x.金额)
                .ToList<dynamic>();
        }
    }
}