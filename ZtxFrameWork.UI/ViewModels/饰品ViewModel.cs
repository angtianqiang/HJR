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
using System.Data.Entity;

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
            DB.Configuration.LazyLoadingEnabled = true;
            DB.Configuration.ProxyCreationEnabled = true;

            Init();

        }

        public async void Init()
        {
           var t1 =await DbFactory.Instance.CreateDbContext().饰品类型s.OrderBy(t => t.排序号).ToListAsync();
             var t2= await DbFactory.Instance.CreateDbContext().单位s.OrderBy(t => t.排序号).ToListAsync();
             var t3= await DbFactory.Instance.CreateDbContext().重量单位s.OrderBy(t => t.排序号).ToListAsync();
             var t4= await DbFactory.Instance.CreateDbContext().黄金种类s.OrderBy(t => t.排序号).ToListAsync();
             var t5= await DbFactory.Instance.CreateDbContext().材质s.OrderBy(t => t.排序号).ToListAsync();
            饰品类型Source = t1;
            单位Source = t2;
            重量单位Source = t3;
            黄金种类Source = t4;
            材质Source = t5;
        }
        public virtual List<饰品类型> 饰品类型Source { get; set; }
        public virtual List<单位> 单位Source { get; set; }
        public virtual List<重量单位> 重量单位Source { get; set; }
        public virtual List<黄金种类> 黄金种类Source { get; set; }
        public virtual List<材质> 材质Source { get; set; }
       
    }
}