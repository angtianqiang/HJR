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
    public class 饰品类型CollectionViewModel : CollectionViewModel<饰品类型, ZtxDB, long>
    {
        public static 饰品类型CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品类型CollectionViewModel());
        }
        protected 饰品类型CollectionViewModel() : base(DbFactory.Instance, x => x.饰品类型s, query => query.OrderBy(x=>x.排序号), x => x.ID, t => InitEntity(t), permissionTitle: "饰品类型")
        {

        }
        static public void InitEntity(饰品类型 NewEntity)
        { }
    }
}