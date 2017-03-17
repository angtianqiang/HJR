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
using System.Windows.Input;
using ZtxFrameWork.UI.Comm.UI;
using ZtxFrameWork.UI.Extensions;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 盘点表ViewModel : SingleObjectViewModel<盘点表, ZtxDB, long>
    {
        public static 盘点表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 盘点表ViewModel());
        }
        protected 盘点表ViewModel() : base(DbFactory.Instance, x => x.盘点表s, x=>x.ID, x => x.编号, "盘点表")
        {
            if (this.IsInDesignMode()) return;
        }
    }
}