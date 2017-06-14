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
    public class 销售单明细表ViewModel : DynamicQueryCollectionViewModel<销售单明细>
    {
        public static 销售单明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售单明细表ViewModel());
        }

        protected 销售单明细表ViewModel() : base("销售单明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 销售单明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<销售单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().销售单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.销售单.编号,
                    分店 = t.销售单.分店.名称,
                    会员 = t.销售单.会员.姓名,
                    饰品编号 = t.饰品.编号,
                    日期 = t.销售单.日期,
                    品名 = t.饰品.品名.名称,
                    材质 = t.饰品.材质.名称,
                    电镀方式 = t.饰品.电镀方式.名称,
                    石头颜色 = t.饰品.石头颜色.名称,
                    单位 = t.饰品.单位.名称,
                    重量单位 = t.饰品.重量单位.名称,
                    数量 = t.数量,
                    重量 = t.重量,
                    折扣 = t.折扣,
                    折后销售价 = System.Math.Round(t.销售价 * t.折扣, 2),
                    当时金属价 = t.当时金属价,
                    金额 = t.金额,
                    状态 = t.销售单.状态,
                    备注 = t.销售单.备注
                })
                .ToList<dynamic>();
        }
    }
}