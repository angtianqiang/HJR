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
    public class 材质CollectionViewModel : CollectionViewModel<材质, ZtxDB, long>
    {
        public static 材质CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 材质CollectionViewModel());
        }
        protected 材质CollectionViewModel() : base(DbFactory.Instance, x => x.材质s, query => query.OrderBy(x=>x.排序号), x => x.ID, permissionTitle: "材质")
        {

        }
        static public void InitEntity(材质 NewEntity)
        { }
    }
}