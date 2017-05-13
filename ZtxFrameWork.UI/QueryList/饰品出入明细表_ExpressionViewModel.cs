using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data.Model;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DevExpress.Data.Utils;
using System.Linq.Expressions;
using ZtxFrameWork.UI.Comm.Utils;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 饰品出入明细表_ExpressionViewModel : EntityExpressionBuilder<库存出入明细>
    {
        public static 饰品出入明细表_ExpressionViewModel Create(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            return ViewModelSource.Create(() => new 饰品出入明细表_ExpressionViewModel(hiddenProperties, additionalProperties));
        }

    protected 饰品出入明细表_ExpressionViewModel(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties) : base(hiddenProperties, additionalProperties)
        {
          
        }

    protected override bool DataValidate()
    {
        return base.DataValidate();
    }
    protected override void SetLoadData()
    {
        base.SetLoadData();
        StartCode = null;
            开始日期 = Helpers.DateTimeHelper.GetCurrentMonthFistDay();
            结束日期 = Helpers.DateTimeHelper.GetCurrentMonthCurrentDay();
    }
    protected override void BuilderExpression()
    {
        this.AdvancedExpression = CriteriaOperatorToExpressionConverter.GetEntityFrameworkWhere<库存出入明细>(this.AdvancedCriteria);

        if (!string.IsNullOrEmpty(StartCode))
        {
            Expression<Func<库存出入明细, bool>> d = (x) => x.饰品.编号.StartsWith(StartCode);
            this.AdvancedExpression = this.AdvancedExpression.And(d);
        }
            Expression<Func<库存出入明细, bool>> d2 = (x) => x.日期 >= 开始日期;
            Expression<Func<库存出入明细, bool>> d3 = (x) => x.日期 <= 结束日期;
            this.AdvancedExpression = this.AdvancedExpression.And(d2);
            this.AdvancedExpression = this.AdvancedExpression.And(d3);
        }

    public virtual string StartCode { get; set; }
        public virtual DateTime 开始日期 { get; set; }
        public virtual DateTime 结束日期 { get; set; }
    }
}