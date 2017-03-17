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
    public class 调拨单ViewModel : SingleObjectViewModel<调拨单, ZtxDB, long>
    {
        public static 调拨单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 调拨单ViewModel());
        }
        protected 调拨单ViewModel() : base(DbFactory.Instance, x => x.调拨单s, x => x.ID, x => x.编号, "调拨单")
        {
            if (this.IsInDesignMode()) return;
        }
    }
}