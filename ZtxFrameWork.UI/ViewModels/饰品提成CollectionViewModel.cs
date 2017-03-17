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
    public class 饰品提成CollectionViewModel : CollectionViewModel<饰品提成, ZtxDB, long>
    {
        public static 饰品提成CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品提成CollectionViewModel());
        }
        protected 饰品提成CollectionViewModel() : base(DbFactory.Instance, x => x.饰品提成s, query => query.OrderBy(x=>x.饰品.品名), x => x.ID, t => InitEntity(t), permissionTitle: "饰品提成")
        {

        }
        static public void InitEntity(饰品提成 NewEntity)
        { }
    }
}