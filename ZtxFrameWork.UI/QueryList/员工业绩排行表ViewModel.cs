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
    public class 员工业绩排行表ViewModel : DynamicQueryCollectionViewModel<销售单明细>
    {
        public static 员工业绩排行表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 员工业绩排行表ViewModel());
        }

        protected 员工业绩排行表ViewModel() : base("员工业绩排行表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 员工业绩排行表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<销售单明细, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().销售单明细s
                .Where(AdvancedExpression)
                .Where(t => t.销售单.备注 !="N")
                .GroupBy(x => new
                {
                 
                    x.销售单.操作员.DispalyName
                 
                })
                .Select(t => new
                {
                 
                    员工姓名 = t.Key.DispalyName,                  
                    数量 = t.Sum(a => a.数量),
                    重量 = t.Sum(a => a.重量),
                    金额 = t.Sum(a => a.金额)
                })
                .OrderByDescending(x => x.金额)
                .ToList<dynamic>();
        }
    }
}