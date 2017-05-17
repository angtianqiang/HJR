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
    public class 电镀方式ViewModel : SingleObjectViewModel<电镀方式, ZtxDB, long>
    {
        public static 电镀方式ViewModel Create()
        {
            return ViewModelSource.Create(() => new 电镀方式ViewModel());
        }

        protected 电镀方式ViewModel() : base(DbFactory.Instance, x => x.电镀方式s, x => x.ID, x => x.名称, "电镀方式")
        {


        }
    }
}