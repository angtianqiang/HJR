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
    public class 分店ViewModel : SingleObjectViewModel<分店, ZtxDB, long>
    {
        public static 分店ViewModel Create()
        {
            return ViewModelSource.Create(() => new 分店ViewModel());
        }
        protected 分店ViewModel() : base(DbFactory.Instance, x => x.分店s, x=>x.ID, x => x.名称)
        {


        }
    }
}