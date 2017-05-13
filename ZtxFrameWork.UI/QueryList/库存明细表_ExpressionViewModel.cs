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
    public class 库存明细表_ExpressionViewModel : EntityExpressionBuilder<饰品>
    {
        public static 库存明细表_ExpressionViewModel Create(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            return ViewModelSource.Create(() => new 库存明细表_ExpressionViewModel(hiddenProperties, additionalProperties));
        }

        protected 库存明细表_ExpressionViewModel(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties) : base(hiddenProperties, additionalProperties)
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
        }
        protected override void BuilderExpression()
        {
            this.AdvancedExpression = CriteriaOperatorToExpressionConverter.GetEntityFrameworkWhere<饰品>(this.AdvancedCriteria);

            if (!string.IsNullOrEmpty(StartCode))
            {
                Expression<Func<饰品, bool>> d = (x) => x.编号.StartsWith(StartCode);
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }
        }

        public virtual string StartCode { get; set; }
    }
}