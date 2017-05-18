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
    public class 饰品类别ViewModel : SingleObjectViewModel<饰品类别, ZtxDB, long>
    {
        public static 饰品类别ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品类别ViewModel());
        }
        protected 饰品类别ViewModel() : base(DbFactory.Instance, x => x.饰品类别s, x=>x.ID, x => x.名称, "品名")
        {


        }
    }
}