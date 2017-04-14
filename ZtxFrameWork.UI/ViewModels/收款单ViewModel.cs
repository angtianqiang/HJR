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
    public class 收款单ViewModel : SingleObjectViewModel<收款单, ZtxDB, long>
    {
        public static 收款单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 收款单ViewModel());
        }
        protected 收款单ViewModel() : base(DbFactory.Instance, x => x.收款单s, x=>x.ID, x => x.编号, "收款单")
        {
            if (this.IsInDesignMode()) return;
            Init();
          
            Messenger.Default.Register<string>(this, "销售单号更改" + Token, m =>
            {
                SelectChildEntity.销售单ID = null;
                SelectChildEntity.销售单 = null;
            });
            Messenger.Default.Register<string>(this, "销售退货单号更改" + Token, m =>
            {
                SelectChildEntity.销售退货单ID = null;
                SelectChildEntity.销售退货单 = null;
            });
            Messenger.Default.Register<string>(this, "本次收入金额更改" + Token, m =>
            {
                //  var item = SelectChildEntity;
                //   item.金额 = item.计价方式 == 费用计法.按件 ? item.单价 * item.数量 : item.单价 * item.重量;
                UpdateTotal();
            });
        }
        public async void Init()
        {
           
           var t1 =await DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
          var t2=  await DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
          var t3=  await DbFactory.Instance.CreateDbContext().会员s.OrderBy(t => t.编号).ToListAsync();
            操作员Source = t1;
            分店Source = t2;
            会员Source = t3;

        }
        protected override IQueryable<收款单> DbInclude(ObjectSet<收款单> dbSet)
        {
            return dbSet.Include(t => t.收款单明细s);
        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        public virtual List<会员> 会员Source { get; set; }
    
        #region 明细表操作

        private void UpdateTotal()
        {
            Entity.实收金额 = Entity.收款单明细s.Sum(t => t.本次收入金额);
            Entity.应收金额 = Entity.收款单明细s.Sum(t => t.应收金额);

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
            List<dynamic> list = db.销售单s
                  .Where(t => t.编号.StartsWith(startStr) && t.状态 != "N" && t.未收金额 > 0m && t.分店ID == Entity.分店ID)
                .Select(t => new { ID = t.ID, 编号 = t.编号, 品名 = t.日期,  总金额 = t.总金额, 已收金额 = t.已收金额, 未收金额 = t.未收金额 })
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
                    SelectChildEntity.销售单ID = VM.SelectEntity.ID;
                    this.DB.Entry(SelectChildEntity).Reference(t => t.销售单).Load();
                    SelectChildEntity.销售单号 = VM.SelectEntity.编号;

                    SelectChildEntity.销售退货单ID = null;
                    SelectChildEntity.销售退货单 = null;
                    SelectChildEntity.销售退货单号 = "";

                    SelectChildEntity.应收金额 = SelectChildEntity.本次收入金额 = VM.SelectEntity.未收金额;
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
            List<dynamic> list = db.销售退货单s
                  .Where(t => t.编号.StartsWith(startStr) && t.状态 != "N" && t.未付金额 > 0m && t.分店ID == Entity.分店ID)
                .Select(t => new { ID = t.ID, 编号 = t.编号, 品名 = t.日期,  总金额 = t.总金额, 已付金额 = t.已付金额, 未付金额 = t.未付金额 })
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
                    SelectChildEntity.销售单ID = null;
                    SelectChildEntity.销售单 = null;
                    SelectChildEntity.销售单号 = "";

                    SelectChildEntity.销售退货单ID = VM.SelectEntity.ID;
                    this.DB.Entry(SelectChildEntity).Reference(t => t.销售退货单).Load();
                    SelectChildEntity.销售退货单 = VM.SelectEntity.编号;

                    SelectChildEntity.应收金额 = SelectChildEntity.本次收入金额 = VM.SelectEntity.未付金额 * -1m;
                    UpdateTotal();
                }
            }
        }

        public virtual 收款单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;


            var item = DB.收款单明细s.Create();

            item.DirtyState = DirtyState.Added;


            if (Entity.收款单明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.收款单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.收款单明细s.Add(item);
            UpdateTotal();
            Mouse.OverrideCursor = null;
        }



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

            Entity.收款单明细s.Remove(temp);
            DB.收款单明细s.Remove(temp);
            UpdateTotal();
            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.收款单明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170318 单据确认操作
        protected override void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 收款单 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.收款单明细s.Count <= 0)
                throw new Exception("没有单身内容");
            foreach (var item in entity.收款单明细s)
            {
                if (item.销售单ID > 0)
                {
                    var temp = dbContext.销售单s.Where(t => t.ID == item.销售单ID && t.状态 != "N").Single();
                    temp.已收金额 += item.本次收入金额;
                    temp.未收金额 = temp.总金额 - temp.已收金额;

                }
                if (item.销售退货单ID > 0)
                {
                    var temp = dbContext.销售退货单s.Where(t => t.ID == item.销售退货单ID && t.状态 != "N").Single();
                    temp.已付金额 += item.本次收入金额 * -1m;
                    temp.未付金额 = temp.总金额 - temp.已付金额;
                }


            }
        }

        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 收款单 entity)
        {
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            foreach (var item in entity.收款单明细s)
            {
                if (item.销售单ID > 0)
                {
                    var temp = dbContext.销售单s.Where(t => t.ID == item.销售单ID && t.状态 != "N").Single();
                    temp.已收金额 -= item.本次收入金额;
                    temp.未收金额 = temp.总金额 - temp.已收金额;

                }
                if (item.销售退货单ID > 0)
                {
                    var temp = dbContext.销售退货单s.Where(t => t.ID == item.销售退货单ID && t.状态 != "N").Single();
                    temp.已付金额 -= item.本次收入金额 * -1m;
                    temp.未付金额 = temp.总金额 - temp.已付金额;
                }


            }
        }
        #endregion
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 收款单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.收款单明细s.RemoveRange(entity.收款单明细s);
        }
        #endregion
    }
}