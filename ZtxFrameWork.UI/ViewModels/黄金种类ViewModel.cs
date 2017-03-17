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
    public class 黄金种类ViewModel : SingleObjectViewModel<黄金种类, ZtxDB, long>
    {
        public static 黄金种类ViewModel Create()
        {
            return ViewModelSource.Create(() => new 黄金种类ViewModel());
        }
        protected 黄金种类ViewModel() : base(DbFactory.Instance, x => x.黄金种类s, x=>x.ID, x => x.名称, "黄金种类")
        {


        }
    }
}