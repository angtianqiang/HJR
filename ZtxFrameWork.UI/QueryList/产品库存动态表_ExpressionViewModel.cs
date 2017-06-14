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
using ZtxFrameWork.UI.Comm.DataModel;
using System.Linq;
using System.Data.Entity;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 产品库存动态表_ExpressionViewModel : EntityExpressionBuilder<入库单明细>
    {
        public static 产品库存动态表_ExpressionViewModel Create(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            return ViewModelSource.Create(() => new 产品库存动态表_ExpressionViewModel(hiddenProperties, additionalProperties));
        }

        protected 产品库存动态表_ExpressionViewModel(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties) : base(hiddenProperties, additionalProperties)
        {
            this.AdvancedExpression = CriteriaOperatorToExpressionConverter.GetEntityFrameworkWhere<入库单明细>(this.AdvancedCriteria);
            Expression<Func<入库单明细, bool>> d;
         
           
                d = (x) => x.入库单.分店ID == 分店ID;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
           
        }
        public virtual long 分店ID { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        protected override void SetLoadData()
        {
            base.SetLoadData();
            分店Source= Helpers.CacheHelper.分店Source;
            分店ID = App.Current分店.ID;
        }
    }
}