using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows.Input;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.UI;
using ZtxFrameWork.UI.Comm.ViewModel;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 付款单ViewModel : SingleObjectViewModel<付款单, ZtxDB, long>
    {
        public static 付款单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 付款单ViewModel());
        }
        protected 付款单ViewModel() : base(DbFactory.Instance, x => x.付款单s, x => x.ID, x => x.编号, "付款单")
        {
            if (this.IsInDesignMode()) return;
            Init();
            Messenger.Default.Register<string>(this, "入库单号更改" + Token, m =>
            {
                SelectChildEntity.入库单ID = null;
                SelectChildEntity.入库单 = null;
            });
            Messenger.Default.Register<string>(this, "入库单号更改" + Token, m =>
            {
                SelectChildEntity.退库单ID = null;
                SelectChildEntity.退库单 = null;
            });
            Messenger.Default.Register<string>(this, "本次支付金额更改" + Token, m =>
            {
                //  var item = SelectChildEntity;
                //   item.金额 = item.计价方式 == 费用计法.按件 ? item.单价 * item.数量 : item.单价 * item.重量;
                UpdateTotal();
            });
        }
        public async void Init()
        {
          var t1   = await DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
          var t2=   await DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
          var t3   = await DbFactory.Instance.CreateDbContext().供应商s.OrderBy(t => t.简称).ToListAsync();
            操作员Source = t1;
            分店Source = t2;
            供应商Source  = t3;

        }
        protected override IQueryable<付款单> DbInclude(ObjectSet<付款单> dbSet)
        {
            return dbSet.Include(t => t.付款单明细s);
        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        //   public virtual List<会员> 会员Source { get; set; }
        public virtual List<供应商> 供应商Source { get; set; }
        #region 明细表操作

        private void UpdateTotal()
        {
            Entity.实付金额 = Entity.付款单明细s.Sum(t => t.本次支付金额);
            Entity.应付金额 = Entity.付款单明细s.Sum(t => t.应付金额);

        }

        protected override void UpdateCommands()
        {
            base.UpdateCommands();
            this.RaiseCanExecuteChanged(x => x.AddChildRow());
            this.RaiseCanExecuteChanged(x => x.DeleteChildRow());


        }


        //入库单
        public virtual void ShowList(object startCode)
        {
            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = DB;
            List<dynamic> list = db.入库单s.Include(t => t.供应商)
                  .Where(t => t.编号.StartsWith(startStr) && t.供应商.ID == Entity.供应商ID && t.状态 != "N" && t.未付金额 > 0m && t.分店ID == Entity.分店ID)
                .Select(t => new { ID = t.ID, 编号 = t.编号, 品名 = t.日期, 供应商 = t.供应商.简称, 总金额 = t.总金额, 已付金额 = t.已付金额, 未付金额 = t.未付金额 })
                  .ToList<dynamic>();
            //if (list.Count==1)
            //{
            //    SelectChildEntity.饰品ID = list[0].ID;
            //this.DB.Entry(SelectChildEntity).Reference(t =>t.饰品).Load();


            //}
            //else
            {
                CommQueryListViewModel VM = ViewModelSource.Create<CommQueryListViewModel>(() => new CommQueryListViewModel() { Title = "清单", Entities = list });
                IDocument doc = QueryListManagerService.CreateDocument(Utils.ApplictionConfigValue.CommQueryListViewName, VM);
                doc.Show();
                if (VM.IsSelect == true)
                {
                    SelectChildEntity.入库单ID = VM.SelectEntity.ID;
                    this.DB.Entry(SelectChildEntity).Reference(t => t.入库单).Load();
                    SelectChildEntity.入库单号 = VM.SelectEntity.编号;

                    SelectChildEntity.退库单ID = null;
                    SelectChildEntity.退库单 = null;
                    SelectChildEntity.退库单号 = "";

                    SelectChildEntity.应付金额 = SelectChildEntity.本次支付金额 = VM.SelectEntity.未付金额;
                    UpdateTotal();
                }
            }
        }
        //退库单
        public virtual void ShowList2(object startCode)
        {
            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = DB;
            List<dynamic> list = db.退库单s.Include(t => t.供应商)
                  .Where(t => t.编号.StartsWith(startStr) && t.供应商.ID == Entity.供应商ID && t.状态 != "N" && t.未收金额 > 0m && t.分店ID == Entity.分店ID)
                .Select(t => new { ID = t.ID, 编号 = t.编号, 品名 = t.日期, 供应商 = t.供应商.简称, 总金额 = t.总金额, 已收金额 = t.已收金额, 未收金额 = t.未收金额 })
                  .ToList<dynamic>();
            //if (list.Count==1)
            //{
            //    SelectChildEntity.饰品ID = list[0].ID;
            //this.DB.Entry(SelectChildEntity).Reference(t =>t.饰品).Load();


            //}
            //else
            {
                CommQueryListViewModel VM = ViewModelSource.Create<CommQueryListViewModel>(() => new CommQueryListViewModel() { Title = "清单", Entities = list });
                IDocument doc = QueryListManagerService.CreateDocument(Utils.ApplictionConfigValue.CommQueryListViewName, VM);
                doc.Show();
                if (VM.IsSelect == true)
                {
                    SelectChildEntity.入库单ID = null;
                    SelectChildEntity.入库单 = null;
                    SelectChildEntity.入库单号 = "";

                    SelectChildEntity.退库单ID = VM.SelectEntity.ID;
                    this.DB.Entry(SelectChildEntity).Reference(t => t.退库单).Load();
                    SelectChildEntity.退库单号 = VM.SelectEntity.编号;

                    SelectChildEntity.应付金额 = SelectChildEntity.本次支付金额 = VM.SelectEntity.未收金额 * -1m;
                    UpdateTotal();
                }
            }
        }

        public virtual 付款单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;


            var item = DB.付款单明细s.Create();

            item.DirtyState = DirtyState.Added;


            if (Entity.付款单明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.付款单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.付款单明细s.Add(item);
            UpdateTotal();
            Mouse.OverrideCursor = null;
        }



        public virtual bool CanAddChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.分店ID != 0 && Entity.供应商ID != 0;
        }
        public virtual void DeleteChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var temp = SelectChildEntity;

            Entity.付款单明细s.Remove(temp);
            DB.付款单明细s.Remove(temp);
            UpdateTotal();
            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.付款单明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170318 单据确认操作
        protected override void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 付款单 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.付款单明细s.Count <= 0)
                throw new Exception("没有单身内容");
            foreach (var item in entity.付款单明细s)
            {
                if (item.入库单ID > 0)
                {
                    var temp = dbContext.入库单s.Where(t => t.ID == item.入库单ID && t.状态 != "N").Single();
                    temp.已付金额 += item.本次支付金额;
                    temp.未付金额 = temp.总金额 - temp.已付金额;

                }
                if (item.退库单ID > 0)
                {
                    var temp = dbContext.退库单s.Where(t => t.ID == item.退库单ID && t.状态 != "N").Single();
                    temp.已收金额 += item.本次支付金额 * -1m;
                    temp.未收金额 = temp.总金额 - temp.已收金额;
                }


            }
        }

        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 付款单 entity)
        {
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            foreach (var item in entity.付款单明细s)
            {
                if (item.入库单ID > 0)
                {
                    var temp = dbContext.入库单s.Where(t => t.ID == item.入库单ID && t.状态 != "N").Single();
                    temp.已付金额 -= item.本次支付金额;
                    temp.未付金额 = temp.总金额 - temp.已付金额;

                }
                if (item.退库单ID > 0)
                {
                    var temp = dbContext.退库单s.Where(t => t.ID == item.退库单ID && t.状态 != "N").Single();
                    temp.已收金额 -= item.本次支付金额 * -1m;
                    temp.未收金额 = temp.总金额 - temp.已收金额;
                }


            }
        }
        #endregion
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 付款单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.付款单明细s.RemoveRange(entity.付款单明细s);
        }
        #endregion
    }
}