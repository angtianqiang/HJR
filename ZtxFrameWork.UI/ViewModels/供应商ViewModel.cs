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
    public class 供应商ViewModel : SingleObjectViewModel<供应商, ZtxDB, long>
    {
        public static 供应商ViewModel Create()
        {
            return ViewModelSource.Create(() => new 供应商ViewModel());
        }
        protected 供应商ViewModel() : base(DbFactory.Instance, x => x.供应商s, x=>x.ID, x =>x.简称)
        {


        }
    }
}