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
using System.Collections.Generic;
using System.Windows.Input;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 分店库存ViewModel : CollectionViewModel<库存, ZtxDB, long>
    {
        public static 分店库存ViewModel Create()
        {
            return ViewModelSource.Create(() => new 分店库存ViewModel());
        }
        protected 分店库存ViewModel() : base(DbFactory.Instance, x => x.库存s, query => query.Include(t=>t.饰品).Include(t => t.饰品.品名).Include(t => t.饰品.饰品类型).Include(t => t.饰品.材质).Include(t => t.饰品.电镀方式).Include(t => t.饰品.石头颜色).Include(t => t.饰品.饰品图片).Include(t=>t.分店).OrderByDescending(x =>x.饰品.编号), x =>x.ID, permissionTitle: "分店库存")
        {
            Init();
        }
        public void Init()
        {
            分店Source = Helpers.CacheHelper.分店Source;

        }

        public virtual List<分店> 分店Source { get; set; }

        public void ReshData(long 分店ID)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (分店ID == 99)
            {
                this.FilterExpression =t=>1==1;
                this.Refresh();
                Mouse.OverrideCursor = null;
                return;
            }
            this.FilterExpression = t => t.分店ID == 分店ID;
            this.Refresh();
            Mouse.OverrideCursor = null;
        }
      

    }
}