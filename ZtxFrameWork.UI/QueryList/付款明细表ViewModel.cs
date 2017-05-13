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
    public class 付款明细表ViewModel : DynamicQueryCollectionViewModel<付款单明细>
    {
        public static 付款明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 付款明细表ViewModel());
        }

        protected 付款明细表ViewModel() : base("付款明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 付款单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 付款明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<付款单明细, bool>> AdvancedExpression)
        {


            return DbFactory.Instance.CreateDbContext().付款单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    编号 = t.入库单.编号,
                    供应商 = t.入库单.供应商.简称,
                    日期 = t.付款单.日期,
                    应付金额 = t.应付金额,
                    本次支付金额 = t.本次支付金额,
                    分店 = t.付款单.分店.名称,
                    操作员 = t.付款单.操作员.DispalyName,
                    状态 = t.付款单.状态
                 
                })
                .ToList<dynamic>();
        }
    }


}