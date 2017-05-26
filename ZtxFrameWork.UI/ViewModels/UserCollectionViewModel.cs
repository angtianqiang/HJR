using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;
using System.Linq;


namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class UserCollectionViewModel : CollectionViewModel<User, ZtxDB, long>
    {
        public static UserCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new UserCollectionViewModel());
        }
        protected UserCollectionViewModel() : base(DbFactory.Instance, x => x.Users, query => query.OrderBy(x=>x.DispalyName), x => x.ID,x=>InitEntity(x), permissionTitle: "用户管理")
        {

        }
        static public void InitEntity(User NewEntity)
        {
            NewEntity.PassWord = Utilities.SecretUtil.md5("1111");

        }

      
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, User entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            if (!dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).IsLoaded)
            {
                dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).Load();
            }
         //   dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).Load();
            for (int i = 0; i < entity.UserAuthorityModuleMappings.Count; i++)
            {

                dbContext.Entry(entity.UserAuthorityModuleMappings[i]).State = System.Data.Entity.EntityState.Deleted;
            }
        }

    }
}