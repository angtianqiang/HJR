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
    public class 调拨明细表_ExpressionViewModel : EntityExpressionBuilder<调拨单明细>
    {
        public static 调拨明细表_ExpressionViewModel Create(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            return ViewModelSource.Create(() => new 调拨明细表_ExpressionViewModel(hiddenProperties, additionalProperties));
        }

        protected 调拨明细表_ExpressionViewModel(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties) : base(hiddenProperties, additionalProperties)
        {
            Init();
        }

        protected override bool DataValidate()
        {
            return base.DataValidate();
        }
        protected override void SetLoadData()
        {
            base.SetLoadData();
            开始日期 = Helpers.DateTimeHelper.GetCurrentMonthFistDay();
            结束日期 = Helpers.DateTimeHelper.GetCurrentMonthCurrentDay();
            调拨员ID = 签收员ID = 源分店ID = 目标分店ID = null;
        }
        protected override void BuilderExpression()
        {
            this.AdvancedExpression = CriteriaOperatorToExpressionConverter.GetEntityFrameworkWhere<调拨单明细>(this.AdvancedCriteria);
            Expression<Func<调拨单明细, bool>> d;
            if (调拨员ID.HasValue)
            {
                d = (x) => x.调拨单.调拨员ID == 调拨员ID.Value;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }
            if (签收员ID.HasValue)
            {
                d = (x) => x.调拨单.签收员ID == 签收员ID.Value;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }
            //if (会员ID.HasValue)
            //{
            //    d = (x) => x.销售退货单.会员ID == 会员ID.Value;
            //    this.AdvancedExpression = this.AdvancedExpression.And(d);
            //}
            if (源分店ID.HasValue)
            {
                d = (x) => x.调拨单.源分店ID == 源分店ID.Value;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }
            if (目标分店ID.HasValue)
            {
                d = (x) => x.调拨单.目标分店ID == 目标分店ID.Value;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }

            d = (x) => x.调拨单.日期 >= 开始日期 && x.调拨单.日期 <= 结束日期;
            this.AdvancedExpression = this.AdvancedExpression.And(d);
        }

        public virtual DateTime 开始日期 { get; set; }
        public virtual DateTime 结束日期 { get; set; }
        public virtual long? 调拨员ID { get; set; }
        public virtual long? 签收员ID { get; set; }
        public virtual long? 源分店ID { get; set; }
        public virtual long? 目标分店ID { get; set; }


         public virtual List<User> 调拨员Source { get; set; }
        public virtual List<User> 签收员Source { get; set; }
        public virtual List<分店> 源分店Source { get; set; }
        public virtual List<分店> 目标分店Source { get; set; }

        public async void Init()
        {
           
            var t2 = DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
            var t3 = DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();



            调拨员Source= 签收员Source = await t2;
            源分店Source= 目标分店Source = await t3;
           
        }
    }
}