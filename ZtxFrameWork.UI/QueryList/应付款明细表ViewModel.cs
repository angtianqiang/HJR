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
    public class 应付款明细表ViewModel : DynamicQueryCollectionViewModel<入库单>
    {
        public static 应付款明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 应付款明细表ViewModel());
        }

        protected 应付款明细表ViewModel() : base("应付款明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 应付款明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<入库单, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().入库单s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.编号,
                    分店 = t.分店.名称,
                    供应商 = t.供应商.简称,
                    日期 = t.日期,
                    总金额 = t.总金额,
                    已付金额 = t.已付金额,
                    未付金额 = t.未付金额,                  
                    状态 = t.状态,
                    备注 = t.备注
                })
                .ToList<dynamic>();
        }
    }
}