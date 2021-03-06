﻿using System;
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
    public class 收款明细表ViewModel : DynamicQueryCollectionViewModel<收款单明细>
    {
        public static 收款明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 收款明细表ViewModel());
        }

        protected 收款明细表ViewModel() : base("收款明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 付款单明细().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 收款明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<收款单明细, bool>> AdvancedExpression)
        {


            return DbFactory.Instance.CreateDbContext().收款单明细s
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    收款单号 = t.收款单.编号,
                    销售单号 = t.销售单.编号,
                    销售退货单号 = t.销售退货单.编号,
                    会员 = t.收款单.会员.姓名,
                    日期 = t.收款单.收款日期,
                    应收金额 = t.应收金额,
                    本次收入金额 = t.本次收入金额,
                    分店 = t.收款单.分店.名称,
                    操作员 = t.收款单.操作员.DispalyName,
                    状态 = t.收款单.状态

                })
                .ToList<dynamic>();
        }
    }


}