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
        protected 饰品ViewModel() : base(DbFactory.Instance, x => x.饰品s, x=>x.ID, x => x.编号, "饰品")
        {
            DB.Configuration.LazyLoadingEnabled = true;
            DB.Configuration.ProxyCreationEnabled = true;

         Init();
            //Init1();
            //Init2();
            //Init3();
            //Init4();
            //Init5();
            //Init6();
            //Init7();
            //this.Entity.PropertyChanged += (s, e) => {
            //    if (e.PropertyName== "类别ID")
            //    {
            //        饰品类型Source = DB.饰品类型s.Where(t => t.类别ID == this.Entity.类别ID).ToList();
            //    }
            //};
        }


        public async void Init1() => 饰品类别Source = await DbFactory.Instance.CreateDbContext().饰品类别s.OrderBy(t => t.排序号).ToListAsync();
            public async void Init2() => 单位Source = await DbFactory.Instance.CreateDbContext().单位s.OrderBy(t => t.排序号).ToListAsync();
            public async void Init3() => 重量单位Source = await DbFactory.Instance.CreateDbContext().重量单位s.OrderBy(t => t.排序号).ToListAsync();
            public async void Init4() => 黄金种类Source = await DbFactory.Instance.CreateDbContext().黄金种类s.OrderBy(t => t.排序号).ToListAsync();
            public async void Init5() => 材质Source = await DbFactory.Instance.CreateDbContext().材质s.OrderBy(t => t.排序号).ToListAsync();
            public async void Init6() => 石头颜色Source = await  DbFactory.Instance.CreateDbContext().石头颜色s.OrderBy(t => t.排序号).ToListAsync();
            public async void Init7() => 电镀方式Source = await  DbFactory.Instance.CreateDbContext().电镀方式s.OrderBy(t => t.排序号).ToListAsync();
        //protected override IQueryable<饰品> DbInclude(DbSet<饰品> dbSet)
        //{
        //    return dbSet.Include(t => t.饰品图片);
        //}

        public async void Update饰品类型Source()
        {
            饰品类型Source = await DbFactory.Instance.CreateDbContext().饰品类型s.Where(t => t.类别ID == this.Entity.类别ID).ToListAsync();
        }
        public  void Init()
        {
            饰品类别Source =DB.饰品类别s.OrderBy(t => t.排序号).ToList();
            饰品类型Source = DB.饰品类型s.OrderBy(t => t.排序号).ToList();
            单位Source = DB.单位s.OrderBy(t => t.排序号).ToList();
            重量单位Source = DB.重量单位s.OrderBy(t => t.排序号).ToList();
            黄金种类Source = DB.黄金种类s.OrderBy(t => t.排序号).ToList();
            材质Source = DB.材质s.OrderBy(t => t.排序号).ToList();
            石头颜色Source = DB.石头颜色s.OrderBy(t => t.排序号).ToList();
            电镀方式Source = DB.电镀方式s.OrderBy(t => t.排序号).ToList();


        }

        public virtual List<饰品类别> 饰品类别Source { get; set; }
        public virtual List<饰品类型> 饰品类型Source { get; set; }
        public virtual List<单位> 单位Source { get; set; }
        public virtual List<重量单位> 重量单位Source { get; set; }
        public virtual List<黄金种类> 黄金种类Source { get; set; }
        public virtual List<材质> 材质Source { get; set; }
        public virtual List<石头颜色> 石头颜色Source { get; set; }
        public virtual List<电镀方式 > 电镀方式Source { get; set; }

    }
}