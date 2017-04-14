using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data.Model
{
   public class 订单:VHObject
    {
        public long ID { get; set; }
      
        public string 订单号 { get; set; }
        private string  _客户名称;
        public string 客户名称
        {
            get { return _客户名称; }
            set { Set<string>(() => this.客户名称, ref _客户名称, value); }
        }


        public virtual VHObjectList<订单明细> 订单明细 { get; set; }
    }
    public class 订单明细 : VHObject
    {
        public long ID { get; set; }
    
[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int 序号 { get; set; }
        public int 数量 { get; set; }
        public string 订单号 { get; set; }

        public long 订单ID { get; set; }
        public virtual 订单 订单 { get; set; }
        public long 产品ID { get; set; }
        public virtual 产品 产品 { get; set; }
    }
    public class 产品 : VHObject
    {
        public long ID { get; set; }

        public string 规格 { get; set; }
        public string 番号 { get; set; }

        public long 颜色ID { get; set; }
        public virtual 颜色 颜色 { get; set; }
    }
    public class 颜色 : VHObject
    {
        public long ID { get; set; }

        public string 颜色内容 { get; set; }
    }
}
