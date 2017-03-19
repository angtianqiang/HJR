using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.Data.Mapping.Extensions;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace ZtxFrameWork.Data.Mapping
{
    public class AuthorityModuleUserAuthorityModuleMappingMap : ZtxEntityTypeConfiguration<UserAuthorityModuleMapping>
    {
        public AuthorityModuleUserAuthorityModuleMappingMap()
        {
            this.HasKey(a => a.ID);
            this.Require(t => t.User, t => t.UserAuthorityModuleMappings, t => t.UserID);
            this.Require(t => t.AuthorityModule, t => t.UserAuthorityModuleMappings, t => t.AuthorityModuleID);
        }
    }
    public class AuthorityModuleMap : ZtxEntityTypeConfiguration<AuthorityModule>
    {
        public AuthorityModuleMap()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.ViewTitle).IsRequired().HasMaxLength(120)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_AuthorityModule_ViewTitle") { IsUnique = true }));
        }
    }
    public class 分店Map : ZtxEntityTypeConfiguration<分店>
    {
        public 分店Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40);
            //   .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UserName") { IsUnique = true }));
        }
    }
    public class 单位Map : ZtxEntityTypeConfiguration<单位>
    {
        public 单位Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.名称).IsRequired().HasMaxLength(40)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_单位_名称") { IsUnique = true }));
        }
    }
    public class 重量单位Map : ZtxEntityTypeConfiguration<重量单位>
    {
        public 重量单位Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.名称).IsRequired().HasMaxLength(40)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_重量单位_名称") { IsUnique = true }));
        }
    }
    public class 材质Map : ZtxEntityTypeConfiguration<材质>
    {
        public 材质Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.名称).IsRequired().HasMaxLength(40)
             .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_材质_名称") { IsUnique = true }));
        }

    }
    public class 黄金种类Map : ZtxEntityTypeConfiguration<黄金种类>
    {
        public 黄金种类Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.名称).IsRequired().HasMaxLength(40)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_黄金种类_名称") { IsUnique = true }));
        }
    }
    public class 饰品类别Map : ZtxEntityTypeConfiguration<饰品类别>
    {
        public 饰品类别Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.名称).IsRequired().HasMaxLength(40)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_饰品类别_名称") { IsUnique = true }));

        }
    }
    public class 饰品类型Map : ZtxEntityTypeConfiguration<饰品类型>
    {
        public 饰品类型Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.名称).IsRequired().HasMaxLength(40)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_饰品类型_名称", 1) { IsUnique = true }));
            this.Property(t => t.类别ID).IsRequired()
             .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_饰品类型_名称", 2) { IsUnique = true }));

            this.Require(t => t.饰品类别, t => t.饰品类型s, t => t.类别ID);
        }
    }
    public class 饰品Map : ZtxEntityTypeConfiguration<饰品>
    {
        public 饰品Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_饰品_编号") { IsUnique = true }));
            this.Property(t => t.条码).HasMaxLength(60)
     .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_饰品_条码") { IsUnique = true }));


            this.HasOptional(t => t.饰品图片).WithMany().HasForeignKey(t => t.饰品图片ID).WillCascadeOnDelete(false);
         
            this.Require(t => t.饰品类型, t => t.饰品s, t => t.类型ID);
            this.Require(t => t.单位, t => t.饰品s, t => t.单位ID);
            this.Require(t => t.重量单位, t => t.饰品s, t => t.重量单位ID);
            this.Require(t => t.黄金种类, t => t.饰品s, t => t.黄金种类ID);
            this.Require(t => t.材质, t => t.饰品s, t => t.材质ID);
        }
    }
    public class 饰品提成Map : ZtxEntityTypeConfiguration<饰品提成>
    {
        public 饰品提成Map()
        {
            this.HasKey(a => a.ID);
            this.HasRequired(t => t.饰品).WithMany(t=>t.饰品提成s).HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }
    public class 会员Map : ZtxEntityTypeConfiguration<会员>
    {
        public 会员Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_会员_编号") { IsUnique = true }));
        }
    }
    public class 供应商Map : ZtxEntityTypeConfiguration<供应商>
    {
        public 供应商Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_供应商_编号") { IsUnique = true }));

        }
    }
    public class 销售单Map : ZtxEntityTypeConfiguration<销售单>
    {
        public 销售单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_销售单_编号") { IsUnique = true }));

            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.会员).WithMany().HasForeignKey(t => t.会员ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.操作员).WithMany().HasForeignKey(t => t.操作员ID).WillCascadeOnDelete(false);

        }
    }
    public class 销售单明细Map : ZtxEntityTypeConfiguration<销售单明细>
    {
        public 销售单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.销售单).WithMany(t => t.销售单明细s).HasForeignKey(t => t.销售单ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }
    public class 销售退货单Map : ZtxEntityTypeConfiguration<销售退货单>
    {
        public 销售退货单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_销售退货单_编号") { IsUnique = true }));

            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.会员).WithMany().HasForeignKey(t => t.会员ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.操作员).WithMany().HasForeignKey(t => t.操作员ID).WillCascadeOnDelete(false);
        }
    }
    public class 销售退货单明细Map : ZtxEntityTypeConfiguration<销售退货单明细>
    {
        public 销售退货单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.销售退货单).WithMany(t => t.销售退货单明细s).HasForeignKey(t => t.销售退货单ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }
    public class 收款单Map : ZtxEntityTypeConfiguration<收款单>
    {
        public 收款单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_收款单_编号") { IsUnique = true }));
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.会员).WithMany().HasForeignKey(t => t.会员ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.销售单).WithMany().HasForeignKey(t => t.销售单ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.销售退货单).WithMany().HasForeignKey(t => t.销售退货单ID).WillCascadeOnDelete(false);

        }
    }
    public class 库存Map : ZtxEntityTypeConfiguration<库存>
    {
        public 库存Map()
        {
            this.HasKey(a => a.ID);
            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);
        }
    }
    public class 入库单Map : ZtxEntityTypeConfiguration<入库单>
    {
        public 入库单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_入库单_编号") { IsUnique = true }));

         
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.供应商).WithMany().HasForeignKey(t => t.供应商ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.操作员).WithMany().HasForeignKey(t => t.操作员ID).WillCascadeOnDelete(false);
        }
    }
    public class 入库单明细Map : ZtxEntityTypeConfiguration<入库单明细>
    {
        public 入库单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.入库单).WithMany(t => t.入库单明细s).HasForeignKey(t => t.入库单ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }
    public class 退库单Map : ZtxEntityTypeConfiguration<退库单>
    {
        public 退库单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_退库单_编号") { IsUnique = true }));
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.供应商).WithMany().HasForeignKey(t => t.供应商ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.操作员).WithMany().HasForeignKey(t => t.操作员ID).WillCascadeOnDelete(false);
        }
    }
    public class 退库单明细Map : ZtxEntityTypeConfiguration<退库单明细>
    {
        public 退库单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.退库单).WithMany(t => t.退库单明细s).HasForeignKey(t => t.退库单ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.入库单明细).WithMany().HasForeignKey(t => t.入库单明细ID).WillCascadeOnDelete(false);
        }
    }
    public class 盈亏单Map : ZtxEntityTypeConfiguration<盈亏单>
    {
        public 盈亏单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_ 盈亏单_编号") { IsUnique = true }));
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.操作员).WithMany().HasForeignKey(t => t.操作员ID).WillCascadeOnDelete(false);
        }
    }
    public class 盈亏单明细Map : ZtxEntityTypeConfiguration<盈亏单明细>
    {
        public 盈亏单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.盈亏单).WithMany(t => t.盈亏单明细s).HasForeignKey(t => t.盈亏单id).WillCascadeOnDelete(false);

            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }
    public class 调拨单Map : ZtxEntityTypeConfiguration<调拨单>
    {
        public 调拨单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_调拨单_编号") { IsUnique = true }));
            this.HasRequired(t => t.源分店).WithMany().HasForeignKey(t => t.源分店ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.目标分店).WithMany().HasForeignKey(t => t.目标分店ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.调拨员).WithMany().HasForeignKey(t => t.调拨员ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.签收员).WithMany().HasForeignKey(t => t.签收员ID).WillCascadeOnDelete(false);
        }
    }
    public class 调拨单明细Map : ZtxEntityTypeConfiguration<调拨单明细>
    {
        public 调拨单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.调拨单).WithMany(t => t.调拨单明细s).HasForeignKey(t => t.调拨单ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }
    public class 付款单Map : ZtxEntityTypeConfiguration<付款单>
    {
        public 付款单Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_付款单_编号") { IsUnique = true }));
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);

            this.HasOptional(t => t.会员).WithMany().HasForeignKey(t => t.会员ID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.供应商).WithMany().HasForeignKey(t => t.供应ID).WillCascadeOnDelete(false);
        }
    }
    public class 付款单明细Map : ZtxEntityTypeConfiguration<付款单明细>
    {
        public 付款单明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.付款单).WithMany(t => t.付款单明细s).HasForeignKey(t => t.付款单ID).WillCascadeOnDelete(false);

            this.HasOptional(t => t.入库单).WithMany().HasForeignKey(t => t.入库单ID).WillCascadeOnDelete(false);
            this.HasOptional(t => t.退库单).WithMany().HasForeignKey(t => t.退库单ID).WillCascadeOnDelete(false);

        }
    }
    public class 库存出入明细Map : ZtxEntityTypeConfiguration<库存出入明细>
    {
        public 库存出入明细Map()
        {
            this.HasKey(a => a.ID);
            this.HasRequired(t => t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
            this.Property(t => t.单据ID).IsRequired()
     .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_库存出入明细_单据编号单据ID",1) { IsUnique = true }));
            this.Property(t => t.单据编号).IsRequired().HasMaxLength(40)
  .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_库存出入明细_单据编号单据ID", 2) { IsUnique = true }));
        }
    }
    public class 盘点表Map : ZtxEntityTypeConfiguration<盘点表>
    {
        public 盘点表Map()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.编号).IsRequired().HasMaxLength(40)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_盘点表_编号") { IsUnique = true }));
            this.HasRequired(t => t.分店).WithMany().HasForeignKey(t => t.分店ID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.操作员).WithMany().HasForeignKey(t => t.操作员ID).WillCascadeOnDelete(false);
        }
    }
    public class 盘点表明细Map : ZtxEntityTypeConfiguration<盘点表明细>
    {
        public 盘点表明细Map()
        {
            this.HasKey(a => a.ID);


            this.HasRequired(t => t.盘点表).WithMany(t => t.盘点表明细s).HasForeignKey(t => t.盘点表ID).WillCascadeOnDelete(false);

            this.HasRequired(t =>t.饰品).WithMany().HasForeignKey(t => t.饰品ID).WillCascadeOnDelete(false);
        }
    }




}
