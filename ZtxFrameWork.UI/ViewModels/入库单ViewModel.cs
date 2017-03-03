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
using System.Windows.Input;
using ZtxFrameWork.UI.Comm.UI;
using ZtxFrameWork.UI.Extensions;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 入库单ViewModel : SingleObjectViewModel<入库单, ZtxDB, long>
    {
        public static 入库单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 入库单ViewModel());
        }
        protected 入库单ViewModel() : base(DbFactory.Instance, x => x.入库单s, x=>x.ID, x => x.编号)
        {
            if (this.IsInDesignMode()) return;


          //  Entity.入库单明细s.AcceTChanges();

            var db = dbFactory.CreateDbContext();
            操作员Source = db.Users.Where(t=>t.IsFrozen==false).OrderBy(t => t.UserName).ToList();
            分店Source = db.分店s.OrderBy(t => t.名称).ToList();

            供应商Source = db.供应商s.OrderBy(t => t.简称).ToList();


            Messenger.Default.Register<string>(this, "饰品编号更改" + Token, m =>
            {
                SelectChildEntity.饰品ID = 0;
                SelectChildEntity.饰品 = null;
            });

            Messenger.Default.Register<string>(this, "更新金额" + Token, m =>
            {
                var item = SelectChildEntity;
                item.金额 = item.计价方式 == "按件" ? item.单价 * item.数量 : item.单价 * item.重量 * 10;
                Entity.总金额 = Entity.入库单明细s.Max(t => t.金额);
            });

        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        public virtual List<供应商> 供应商Source { get; set; }


        public virtual bool IsReadOnly { get; set; }//根据单据的生效状态和权限判断是否可编辑
        protected override void UpdateCommands()
        {
            base.UpdateCommands();
            this.RaiseCanExecuteChanged(x => x.AddChildRow());
            this.RaiseCanExecuteChanged(x => x.DeleteChildRow());
            if (Entity.状态=="N")
            {
                IsReadOnly = false;
            }
            else
            {
                IsReadOnly = true;
            }

        }
        protected IDocumentManagerService QueryListManagerService { get { return this.GetRequiredService<IDocumentManagerService>("QueryListDocumentManagerService"); } }
        //colleciton页面的服务
        protected virtual IDocumentManagerService GetDocumentManagerService() {return this.GetRequiredService<IDocumentManagerService>("SignleObjectDocumentManagerService"); }


      

        public virtual void ShowList(object startCode)
        {
            var a = GetDocumentManagerService();

            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = dbFactory.CreateDbContext();
          List<dynamic> list=  db.饰品s.Where(t => t.编号.StartsWith(startStr)).Select(t => new {ID=t.ID, 编号=t.编号}).ToList< dynamic>();
            //if (list.Count==1)
            //{
            //    SelectChildEntity.饰品ID = list[0].ID;
            //this.DB.Entry(SelectChildEntity).Reference(t =>t.饰品).Load();
              
         
            //}
            //else
            {
               CommQueryListViewModel VM = ViewModelSource.Create< CommQueryListViewModel>(() => new CommQueryListViewModel() { Title = "饰品清单", Entities = list });
      IDocument doc=    QueryListManagerService.CreateDocument("CommQueryListView", VM);
                doc.Show();
                if (VM.IsSelect==true)
                {
                    SelectChildEntity.饰品ID = VM.SelectEntity.ID;
                    this.DB.Entry(SelectChildEntity).Reference(t => t.饰品).Load();
                    SelectChildEntity.饰品编号 = SelectChildEntity.饰品.编号;
                }
            }
        }


        public virtual 入库单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;
          
        
            var item = DB.入库单明细s.Create();
           // item.PropertyChanged += Item_PropertyChanged;
            item.DirtyState = DirtyState.Added;
            item.计价方式 = "按重";
          //  item.金额 = 12.0M;
            if (Entity.入库单明细s.Count==0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.入库单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.入库单明细s.Add(item);
            Mouse.OverrideCursor = null;
        }

        //private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    入库单明细 item =sender as 入库单明细;
        //    //string col1 = e.GetPropertyName(() => item.数量);
        //    //string col2 = e.GetPropertyName(() => item.重量;
        //    //string col3 = e.GetPropertyName(() => item.单价);
        //    //string col4 = e.GetPropertyName(() => item.计价方式);
        //    switch (e.PropertyName)
        //    {
        //        case "数量":
        //        case "重量":
        //        case "单价":
        //        case "计价方式":
        //            item.金额 = item.计价方式 == "按件" ? item.单价 * item.数量 : item.单价 * item.重量 * 10;
        //            break;
        //        default:
        //            break;
        //    }


          
        //}

        public virtual bool CanAddChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.分店ID != 0;
        }
        public virtual void DeleteChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var temp = SelectChildEntity;
        
            Entity.入库单明细s.Remove(temp);
            DB.入库单明细s.Remove(temp);
            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.入库单明细s.Count > 0 && SelectChildEntity != null;
        }
        public virtual void  Confirm()
        {
            Entity.状态 = "Y";
            base.Save();
            UpdateCommands();
        }
        public virtual bool CanConfirm()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return !DB.ChangeTracker.HasChanges() && Entity.状态 == "N";
        }
        public virtual void UnConfirm()
        {
            Entity.状态 = "N";
            base.Save();
            UpdateCommands();
        }
        public virtual bool CanUnConfirm()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return !DB.ChangeTracker.HasChanges() && Entity.状态 == "Y";
        }

    }
}