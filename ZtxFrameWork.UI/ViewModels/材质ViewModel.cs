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
    public class 材质ViewModel : SingleObjectViewModel<材质, ZtxDB, long>
    {
        public static 材质ViewModel Create()
        {
            return ViewModelSource.Create(() => new 材质ViewModel());
        }
        protected 材质ViewModel() : base(DbFactory.Instance, x => x.材质s, x=>x.ID, x => x.名称)
        {


        }
    }
}