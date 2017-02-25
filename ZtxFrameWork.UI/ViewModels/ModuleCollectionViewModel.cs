using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class ModuleCollectionViewModel: CollectionViewModel<Module, ZtxDB, long>
    {
         public static ModuleCollectionViewModel Create()
    {
        return ViewModelSource.Create(() => new ModuleCollectionViewModel());
    }
    protected ModuleCollectionViewModel():base(DbFactory.Instance, x => x.Modules, x => x, x => x.ID)
        {

    }
}
}