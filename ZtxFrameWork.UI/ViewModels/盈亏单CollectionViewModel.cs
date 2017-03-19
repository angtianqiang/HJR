using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data;
using System.Linq;
using System.Data.Entity;
namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 盈亏单CollectionViewModel : CollectionViewModel< 盈亏单, ZtxDB, long>
    {
        public static  盈亏单CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new  盈亏单CollectionViewModel());
        }
        protected  盈亏单CollectionViewModel() : base(DbFactory.Instance, x => x. 盈亏单s, query => query.OrderBy(x=>x.编号), x =>x.ID,t=>InitEntity(t), permissionTitle: "盈亏单")
        {

        }
        private Action< 盈亏单> b = InitEntity;
        static public void InitEntity( 盈亏单 NewEntity)
        {
            NewEntity.编号 = GetNewCode("YK", DbFactory.Instance, x => x. 盈亏单s, t => t.编号);
           NewEntity.日期 = DateTime.Now;
            NewEntity.操作员ID = App.CurrentUser.ID;
            NewEntity.状态 = "N";
        }




    }
}