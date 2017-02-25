using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.Data.Mapping
{
  public  class 产品Map:ZtxEntityTypeConfiguration<产品>

    {
        public 产品Map()
        {
            this.HasKey(a => a.ID);
            this.HasRequired(a => a.颜色).WithMany().HasForeignKey(a => a.颜色ID).WillCascadeOnDelete(false);
        }
    }
    public class 订单明细Map : ZtxEntityTypeConfiguration<订单明细>

    {
        public 订单明细Map()
        {
            this.HasKey(a => a.ID);
            this.HasRequired(a => a.产品).WithMany().HasForeignKey(a => a.产品ID).WillCascadeOnDelete(false);
        }
    }
    public class 订单Map : ZtxEntityTypeConfiguration<订单>

    {
        public 订单Map()
        {
            this.HasKey(a => a.ID);
            this.HasMany(a => a.订单明细).WithRequired().HasForeignKey(a => a.订单ID).WillCascadeOnDelete(false);
        }
    }
}
