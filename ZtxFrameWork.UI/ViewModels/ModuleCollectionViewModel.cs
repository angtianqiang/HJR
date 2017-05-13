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
    public class ModuleCollectionViewModel: CollectionViewModel<Module, ZtxDB, long>
    {
         public static ModuleCollectionViewModel Create()
    {
        return ViewModelSource.Create(() => new ModuleCollectionViewModel());
    }
    protected ModuleCollectionViewModel():base(DbFactory.Instance, x => x.Modules, query => query.OrderByDescending(x => x.ID).Take(App.ViewTopCount), x => x.ID, permissionTitle: "系统模块")
        {

    }
        static public void InitEntity(Module NewEntity)
        { }
      
    }
}