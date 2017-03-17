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
    public class 付款单ViewModel : SingleObjectViewModel<付款单, ZtxDB, long>
    {
        public static 付款单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 付款单ViewModel());
        }
        protected 付款单ViewModel() : base(DbFactory.Instance, x => x.付款单s, x => x.ID, x => x.编号, "付款单")
        {
            if (this.IsInDesignMode()) return;
        }
    }
}