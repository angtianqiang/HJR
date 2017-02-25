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
    public class 会员ViewModel : SingleObjectViewModel<会员, ZtxDB, long>
    {
        public static 会员ViewModel Create()
        {
            return ViewModelSource.Create(() => new 会员ViewModel());
        }
        protected 会员ViewModel() : base(DbFactory.Instance, x => x.会员s, x=>x.ID, x => x.姓名)
        {


        }
    }
}