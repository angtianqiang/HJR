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
    public class 盘点表ViewModel : SingleObjectViewModel<盘点表, ZtxDB, long>
    {
        public static 盘点表ViewModel Create()
        {
            return ViewModelSource.Create(() => new 盘点表ViewModel());
        }
        protected 盘点表ViewModel() : base(DbFactory.Instance, x => x.盘点表s, x => x.ID, x => x.编号, "盘点表")
        {
            if (this.IsInDesignMode()) return;

            Init();

            Messenger.Default.Register<string>(this, "饰品编号更改" + Token, m =>
            {
                SelectChildEntity.饰品ID = 0;
                SelectChildEntity.饰品 = null;
            });
        }
        public async void Init()
        {

            var t1 = await DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
            var t2 = await DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
            操作员Source = t1;
            分店Source = t2;


        }
        protected override void SetDetailsDirtyState()
        {
            base.SetDetailsDirtyState();
            foreach (var item in Entity.盘点表明细s)
            {
                item.DirtyState = DirtyState.UnChanged;
            }
        }
        protected override IQueryable<盘点表> DbInclude(ObjectSet<盘点表> dbSet)
        {
            return dbSet.Include(t => t.盘点表明细s);
        }
        public virtual List<User> 操作员Source { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        #region 明细表操作



        protected override void UpdateCommands()
        {
            base.UpdateCommands();
            this.RaiseCanExecuteChanged(x => x.AddChildRow());
            this.RaiseCanExecuteChanged(x => x.DeleteChildRow());
            this.RaiseCanExecuteChanged(x => x.BuidChild());

        }



        public virtual void ShowList(object startCode)
        {
            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = DB;
            List<dynamic> list = db.饰品s.Include(t => t.单位).Include(t => t.重量单位)
                  .Where(t => t.编号.StartsWith(startStr))
                .Select(t => new
                {
                    ID = t.ID,
                    编号 = t.编号,
                    品名 = t.品名.名称,
                    材质 = t.材质.名称,
                    电镀方式 = t.电镀方式.名称,
                    石头颜色 = t.石头颜色.名称,
                    单位 = t.单位.名称,
                    重量单位 = t.重量单位.名称,
                    尺寸 = t.尺寸,
                    工费计法 = t.工费计法
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
                    //SelectChildEntity.饰品ID = VM.SelectEntity.ID;
                    //this.DB.Entry(SelectChildEntity).Reference(t => t.饰品).Load();

                    long tmepID = VM.SelectEntity.ID;
                    SelectChildEntity.饰品 = db.饰品s.Include(t => t.单位).Include(t => t.重量单位).Include(t => t.石头颜色).Include(t => t.电镀方式).Include(t => t.材质).Where(t => t.ID == tmepID).First();


                    SelectChildEntity.饰品编号 = SelectChildEntity.饰品.编号;
                }
            }
        }


        public virtual 盘点表明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var item = DB.盘点表明细s.Create();
            item.DirtyState = DirtyState.Added;
            if (Entity.盘点表明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.盘点表明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.盘点表明细s.Add(item);
            Mouse.OverrideCursor = null;
        }



        public virtual bool CanAddChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return false;//盘点表明细是直接生成的
                         //   return Entity != null && Entity.状态 == "N" && Entity.分店ID != 0;
        }
        public virtual void DeleteChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var temp = SelectChildEntity;
            Entity.盘点表明细s.Remove(temp);
            DB.盘点表明细s.Remove(temp);
            Mouse.OverrideCursor = null;
        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return false;//盘点表明细是直接生成的
                         //   return Entity != null && Entity.状态 == "N" && Entity.盘点表明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170319 生成明细

        public virtual void BuidChild()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            foreach (var item in DB.库存s.Include(t => t.饰品).Where(t => t.分店ID == Entity.分店ID).OrderBy(t => t.饰品.编号).AsEnumerable())
            {

                var temp = DB.盘点表明细s.Create();
                item.DirtyState = DirtyState.Added;
                if (Entity.盘点表明细s.Count == 0)
                {
                    temp.序号 = 1;
                }
                else
                {
                    temp.序号 = Entity.盘点表明细s.Select(t => t.序号).Max() + 1;
                }
                temp.盘点前数量 = temp.实盘数量 = item.数量;
                temp.盘点前重量 = temp.实盘重量 = item.重量;
                temp.盘点前金额 = temp.实盘金额 = item.金额;
                temp.饰品编号 = item.饰品.编号;
                temp.饰品ID = item.饰品ID;
                Entity.盘点表明细s.Add(temp);

            }
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanBuidChild()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.分店ID != 0 && Entity.盘点表明细s.Count <= 0;
        }
        #endregion
        #region 20170318 单据确认操作

        protected override async void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 盘点表 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.盘点表明细s.Count <= 0)
                throw new Exception("没有单身内容");
            //生成盈亏单
            var ykd = dbContext.盈亏单s.Create();
            entity.盈亏单 = ykd;

            ykd.编号 = await CollectionViewModel<盈亏单, ZtxDB, long>.GetNewCode("YK", DbFactory.Instance, x => x.盈亏单s, t => t.编号);
            ykd.日期 = entity.日期;
            ykd.操作员ID = entity.操作员ID;
            ykd.状态 = "Y";//直接生效
            ykd.分店ID = entity.分店ID;

            int k = 1;
            foreach (var item in entity.盘点表明细s)
            {

                var 差异数量 = item.实盘数量 - item.盘点前数量;
                var 差异重量 = item.实盘重量 - item.盘点前重量;

                //生成盈亏单明细
                var a = dbContext.盈亏单明细s.Create();
                ykd.盈亏单明细s.Add(a);
                a.序号 = k++;
                a.饰品ID = item.饰品ID;
                a.盈亏数量 = 差异数量;
                a.盈亏重量 = 差异重量;
                a.盈亏金额 = 0;

                //按盈亏单的生效逻辑写数据

                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.分店ID).SingleOrDefault();
                if (temp == null)
                {
                    temp = dbContext.库存s.Add(new 库存() { 饰品ID = item.饰品ID, 分店ID = entity.分店ID });
                }

                temp.数量 += a.盈亏数量;
                temp.重量 += a.盈亏重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.饰品ID).Single();
                product.库存数量 += a.盈亏数量;
                product.库存重量 += a.盈亏重量;
                product.库存总金额 += a.盈亏金额;
                // 成本
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //成本小计
                product.账面成本小计 += a.盈亏金额;
                //写出入明细
            }
            ykd.数量 = ykd.盈亏单明细s.Sum(t => t.盈亏数量);
            dbContext.盈亏单s.Add(ykd);
            dbContext.SaveChanges();
            //先保存数据才完出入明细，不然ID为0
            foreach (var item in dbContext.盈亏单明细s.Where(t => t.盈亏单.编号 == ykd.编号).AsEnumerable())
            {
                dbContext.库存出入明细s.Add(new 库存出入明细()
                {
                    日期 = ykd.日期,
                    相关单据 = "盈亏单",
                    单据ID = item.ID,
                    单据编号 = ykd.编号,
                    出入别 = "I",
                    数量 = item.盈亏数量,
                    重量 = item.盈亏重量,
                    金额 = item.盈亏金额,
                    分店ID = ykd.分店ID,
                    饰品ID = item.饰品ID
                });
            }





        }


        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 盘点表 entity)
        {
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            var ykd = dbContext.盈亏单s.Where(t => t.ID == entity.盈亏单ID && t.状态 != "N").SingleOrDefault();
            if (ykd == null)
            {
                throw new Exception("未找到对应的盈亏单");
            }



            //删除出入明细
            foreach (var item in dbContext.盈亏单明细s.Where(t => t.盈亏单id == ykd.ID).ToList())
            {
                var temp = dbContext.库存出入明细s.Where(t => t.单据ID == item.ID && t.单据编号 == ykd.编号).Single();
                dbContext.Entry(temp).State = EntityState.Deleted;
            }



            foreach (var item in ykd.盈亏单明细s)
            {


                //更新分库库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.分店ID).SingleOrDefault();
                if (temp == null)
                {
                    temp = dbContext.库存s.Add(new 库存() { 饰品ID = item.饰品ID, 分店ID = entity.分店ID });
                }

                temp.数量 += item.盈亏数量;
                temp.重量 += item.盈亏重量;

                //更新总库存 
                var product = dbContext.饰品s.Where(t => t.ID == item.饰品ID).Single();
                product.库存数量 += item.盈亏数量;
                product.库存重量 += item.盈亏重量;
                product.库存总金额 += item.盈亏金额;
                // 成本
                product.按件成本价 = product.库存数量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存数量, 2);
                product.按重成本价 = product.库存重量 == 0.00m ? 0.00m : System.Math.Round(product.库存总金额 / product.库存重量, 2);
                //成本小计
                product.账面成本小计 += item.盈亏金额;
                //写出入明细


            }
            entity.盈亏单ID = null;
            dbContext.盈亏单明细s.RemoveRange(ykd.盈亏单明细s);


            dbContext.Entry(ykd).State = EntityState.Deleted;

            dbContext.SaveChanges();

        }
        #endregion

        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 盘点表 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.盘点表明细s.RemoveRange(entity.盘点表明细s);
        }
        #endregion

    }
}
