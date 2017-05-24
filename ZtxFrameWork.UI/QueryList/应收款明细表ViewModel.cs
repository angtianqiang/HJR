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
    public class 应收款明细表ViewModel : DynamicQueryCollectionViewModel<销售单>
    {
        public static 应收款明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 应收款明细表ViewModel());
        }

        protected 应收款明细表ViewModel() : base("应收款明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 应收款明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<销售单, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().销售单s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    销售编号 = t.编号,
                    分店=t.分店.名称,
                    会员 = t.会员.姓名,
                    日期 = t.日期,
                    数量 = t.销售单明细s.Sum(p => p.数量),
                    重量 = t.销售单明细s.Sum(p=>p.重量),
                    总金额 = t.总金额,
                    已收金额 = t.已收金额,
                    未收金额 = t.未收金额,  
                 
                    状态 = t.状态,
                    备注 = t.备注
                })
                .ToList<dynamic>();
        }
    }
}