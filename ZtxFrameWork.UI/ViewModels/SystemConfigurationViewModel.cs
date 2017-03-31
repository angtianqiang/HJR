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
    public class SystemConfigurationViewModel : SingleObjectViewModel<SystemConfiguration, ZtxDB, long>
    {
        public static SystemConfigurationViewModel Create()
        {
            return ViewModelSource.Create(() => new SystemConfigurationViewModel());
        }
        protected SystemConfigurationViewModel() : base(DbFactory.Instance, x => x.SystemConfigurations, x => x.ID, x => x.Token,"系统参数配置")
        { }

      
        protected override void OnEntitySaved(ZtxDB dbContext, long primaryKey, SystemConfiguration entity, bool isNewEntity)
        {
            base.OnEntitySaved(dbContext, primaryKey, entity, isNewEntity);

            App.SystemConfigs = DbFactory.Instance.CreateDbContext().SystemConfigurations.ToList();
        }
    }
}