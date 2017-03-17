using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.Data
{
    public class ZtxDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<ZtxDB>
    {
        protected override void Seed(ZtxDB context)
        {
            base.Seed(context);



        }
    }
    public class ZtxDB : DbContext
    {
        public static bool Inited = false;
        public static void Init(ZtxDB context)
        {
            if (Inited == true)
            {
                return;
            }
            var user = context.Users.Where(t => t.UserName == "F").FirstOrDefault();
            if (user == null)
            {
                context.Users.Add(new Model.User() { UserName = "F", PassWord = "35c7b9b6578943f2164bef3eb331acf9", DispalyName = "ZTX", Department = "开发部" });
            }
            var parentMoudle = context.Modules.Where(t => t.ModuleTitle == "销售管理").FirstOrDefault();
            Model.Module m = null;
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "销售管理", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "销售单CollectionView", Parent = p, ModuleTitle = "销售单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "销售单", "销售管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "销售退货单CollectionView", Parent = p, ModuleTitle = "销售退货单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "销售退货单", "销售管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "收款单CollectionView", Parent = p, ModuleTitle = "收款单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "收款单", "销售管理");

            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "库存管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "库存管理", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "入库单CollectionView", Parent = p, ModuleTitle = "采购入库单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "采购入库单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "退库单CollectionView", Parent = p, ModuleTitle = "采购退货单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "采购退货单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "盈亏单CollectionView", Parent = p, ModuleTitle = "盈亏单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "盈亏单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "调拨单CollectionView", Parent = p, ModuleTitle = "调拨单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "调拨单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "付款单CollectionView", Parent = p, ModuleTitle = "付款单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "付款单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "盘点表CollectionView", Parent = p, ModuleTitle = "盘点表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "盘点表", "库存管理");
            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "财务管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "财务管理", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "付款单CollectionView", Parent = p, ModuleTitle = "付款单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                m = context.Modules.Add(new Model.Module() { DocumentType = "收款单CollectionView", Parent = p, ModuleTitle = "收款单", ModuleInfo = Model.ModuleInfo.MoudleAction });

            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "基础资料").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "基础资料", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });

                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品CollectionView", Parent = p, ModuleTitle = "饰品", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品", "基础资料",Confirm:false,UnConfirm:false,Audit:false,UnAudit:false   );
                m = context.Modules.Add(new Model.Module() { DocumentType = "分店CollectionView", Parent = p, ModuleTitle = "分店", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "分店", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "单位CollectionView", Parent = p, ModuleTitle = "单位", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "单位", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "重量单位CollectionView", Parent = p, ModuleTitle = "重量单位", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "重量单位", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "材质CollectionView", Parent = p, ModuleTitle = "材质", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "材质", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "黄金种类CollectionView", Parent = p, ModuleTitle = "黄金种类", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "黄金种类", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品类别CollectionView", Parent = p, ModuleTitle = "饰品类别", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品类别", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品类型CollectionView", Parent = p, ModuleTitle = "饰品类型", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品类型", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品提成CollectionView", Parent = p, ModuleTitle = "饰品提成", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品提成", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "会员CollectionView", Parent = p, ModuleTitle = "会员", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "会员", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "供应商CollectionView", Parent = p, ModuleTitle = "供应商", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "供应商", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
            }

            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "报表管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "报表管理", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "采购入库单明细表View", Parent = p, ModuleTitle = "采购入库单明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "采购入库单明细表", "报表管理", Add:false, Edit:false,Delete:false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "系统管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "系统管理", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "ModuleCollectionView", Parent = p, ModuleTitle = "系统模块", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "系统模块", "系统管理", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "UserCollectionView", Parent = p, ModuleTitle = "用户管理", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "用户管理", "系统管理", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "AuthorityModuleCollectionView", Parent = p, ModuleTitle = "模块权限配置", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "模块权限配置", "系统管理",  Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "UserAuthorityModuleMappingCollectionView", Parent = p, ModuleTitle = "用户权限管理", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "用户权限管理", "系统管理", Add: false,  Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "SystemConfigurationCollectionView", Parent = p, ModuleTitle = "系统参数配置", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "系统参数配置", "系统管理", Add: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "ChangePasswordView", Parent = p, ModuleTitle = "更改登录密码", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "更改登录密码", "系统管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
            }

            //添加页面模块






            context.SaveChanges();
            #region 注册字典

            var unit = context.单位s.Where(t => t.名称 == "pcs").FirstOrDefault();
            if (unit == null)
            {
                unit = context.单位s.Add(new 单位() { 名称 = "pcs", 排序号 = "001" });
            }
            var unit2 = context.单位s.Where(t => t.名称 == "pcs").FirstOrDefault();
            if (unit2 == null)
            {
                context.单位s.Add(new 单位() { 名称 = "件", 排序号 = "002" });
            }

            if (context.材质s.Where(T => T.名称 == "银").FirstOrDefault() == null)
            {
                context.材质s.Add(new 材质() { 名称 = "银", 排序号 = "001" });
            }
            if (context.材质s.Where(T => T.名称 == "金").FirstOrDefault() == null)
            {
                context.材质s.Add(new 材质() { 名称 = "金", 排序号 = "002" });
            }

            if (context.分店s.Where(T => T.名称 == "中山总店").FirstOrDefault() == null)
            {
                context.分店s.Add(new 分店() { 名称 = "中山总店", 编号 = "总店", QQ = "330464", 传真号码 = "56761354", 地址 = "中山三路", 备注 = "", 是否允许修改今日金价 = true, WeiXi = "1321", 是否总店 = true, 联系人 = "郑总", 联系电话 = "1372468713" });
            }
            if (context.供应商s.Where(T => T.编号 == "MB").FirstOrDefault() == null)
            {
                context.供应商s.Add(new 供应商() { 编号 = "MB", 全称 = "铭邦五金厂", 地址 = "塘厦林村", 简称 = "铭邦", 联系人 = "陶先生", 联系电话 = "13834834331", 备注 = "测试的供应商备注" });
            }
            if (context.黄金种类s.Where(T => T.名称 == "pt995").FirstOrDefault() == null)
            {
                context.黄金种类s.Add(new 黄金种类() { 名称 = "pt995", 排序号 = "001" });
            }
            if (context.黄金种类s.Where(T => T.名称 == "pt999").FirstOrDefault() == null)
            {
                context.黄金种类s.Add(new 黄金种类() { 名称 = "pt999", 排序号 = "002" });
            }
            if (context.会员s.Where(T => T.编号 == "0001").FirstOrDefault() == null)
            {
                context.会员s.Add(new 会员() { 编号 = "0001", 姓名 = "郑天祥", 初始积分 = 500, 固定电话 = "0835-7854545", WeiXin = "3051654", QQ = "", 备注 = "会员001", Mail = "angtiangi@163.ocm", 已用积分 = 0, 性别 = "男", 总积分 = 0, 手机号 = "13724485440", 折扣 = 0.98m, 是否停用 = false, 销费金额 = 0 });
            }




            饰品类别 s1 = null;
            饰品类别 s2 = null;

            if (context.饰品类别s.Where(T => T.名称 == "手饰").FirstOrDefault() == null)
            {
                s1 = context.饰品类别s.Add(new 饰品类别() { 名称 = "手饰", 排序号 = "001" });

            }
            if (context.饰品类别s.Where(T => T.名称 == "耳环").FirstOrDefault() == null)
            {
                s2 = context.饰品类别s.Add(new 饰品类别() { 名称 = "耳环", 排序号 = "001" });
            }

            if (context.饰品类型s.Where(T => T.名称 == "饰品类型1").FirstOrDefault() == null)
            {
                context.饰品类型s.Add(new 饰品类型() { 名称 = "饰品类型1", 排序号 = "001", 饰品类别 = s1 });
            }
            if (context.饰品类型s.Where(T => T.名称 == "饰品类型2").FirstOrDefault() == null)
            {
                context.饰品类型s.Add(new 饰品类型() { 名称 = "饰品类型2", 排序号 = "001", 饰品类别 = s1 });
            }

            if (context.饰品s.Where(T => T.编号 == "饰品类型2").FirstOrDefault() == null)
            {

            }
            #endregion
            #region 加载系统配置
            SystemConfiguration config = null;
            config = context.SystemConfigurations.Where(t => t.Token == "DestroyOnClose").FirstOrDefault();
            if (config==null)
            {
                context.SystemConfigurations.Add(new SystemConfiguration()
                {
                     Token= "DestroyOnClose",
                      Category="全局系统设置",
                      TokenValue="0",
                      Des= "是否启用页面缓存",
                      Remark= "0:关闭页面文档后，释放内存；1：关闭页面文档后，留文档"
                }
                );
            }
            config = context.SystemConfigurations.Where(t => t.Token == "ViewOpenMode").FirstOrDefault();
            if (config == null)
            {
                context.SystemConfigurations.Add(new SystemConfiguration()
                {
                    Token = "ViewOpenMode",
                    Category = "全局系统设置",
                    TokenValue = "0",
                    Des = "明细页细打开模块",
                    Remark = "0:在新窗口中打开；1：在Tab页中打开"
                }
                );
            }
            #endregion
            context.SaveChanges();

            Inited = true;
        }
        protected static void InitAuthorityModule(ZtxDB context, string ModuleViewString, string category, bool Navigate = true, bool Add = true, bool Edit = true, bool Delete = true, bool Export=true,bool Print = true, bool Preview = true, bool Design = true, bool Confirm = true, bool UnConfirm = true, bool Audit = true, bool UnAudit = true)
        {
            var view = context.AuthorityModules.Where(t => t.ViewTitle == ModuleViewString).FirstOrDefault();
            if (view == null)
            {
                var v = context.AuthorityModules.Add(new AuthorityModule() { ViewTitle = ModuleViewString, Category = category, Navigate = Navigate, Add = Add, Edit = Edit, Delete = Delete,Export=Export, Print = Print, Preview = Preview, Design = Design, Confirm = Confirm, UnConfirm = UnConfirm, Audit = Audit, UnAudit = UnAudit });


                foreach (var item in context.Users.AsEnumerable())
                {
                    v.UserAuthorityModuleMappings.Add(new UserAuthorityModuleMapping() { UserID = item.ID, Navigate = Navigate, Add = Add, Edit = Edit, Delete = Delete,Export=Export, Print = Print, Preview = Preview, Design = Design, Confirm = Confirm, UnConfirm = UnConfirm, Audit = Audit, UnAudit = UnAudit });
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//在全局里关掉所有主外键关系的级联删除

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
         .Where(type => !String.IsNullOrEmpty(type.Namespace))
         .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
             type.BaseType.GetGenericTypeDefinition() == typeof(ZtxEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
        public ZtxDB() : this("Data Source=127.0.0.1;Initial Catalog=ztxFrameWork2;Integrated Security=True")
        { }
        public ZtxDB(string conn) : base(conn)
        {
            // this.Configuration.ProxyCreationEnabled = true;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Database.SetInitializer<ZtxDB>(new ZtxDropCreateDatabaseIfModelChanges());
            }
            else
            {
                Database.SetInitializer<ZtxDB>(null);

            }
            //  this.Database.Initialize(true);
        }

        private IList GetModifiedObjects()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            HashSet<Object> modifiedObjects = new HashSet<Object>();
            context.DetectChanges();
            foreach (ObjectStateEntry objectStateEntry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
            {
                if (objectStateEntry.Entity != null)
                {
                    SetEntityStatie(objectStateEntry, objectStateEntry.Entity);
                    modifiedObjects.Add(objectStateEntry.Entity);
                }
                else if (objectStateEntry.IsRelationship)
                {
                    DbDataRecord relationshipValues = null;
                    if (objectStateEntry.State == EntityState.Added)
                    {
                        relationshipValues = objectStateEntry.CurrentValues;
                    }
                    else if (objectStateEntry.State == EntityState.Deleted)
                    {
                        relationshipValues = objectStateEntry.OriginalValues;
                    }
                    if (relationshipValues != null)
                    {
                        for (Int32 i = 0; i < relationshipValues.FieldCount; i++)
                        {
                            if (relationshipValues[i] is EntityKey)
                            {
                                Object obj = null;
                                if (context.TryGetObjectByKey((EntityKey)relationshipValues[i], out obj))
                                {
                                    SetEntityStatie(objectStateEntry, obj);
                                    modifiedObjects.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            return modifiedObjects.ToList();
        }
        public void SetEntityStatie(ObjectStateEntry objectStateEntry, object obj)
        {
            if (obj is IVHObject)
            {
                switch (objectStateEntry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        ((IVHObject)obj).DirtyState = DirtyState.UnChanged;
                        break;
                    case EntityState.Added:
                        ((IVHObject)obj).DirtyState = DirtyState.Added;
                        break;
                    case EntityState.Deleted:
                        ((IVHObject)obj).DirtyState = DirtyState.Deleted;
                        break;
                    case EntityState.Modified:
                        ((IVHObject)obj).DirtyState = DirtyState.Modified;
                        break;
                    default:
                        break;
                }
            }
        }
        public override int SaveChanges()
        {
            try
            {
                IList modifiedObjects = GetModifiedObjects();
                foreach (Object obj in modifiedObjects)
                {
                    if (obj is IVHObject)
                    {
                        ((IVHObject)obj).OnSaving();
                    }
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var item in error.ValidationErrors)
                    {
                        sb.AppendLine(item.PropertyName + ": " + item.ErrorMessage);
                    }
                }
                //    Logger.Error("SaveChanges.DbEntityValidation", ex.GetAllMessages() + sb);
                throw;
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Model.Module> Modules { get; set; }
        public DbSet<AuthorityModule> AuthorityModules { get; set; }        
        public DbSet<UserAuthorityModuleMapping> UserAuthorityModuleMappings { get; set; }
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }

        public DbSet<分店> 分店s { get; set; }
        public DbSet<单位> 单位s { get; set; }
        public DbSet<重量单位> 重量单位s { get; set; }
        public DbSet<材质> 材质s { get; set; }
        public DbSet<黄金种类> 黄金种类s { get; set; }
        public DbSet<饰品类别> 饰品类别s { get; set; }
        public DbSet<饰品类型> 饰品类型s { get; set; }
        public DbSet<饰品> 饰品s { get; set; }
        public DbSet<饰品图片> 饰品图片s { get; set; }
        public DbSet<饰品提成> 饰品提成s { get; set; }
        public DbSet<会员> 会员s { get; set; }
        public DbSet<供应商> 供应商s { get; set; }
        public DbSet<销售单> 销售单s { get; set; }
        public DbSet<销售单明细> 销售单明细s { get; set; }
        public DbSet<销售退货单> 销售退货单s { get; set; }
        public DbSet<销售退货单明细> 销售退货单明细s { get; set; }
        public DbSet<收款单> 收款单s { get; set; }
        public DbSet<库存> 库存s { get; set; }
        public DbSet<入库单> 入库单s { get; set; }
        public DbSet<入库单明细> 入库单明细s { get; set; }
        public DbSet<退库单> 退库单s { get; set; }
        public DbSet<退库单明细> 退库单明细s { get; set; }
        public DbSet<盈亏单> 盈亏单s { get; set; }
        public DbSet<盈亏单明细> 盈亏单明细s { get; set; }
        public DbSet<调拨单> 调拨单s { get; set; }
        public DbSet<调拨单明细> 调拨单明细s { get; set; }
        public DbSet<付款单> 付款单s { get; set; }
        public DbSet<付款单明细> 付款单明细s { get; set; }
        public DbSet<库存出入明细> 库存出入明细s { get; set; }
        public DbSet<盘点表> 盘点表s { get; set; }
        public DbSet<盘点表明细> 盘点表明细s { get; set; }



        public DbSet<订单> 订单s { get; set; }
        public DbSet<订单明细> 订单明细s { get; set; }
        public DbSet<产品> 产品s { get; set; }
        public DbSet<颜色> 颜色s { get; set; }
    }

    public abstract class ZtxEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected ZtxEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
    }
}
