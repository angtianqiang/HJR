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
    public class SystemConfigurationCollectionViewModel : CollectionViewModel<SystemConfiguration, ZtxDB, long>
    {
        public static SystemConfigurationCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new SystemConfigurationCollectionViewModel());
        }
        static public void InitEntity(Module NewEntity)
        { }
        protected SystemConfigurationCollectionViewModel() : base(DbFactory.Instance, x => x.SystemConfigurations, query => query.OrderBy(x => x.Category), x => x.ID,  permissionTitle: "用户管理")
        {

        }
        public override bool CanNew()
        {
            return false;
        }
    }
}