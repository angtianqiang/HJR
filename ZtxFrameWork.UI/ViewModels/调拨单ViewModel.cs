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
    public class 调拨单ViewModel : SingleObjectViewModel<调拨单, ZtxDB, long>
    {
        public static 调拨单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 调拨单ViewModel());
        }
        protected 调拨单ViewModel() : base(DbFactory.Instance, x => x.调拨单s, x => x.ID, x => x.编号, "调拨单")
        {

            if (this.IsInDesignMode()) return;
            var db = dbFactory.CreateDbContext();
            签收员Source= 调拨员Source = db.Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToList();
            源分店Source = 目标分店Source = db.分店s.OrderBy(t => t.名称).ToList();
            Messenger.Default.Register<string>(this, "饰品编号更改" + Token, m =>
            {
                SelectChildEntity.饰品ID = 0;
                SelectChildEntity.饰品 = null;
            });
            Messenger.Default.Register<string>(this, "更新数量" + Token, m =>
            {
               
                Entity.数量 = Entity.调拨单明细s.Sum(t => t.数量);
            });
        }
        public virtual List<User> 签收员Source { get; set; }
        public virtual List<User> 调拨员Source { get; set; }
        public virtual List<分店> 源分店Source { get; set; }
        public virtual List<分店> 目标分店Source { get; set; }
        #region 明细表操作



        protected override void UpdateCommands()
        {
            base.UpdateCommands();
            this.RaiseCanExecuteChanged(x => x.AddChildRow());
            this.RaiseCanExecuteChanged(x => x.DeleteChildRow());


        }



        public virtual void ShowList(object startCode)
        {
            string startStr = startCode?.ToString() ?? "";

            Keyboard.Focus(null);//更新界面的值

            var db = dbFactory.CreateDbContext();
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
                    this.DB.Entry(SelectChildEntity).Reference(t => t.饰品).Load();
                    SelectChildEntity.饰品编号 = SelectChildEntity.饰品.编号;
                }
            }
        }


        public virtual 调拨单明细 SelectChildEntity { get; set; }


        public virtual void AddChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;


            var item = DB.调拨单明细s.Create();
          
            item.DirtyState = DirtyState.Added;
           
          
            if (Entity.调拨单明细s.Count == 0)
            {
                item.序号 = 1;
            }
            else
            {
                item.序号 = Entity.调拨单明细s.Select(t => t.序号).Max() + 1;
            }
            Entity.调拨单明细s.Add(item);
            Mouse.OverrideCursor = null;
        }



        public virtual bool CanAddChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.源分店ID != 0 && Entity.目标分店ID != 0;
        }
        public virtual void DeleteChildRow()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var temp = SelectChildEntity;

            Entity.调拨单明细s.Remove(temp);
            DB.调拨单明细s.Remove(temp);
            Mouse.OverrideCursor = null;

        }


        public virtual bool CanDeleteChildRow()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return Entity != null && Entity.状态 == "N" && Entity.调拨单明细s.Count > 0 && SelectChildEntity != null;
        }

        #endregion
        #region 20170318 单据确认操作
        protected override void OnBeforeEntityConfirmed(ZtxDB dbContext, long primaryKey, 调拨单 entity)
        {
            base.OnBeforeEntityConfirmed(dbContext, primaryKey, entity);
            if (entity.调拨单明细s.Count <= 0)
                throw new Exception("没有单身内容");
            foreach (var item in entity.调拨单明细s)
            {
                //更新源库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.源分店ID).SingleOrDefault();
                if (temp == null)
                {
                    temp = dbContext.库存s.Add(new 库存() { 饰品ID = item.饰品ID, 分店ID = entity.源分店ID });
                }
               
                    temp.数量-= item.数量;
                    temp.重量 -= item.重量;
                //更新目标分店库存
                var temp1 = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.目标分店ID).SingleOrDefault();
                if (temp1 == null)
                {
                    temp1 = dbContext.库存s.Add(new 库存() { 饰品ID = item.饰品ID, 分店ID = entity.目标分店ID });
                }

                temp1.数量 += item.数量;
                temp1.重量 += item.重量;



              
                //写出入明细
                dbContext.库存出入明细s.Add(new 库存出入明细()
                {
                    日期 = DateTime.Now,
                    相关单据 = "调拨单",
                    单据ID = item.ID,
                    单据编号 = entity.编号,
                    出入别 = "O",
                    数量 = item.数量*-1,
                    重量 = item.重量*-1m,                  
                    分店ID = entity.源分店ID,
                    饰品ID = item.饰品ID
                });
                dbContext.库存出入明细s.Add(new 库存出入明细()
                {
                    日期 = DateTime.Now,
                    相关单据 = "调拨单",
                    单据ID = item.ID,
                    单据编号 = entity.编号,
                    出入别 = "I",
                    数量 = item.数量,
                    重量 = item.重量 ,
                    分店ID = entity.目标分店ID,
                    饰品ID = item.饰品ID
                });
            }
        }

        protected override void OnBeforeEntityUnConfirmed(ZtxDB dbContext, long primaryKey, 调拨单 entity)
        {
            base.OnBeforeEntityUnConfirmed(dbContext, primaryKey, entity);
            foreach (var item in entity.调拨单明细s)
            {
                //更新分库库存
                //更新源库存
                var temp = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.源分店ID).SingleOrDefault();
              
                temp.数量 += item.数量;
                temp.重量 += item.重量;
                //更新目标分店库存
                var temp1 = dbContext.库存s.Where(t => t.饰品ID == item.饰品ID && t.分店ID == entity.目标分店ID).SingleOrDefault();
             
                temp1.数量 -= item.数量;
                temp1.重量 -= item.重量;

                //写出入明细
                var inOutDetails = dbContext.库存出入明细s.Where(t => t.单据ID == item.ID && t.单据编号 == entity.编号 && t.出入别=="I").Single();
                dbContext.Entry(inOutDetails).State = EntityState.Deleted;
                var inOutDetails2 = dbContext.库存出入明细s.Where(t => t.单据ID == item.ID && t.单据编号 == entity.编号 && t.出入别 == "O").Single();
                dbContext.Entry(inOutDetails2).State = EntityState.Deleted;
            }
        }
        #endregion
        #region 20170320 删除时同时删除子表
        protected override void OnBeforeEntityDeleted(ZtxDB dbContext, long primaryKey, 调拨单 entity)
        {
            base.OnBeforeEntityDeleted(dbContext, primaryKey, entity);
            dbContext.调拨单明细s.RemoveRange(entity.调拨单明细s);
        }
        #endregion
    }
}