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
    public class 石头颜色ViewModel : SingleObjectViewModel<石头颜色, ZtxDB, long>
    {
        public static 石头颜色ViewModel Create()
        {
            return ViewModelSource.Create(() => new 石头颜色ViewModel());
        }
     
        protected 石头颜色ViewModel() : base(DbFactory.Instance, x => x.石头颜色s, x => x.ID, x => x.名称, "石头颜色")
        {


        }
    }
}