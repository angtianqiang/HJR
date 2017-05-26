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
    public class AuthorityModuleCollectionViewModel : CollectionViewModel<AuthorityModule, ZtxDB, long>
    {
        public static AuthorityModuleCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new AuthorityModuleCollectionViewModel());
        }
        protected AuthorityModuleCollectionViewModel() : base(DbFactory.Instance, x => x.AuthorityModules, query => query.OrderBy(x=>x.Category).OrderByDescending(x=>x.ID).Take(App.ViewTopCount), x => x.ID,permissionTitle: "模块权限配置")
        {

        }
        static public void InitEntity(AuthorityModule NewEntity)
        { }

        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, AuthorityModule entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            if (!dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).IsLoaded)
            {
                dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).Load();
            }
          //  dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).Load();
            for (int i = 0; i < entity.UserAuthorityModuleMappings.Count; i++)
            {
                
                dbContext.Entry(entity.UserAuthorityModuleMappings[i]).State = System.Data.Entity.EntityState.Deleted;
            }

        }
      

    }
}