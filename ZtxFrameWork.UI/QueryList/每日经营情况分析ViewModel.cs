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
using DevExpress.XtraReports.UI;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 每日经营情况分析ViewModel : DynamicQueryCollectionViewModel<销售单明细>
    {
        public static 每日经营情况分析ViewModel Create()
        {
            return ViewModelSource.Create(() => new 每日经营情况分析ViewModel());
        }

        protected 每日经营情况分析ViewModel() : base("每日经营情况分析")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 每日经营情况分析_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }

        public virtual Data CurrentData { get; set; }
        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<销售单明细, bool>> AdvancedExpression)
        {
            CurrentData = ViewModelSource.Create(() => new Data()); 
            每日经营情况分析_ExpressionViewModel ExpressionViewModel = this.EntityExpressionViewModel as 每日经营情况分析_ExpressionViewModel;
            var dt = new DateTime(ExpressionViewModel.日期.Year, ExpressionViewModel.日期.Month, ExpressionViewModel.日期.Day);

            //销售
            Expression<Func<销售单明细, bool>> d1 = (t) => t.销售单.状态 != "N" && t.销售单.日期<= ExpressionViewModel.日期 && t.销售单.日期>=dt;
            if (ExpressionViewModel.分店ID.HasValue)
            {
                d1.And((t) => t.销售单.分店ID == ExpressionViewModel.分店ID.Value);
            }         

       var p1=    DbFactory.Instance.CreateDbContext().销售单明细s  .Where(d1).ToList();
            foreach (var item in p1)
            {
                CurrentData.当日销售笔数 +=1;
                CurrentData.当日销售数量 += item.数量;
                CurrentData.当日销售金额 += item.金额;
                CurrentData.当日销售重量 += item.重量;
            }
            //销售退库
            Expression<Func<销售退货单明细, bool>> d2 = (t) => t.销售退货单.状态 != "N" && t.销售退货单.日期 <= ExpressionViewModel.日期 && t.销售退货单.日期 >= dt;
            if (ExpressionViewModel.分店ID.HasValue)
            {
                d2.And((t) => t.销售退货单.分店ID == ExpressionViewModel.分店ID.Value);
            }

            var p2 = DbFactory.Instance.CreateDbContext().销售退货单明细s.Where(d2).ToList();
            foreach (var item in p2)
            {
                CurrentData.当日销售退货笔数 += 1;
                CurrentData.当日销售退货数量 += item.数量;
                CurrentData.当日销售退货金额 += item.金额;
                CurrentData.当日销售退货重量 += item.重量;
            }
            //采购入库
            Expression<Func<入库单明细, bool>> d3 = (t) => t.入库单.状态 != "N" && t.入库单.日期 <= ExpressionViewModel.日期 && t.入库单.日期 >= dt;
            if (ExpressionViewModel.分店ID.HasValue)
            {
                d3.And((t) => t.入库单.分店ID == ExpressionViewModel.分店ID.Value);
            }

            var p3 = DbFactory.Instance.CreateDbContext().入库单明细s.Where(d3).ToList();
            foreach (var item in p3)
            {
                CurrentData.当日采购入库笔数 += 1;
                CurrentData.当日采购入库数量 += item.数量;
                CurrentData.当日采购入库重量 += item.重量;
                CurrentData.当日采购入库金额 += item.金额;
            }

            //采购退货
            Expression<Func<退库单明细, bool>> d4 = (t) => t.退库单.状态 != "N" && t.退库单.日期 <= ExpressionViewModel.日期 && t.退库单.日期 >= dt;
            if (ExpressionViewModel.分店ID.HasValue)
            {
                d4.And((t) => t.退库单.分店ID == ExpressionViewModel.分店ID.Value);
            }

            var p4 = DbFactory.Instance.CreateDbContext().退库单明细s.Where(d4).ToList();
            foreach (var item in p4)
            {
                CurrentData.当日采购退货笔数 += 1;
                CurrentData.当日采购退货数量 += item.数量;
                CurrentData.当日采购退货重量 += item.金额;
                CurrentData.当日采购退货金额 += item.重量;
            }
            //付款
            Expression<Func<付款单, bool>> d5 = (t) => t.状态 != "N" && t.日期 <= ExpressionViewModel.日期 && t.日期 >= dt;
            if (ExpressionViewModel.分店ID.HasValue)
            {
                d5.And((t) => t.分店ID == ExpressionViewModel.分店ID.Value);
            }

            var p5 = DbFactory.Instance.CreateDbContext().付款单s.Where(d5).ToList();
            foreach (var item in p5)
            {
           
                CurrentData.当日付款金额 += item.实付金额;
            }
            //收款
            Expression<Func<收款单, bool>> d6 = (t) => t.状态 != "N" && t.收款日期 <= ExpressionViewModel.日期 && t.收款日期 >= dt;
            if (ExpressionViewModel.分店ID.HasValue)
            {
                d6.And((t) => t.分店ID == ExpressionViewModel.分店ID.Value);
            }

            var p6 = DbFactory.Instance.CreateDbContext().收款单s.Where(d6).ToList();
            foreach (var item in p6)
            {

                CurrentData.当日收款金额 += item.实收金额;
            }
            return null;
        }
        protected override void SetReportDataSource(XtraReport report)
        {
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            objectDataSource.DataSource = CurrentData;
            report.DataSource = objectDataSource ;
        }
        public class Data
        {
            public virtual int 当日销售笔数 { get; set; }
            public virtual int 当日销售数量 { get; set; }
            public virtual decimal 当日销售重量 { get; set; }
            public virtual decimal 当日销售金额 { get; set; }

            public virtual int 当日销售退货笔数 { get; set; }
            public virtual int 当日销售退货数量 { get; set; }
            public virtual decimal 当日销售退货重量 { get; set; }
            public virtual decimal 当日销售退货金额 { get; set; }

            public virtual int 当日采购入库笔数 { get; set; }
            public virtual int 当日采购入库数量 { get; set; }
            public virtual decimal 当日采购入库重量 { get; set; }
            public virtual decimal 当日采购入库金额 { get; set; }

            public virtual int 当日采购退货笔数 { get; set; }
            public virtual int 当日采购退货数量 { get; set; }
            public virtual decimal 当日采购退货重量 { get; set; }
            public virtual decimal 当日采购退货金额 { get; set; }

           
            public virtual decimal 当日付款金额 { get; set; }

            public virtual decimal 当日收款金额 { get; set; }
        }

    

    }
}