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
    public class 调拨明细表ViewModel : DynamicQueryCollectionViewModel<调拨单明细>
    {
        public static 调拨明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 调拨明细表ViewModel());
        }

        protected 调拨明细表ViewModel() : base("调拨明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 调拨明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<调拨单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().调拨单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.调拨单.编号,
                    日期 = t.调拨单.日期,
                    源分店 = t.调拨单.源分店.名称,
                    目标分店 = t.调拨单.目标分店.名称,                 
                    饰品编号 = t.饰品.编号,
                    饰品类型 = t.饰品.饰品类型.名称,
                    数量 = t.数量,
                    重量 = t.重量,
                    调拨员 = t.调拨单.调拨员.DispalyName,
                    签收员 = t.调拨单.签收员.DispalyName,                  
                    状态 = t.调拨单.状态,
                    备注 = t.调拨单.备注
                })
                .ToList<dynamic>();
        }
    }
}