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
using System.Data.Entity;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 单位ViewModel : SingleObjectViewModel<单位, ZtxDB, long>
    {
        public static 单位ViewModel Create()
        {
            return ViewModelSource.Create(() => new 单位ViewModel());
        }
        public DbSet<单位> 单位s { get; set; }
        protected 单位ViewModel() : base(DbFactory.Instance, x => x.单位s, x=>x.ID, x => x.名称, "单位")
        {
          

        }
    }
}