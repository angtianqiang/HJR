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
using Magicodes.ExporterAndImporter.Core;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using System.Windows.Input;
using System.Collections.Generic;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 饰品CollectionViewModel : CollectionViewModel<饰品, ZtxDB, long>
    {
        public static 饰品CollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new 饰品CollectionViewModel());
        }
        protected 饰品CollectionViewModel() : base(DbFactory.Instance, x => x.饰品s, query => query.Include(t => t.品名).Include(t => t.饰品类型).Include(t => t.石头颜色).Include(t => t.电镀方式).Include(t=>t.材质).OrderByDescending(x => x.编号).Take(App.ViewTopCount), x => x.ID,t=>InitEntity(t), permissionTitle: "饰品", allowPage: true)
        {

        }
        static public void InitEntity(饰品 NewEntity)
        {
            NewEntity.饰品图片 = new 饰品图片();
          
        }

        protected override 饰品 ReloadCore(饰品 entity)
        {
     

            DB.Entry(entity).Reload();
            DB.Entry(entity.饰品图片).Reload();
            return ReadOnlyDbSet.Find(this.getPrimaryKeyExpression.Compile()(entity));
       
    }
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 饰品 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.库存s.RemoveRange(dbContext.库存s.Where(t => t.饰品ID == entity.ID).ToArray());
        }






        #region 数据导入


        public class Import饰品
        {


       







            [ImporterHeader(Name = "编号", IsIgnore = false)]
            [Required(ErrorMessage = "编号不能为空")]
            public string 编号 { get; set; }

            [ImporterHeader(Name = "条码", IsIgnore = false)]
            [Required(ErrorMessage = "条码不能为空")]
            public string 条码 { get; set; }


            [ImporterHeader(Name = "饰品类别", IsIgnore = false)]
            [Required(ErrorMessage = "饰品类别不能为空")]
            public virtual string 品名 { get; set; } //饰品类别

            [ImporterHeader(Name = "饰品类型", IsIgnore = false)]
            [Required(ErrorMessage = "饰品类型不能为空")]
            public string 饰品类型 { get; set; }

            [ImporterHeader(Name = "柜号", IsIgnore = false)]
            public string 柜号 { get; set; }



            [ImporterHeader(Name = "材质", IsIgnore = false)]
            [Required(ErrorMessage = "材质不能为空")]
            public virtual string 材质 { get; set; }



            [ImporterHeader(Name = "电镀方式", IsIgnore = false)]
            [Required(ErrorMessage = "电镀方式不能为空")]
            public virtual string 电镀方式 { get; set; }



            [ImporterHeader(Name = "石头颜色", IsIgnore = false)]
            [Required(ErrorMessage = "石头颜色不能为空")]
            public virtual string 石头颜色 { get; set; }


            [ImporterHeader(Name = "净金属重", IsIgnore = false)]
          //  [Required(ErrorMessage = "净金属重不能为空")]
            public decimal 净重 { get; set; } //净金属重


            [ImporterHeader(Name = "尺寸", IsIgnore = false)]
            public string 尺寸 { get; set; }


            [ImporterHeader(Name = "重量单位", IsIgnore = false)]
            [Required(ErrorMessage = "重量单位不能为空")]
            public virtual String 重量单位 { get; set; }


            [ImporterHeader(Name = "单重", IsIgnore = false)]
            public decimal 单重 { get; set; }


            [ImporterHeader(Name = "费用计法", IsIgnore = false)]
            [Required(ErrorMessage = "费用计法不能为空")]
            public 费用计法 工费计法 { get; set; }


            [ImporterHeader(Name = "单位", IsIgnore = false)]
            [Required(ErrorMessage = "单位不能为空")]
            public virtual String 单位 { get; set; }




            [ImporterHeader(Name = "按件批发价", IsIgnore = false)]
            public decimal 按件批发价 { get; set; }
            [ImporterHeader(Name = "按重批发价", IsIgnore = false)]
            public decimal 按重批发价 { get; set; }
            [ImporterHeader(Name = "批发工费", IsIgnore = false)]
            public decimal 批发工费 { get; set; }

            [ImporterHeader(Name = "按件成本价", IsIgnore = false)]
            public decimal 按件成本价 { get; set; }
            [ImporterHeader(Name = "按重成本价", IsIgnore = false)]
            public decimal 按重成本价 { get; set; }
            [ImporterHeader(Name = "成本工费", IsIgnore = false)]
            public decimal 成本工费 { get; set; }

            [ImporterHeader(Name = "库存上限", IsIgnore = false)]
            public Int32 库存上限 { get; set; }
            [ImporterHeader(Name = "库存下限", IsIgnore = false)]
            public Int32 库存下限 { get; set; }


          


            [ImporterHeader(Name = "备注", IsIgnore = false)]
            public string 备注 { get; set; }










    }

        public virtual async void Import()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择导入的清单文件";
            //   openFileDialog.Filter = "Excel Files|*.xls|Excel Files|*.xlsx";
            openFileDialog.Filter = "Excel Files|*.xlsx";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = false;

            bool DialogResult = openFileDialog.ShowDialog() == true && !string.IsNullOrEmpty(openFileDialog.FileName);
            if (DialogResult == false) return;



            Mouse.OverrideCursor = Cursors.Wait;
            //20200917改用Magicodes.IE组件
            IImporter Importer = new Magicodes.ExporterAndImporter.Excel.ExcelImporter();
            var import = await Importer.Import<Import饰品>(openFileDialog.FileName);
            //   import.ShouldNotBeNull();
            if (import.Exception != null)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.Show(import.Exception.ToString());
                return;
            }

            if (import.RowErrors.Count > 0)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.Show(Newtonsoft.Json.JsonConvert.SerializeObject(import.RowErrors));
                return;
            }


            //20170330更改事务方式
        
             
                        var newDB = dbFactory.CreateDbContext();
         List< 饰品类别>   饰品类别Temp = newDB.饰品类别s.ToList();
            List<饰品类型> 饰品类型Temp = newDB.饰品类型s.ToList();
            List<材质> 材质Temp = newDB.材质s.ToList();
            List<电镀方式> 电镀方式Temp = newDB.电镀方式s.ToList();
            List<石头颜色> 石头颜色Temp = newDB.石头颜色s.ToList();
            List<重量单位> 重量单位Temp = newDB.重量单位s.ToList();
            List<单位> 单位Temp = newDB.单位s.ToList();
        



            foreach (var item in import.Data)
            {
                var newRow = newDB.饰品s.Create();
                newRow.DirtyState = DirtyState.Added;

                if (newDB.饰品s.Count(t=>t.编号==item.编号)>0)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中已有饰品编码为：{ item.编号}的档案！", "系统提示");
                    return;
                }


                newRow.编号 = item.编号;
                newRow.条码 = item.条码;

                var 饰品类别 = 饰品类别Temp.FirstOrDefault(T => T.名称 == item.品名);
                if (饰品类别==null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到饰品类别为：{ item.品名}的档案！", "系统提示");
                    return;
                }
                newRow.类别ID = 饰品类别.ID;


                var 饰品类型 = 饰品类型Temp.FirstOrDefault(T => T.名称 == item.饰品类型&& T.类别ID== newRow.类别ID);
                if (饰品类型 == null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到饰品类型为：{ item.饰品类型}的档案！", "系统提示");
                    return;
                }

                newRow.类型ID = 饰品类型.ID;

                newRow.柜号 = item.柜号;


                var 材质 = 材质Temp.FirstOrDefault(T => T.名称 == item.材质);
                if (材质 == null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到材质为：{ item.材质}的档案！", "系统提示");
                    return;
                }

                newRow.材质ID = 材质.ID;


                var 电镀方式 = 电镀方式Temp.FirstOrDefault(T => T.名称 == item.电镀方式);
                if (电镀方式 == null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到电镀方式为：{ item.电镀方式}的档案！", "系统提示");
                    return;
                }

                newRow.电镀方式ID = 电镀方式.ID;



                var 石头颜色 = 石头颜色Temp.FirstOrDefault(T => T.名称 == item.石头颜色);
                if (石头颜色 == null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到石头颜色为：{ item.石头颜色}的档案！", "系统提示");
                    return;
                }
                newRow.石头颜色ID = 石头颜色.ID;


                newRow.净重 = item.净重;


                newRow.尺寸 = item.尺寸;


                var 重量单位 = 重量单位Temp.FirstOrDefault(T => T.名称 == item.重量单位);
                if (重量单位 == null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到重量单位为：{ item.重量单位}的档案！", "系统提示");
                    return;
                }
                newRow.重量单位ID = 重量单位.ID;


                newRow.单重 = item.单重;
                newRow.工费计法 = item.工费计法;

                var 单位 = 单位Temp.FirstOrDefault(T => T.名称 == item.单位);
                if (单位 == null)
                {
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage($"系统中未找到单位为：{ item.单位}的档案！", "系统提示");
                    return;
                }
                newRow.单位ID = 单位.ID;


                newRow.按件批发价 = item.按件批发价;
                newRow.按重批发价 = item.按重批发价;

                newRow.批发工费 = item.批发工费;
                newRow.按件成本价 = item.按件成本价;
                newRow.按重成本价 = item.按重成本价;
                newRow.成本工费 = item.成本工费;
                newRow.库存上限 = item.库存上限;
                newRow.库存下限 = item.库存下限;
                newRow.备注 = item.备注;

                newDB.饰品s.Add(newRow);
            }


            try
            {
        var k=        newDB.SaveChanges();
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage($"{k/2}款饰品资料导入成功！", "系统提示");
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.Message + e.InnerException?.Message??"", "系统提示");
            }

         
        }
        public virtual bool CanImport()
        {
      
            if (this.IsInDesignMode()) return true;

            return !IsLoading && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Add;
        }

        public virtual async void BuildTemplate()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "导入导出模块";
            dlg.ValidateNames = true;
            dlg.FileName = "饰品导入模板.xlsx";
            dlg.Filter = "Excel文件 | *.xlsx";

            if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
            {
                Mouse.OverrideCursor = Cursors.Wait;
                IImporter Importer = new Magicodes.ExporterAndImporter.Excel.ExcelImporter();
                await Importer.GenerateTemplate<Import饰品>(dlg.FileName);
                Mouse.OverrideCursor = null;
                if (MessageBoxService.ShowMessage("模板已生成,是否现在打开？", "系统提示", MessageButton.YesNo) != MessageResult.Yes)
                    return;
                //   DevExpress.XtraPrinting.Native.ProcessLaunchHelper.StartProcess(dlg.FileName, false);

                //20201028升级DEV20.2.3不支持此方法了
                DevExpress.XtraPrinting.Native.ProcessLaunchHelper.StartProcess(dlg.FileName);
            }

        }





        #endregion
    }
}