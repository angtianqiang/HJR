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
    public class 收款单ViewModel : SingleObjectViewModel<收款单, ZtxDB, long>
    {
        public static 收款单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 收款单ViewModel());
        }
        protected 收款单ViewModel() : base(DbFactory.Instance, x => x.收款单s, x=>x.ID, x => x.编号, "收款单")
        {
         

        }
       
    }
}