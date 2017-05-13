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
using System.IO;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class ModuleViewModel : SingleObjectViewModel<Module, ZtxDB, long>
    {
        public static ModuleViewModel Create()
        {
            return ViewModelSource.Create(() => new ModuleViewModel());
        }
        protected ModuleViewModel() : base(DbFactory.Instance, x => x.Modules,x=>x.ID, x => x.ModuleTitle,"系统模块")
        {
           var db = DB;
            ModuleGroup = db.Modules.Where(x => x.ModuleInfo != ModuleInfo.MoudleAction).ToList();
            var imagesPath = System.Environment.CurrentDirectory + @"\MoudleImages";
            var dir = new DirectoryInfo(imagesPath);
            Images = dir.GetFiles("*.png", SearchOption.TopDirectoryOnly).Select(t => t.Name).ToList(); ;
          //图标24*24 PADDING 2
        }

        public virtual List<Module> ModuleGroup { get; set; }

        public virtual List<string> Images { get; set; }
    }
}