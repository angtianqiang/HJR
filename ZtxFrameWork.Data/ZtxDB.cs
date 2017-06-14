using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Threading;
using System.Threading.Tasks;

using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.Data
{
    public class ZtxCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<ZtxDB> // DropCreateDatabaseIfModelChanges<ZtxDB>
    {
        protected override void Seed(ZtxDB context)
        {
            base.Seed(context);

            ZtxDB.Init(context);

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
            context.SaveChanges();

            var parentMoudle = context.Modules.Where(t => t.ModuleTitle == "销售管理").FirstOrDefault();
            Model.Module m = null;
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "销售管理", ImageName = "Sales-Order.png", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "销售单CollectionView", ImageName = "Shopping Basket Add-WF.png", Parent = p, ModuleTitle = "销售单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "销售单", "销售管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "销售退货单CollectionView", ImageName = "Shopping Basket Delete-WF.png", Parent = p, ModuleTitle = "销售退货单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "销售退货单", "销售管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "收款单CollectionView", ImageName = "Finance-03-WF.png", Parent = p, ModuleTitle = "收款单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "收款单", "销售管理");

            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "库存管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "库存管理", ImageName = "Cupboard.png", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });

                m = context.Modules.Add(new Model.Module() { DocumentType = "分店库存View", ImageName = "Shopping OK-WF.png", Parent = p, ModuleTitle = "分店库存", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "分店库存", "库存管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "入库单CollectionView", ImageName = "Shopping OK-WF.png", Parent = p, ModuleTitle = "采购入库单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "采购入库单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "退库单CollectionView", ImageName = "Shopping Remove-01-WF.png", Parent = p, ModuleTitle = "采购退货单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "采购退货单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "盈亏单CollectionView", ImageName = "DocumentRemove-WF.png", Parent = p, ModuleTitle = "盈亏单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "盈亏单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "调拨单CollectionView", ImageName = "Document-Exchange.png", Parent = p, ModuleTitle = "调拨单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "调拨单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "付款单CollectionView", ImageName = "Animation-03.png", Parent = p, ModuleTitle = "付款单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "付款单", "库存管理");
                m = context.Modules.Add(new Model.Module() { DocumentType = "盘点表CollectionView", ImageName = "Documents-02.png", Parent = p, ModuleTitle = "盘点表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "盘点表", "库存管理");
            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "财务管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "财务管理", ImageName = "Finance-03-WF.png", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "付款单CollectionView", ImageName = "Finance-03-WF.png", Parent = p, ModuleTitle = "付款单", ModuleInfo = Model.ModuleInfo.MoudleAction });
                m = context.Modules.Add(new Model.Module() { DocumentType = "收款单CollectionView", ImageName = "Animation-03.png", Parent = p, ModuleTitle = "收款单", ModuleInfo = Model.ModuleInfo.MoudleAction });

            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "基础资料").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "基础资料", ImageName = "Gear-02-WF.png", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });

                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品CollectionView", ImageName = "Bullet-WF.png", Parent = p, ModuleTitle = "饰品", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "分店CollectionView", ImageName = "Bank.png", Parent = p, ModuleTitle = "分店", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "分店", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);


                m = context.Modules.Add(new Model.Module() { DocumentType = "电镀方式CollectionView", ImageName = "Gear--03WF.png", Parent = p, ModuleTitle = "电镀方式", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "电镀方式", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "石头颜色CollectionView", ImageName = "Gear--03WF.png", Parent = p, ModuleTitle = "石头颜色", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "石头颜色", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);


                m = context.Modules.Add(new Model.Module() { DocumentType = "单位CollectionView", ImageName = "Gear--03WF.png", Parent = p, ModuleTitle = "单位", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "单位", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "重量单位CollectionView", ImageName = "Gear--03WF.png", Parent = p, ModuleTitle = "重量单位", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "重量单位", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "材质CollectionView", ImageName = "Gear--03WF.png", Parent = p, ModuleTitle = "材质", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "材质", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                // m = context.Modules.Add(new Model.Module() { DocumentType = "黄金种类CollectionView", Parent = p, ModuleTitle = "黄金种类", ModuleInfo = Model.ModuleInfo.MoudleAction });
                //  InitAuthorityModule(context, "黄金种类", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品类别CollectionView", ImageName = "Gear-01-WF.png", Parent = p, ModuleTitle = "品名", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "品名", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品类型CollectionView", ImageName = "Gear--03WF.png", Parent = p, ModuleTitle = "饰品类型", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品类型", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                //m = context.Modules.Add(new Model.Module() { DocumentType = "饰品提成CollectionView", Parent = p, ModuleTitle = "饰品提成", ModuleInfo = Model.ModuleInfo.MoudleAction });
                //InitAuthorityModule(context, "饰品提成", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "会员CollectionView", ImageName = "User.png", Parent = p, ModuleTitle = "会员", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "会员", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "供应商CollectionView", ImageName = "Team-02.png", Parent = p, ModuleTitle = "供应商", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "供应商", "基础资料", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
            }

            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "报表管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                //   var p = context.Modules.Add(new Model.Module() { ModuleTitle = "报表管理", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "报表管理", ImageName = "Documents-02.png", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });

                var p1 = context.Modules.Add(new Model.Module() { ModuleTitle = "财务报表", ImageName = "edit.png", Parent = p, ModuleInfo = Model.ModuleInfo.MoudleSubGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                var p2 = context.Modules.Add(new Model.Module() { ModuleTitle = "库存报表", ImageName = "edit.png", Parent = p, ModuleInfo = Model.ModuleInfo.MoudleSubGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                var p3 = context.Modules.Add(new Model.Module() { ModuleTitle = "销售报表", ImageName = "edit.png", Parent = p, ModuleInfo = Model.ModuleInfo.MoudleSubGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });

                m = context.Modules.Add(new Model.Module() { DocumentType = "应收款明细表View", ImageName = "Document Find-WF.png", Parent = p1, ModuleTitle = "应收款明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "应收款明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "应付款明细表View", ImageName = "Graph-03.png", Parent = p1, ModuleTitle = "应付款明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "应付款明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "付款明细表View", ImageName = "Document Find-WF.png", Parent = p1, ModuleTitle = "付款明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "付款明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "收款明细表View", ImageName = "Document Find-WF.png", Parent = p1, ModuleTitle = "收款明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "收款明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);


                m = context.Modules.Add(new Model.Module() { DocumentType = "库存明细表View", ImageName = "Document Find-WF.png", Parent = p2, ModuleTitle = "库存明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "库存明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "采购入库单明细表View", ImageName = "Document Find-WF.png", Parent = p2, ModuleTitle = "采购入库单明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "采购入库单明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品出入明细表View", ImageName = "Document Find-WF.png", Parent = p2, ModuleTitle = "饰品出入明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品出入明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "供应商退货明细表View", ImageName = "Document Find-WF.png", Parent = p2, ModuleTitle = "供应商退货明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "供应商退货明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "盈亏单明细表View", ImageName = "Document Find-WF.png", Parent = p2, ModuleTitle = "盈亏单明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "盈亏单明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "调拨明细表View", ImageName = "Document Find-WF.png", Parent = p2, ModuleTitle = "调拨明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "调拨明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);

                m = context.Modules.Add(new Model.Module() { DocumentType = "销售单明细表View", ImageName = "Document Edit-WF.png", Parent = p3, ModuleTitle = "销售单明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "销售单明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "销售退货单明细表View", ImageName = "Document Edit-WF.png", Parent = p3, ModuleTitle = "销售退货单明细表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "销售退货单明细表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "饰品销售排行表View", ImageName = "DataHistogram-WF.png", Parent = p3, ModuleTitle = "饰品销售排行表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "饰品销售排行表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "员工业绩排行表View", ImageName = "Banker-01-WF.png", Parent = p3, ModuleTitle = "员工业绩排行表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "员工业绩排行表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "每日经营情况分析View", ImageName = "Graph-01.png", Parent = p3, ModuleTitle = "每日经营情况分析", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "每日经营情况分析", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "产品库存动态表View", ImageName = "Graph-01.png", Parent = p3, ModuleTitle = "产品库存动态表", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "产品库存动态表", "报表管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);

            }
            parentMoudle = context.Modules.Where(t => t.ModuleTitle == "系统管理").FirstOrDefault();
            if (parentMoudle == null)
            {
                var p = context.Modules.Add(new Model.Module() { ModuleTitle = "系统管理", ImageName = "Tools.png", ModuleInfo = Model.ModuleInfo.ModuleGroup, CreateOn = DateTime.Now, ModifiedOn = DateTime.Now });
                m = context.Modules.Add(new Model.Module() { DocumentType = "ModuleCollectionView", ImageName = "Gear-01-WF.png", Parent = p, ModuleTitle = "系统模块", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "系统模块", "系统管理", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "UserCollectionView", ImageName = "User-Group.png", Parent = p, ModuleTitle = "用户管理", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "用户管理", "系统管理", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "AuthorityModuleCollectionView", ImageName = "Login2-WF.png", Parent = p, ModuleTitle = "模块权限配置", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "模块权限配置", "系统管理", Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "UserAuthorityModuleMappingCollectionView", ImageName = "Gear-02-WF.png", Parent = p, ModuleTitle = "用户权限管理", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "用户权限管理", "系统管理", Add: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "SystemConfigurationCollectionView", ImageName = "Graph-03.png", Parent = p, ModuleTitle = "系统参数配置", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "系统参数配置", "系统管理", Add: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "ChangePasswordView", ImageName = "Login-WF.png", Parent = p, ModuleTitle = "更改登录密码", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "更改登录密码", "系统管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "DbOperatorLogCollectionView", ImageName = "edit.png", Parent = p, ModuleTitle = "数据存储操作日志", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "数据存储操作日志", "系统管理", Add: false,Edit:false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "BillStateChangeLogCollectionView", ImageName = "Database Connection-WF.png", Parent = p, ModuleTitle = "单据状态更改日志", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "单据状态更改日志", "系统管理", Add: false,Edit:false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
                m = context.Modules.Add(new Model.Module() { DocumentType = "店面切换View", ImageName = "Bank.png", Parent = p, ModuleTitle = "店面切换", ModuleInfo = Model.ModuleInfo.MoudleAction });
                InitAuthorityModule(context, "店面切换", "系统管理", Add: false, Edit: false, Delete: false, Confirm: false, UnConfirm: false, Audit: false, UnAudit: false);
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
            var unit3 = context.重量单位s.Where(t => t.名称 == "g").FirstOrDefault();
            if (unit3 == null)
            {
                context.重量单位s.Add(new 重量单位() { 名称 = "g", 排序号 = "001" });
            }
            if (context.材质s.Where(T => T.名称 == "银").FirstOrDefault() == null)
            {
                context.材质s.Add(new 材质() { 名称 = "银", 当前价 = 5.45m, 排序号 = "001" });
            }
            if (context.材质s.Where(T => T.名称 == "金").FirstOrDefault() == null)
            {
                context.材质s.Add(new 材质() { 名称 = "金", 当前价 = 9.385m, 排序号 = "002" });
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
            if (config == null)
            {
                context.SystemConfigurations.Add(new SystemConfiguration()
                {
                    Token = "DestroyOnClose",
                    Category = "全局系统设置",
                    TokenValue = "0",
                    Des = "是否启用页面缓存",
                    Remark = "0:关闭页面文档后，释放内存；1：关闭页面文档后，留文档"
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
            config = context.SystemConfigurations.Where(t => t.Token == "ViewTopCount").FirstOrDefault();
            if (config == null)
            {
                context.SystemConfigurations.Add(new SystemConfiguration()
                {
                    Token = "ViewTopCount",
                    Category = "页面显示行数",
                    TokenValue = "1000",
                    Des = "页面默认显示的最大行数",
                    Remark = ""
                }
                );
            }
            #endregion
            context.SaveChanges();

            Inited = true;
        }
        protected static void InitAuthorityModule(ZtxDB context, string permissionTitle, string category, bool Navigate = true, bool Add = true, bool Edit = true, bool Delete = true, bool Export = true, bool Print = true, bool Preview = true, bool Design = true, bool Confirm = true, bool UnConfirm = true, bool Audit = true, bool UnAudit = true)
        {
            var view = context.AuthorityModules.Where(t => t.ViewTitle == permissionTitle).FirstOrDefault();
            if (view == null)
            {
                var v = context.AuthorityModules.Add(new AuthorityModule() { ViewTitle = permissionTitle, Category = category, Navigate = Navigate, Add = Add, Edit = Edit, Delete = Delete, Export = Export, Print = Print, Preview = Preview, Design = Design, Confirm = Confirm, UnConfirm = UnConfirm, Audit = Audit, UnAudit = UnAudit });


                foreach (var item in context.Users.AsEnumerable())
                {
                    v.UserAuthorityModuleMappings.Add(new UserAuthorityModuleMapping() { UserID = item.ID, Navigate = Navigate, Add = Add, Edit = Edit, Delete = Delete, Export = Export, Print = Print, Preview = Preview, Design = Design, Confirm = Confirm, UnConfirm = UnConfirm, Audit = Audit, UnAudit = UnAudit });
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
        //自动迁够是用到
        public ZtxDB() : this("Data Source=127.0.0.1;Initial Catalog=ztxFrameWork3;Integrated Security=True;MultipleActiveResultSets=true")
        { InitializeDbContext(); }
        public ZtxDB(string conn) : base(conn)
        {

            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    Database.SetInitializer<ZtxDB>(new ZtxCreateDatabaseIfNotExists());
            //}
            //else
            //{
            //    //    Database.SetInitializer<ZtxDB>(null);

            //    Database.SetInitializer<ZtxDB>(new ZtxCreateDatabaseIfNotExists());
            //}
             Database.SetInitializer<ZtxDB>(null   );
           //   this.Database.Initialize(false);
            InitializeDbContext();
        }
        public ZtxDB(DbConnection con) : base(con, contextOwnsConnection: false) { InitializeDbContext(); }

        private void InitializeDbContext()
        {

            this.Configuration.LazyLoadingEnabled = false;
           this.Configuration.ProxyCreationEnabled = false;

            var context = ((IObjectContextAdapter)this).ObjectContext;


            context.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
            context.ObjectMaterialized += Context_ObjectMaterialized;
        }

        private void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (e.Entity is IEntity)
            {
                ((IEntity)e.Entity).OnLoaded();
            }
        }

        private void ObjectStateManager_ObjectStateManagerChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {




            if (e.Action == CollectionChangeAction.Add)
            {
                if (e.Element is INotifyPropertyChanged)
                {
                    // ((INotifyPropertyChanged)e.Element).PropertyChanged -= Object_PropertyChanged;
                    // ((INotifyPropertyChanged)e.Element).PropertyChanged += Object_PropertyChanged;
                }
                //设置添加对象的时间和用户



                if (e.Element is IEntity)
                {

                }
                if (e.Element is IDbContextLink)
                {
                    ((IDbContextLink)e.Element).DbContext = this;
                }
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                if (e.Element is INotifyPropertyChanged)
                {
                    //  ((INotifyPropertyChanged)e.Element).PropertyChanged -= Object_PropertyChanged;
                }
                if (e.Element is IDbContextLink)
                {
                    ((IDbContextLink)e.Element).DbContext = null;
                }
            }
        }

        private void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //#region 20170418
            //var entity = sender as Model.Entity;
            //if (entity != null && entity.DbContext != null)
            //{
            //    if (e.PropertyName.ToUpper() == "ID") return;

            //    var state = entity.DbContext.Entry(entity).State;


            //    switch (state)
            //    {
            //        case EntityState.Detached:
            //            entity.DirtyState = DirtyState.UnChanged;
            //            break;
            //        case EntityState.Unchanged:
            //            entity.DirtyState = DirtyState.UnChanged;
            //            break;
            //        case EntityState.Added:
            //            entity.DirtyState = DirtyState.Added;
            //            break;
            //        case EntityState.Deleted:
            //            entity.DirtyState = DirtyState.Deleted;
            //            break;
            //        case EntityState.Modified:
            //            entity.DirtyState = DirtyState.Modified;
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //#endregion
        }

        protected override void Dispose(bool disposing)
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            context.ObjectStateManager.ObjectStateManagerChanged -= ObjectStateManager_ObjectStateManagerChanged;
            context.ObjectMaterialized -= Context_ObjectMaterialized;
            base.Dispose(disposing);
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
        public void NotityChanging()
        {
            IList modifiedObjects = GetModifiedObjects();
            foreach (Object obj in modifiedObjects)
            {
                if (obj is IEntity)
                {
                    #region 20170411 首次生成数据库，添加种子数据库时，未注册 ObjectStateManager_ObjectStateManagerChanged DB上下文为NULL(EF执行的先后顺序解决了）
                    if (((IEntity)obj).DbContext == null)
                    {
                        ((IEntity)obj).DbContext = this;
                    }
                    #endregion
                    ((IEntity)obj).OnSaving();
                }
            }
        }
        public override int SaveChanges()
        {
            //try
            //{
            NotityChanging();
            return base.SaveChanges();
            //  }
            //catch (DbEntityValidationException ex)
            //{
            //    var sb = new StringBuilder();
            //    foreach (var error in ex.EntityValidationErrors)
            //    {
            //        foreach (var item in error.ValidationErrors)
            //        {
            //            sb.AppendLine(item.PropertyName + ": " + item.ErrorMessage);
            //        }
            //    }
            //    //    Logger.Error("SaveChanges.DbEntityValidation", ex.GetAllMessages() + sb);
            //    throw new Exception(sb.ToString()) ;
            //}

            //catch (Exception e)
            //{

            //    throw e;
            //}
        }
        public override Task<int> SaveChangesAsync()
        {
            NotityChanging();
            return base.SaveChangesAsync();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            NotityChanging();
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Model.Module> Modules { get; set; }
        public DbSet<AuthorityModule> AuthorityModules { get; set; }
        public DbSet<UserAuthorityModuleMapping> UserAuthorityModuleMappings { get; set; }
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public DbSet<DbOperatorLog> DbOperatorLogs { get; set; }
        public DbSet<BillStateChangeLog> BillStateChangeLogs { get; set; }

        public DbSet<分店> 分店s { get; set; }
        public DbSet<单位> 单位s { get; set; }
        public DbSet<重量单位> 重量单位s { get; set; }
        public DbSet<材质> 材质s { get; set; }
        public DbSet<黄金种类> 黄金种类s { get; set; }
        public DbSet<饰品类别> 饰品类别s { get; set; }
        public DbSet<饰品类型> 饰品类型s { get; set; }
        public DbSet<电镀方式> 电镀方式s { get; set; }
        public DbSet<石头颜色> 石头颜色s { get; set; }
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
        public DbSet<收款单明细> 收款单明细s { get; set; }
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

        public ObjectSet<T> GetObjectSet<T>() where T : class
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            ObjectSet<T> set = objectContext.CreateObjectSet<T>();
            set.MergeOption = MergeOption.OverwriteChanges;

            return set;
        }

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
