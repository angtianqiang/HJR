using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class 销售单ViewModel : SingleObjectViewModel<销售单, ZtxDB, long>
    {
        public static 销售单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 销售单ViewModel());
        }
        protected 销售单ViewModel() : base(DbFactory.Instance, x => x.销售单s, x => x.ID, x => x.编号, "销售单")
        {
            if (this.IsInDesignMode()) return;

            //   var db = DB;
            var db = this.DB;
            操作员Source = db.Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToList();
            分店Source = db.分店s.OrderBy(t => t.名称).ToList();
            System.Diagnostics.Debug.WriteLine($"销售单ViewModel(): { System.Threading.Thread.CurrentThread.ManagedThreadId}");
            //  供应商Source = db.供应商s.OrderBy(t => t.简称).ToList();
            会员Source = db.会员s.OrderBy(t => t.编号).ToList();
            Messenger.Default.Register<string>(this, "饰品编号更改" + Token, m =>
            {
                SelectChildEntity.饰品ID = 0;
                SelectChildEntity.饰品 = null;
            });
            Messenger.Default.Register<string>(this, "更新金额" + Token, m =>
            {

                UpdatePrice();
            });
        }

        public void UpdatePrice()
        {
            var item = SelectChildEntity;
            item.销售价 = item.工费计法 == 费用计法.按件 ? item.饰品?.QtyPrice?? 0 : item.饰品?.WeightPrice??0;
            item.工费 = item.工费计法 == 费用计法.按件 ? item.饰品?.批发工费 ?? 0: item.饰品?.批发工费??0;
            //   item.重量 = item.数量 * item.饰品.单重;
            item.折前价 = item.工费计法 == 费用计法.按件 ? item.数量 * item.销售价 : item.重量 * item.销售价;
            item.金额 = item.折前价 * item.折扣;
            UpdateTotal();
        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        //   public virtual List<供应商> 供应商Source { get; set; }
        public virtual List<会员> 会员Source { get; set; }
        #region 明细表操作
        private void UpdateTotal()
        {
            Entity.总金额 = Entity.销售单明细s.Sum(t => t.金额);
            Entity.未收金额 = Entity.总金额 - Entity.已收金额;
            Entity.数量 = Entity.销售单明细s.Sum(t => t.数量);
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
            List<dynamic> list = db.饰品s.Include(t => t.单位).Include(t => t.重量单位)
                  .Where(t => t.编号.StartsWith(startStr))
                .Select(t => new { ID = t.ID, 编号 = t.编号, 品名 = t.品名, 单位 = t.单位.名称, 重量单位 = t.重量单位.名称, 尺寸 = t.尺寸, 工费计法 = t.工费计法 })
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
                    SelectChildEntity.饰品ID = VM.SelectEntity.ID;
                    if (this.DB.Entry(SelectChildEntity).Reference(t => t.饰品).IsLoaded)
                    {
                        this.DB.Entry(SelectChildEntity.饰品).Reload();
                    }
                    else
                    {
                        this.DB.Entry(SelectChildEntity).Reference(t => t.饰品).Load();
                    }
                    SelectChildEntity.饰品编号 = SelectChildEntity.饰品.编号;
                    UpdatePrice();
                }
            }
        }


        public virtual 销售单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;


            var item = DB.销售单明细s.Create();
            // item.PropertyChanged += Item_PropertyChanged;
            item.DirtyState = DirtyState.Added;
            item.工费计法 = 费用计法.按重;
            item.折扣 = 1M;
            //  item.金额 = 12.0M;
            if (Entity.销售单明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.销售单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.销售单明细s.Add(item);
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

            Entity.销售单明细s.Remove(temp);
            DB.销售单明细s.Remove(temp);
            UpdateTotal();
            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.销售单明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170318 单据确认操作
        protected override void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 销售单 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.销售单明细s.Count <= 0)
                throw new Exception("没有单身内容");
            foreach (var item in entity.销售单明细s)
            {
                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.分店ID).SingleOrDefault();
                if (temp == null)
                {
                    temp = dbContext.库存s.Add(new 库存() { 饰品ID = item.饰品ID, 分店ID = entity.分店ID });
                }

                temp.数量 -= item.数量;
                temp.重量 -= item.重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.饰品ID).Single();
                product.库存数量 -= item.数量;
                product.库存重量 -= item.重量;
                //  product.库存总金额 += item.金额;

                //成本小计
                product.账面成本小计 -= item.工费计法 == 费用计法.按件 ? item.饰品.按件成本价 * item.数量 : item.饰品.按重成本价 * item.重量;
                product.库存总金额 -= item.工费计法 == 费用计法.按件 ? item.饰品.按件成本价 * item.数量 : item.饰品.按重成本价 * item.重量;

                // 成本              
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //写出入明细
                dbContext.库存出入明细s.Add(new 库存出入明细()
                {
                    日期 = DateTime.Now,
                    相关单据 = "销售单",
                    单据ID = item.ID,
                    单据编号 = entity.编号,
                    出入别 = "O",
                    数量 = item.数量,
                    重量 = item.重量,
                    金额 = item.工费计法 == 费用计法.按件 ? item.饰品.按件成本价 * item.数量 : item.饰品.按重成本价 * item.重量,
                    分店ID = entity.分店ID,
                    饰品ID = item.饰品ID
                });
            }
        }

        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 销售单 entity)
        {
            if (entity.已收金额 != 0)
            {
                throw new Exception("已收金额不为0");
            }
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            foreach (var item in entity.销售单明细s)
            {
                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.分店ID).Single();
                temp.数量 += item.数量;
                temp.重量 += item.重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.饰品ID).Single();
                product.库存数量 += item.数量;
                product.库存重量 += item.重量;
                //  product.库存总金额 -= item.金额;
                //成本小计
                product.账面成本小计 += item.工费计法 == 费用计法.按件 ? item.饰品.按件成本价 * item.数量 : item.饰品.按重成本价 * item.重量;
                product.库存总金额 += item.工费计法 == 费用计法.按件 ? item.饰品.按件成本价 * item.数量 : item.饰品.按重成本价 * item.重量;

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
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 销售单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.销售单明细s.RemoveRange(entity.销售单明细s);
        }
        #endregion


        #region 20170321 收款 退货操作
        public virtual void SK()
        {
            //生成收款单
            long parKey = GetPrimaryKey(Entity);
            long NewKey = 0;
            using (var dbContext = DbFactory.Instance.CreateDbContext())
            {
                var skd = dbContext.收款单s.Create();


                skd.编号 = CollectionViewModel<收款单, ZtxDB, long>.GetNewCode("SK", DbFactory.Instance, x => x.收款单s, t => t.编号);
                skd.收款日期 = DateTime.Now;
                skd.会员ID = Entity.操作员ID;
                //  skd.状态 = "Y";//直接生效
                skd.分店ID = Entity.分店ID;
                skd.操作员ID = Entity.操作员ID;

                dbContext.收款单s.Add(skd);
                var temp = new 收款单明细()
                {
                    序号 = 1,
                    应收金额 = Entity.未收金额,
                    本次收入金额 = Entity.未收金额,
                    销售单ID = Entity.ID,
                    销售单号 = Entity.编号,

                };
                dbContext.收款单明细s.Add(temp);
      skd.收款单明细s.Add(temp);

                skd.实收金额 = skd.收款单明细s.Sum(t => t.本次收入金额);
                skd.应收金额 = skd.收款单明细s.Sum(t => t.应收金额);

                //    var temp = dbContext.销售单s.Where(t => t.ID == parKey).Single();

                //    temp.已收金额 += Entity.未收金额;
                //     temp.未收金额 = temp.总金额 - temp.已收金额;
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
            return Entity.已收金额 < Entity.总金额 && ((dynamic)Entity).状态 != "N";
        }
        public virtual void TH()
        {
            //生成退货单
            long parKey = GetPrimaryKey(Entity);
            long NewKey = 0;
            using (var dbContext = DbFactory.Instance.CreateDbContext())
            {
                var skd = dbContext.销售退货单s.Create();


                skd.编号 = CollectionViewModel<销售退货单, ZtxDB, long>.GetNewCode("TH", DbFactory.Instance, x => x.销售退货单s, t => t.编号);
                skd.日期 = DateTime.Now;
                skd.会员ID = Entity.操作员ID;
                //  skd.状态 = "Y";//直接生效
                skd.分店ID = Entity.分店ID;
                skd.操作员ID = Entity.操作员ID;
                skd.状态 = "N";
                //  skd.销售单ID = Entity.ID;
                // skd.金额 = Entity.已付金额* -1m;

                dbContext.销售退货单s.Add(skd);
                int k = 1;
                foreach (var item in Entity.销售单明细s)
                {
                    var temp = new 销售退货单明细()
                    {
                        序号 = k++,
                        工费 = item.工费,
                        工费计法 = item.工费计法,
                        折前价 = item.折前价,
                        折扣 = item.折扣,
                        数量 = item.数量,
                        重量 = item.重量,
                        金额 = item.金额,
                        销售价 = item.销售价,
                        销售单号 = Entity.编号,
                        销售单明细ID = item.ID

                    };
            

                    dbContext.销售退货单明细s.Add(temp);
                    skd.销售退货单明细s.Add(temp);
                }


                skd.总金额 = skd.销售退货单明细s.Sum(t => t.销售价);
                skd.未付金额 = skd.总金额 - skd.已付金额;
                skd.数量 = skd.销售退货单明细s.Sum(t => t.数量);

                //var temp = dbContext.销售退货单s.Where(t => t.ID == parKey).Single();
                //temp.已付金额 -= Entity.已付金额;
                //temp.未付金额 += temp.总金额 - temp.已付金额;
                dbContext.SaveChanges();
                NewKey = skd.ID;
            }

            base.LoadEntityByKey(base.PrimaryKey);
            GetDocumentManagerService().ShowExistingEntityDocument<销售退货单, long>(this, NewKey);
        }
        public virtual bool CanTH()
        {

            if (this.IsInDesignMode())
                return true;
            return ((dynamic)Entity).状态 != "N";
        }
        #endregion
    }
}