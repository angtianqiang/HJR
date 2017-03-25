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
    public class 饰品出入明细表ViewModel : DynamicQueryCollectionViewModel<库存出入明细>
    {
        public static 饰品出入明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品出入明细表ViewModel());
        }

        protected 饰品出入明细表ViewModel():base("饰品出入明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 入库单().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 饰品出入明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<库存出入明细, bool>> AdvancedExpression)
        {
            //// 局部函数
            //Expression<Func<库存出入明细, bool>> GetRemoteData(库存出入明细 s)
            //{
        
               
            //}
            return DbFactory.Instance.CreateDbContext().库存出入明细s.Include(t=>t.饰品).Include(t=>t.分店)
                .Where(AdvancedExpression)
                .OrderBy(t=>t.日期)
                .Select(t => new { 编号 = t.饰品.编号, 品名 = t.饰品.品名, 分店=t.分店.名称, 日期 = t.日期, 出入别 = t.出入别, 单据类型=t.相关单据, 单据编号=t.单据编号, 数量=t.数量, 重量=t.重量, 金额=t.金额 })
                .ToList<dynamic>();
        }
    }


}