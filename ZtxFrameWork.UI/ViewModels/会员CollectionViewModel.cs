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
    public class 会员CollectionViewModel : CollectionViewModel<会员, ZtxDB, long>
    {
        public static 会员CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 会员CollectionViewModel());
        }
        protected 会员CollectionViewModel() : base(DbFactory.Instance, x => x.会员s, query => query.OrderBy(x=>x.编号), x => x.ID)
        {

        }
    }
}