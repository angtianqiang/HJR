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
    public class 黄金种类CollectionViewModel : CollectionViewModel<黄金种类, ZtxDB, long>
    {
        public static 黄金种类CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 黄金种类CollectionViewModel());
        }
        protected 黄金种类CollectionViewModel() : base(DbFactory.Instance, x => x.黄金种类s, query => query.OrderBy(x=>x.排序号), x => x.ID, t => InitEntity(t), permissionTitle: "黄金种类")
        {

        }
        static public void InitEntity(黄金种类 NewEntity)
        { }
    }
}