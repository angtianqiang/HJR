﻿using System;
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
    public class AuthorityModuleCollectionViewModel : CollectionViewModel<AuthorityModule, ZtxDB, long>
    {
        public static AuthorityModuleCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new AuthorityModuleCollectionViewModel());
        }
        protected AuthorityModuleCollectionViewModel() : base(DbFactory.Instance, x => x.AuthorityModules, query => query.OrderBy(x=>x.Category).OrderBy(x=>x.ViewTitle), x => x.ID,permissionTitle: "模块权限配置")
        {

        }
        static public void InitEntity(AuthorityModule NewEntity)
        { }

        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, AuthorityModule entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);

            dbContext.Entry(entity).Reference(t => t.UserAuthorityModuleMappings).Load();
            foreach (var item in entity.UserAuthorityModuleMappings)
            {
                dbContext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
        }
      

    }
}