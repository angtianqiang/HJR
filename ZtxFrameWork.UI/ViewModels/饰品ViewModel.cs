using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using System.Collections.Generic;
using ZtxFrameWork.UI.Comm.DataModel;
using DevExpress.Mvvm.POCO;
using System.Linq;
namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 饰品ViewModel : SingleObjectViewModel<饰品, ZtxDB, long>
    {
        public static 饰品ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品ViewModel());
        }
        protected 饰品ViewModel() : base(DbFactory.Instance, x => x.饰品s, x=>x.ID, x => x.品名, "饰品")
        {

            var db = dbFactory.CreateDbContext();
            饰品类型Source = db.饰品类型s.OrderBy(t => t.排序号).ToList();
            单位Source = db.单位s.OrderBy(t => t.排序号).ToList();

            重量单位Source = db.重量单位s.OrderBy(t => t.排序号).ToList();
            黄金种类Source = db.黄金种类s.OrderBy(t => t.排序号).ToList();
            材质Source = db.材质s.OrderBy(t => t.排序号).ToList();

        }
        public virtual List<饰品类型> 饰品类型Source { get; set; }
        public virtual List<单位> 单位Source { get; set; }
        public virtual List<重量单位> 重量单位Source { get; set; }
        public virtual List<黄金种类> 黄金种类Source { get; set; }
        public virtual List<材质> 材质Source { get; set; }
       
    }
}