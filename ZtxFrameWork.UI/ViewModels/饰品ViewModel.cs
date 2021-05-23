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
using System.Data.Entity.Core.Objects;
using System.Windows.Media.Imaging;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 饰品ViewModel : SingleObjectViewModel<饰品, ZtxDB, long>
    {
        public static 饰品ViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品ViewModel());
        }
        protected 饰品ViewModel() : base(DbFactory1.Instance, x => x.饰品s, x => x.ID, x => x.编号, "饰品")
        {
            //      DB.Configuration.LazyLoadingEnabled = true;
            //      DB.Configuration.ProxyCreationEnabled = true;

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
        protected override void OnEntityChanged()
        {
            base.OnEntityChanged();
          image = new BitmapImage(new Uri($"{App.HttpPath}/{Entity.编号}.png"));
        }

        public async void Init1() => 饰品类别Source = await DbFactory.Instance.CreateDbContext().饰品类别s.OrderBy(t => t.排序号).ToListAsync();
        public async void Init2() => 单位Source = await DbFactory.Instance.CreateDbContext().单位s.OrderBy(t => t.排序号).ToListAsync();
        public async void Init3() => 重量单位Source = await DbFactory.Instance.CreateDbContext().重量单位s.OrderBy(t => t.排序号).ToListAsync();
        public async void Init4() => 黄金种类Source = await DbFactory.Instance.CreateDbContext().黄金种类s.OrderBy(t => t.排序号).ToListAsync();
        public async void Init5() => 材质Source = await DbFactory.Instance.CreateDbContext().材质s.OrderBy(t => t.排序号).ToListAsync();
        public async void Init6() => 石头颜色Source = await DbFactory.Instance.CreateDbContext().石头颜色s.OrderBy(t => t.排序号).ToListAsync();
        public async void Init7() => 电镀方式Source = await DbFactory.Instance.CreateDbContext().电镀方式s.OrderBy(t => t.排序号).ToListAsync();


    //    protected override IQueryable<饰品> DbInclude(ObjectSet<饰品> dbSet)
    //    {
    ////        return dbSet.Include(t => t.饰品图片);
    //    }
        public async void Update饰品类型Source()
        {
            if (this.Entity == null)
            {
                return;
            }
            饰品类型Source = await DbFactory.Instance.CreateDbContext().饰品类型s.Where(t => t.类别ID == this.Entity.类别ID).ToListAsync();
        }
        public void Init()
        {
            饰品类别Source = Helpers.CacheHelper.饰品类别Source;
            饰品类型Source = Helpers.CacheHelper.饰品类型Source;
            单位Source = Helpers.CacheHelper.单位Source;
            重量单位Source = Helpers.CacheHelper.重量单位Source;
            黄金种类Source = Helpers.CacheHelper.黄金种类Source;
            材质Source = Helpers.CacheHelper.材质Source;
            石头颜色Source = Helpers.CacheHelper.石头颜色Source;
            电镀方式Source = Helpers.CacheHelper.电镀方式Source;


        }

        public virtual List<饰品类别> 饰品类别Source { get; set; }
        public virtual List<饰品类型> 饰品类型Source { get; set; }
        public virtual List<单位> 单位Source { get; set; }
        public virtual List<重量单位> 重量单位Source { get; set; }
        public virtual List<黄金种类> 黄金种类Source { get; set; }
        public virtual List<材质> 材质Source { get; set; }
        public virtual List<石头颜色> 石头颜色Source { get; set; }
        public virtual List<电镀方式> 电镀方式Source { get; set; }
        #region 20170320 删除时同时删除子表

        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 饰品 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.库存s.RemoveRange(dbContext.库存s.Where(t => t.饰品ID == entity.ID).ToArray());
            var pic = dbContext.饰品图片s.Where(t => t.ID == entity.饰品图片ID).SingleOrDefault();
            if (pic != null)
            {
                dbContext.饰品图片s.Remove(pic);
            }

        }
        #endregion


        //20180314 绑定到HTTP图片

        protected IOpenFileDialogService OpenFileDialogService { get { return this.GetService<IOpenFileDialogService>(); } }
        public virtual BitmapImage image { get; set; }
        public void UploadFile()
        {
            if (Entity == null || string.IsNullOrEmpty(Entity.编号))
            {
                MessageBoxService.ShowMessage("请先输入饰品编号");
                return;
            }

            //"图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            OpenFileDialogService.Filter = "图像文件(*.jpg; *.jpg; *.jpeg; *.gif; *.png)| *.jpg; *.jpeg; *.gif; *.png";
            OpenFileDialogService.FilterIndex = 1;
            string ResultFileName = string.Empty;
            bool DialogResult = OpenFileDialogService.ShowDialog();

            if (!DialogResult)
            {
                ResultFileName = string.Empty;
                return;
            }
            else
            {
                ResultFileName = OpenFileDialogService.Files.First().GetFullName();
                //   ResultFileName = file.Name;

            }

            try
            {

                //P@ssw0rd  远程密码

                //  FluentFTP.FtpClient client = new FluentFTP.FtpClient("124.232.147.213", 21,"ZTX", "@dm!N1818#");
                FluentFTP.FtpClient client = new FluentFTP.FtpClient(App.FtpPath, 21, "ZTX", "@dm!N1818#");
                client.Connect();
            client.UploadFile(ResultFileName, "/" + Entity.编号 + ".png");
            client.Disconnect();
            client.Dispose();

            image = new BitmapImage(new Uri($"{App.HttpPath}/{Entity.编号}.png"));
            }
            catch (Exception ee)
            {
                MessageBoxService.ShowMessage(ee.InnerException.Message,"上传失败");
            }
        }
    }
}