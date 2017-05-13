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
    public class 员工业绩排行表_ExpressionViewModel : EntityExpressionBuilder<销售单明细>
    {
        public static 员工业绩排行表_ExpressionViewModel Create(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            return ViewModelSource.Create(() => new 员工业绩排行表_ExpressionViewModel(hiddenProperties, additionalProperties));
        }

        protected 员工业绩排行表_ExpressionViewModel(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties) : base(hiddenProperties, additionalProperties)
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
            会员ID = 操作员ID = 分店ID = 供应商ID = null;
        }
        protected override void BuilderExpression()
        {
            this.AdvancedExpression = CriteriaOperatorToExpressionConverter.GetEntityFrameworkWhere<销售单明细>(this.AdvancedCriteria);
            Expression<Func<销售单明细, bool>> d;
            if (操作员ID.HasValue)
            {
                d = (x) => x.销售单.操作员ID == 操作员ID.Value;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }
            if (分店ID.HasValue)
            {
                d = (x) => x.销售单.分店ID == 分店ID.Value;
                this.AdvancedExpression = this.AdvancedExpression.And(d);
            }
            //if (会员ID.HasValue)
            //{
            //    d = (x) => x.销售单.会员ID == 会员ID.Value;
            //    this.AdvancedExpression = this.AdvancedExpression.And(d);
            //}
            //if (供应商ID.HasValue)
            //{
            //    d = (x) => x.销售单.供应商ID == 供应商ID.Value;
            //    this.AdvancedExpression = this.AdvancedExpression.And(d);
            //}
            d = (x) => x.销售单.日期 >= 开始日期 && x.销售单.日期 <= 结束日期;
            this.AdvancedExpression = this.AdvancedExpression.And(d);
        }

        public virtual DateTime 开始日期 { get; set; }
        public virtual DateTime 结束日期 { get; set; }
        public virtual long? 会员ID { get; set; }
        public virtual long? 操作员ID { get; set; }
        public virtual long? 分店ID { get; set; }
        public virtual long? 供应商ID { get; set; }


        // public virtual List<会员> 会员Source { get; set; }
         public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        //  public virtual List<供应商> 供应商Source { get; set; }

        public async void Init()
        {
            //  var t1 = DbFactory.Instance.CreateDbContext().会员s.OrderBy(t => t.编号).ToListAsync();
              var t2 = DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
            var t3 = DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
            //  var t4 = DbFactory.Instance.CreateDbContext().供应商s.OrderBy(t => t.简称).ToListAsync();

            //    会员Source = await t1;
             操作员Source = await t2;
            分店Source = await t3;
            //  供应商Source = await t4;
        }
    }
}


