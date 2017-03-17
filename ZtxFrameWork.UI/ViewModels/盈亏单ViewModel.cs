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
    public class 盈亏单ViewModel : SingleObjectViewModel<盈亏单, ZtxDB, long>
    {
        public static 盈亏单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 盈亏单ViewModel());
        }
        protected 盈亏单ViewModel() : base(DbFactory.Instance, x => x.盈亏单s, x => x.ID, x => x.编号, "盈亏单")
        {
            if (this.IsInDesignMode()) return;
        }
    }
}