using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;
using System.Linq;
using System.Data.Entity;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 饰品CollectionViewModel : CollectionViewModel<饰品, ZtxDB, long>
    {
        public static 饰品CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品CollectionViewModel());
        }
        protected 饰品CollectionViewModel() : base(DbFactory.Instance, x => x.饰品s, query => query.Include(t => t.品名).Include(t => t.石头颜色).Include(t => t.电镀方式).Include(t=>t.材质).Include(t=>t.饰品图片).OrderByDescending(x => x.编号).Take(App.ViewTopCount), x => x.ID,t=>InitEntity(t), permissionTitle: "饰品")
        {

        }
        static public void InitEntity(饰品 NewEntity)
        {
            NewEntity.饰品图片 = new 饰品图片();
          
        }

        protected override 饰品 ReloadCore(饰品 entity)
        {
     

            DB.Entry(entity).Reload();
            DB.Entry(entity.饰品图片).Reload();
            return ReadOnlyDbSet.Find(this.getPrimaryKeyExpression.Compile()(entity));
       
    }
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 饰品 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.库存s.RemoveRange(dbContext.库存s.Where(t => t.饰品ID == entity.ID).ToArray());
        }
    }
}