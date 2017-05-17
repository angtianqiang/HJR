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
    public class 电镀方式CollectionViewModel : CollectionViewModel<电镀方式, ZtxDB, long>
    {
        public static 电镀方式CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 电镀方式CollectionViewModel());
        }
        protected 电镀方式CollectionViewModel() : base(DbFactory.Instance, x => x.电镀方式s, query => query.OrderBy(x=>x.排序号), x => x.ID, t => InitEntity(t), permissionTitle: "电镀方式")
        {

        }
        static public void InitEntity(电镀方式 NewEntity)
        { }
    }
}