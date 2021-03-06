﻿using DevExpress.Mvvm;
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
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

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

            Init();
            //Init1();
            //Init2();
            //Init3();
            Messenger.Default.Register<string>(this, "饰品编号更改" + Token, m =>
            {
                SelectChildEntity.饰品ID = 0;
                SelectChildEntity.饰品 = null;
            });
            Messenger.Default.Register<string>(this, "数量更改" + Token, m =>
            {
                var item = SelectChildEntity;
                try
                {
                    item.重量 = item.饰品.单重 * item.数量;
                }
                catch { }
           
                UpdatePrice();
            });
            Messenger.Default.Register<string>(this, "更新折扣" + Token, m =>
            {
                var item = SelectChildEntity;
                item.折后单价 = System.Math.Round(item.销售价* item.折扣, 2);   
                UpdatePrice();
            });
            Messenger.Default.Register<string>(this, "更新折后单价" + Token, m =>
            {
                var item = SelectChildEntity;
                item.折扣 = System.Math.Round(item.折后单价 / item.销售价, 2);
                UpdatePrice();

            });
            Messenger.Default.Register<string>(this, "更新金额" + Token, m =>
            {

                UpdatePrice();
            });
            Messenger.Default.Register<string>(this, "数量更改后" + Token, m =>
            {
                var selectItem = SelectChildEntity;

                //库存验证
                var 库存 = DB.库存s.Where(t => t.饰品ID == selectItem.饰品ID && t.分店ID == App.Current分店.ID).FirstOrDefault();
                if (库存 == null || 库存.数量 <= selectItem.数量)
                {
                    int temp = 库存?.数量 ?? 0;
                    MessageBoxService.ShowMessage($"数量{selectItem.数量}大于库存数{temp},请检查库存");
                }
            });
        }
        public void Init()
        {

            会员Source = Helpers.CacheHelper.会员Source;
            操作员Source = Helpers.CacheHelper.操作员Source;
            分店Source = Helpers.CacheHelper.分店Source;
            //  await Task.WhenAll(t1, t2, t3);

        }
        protected override void SetDetailsDirtyState()
        {
            base.SetDetailsDirtyState();
            foreach (var item in Entity.销售单明细s)
            {
                item.DirtyState = DirtyState.UnChanged;
            }
        }

        public async void Init1() => 分店Source = await DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToListAsync();
        public async void Init2() => 操作员Source = await DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToListAsync();
        public async void Init3() => 会员Source = await DbFactory.Instance.CreateDbContext().会员s.OrderBy(t => t.编号).ToListAsync();

        //protected override IQueryable<销售单> DbInclude(DbSet<销售单> dbSet)
        //{
        //    //dbSet.Include(t => t.会员).Include(t => t.分店).Include(t => t.操作员).Include("销售单明细s.饰品"); 这样也可以
        //    return dbSet.Include(t => t.会员).Include(t => t.分店).Include(t => t.操作员).Include(t => t.销售单明细s.Select(p => p.饰品));
        //}
        protected override IQueryable<销售单> DbInclude(ObjectSet<销售单> dbSet)
        {
            return dbSet.Include(t => t.销售单明细s.Select(p => p.饰品.材质))
                       .Include(t => t.销售单明细s.Select(p => p.饰品.饰品类型))
                  .Include(t => t.销售单明细s.Select(p => p.饰品.品名))
                .Include(t => t.销售单明细s.Select(p => p.饰品.材质))
                .Include(t => t.销售单明细s.Select(p => p.饰品.电镀方式))
                .Include(t => t.销售单明细s.Select(p => p.饰品.石头颜色))
                     .Include(t => t.销售单明细s.Select(p => p.饰品.重量单位))
                         .Include(t => t.销售单明细s.Select(p => p.饰品.单位))
                        .Include(t => t.会员)
                                .Include(t => t.分店)
                                    .Include(t => t.操作员)
                ;
        }
        public void UpdatePrice()
        {
            var item = SelectChildEntity;
            //item.销售价 = System.Math.Round(item.工费计法 == 费用计法.按件 ? item.饰品?.QtyPrice ?? 0 : item.饰品?.WeightPrice ?? 0, 2);
            //item.工费 = item.工费计法 == 费用计法.按件 ? item.饰品?.批发工费 ?? 0 : item.饰品?.批发工费 ?? 0;
       //     item.重量 = item.数量 * item.饰品.单重;
            if (item.工费计法 == 费用计法.按件)
            {
                item.重量 = item.数量 * item.饰品.单重;
            }

            item.折前价 = System.Math.Round(item.工费计法 == 费用计法.按件 ? item.数量 * item.销售价 : item.重量 * item.销售价, 2);


            //      item.金额 = System.Math.Round(item.折前价 * item.折扣, 2);
            item.金额 = System.Math.Round(item.工费计法 == 费用计法.按件 ? item.数量 * item.折后单价 : item.重量 * item.折后单价, 2);
            item.成本= item.工费计法 == 费用计法.按件 ? item.饰品.按件成本价 * item.数量 : item.饰品.按重成本价 * item.重量;

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
            Entity.重量 = Entity.销售单明细s.Sum(t => t.重量);
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
            //20170531 添加过滤条件，当前分店有库存
            var db = DB;
            List<dynamic> list = db.库存s.Where(t => t.数量 > 0 && t.饰品.编号.Contains(startStr) && t.分店ID == Entity.分店ID)
                   .Select(t => new
                   {
                       ID = t.饰品.ID,
                       编号 = t.饰品.编号,
                       品名 = t.饰品.品名.名称,
                       材质 = t.饰品.材质.名称,
                       电镀方式 = t.饰品.电镀方式.名称,
                       石头颜色 = t.饰品.石头颜色.名称,
                       数量 = t.数量,
                       重量 = t.重量,
                       单位 = t.饰品.单位.名称,
                       重量单位 = t.饰品.重量单位.名称,
                       尺寸 = t.饰品.尺寸,
                       工费计法 = t.饰品.工费计法
                   })
                  .ToList<dynamic>();

            //List<dynamic> list = db.饰品s.Include(t => t.单位).Include(t => t.重量单位).Include(t => t.材质)
            //      .Where(t => t.编号.Contains(startStr) )
            //    .Select(t => new
            //    {
            //        ID = t.ID,
            //        编号 = t.编号,
            //        品名 = t.品名.名称,
            //        材质 = t.材质.名称,
            //        电镀方式 = t.电镀方式.名称,
            //        石头颜色 = t.石头颜色.名称,
            //        单位 = t.单位.名称,
            //        重量单位 = t.重量单位.名称,
            //        尺寸 = t.尺寸,
            //        工费计法 = t.工费计法
            //    })
            //      .ToList<dynamic>();
            //if (list.Count==1)
            //{
            //    SelectChildEntity.饰品ID = list[0].ID;
            //this.DB.Entry(SelectChildEntity).Reference(t =>t.饰品).Load();


            //}
            //else
            dynamic currentSelectRow = null;
            if (list.Count == 1)
            {
                currentSelectRow = list[0];
                Messenger.Default.Send<string>("", "选中数据" + Token);
            }
            else
            {
                CommQueryListViewModel VM = ViewModelSource.Create<CommQueryListViewModel>(() => new CommQueryListViewModel() { Title = "饰品清单", Entities = list });
                IDocument doc = QueryListManagerService.CreateDocument(Utils.ApplictionConfigValue.CommQueryListViewName, VM);
                doc.Show();
                if (VM.IsSelect == true)
                {
                    //  SelectChildEntity.饰品ID = VM.SelectEntity.ID;
                    currentSelectRow = VM.SelectEntity;
                }
            }


            if (currentSelectRow == null)
            {
                return;
            }
            饰品 temp1 = this.DB.饰品s.Local.FirstOrDefault(t1 => t1.ID == currentSelectRow.ID);
            if (temp1 != null)
            {
                //材质 temp2 = this.DB.材质s.Local.FirstOrDefault(t2 => t2.ID == temp1.材质ID);
                //if (temp2 != null)
                //{
                //    this.DB.Entry(temp2).State = EntityState.Detached;

                //}
                this.DB.Entry(temp1).State = EntityState.Detached;
            }




            //   this.DB.Entry(SelectChildEntity).Reference(t => t.饰品).Query().Include(t => t.材质).Include(t => t.品名).Include(t => t.石头颜色).Include(t => t.电镀方式).Load();

            long tmepID = currentSelectRow.ID;
            SelectChildEntity.饰品 = db.饰品s.Include(t => t.品名).Include(t => t.单位).Include(t => t.重量单位).Include(t => t.石头颜色).Include(t => t.电镀方式).Include(t => t.材质).Where(t => t.ID == tmepID).First();



            //long newId = VM.SelectEntity.ID;
            //SelectChildEntity.饰品 = this.DB.饰品s.AsNoTracking().Include(t => t.材质).Where(t => t.ID == newId).Single();
            SelectChildEntity.饰品编号 = SelectChildEntity.饰品?.编号??"";
            SelectChildEntity.工费计法 = SelectChildEntity.饰品?.工费计法??0;
            SelectChildEntity.当时金属价 = SelectChildEntity.饰品?.材质?.当前价 ?? 0m;
            //会员提成
            if (Entity.会员ID != null)
            {
                if (!this.DB.Entry(Entity).Reference(t => t.会员).IsLoaded)
                {
                    this.DB.Entry(Entity).Reference(t => t.会员).Load();
                }
                if (Entity.会员.折扣 != 0m)
                {
                    SelectChildEntity.折扣 = Entity.会员.折扣;
                }
            }

            var item = SelectChildEntity;
            item.销售价 = System.Math.Round(item.工费计法 == 费用计法.按件 ? item.饰品?.QtyPrice ?? 0 : item.饰品?.WeightPrice ?? 0, 2);
            item.工费 = item.工费计法 == 费用计法.按件 ? item.饰品?.批发工费 ?? 0 : item.饰品?.批发工费 ?? 0;
            item.折后单价 = System.Math.Round(item.销售价 * item.折扣, 2);
            UpdatePrice();

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
public virtual async void SK()
{
    //生成收款单
    long parKey = GetPrimaryKey(Entity);
    long NewKey = 0;
    using (var dbContext = DbFactory.Instance.CreateDbContext())
    {
        var skd = dbContext.收款单s.Create();

//20180115根据客户提出的要求，编号前加一个分店编号
        skd.编号 = await CollectionViewModel<收款单, ZtxDB, long>.GetNewCode(App.Current分店.编号 + "SK", DbFactory.Instance, x => x.收款单s, t => t.编号);
        skd.收款日期 = DateTime.Now;
        skd.会员ID = Entity.会员ID;
        skd.状态 = "N";
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
    return Entity != null && Entity.已收金额 < Entity.总金额 && ((dynamic)Entity).状态 != "N";
}
public virtual async void TH()
{
    //生成退货单
    long parKey = GetPrimaryKey(Entity);
    long NewKey = 0;
    using (var dbContext = DbFactory.Instance.CreateDbContext())
    {
        var skd = dbContext.销售退货单s.Create();


        skd.编号 = await CollectionViewModel<销售退货单, ZtxDB, long>.GetNewCode("TH", DbFactory.Instance, x => x.销售退货单s, t => t.编号);
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
    return Entity != null && ((dynamic)Entity).状态 != "N";
}
        #endregion
    }
}