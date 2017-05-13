using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using System.Collections.Generic;
using ZtxFrameWork.UI.Comm.DataModel;
using DevExpress.Mvvm.POCO;
using System.Linq;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class UserViewModel : SingleObjectViewModel<User, ZtxDB, long>
    {
        public static UserViewModel Create()
        {
            return ViewModelSource.Create(() => new UserViewModel());
        }
        protected UserViewModel() : base(DbFactory.Instance, x => x.Users, x => x.ID, x => x.UserName,"用户管理")
        {

            //var db = dbFactory.CreateDbContext();
            //饰品类型Source = db.饰品类型s.OrderBy(t => t.排序号).ToList();
            //单位Source = db.单位s.OrderBy(t => t.排序号).ToList();

            //重量单位Source = db.重量单位s.OrderBy(t => t.排序号).ToList();
            //黄金种类Source = db.黄金种类s.OrderBy(t => t.排序号).ToList();
            //材质Source = db.材质s.OrderBy(t => t.排序号).ToList();

        }
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, User entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);

            dbContext.Entry(entity).Collection(t => t.UserAuthorityModuleMappings).Load();
            for (int i = 0; i < entity.UserAuthorityModuleMappings.Count; i++)
            {

                dbContext.Entry(entity.UserAuthorityModuleMappings[i]).State = System.Data.Entity.EntityState.Deleted;
            }
          
        }
        protected override void OnBeforeEntitySaved(ZtxDB dbContext, long primaryKey, User entity, bool isNewEntity)
        {
            base.OnBeforeEntitySaved(dbContext, primaryKey, entity, isNewEntity);
            if (isNewEntity != true) return;
        //添加用户时，添加用户的权限清单
            foreach (var item in dbContext.AuthorityModules.AsEnumerable())
            {
                entity.UserAuthorityModuleMappings.Add(new UserAuthorityModuleMapping() { AuthorityModuleID = item.ID });
            }
           
        }

    }
}