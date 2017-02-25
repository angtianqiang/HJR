using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;
using System.Linq;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 入库单CollectionViewModel : CollectionViewModel<入库单, ZtxDB, long>
    {
        public static 入库单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 入库单CollectionViewModel());
        }
        protected 入库单CollectionViewModel() : base(DbFactory.Instance, x => x.入库单s, query => query.OrderBy(x=>x.编号), x => x.ID)
        {

        }
    }
}