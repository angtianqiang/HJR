using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.UI;
using ZtxFrameWork.UI.Comm.ViewModel;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 销售退货单ViewModel : SingleObjectViewModel<销售退货单, ZtxDB, long>
    {
        public static 销售退货单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售退货单ViewModel());
        }
        protected 销售退货单ViewModel() : base(DbFactory.Instance, x => x.销售退货单s, x => x.ID, x => x.编号, "销售退货单")
        {
            if (this.IsInDesignMode()) return;
            Init();

            Messenger.Default.Register<string>(this, "销售单号更改" + Token, m =>
            {
                SelectChildEntity.销售单明细ID = 0;
                SelectChildEntity.销售单明细 = null;
            });
            Messenger.Default.Register<string>(this, "更新金额" + Token, m =>
            {
                var item = SelectChildEntity;
                //    item.销售价 = item.工费计法 == 费用计法.按件 ? item.饰品.按件批发价 * item.数量 + item.饰品.成本工费 + item.饰品.批发工费 : item.饰品.按重批发价 * item.重量 + item.饰品.成本工费 + item.饰品.批发工费;

            });
        }
        public async void Init()
        {

           var t1 = await DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
          var t2=  await DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
          var t3=  await DbFactory.Instance.CreateDbContext().会员s.OrderBy(t => t.编号).ToListAsync();

            操作员Source = t1;
            分店Source = t2;
            会员Source = t3;
        }
        protected override IQueryable<销售退货单> DbInclude(ObjectSet<销售退货单> dbSet)
        {
            return dbSet.Include(t => t.销售退货单明细s.Select(p=>p.销售单明细));
        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        //  public virtual List<供应商> 供应商Source { get; set; }
        public virtual List<会员> 会员Source { get; set; }
        #region 明细表操作

        private void UpdateTotal()
        {
            Entity.总金额 = Entity.销售退货单明细s.Sum(t => t.销售价);
            Entity.未付金额 = Entity.总金额 - Entity.已付金额;
            Entity.数量 = Entity.销售退货单明细s.Sum(t => t.数量);

        }

        protected override void UpdateCommands()
        {
            base.UpdateCommands();
            this.RaiseCanExecuteChanged(x => x.AddChildRow());
            this.RaiseCanExecuteChanged(x => x.DeleteChildRow());
            this.RaiseCanExecuteChanged(x => x.SK());
            this.RaiseCanExecuteChanged(x => x.TH());
        }



        public virtual void ShowList(object startCode)
        {
            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = DB;
            List<dynamic> list = db.销售单明细s.Include(t => t.销售单).Include(t => t.饰品).Include(t => t.饰品.单位).Include(t => t.饰品.重量单位)
                  .Where(t => t.销售单.编号.StartsWith(startStr) && t.销售单.状态 != "N")
                       .Select(t => new
                       {
                           ID = t.ID,
                           编号 = t.销售单.编号,
                           品名 = t.饰品.品名,
                           单位 = t.饰品.单位.名称,
                           重量单位 = t.饰品.重量单位.名称,
                           尺寸 = t.饰品.尺寸,
                           数量 = t.数量,
                           重量 = t.重量,
                           销售价 = t.销售价,
                           金额 = t.金额,
                           工费计法 = t.工费计法,
                           工费 = t.工费,
                           折扣 = t.折扣,
                           折前价 = t.折前价
                       })
                  .ToList<dynamic>();
            //if (list.Count==1)
            //{
            //    SelectChildEntity.饰品ID = list[0].ID;
            //this.DB.Entry(SelectChildEntity).Reference(t =>t.饰品).Load();


            //}
            //else
            {
                CommQueryListViewModel VM = ViewModelSource.Create<CommQueryListViewModel>(() => new CommQueryListViewModel() { Title = "饰品清单", Entities = list });
                IDocument doc = QueryListManagerService.CreateDocument(Utils.ApplictionConfigValue.CommQueryListViewName, VM);
                doc.Show();
                if (VM.IsSelect == true)
                {
                    SelectChildEntity.销售单明细ID = VM.SelectEntity.ID;
                    this.DB.Entry(SelectChildEntity).Reference(t => t.销售单明细).Load();
                    this.DB.Entry(SelectChildEntity.销售单明细).Reference(t => t.饰品).Load();
                    SelectChildEntity.销售单号 = VM.SelectEntity.编号;
                    SelectChildEntity.数量 = VM.SelectEntity.数量;
                    SelectChildEntity.重量 = VM.SelectEntity.重量;
                    SelectChildEntity.销售价 = VM.SelectEntity.金额;
                    SelectChildEntity.金额 = VM.SelectEntity.金额;
                    SelectChildEntity.工费计法 = VM.SelectEntity.工费计法;
                    SelectChildEntity.工费 = VM.SelectEntity.工费;
                    SelectChildEntity.折扣 = VM.SelectEntity.折扣;
                    SelectChildEntity.折前价 = VM.SelectEntity.折前价;
                    UpdateTotal();
                    //UIElement element = Keyboard.FocusedElement as UIElement;
                    //if (element != null)
                    //{
                    //    element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    //}
                }
            }
        }


        public virtual 销售退货单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;


            var item = DB.销售退货单明细s.Create();
            // item.PropertyChanged += Item_PropertyChanged;
            item.DirtyState = DirtyState.Added;
            item.工费计法 = 费用计法.按重;
            //  item.金额 = 12.0M;
            if (Entity.销售退货单明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.销售退货单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.销售退货单明细s.Add(item);
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

            Entity.销售退货单明细s.Remove(temp);
            DB.销售退货单明细s.Remove(temp);
            UpdateTotal();
            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.销售退货单明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170318 单据确认操作
        protected override void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 销售退货单 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.销售退货单明细s.Count <= 0)
                throw new Exception("没有单身内容");
            foreach (var item in entity.销售退货单明细s)
            {
                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.销售单明细.饰品ID && t.分店ID == entity.分店ID).SingleOrDefault();
                if (temp == null)
                {
                    temp = dbContext.库存s.Add(new 库存() { 饰品ID = item.销售单明细.饰品ID, 分店ID = entity.分店ID });
                }

                temp.数量 += item.数量;
                temp.重量 += item.重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.销售单明细.饰品ID).Single();
                product.库存数量 += item.数量;
                product.库存重量 += item.重量;
                //  product.库存总金额 += item.金额;

                //成本小计
                product.账面成本小计 += item.工费计法 == 费用计法.按件 ? item.销售单明细.饰品.按件成本价 * item.数量 : item.销售单明细.饰品.按重成本价 * item.重量;
                product.库存总金额 += item.工费计法 == 费用计法.按件 ? item.销售单明细.饰品.按件成本价 * item.数量 : item.销售单明细.饰品.按重成本价 * item.重量;

                // 成本              
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //写出入明细
                dbContext.库存出入明细s.Add(new 库存出入明细()
                {
                    日期 = DateTime.Now,
                    相关单据 = "销售退货单",
                    单据ID = item.ID,
                    单据编号 = entity.编号,
                    出入别 = "I",
                    数量 = item.数量,
                    重量 = item.重量,
                    金额 = item.工费计法 == 费用计法.按件 ? item.销售单明细.饰品.按件成本价 * item.数量 : item.销售单明细.饰品.按重成本价 * item.重量,
                    分店ID = entity.分店ID,
                    饰品ID = item.销售单明细.饰品ID
                });
            }
        }

        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 销售退货单 entity)
        {
            if (entity.已付金额 != 0)
            {
                throw new Exception("已付金额不为0");
            }
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            foreach (var item in entity.销售退货单明细s)
            {
                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.销售单明细.饰品ID && t.分店ID == entity.分店ID).Single();
                temp.数量 -= item.数量;
                temp.重量 -= item.重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.销售单明细.饰品ID).Single();
                product.库存数量 -= item.数量;
                product.库存重量 -= item.重量;
                //  product.库存总金额 -= item.金额;
                //成本小计
                product.账面成本小计 -= item.工费计法 == 费用计法.按件 ? item.销售单明细.饰品.按件成本价 * item.数量 : item.销售单明细.饰品.按重成本价 * item.重量;
                product.库存总金额 -= item.工费计法 == 费用计法.按件 ? item.销售单明细.饰品.按件成本价 * item.数量 : item.销售单明细.饰品.按重成本价 * item.重量;

                // 成本              
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //写出入明细
                var inOutDetails = dbContext.库存出入明细s.Where(t => t.单据ID == item.ID && t.单据编号 == entity.编号).Single();
                dbContext.Entry(inOutDetails).State = EntityState.Deleted;
            }
            dbContext.SaveChanges();
        }
        #endregion
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 销售退货单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.销售退货单明细s.RemoveRange(entity.销售退货单明细s);
        }
        #endregion


        #region 20170321 收款 退货操作
        public virtual async void SK()
        {
            //生成收款单
            long parKey = GetPrimaryKey(Entity);
            long NewKey = 0;
            using (var dbContext = DbFactory.Instance.CreateDbContext())
            {
                var skd = dbContext.收款单s.Create();


                skd.编号 = await CollectionViewModel<收款单, ZtxDB, long>.GetNewCode("SK", DbFactory.Instance, x => x.收款单s, t => t.编号);
                skd.收款日期 = DateTime.Now;
                skd.会员ID = Entity.操作员ID;
                //  skd.状态 = "Y";//直接生效
                skd.分店ID = Entity.分店ID;
                skd.操作员ID = Entity.操作员ID;
                //  skd.销售单ID = Entity.ID;
                // skd.金额 = Entity.已付金额* -1m;

                dbContext.收款单s.Add(skd);
                var temp = new 收款单明细()
                {
                    序号 = 1,
                    应收金额 = Entity.未付金额 * -1m,
                    本次收入金额 = Entity.未付金额 * -1m,
                    销售退货单ID = Entity.ID,
                    销售退货单号 = Entity.编号,

                };
                dbContext.收款单明细s.Add(temp);
                skd.收款单明细s.Add(temp);

                skd.实收金额 = skd.收款单明细s.Sum(t => t.本次收入金额);
                skd.应收金额 = skd.收款单明细s.Sum(t => t.应收金额);

                //var temp = dbContext.销售退货单s.Where(t => t.ID == parKey).Single();
                //temp.已付金额 -= Entity.已付金额;
                //temp.未付金额 += temp.总金额 - temp.已付金额;
                dbContext.SaveChanges();
                NewKey = skd.ID;
            }

            base.LoadEntityByKey(base.PrimaryKey);
            GetDocumentManagerService().ShowExistingEntityDocument<收款单, long>(this, NewKey);
        }
        public virtual bool CanSK()
        {
            if (this.IsInDesignMode())
                return true;
            return Entity!=null&& Entity.已付金额 < Entity.总金额 && ((IBillEntity)Entity).状态 != "N";
        }
        public virtual void TH()
        {




        }
        public virtual bool CanTH()
        {

            if (this.IsInDesignMode())
                return true;
            return Entity != null && ((IBillEntity)Entity).状态 != "N";
        }
        #endregion
        ////20170321 用户不新建  和编辑退库单，只
        //public override void UpdateIsReadOnly()
        //{
        //    IsReadOnly = true;
        //}
    }
}