﻿using System;
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
    public class 销售退货单ViewModel : SingleObjectViewModel<销售退货单, ZtxDB, long>
    {
        public static 销售退货单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售退货单ViewModel());
        }
        protected 销售退货单ViewModel() : base(DbFactory.Instance, x => x.销售退货单s, x => x.ID, x => x.编号, "销售退货单")
        {
            if (this.IsInDesignMode()) return;
        }
    }
}