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
    public class 石头颜色CollectionViewModel : CollectionViewModel<石头颜色, ZtxDB, long>
    {
        public static 石头颜色CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 石头颜色CollectionViewModel());
        }
        protected 石头颜色CollectionViewModel() : base(DbFactory.Instance, x => x.石头颜色s, query => query.OrderBy(x=>x.排序号), x => x.ID, t => InitEntity(t), permissionTitle: "石头颜色")
        {

        }
        static public void InitEntity(石头颜色 NewEntity)
        { }
    }
}