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
    public class 退库单ViewModel : SingleObjectViewModel<退库单, ZtxDB, long>
    {
        public static 退库单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 退库单ViewModel());
        }
        protected 退库单ViewModel() : base(DbFactory.Instance, x => x.退库单s, x => x.ID, x => x.编号, "采购退货单")
        {
            if (this.IsInDesignMode()) return;
        }
    }
}