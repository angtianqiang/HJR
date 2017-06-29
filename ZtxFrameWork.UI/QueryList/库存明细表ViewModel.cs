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
    public class 库存明细表ViewModel : DynamicQueryCollectionViewModel<饰品>
    {
        public static 库存明细表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 库存明细表ViewModel());
        }

        protected 库存明细表ViewModel() : base("库存明细表")
        {

            HiddenProperties = new[] {
                  BindableBase.GetPropertyName(()=>new 饰品().ID)
            };
            AdditionalProperties = null;

            this.EntityExpressionViewModel = 库存明细表_ExpressionViewModel.Create(HiddenProperties, AdditionalProperties);
        }


        protected override System.Collections.Generic.List<dynamic> GetNewData(Expression<Func<饰品, bool>> AdvancedExpression)
        {
            return DbFactory.Instance.CreateDbContext().饰品s.Include(t => t.饰品图片)
                .Where(AdvancedExpression)
                .Select(t => new
                {
                    ID = t.ID,
                    编号 = t.编号,
                    品名 = t.品名.名称,
                    材质 = t.材质.名称,
                    电镀方式 = t.电镀方式.名称,
                    石头颜色 = t.石头颜色.名称,
                    单位 = t.单位.名称,
                    重量单位 = t.重量单位.名称,
                    库存数量 = t.库存数量,
                    库存重量 = t.库存重量,
                    批发工费 = t.批发工费,
                    按件批发价 = t.按件批发价,
                    按重批发价 = t.按重批发价,
                    按件成本价 = t.按件成本价,
                    按重成本价 = t.按重成本价,
                    图片 = t.饰品图片.图片,
                    备注=  t.备注
                })
                .ToList<dynamic>();
        }
    }


}