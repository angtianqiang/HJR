﻿using System;
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
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class 退库单ViewModel : SingleObjectViewModel<退库单, ZtxDB, long>
    {
        public static 退库单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 退库单ViewModel());
        }
        protected 退库单ViewModel() : base(DbFactory.Instance, x => x.退库单s, x => x.ID, x => x.编号, "采购退货单")
        {
            if (this.IsInDesignMode()) return;
            Init();


            Messenger.Default.Register<string>(this, "入库单号更改" + Token, m =>
            {
                SelectChildEntity.入库单明细ID = 0;
                SelectChildEntity.入库单明细 = null;
            });

            Messenger.Default.Register<string>(this, "更新金额" + Token, m =>
            {
                var item = SelectChildEntity;
                item.金额 = item.计价方式 == 费用计法.按件 ? item.单价 * item.数量 : item.单价 * item.重量;
                UpdateTotal();
            });

        }
        public  void Init()
        {

            //var t1 = await DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
            //var t2 = await DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
            //var t3 = await DbFactory.Instance.CreateDbContext().供应商s.OrderBy(t => t.简称).ToListAsync();
            操作员Source = Helpers.CacheHelper.操作员Source;
            分店Source = Helpers.CacheHelper.分店Source;
            供应商Source = Helpers.CacheHelper.供应商Source;

        }
        protected override void SetDetailsDirtyState()
        {
            base.SetDetailsDirtyState();
            foreach (var item in Entity.退库单明细s)
            {
                item.DirtyState = DirtyState.UnChanged;
            }
        }
        protected override IQueryable<退库单> DbInclude(ObjectSet<退库单> dbSet)
        {
            return dbSet.Include(t => t.退库单明细s.Select(p => p.入库单明细.饰品))
                   .Include(t => t.退库单明细s.Select(p => p.入库单明细.饰品.品名))
                .Include(t => t.退库单明细s.Select(p => p.入库单明细.饰品.材质))
                .Include(t => t.退库单明细s.Select(p => p.入库单明细.饰品.电镀方式))
                .Include(t => t.退库单明细s.Select(p => p.入库单明细.饰品.石头颜色))
                ;
        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        public virtual List<供应商> 供应商Source { get; set; }
        #region 明细表操作

        private void UpdateTotal()
        {
            Entity.总金额 = Entity.退库单明细s.Sum(t => t.金额);
            Entity.重量 = Entity.退库单明细s.Sum(t => t.重量);
            Entity.数量 = Entity.退库单明细s.Sum(t => t.数量);
            Entity.未收金额 = Entity.总金额 - Entity.已收金额;
        }

        protected override void UpdateCommands()
        {
            base.UpdateCommands();
            this.RaiseCanExecuteChanged(x => x.AddChildRow());
            this.RaiseCanExecuteChanged(x => x.DeleteChildRow());


        }



        public virtual void ShowList(object startCode)
        {
            var a = GetDocumentManagerService();

            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = DB;
            List<dynamic> list = db.入库单明细s.Include(t => t.入库单).Include(t => t.饰品).Include(t => t.饰品.单位).Include(t => t.饰品.重量单位)
                  .Where(t => t.入库单.编号.Contains(startStr) && t.入库单.状态 != "N" && t.入库单.供应商ID == Entity.供应商ID && t.入库单.分店ID == Entity.分店ID)
                .Select(t => new
                {
                    ID = t.ID,
                    编号 = t.入库单.编号,
                    品名 = t.饰品.品名.名称,
                    材质 = t.饰品.材质.名称,
                    电镀方式 = t.饰品.电镀方式.名称,
                    石头颜色 = t.饰品.石头颜色.名称,
                    单位 = t.饰品.单位.名称,
                    重量单位 = t.饰品.重量单位.名称,
                    尺寸 = t.饰品.尺寸,
                    工费计法 = t.饰品.工费计法,
                    数量 = t.数量,
                    重量 = t.重量,
                    单价 = t.单价,
                    金额 = t.金额
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
                    //SelectChildEntity.入库单明细ID = VM.SelectEntity.ID;
                    //this.DB.Entry(SelectChildEntity).Reference(t => t.入库单明细).Load();
                    //this.DB.Entry(SelectChildEntity.入库单明细).Reference(t => t.饰品).Load();

                    long tmepID = VM.SelectEntity.ID;
                    SelectChildEntity.入库单明细 = db.入库单明细s.Include(t => t.饰品.品名).Include(t => t.饰品.单位).Include(t => t.饰品.重量单位).Include(t => t.饰品.石头颜色).Include(t => t.饰品.电镀方式).Include(t => t.饰品.材质).Where(t => t.ID == tmepID).First();


                    SelectChildEntity.入库单号 = VM.SelectEntity.编号;
                    SelectChildEntity.数量 = VM.SelectEntity.数量;
                    SelectChildEntity.重量 = VM.SelectEntity.重量;
                    SelectChildEntity.金额 = VM.SelectEntity.金额;
                    SelectChildEntity.单价 = VM.SelectEntity.单价;
                    UpdateTotal();
                }
            }
        }


        public virtual 退库单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;


            var item = DB.退库单明细s.Create();
            item.DirtyState = DirtyState.Added;
            item.计价方式 = 费用计法.按重;
            //  item.金额 = 12.0M;
            if (Entity.退库单明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.退库单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.退库单明细s.Add(item);
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

            Entity.退库单明细s.Remove(temp);
            DB.退库单明细s.Remove(temp);
            UpdateTotal();

            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.退库单明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170318 单据确认操作
        protected override void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 退库单 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.退库单明细s.Count <= 0)
            {
                throw new Exception("没有单身内容");
            }
            //是否有超退的情况


            foreach (var item in entity.退库单明细s)
            {


                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.入库单明细.饰品ID && t.分店ID == entity.分店ID).SingleOrDefault();
                if (temp == null)
                {
                    // dbContext.库存s.Add(new 库存() { 饰品ID = item.入库单明细.饰品ID, 分店ID = entity.分店ID });
                    throw new Exception("没有库存记录");
                }
                else
                {
                    temp.数量 -= item.数量;
                    temp.重量 -= item.重量;
                }
                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.入库单明细.饰品ID).Single();
                product.库存数量 -= item.数量;
                product.库存重量 -= item.重量;
                product.库存总金额 -= item.金额;
                // 成本
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //成本小计
                product.账面成本小计 -= item.金额;

                //写出入明细
                dbContext.库存出入明细s.Add(new 库存出入明细()
                {
                    日期 = DateTime.Now,
                    相关单据 = "退库单",
                    单据ID = item.ID,
                    单据编号 = entity.编号,
                    出入别 = "O",
                    数量 = item.数量,
                    重量 = item.重量,
                    金额 = item.金额,
                    分店ID = entity.分店ID,
                    饰品ID = item.入库单明细.饰品ID
                });

            }
        }

        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 退库单 entity)
        {
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            if (entity.已收金额 != 0)
            {
                throw new Exception("已收金额不为0");
            }
            //是否有超退的情况
            foreach (var item in entity.退库单明细s)
            {
                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.入库单明细.饰品ID && t.分店ID == entity.分店ID).SingleOrDefault();


                temp.数量 += item.数量;
                temp.重量 += item.重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.入库单明细.饰品ID).Single();
                product.库存数量 += item.数量;
                product.库存重量 += item.重量;
                product.库存总金额 += item.金额;
                // 成本
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //成本小计
                product.账面成本小计 += item.金额;
                //写出入明细
                var inOutDetails = dbContext.库存出入明细s.Where(t => t.单据ID == item.ID && t.单据编号 == entity.编号).Single();
                dbContext.Entry(inOutDetails).State = EntityState.Deleted;
            }

        }
        #endregion
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 退库单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.退库单明细s.RemoveRange(entity.退库单明细s);
        }
        #endregion
    }
}