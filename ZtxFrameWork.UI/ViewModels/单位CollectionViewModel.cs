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
    public class 单位CollectionViewModel : CollectionViewModel<单位, ZtxDB, long>
    {
        public static 单位CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 单位CollectionViewModel());
        }
        protected 单位CollectionViewModel() : base(DbFactory.Instance, x => x.单位s, query => query.OrderBy(x=>x.排序号), x => x.ID, t => InitEntity(t), permissionTitle: "单位")
        {

        }
        static public void InitEntity(单位 NewEntity)
        { }
    }
}