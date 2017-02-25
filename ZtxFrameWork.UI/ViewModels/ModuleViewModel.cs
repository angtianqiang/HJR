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
    public class ModuleViewModel : SingleObjectViewModel<Module, ZtxDB, long>
    {
        public static ModuleViewModel Create()
        {
            return ViewModelSource.Create(() => new ModuleViewModel());
        }
        protected ModuleViewModel() : base(DbFactory.Instance, x => x.Modules,x=>x.ID, x => x.ModuleTitle)
        {
            var db = dbFactory.CreateDbContext();
            ModuleGroup = db.Modules.Where(x => x.ModuleInfo != ModuleInfo.MoudleAction).ToList(); 

        }

        public virtual List<Module> ModuleGroup { get; set; }
    }
}