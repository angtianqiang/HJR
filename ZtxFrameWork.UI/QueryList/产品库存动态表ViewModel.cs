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
using System.ComponentModel.DataAnnotations;

namespace ZtxFrameWork.UI.QueryList
{
    [POCOViewModel]
    public class 产品库存动态表ViewModel: DynamicQueryCollectionViewModel<入库单明细>
    {
        public static 产品库存动态表ViewModel Create()
    {
        return ViewModelSource.Create(() => new 产品库存动态表ViewModel());
    }

    protected 产品库存动态表ViewModel() : base("产品库存动态表")
        {

        HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 退库单明细().ID)
            };
        AdditionalProperties = null;

        this.EntityExpressionViewModel = 产品库存动态表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
    }

        public class 动态库存
        {
            public string 编号 { get; set; }
            public string 条码 { get; set; }
            public string 品名 { get; set; }
            public string 饰品类型 { get; set; }
            public string 单位 { get; set; }
            public decimal 单重 { get; set; }
            public decimal 批发工费 { get; set; }

            public decimal 成本工费 { get; set; }
            public string 工费计法 { get; set; }
            public DateTime 首次入库时间 { get; set; }
            public int 累计入库数量 { get; set; }

            public int 共入库存次数 { get; set; }
            public int? 累计销售数量 { get; set; }
            public int? 共销售次数 { get; set; }
            public decimal 按重成本价 { get; set; }
            public decimal 按件成本价 { get; set; }
            public int 现有库存数量 { get; set; }
            public decimal 现有库存重量 { get; set; }
            [DisplayFormat( DataFormatString = "N0")]
            public decimal? 平均月销数量 { get; set; }
            public string 备注 { get; set; }
        }

    protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<入库单明细, bool>> AdvancedExpression)
    {
         long 分店ID=((产品库存动态表_ExpressionViewModel)this.EntityExpressionViewModel).分店ID;
            var db = DbFactory.Instance.CreateDbContext();
            string sql = string.Format(@" 
                                            select  饰品ID,日期 
                                            into #A
                                            from 入库单明细 AS DD
                                            left join 入库单 AS D ON  DD.入库单ID=D.ID
                                            where d.[分店ID]={0}


                                            SELECT Distinct 饰品ID,首次入库时间=(select min(日期)  from #A as o2  WHERE o2.饰品ID=o1.饰品ID) 
                                            into #B
                                             FROM #A as o1 ORDER BY 饰品ID

                                             /*入库总数量*/
                                             select  饰品ID,SUM(DD2.[数量]) AS 累计入库数量,count(DD2.[数量]) as 共入库存次数
                                            into #C
                                            from 入库单明细 AS DD2
                                            left join 入库单 AS D2 ON  DD2.入库单ID=D2.ID
                                            where D2.[分店ID]={0}
                                            group by 饰品ID

                                            /*销售数量*/

                                            select 
                                             饰品ID,SUM(DD3.[数量]) AS 累计销售数量,count(DD3.[数量]) as 共销售次数
                                             into #D
                                            from  [dbo].[销售单明细] as DD3
                                            left join [dbo].[销售单] AS D3 ON DD3.销售单ID=d3.id
                                            where D3.[分店ID]={0}
                                            group by 饰品ID

                                             /*销售总数量*/

                                            SELECT
                                            sp.[编号],
                                            sp.[条码],
                                            LB.名称 as 品名,
                                            LX.名称 as 饰品类型,
                                            dw.名称 as 单位,
                                            sp.[单重],
                                            sp.[批发工费],
                                            sp.[成本工费],
                                             case	sp.[工费计法]
												when 0 then '按件'
												when 1 then '按重'
												else '其它' end as [工费计法],                                           
                                            首次入库时间,
                                            累计入库数量,
                                            共入库存次数,
                                            累计销售数量,
                                            共销售次数,
                                            sp.[按重成本价],
                                            sp.[按件成本价],
                                            kc.[数量] as 现有库存数量,
                                            kc.[重量] as 现有库存重量,
                                            累计销售数量/(datediff(day,首次入库时间,getdate())/30.0 ) as 平均月销数量,
                                            sp.[备注]
                                            FROM #B
                                            left join #C on #b.饰品ID=#c.饰品ID
                                            left join #D on #c.饰品ID=#d.饰品ID
                                            left join [dbo].[饰品] as sp on sp.id=#b.饰品ID        /*20180115 客户要求显示所有产品的信息 将d.饰品ID改为b.饰品ID*/
                                            LEFT join [dbo].[饰品类别] AS LB ON SP.[类别ID]=LB.ID
                                            LEFT join [dbo].[饰品类型] AS LX ON SP.[类型ID]=LX.ID
                                            left join [dbo].[单位] as dw on SP.[单位ID]=dw.ID
                                            left join [dbo].[库存] as kc on sp.id=kc.饰品ID and kc.[分店ID]={0}
                                            where sp.[编号] is not null
                                            DROP TABLE #A
                                            DROP TABLE #B
                                            DROP TABLE #C
                                            DROP TABLE #D
                                            ",
               分店ID) ;
         return   db.Database.SqlQuery<动态库存>(sql).ToList<dynamic>();
            //求首次入库日期


    }
}
}