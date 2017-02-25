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
    public class 入库单ViewModel : SingleObjectViewModel<入库单, ZtxDB, long>
    {
        public static 入库单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 入库单ViewModel());
        }
        protected 入库单ViewModel() : base(DbFactory.Instance, x => x.入库单s, x=>x.ID, x => x.编号)
        {


        }
    }
}