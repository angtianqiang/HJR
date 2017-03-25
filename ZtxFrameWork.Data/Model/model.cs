using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.Data.Model
{
    public class 分店 : ModelBase
    {
        private string _编号;
        public string 编号
        {
            get { return _编号; }
            set { Set<string>(() => this.编号, ref _编号, value); }
        }
        private string _名称;
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
        private string _联系人;
        public string 联系人
        {
            get { return _联系人; }
            set { Set<string>(() => this.联系人, ref _联系人, value); }
        }
        private string _联系电话;
        public string 联系电话
        {
            get { return _联系电话; }
            set { Set<string>(() => this.联系电话, ref _联系电话, value); }
        }
        private string _传真号码;
        public string 传真号码
        {
            get { return _传真号码; }
            set { Set<string>(() => this.传真号码, ref _传真号码, value); }
        }
        private string _地址;
        public string 地址
        {
            get { return _地址; }
            set { Set<string>(() => this.地址, ref _地址, value); }
        }
        private string _qq;
        public string QQ
        {
            get { return _qq; }
            set { Set<string>(() => this.QQ, ref _qq, value); }
        }
        private string _weiXi;
        public string WeiXi
        {
            get { return _weiXi; }
            set { Set<string>(() => this.WeiXi, ref _weiXi, value); }
        }
        private bool _是否总店;
        public bool 是否总店
        {
            get { return _是否总店; }
            set { Set<bool>(() => this.是否总店, ref _是否总店, value); }
        }
        private bool _是否允许修改今日金价;
        public bool 是否允许修改今日金价
        {
            get { return _是否允许修改今日金价; }
            set { Set<bool>(() => this.是否允许修改今日金价, ref _是否允许修改今日金价, value); }
        }
        private string _备注;
        public string 备注
        {
            get { return _备注; }
            set { Set<string>(() => this.备注, ref _备注, value); }
        }


    }
    public class 单位 : ModelBase
    {
        private string _名称;
        [Display(Name = "单位名称", AutoGenerateField = true)]
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
        private string _排序号;
        [Display(Name = "排序号", AutoGenerateField = false)]
        public string 排序号
        {
            get { return _排序号; }
            set { Set<string>(() => this.排序号, ref _排序号, value); }
        }
        public virtual ICollection<饰品> 饰品s { get; set; }

    }
    public class 重量单位 : ModelBase
    {
        private string _名称;
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
        private string _排序号;
        [Display(Name = "排序号", AutoGenerateField = false)]
        public string 排序号
        {
            get { return _排序号; }
            set { Set<string>(() => this.排序号, ref _排序号, value); }
        }
        public virtual ICollection<饰品> 饰品s { get; set; }
    }
    public class 材质 : ModelBase
    {
        private string _名称;
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
     
        private decimal _当前价;
        public decimal 当前价
        {
            get { return _当前价; }
            set { Set<decimal>(() => this.当前价, ref _当前价, value); }
        }
        private string _排序号;
        [Display(Name = "排序号", AutoGenerateField = false)]
        public string 排序号
        {
            get { return _排序号; }
            set { Set<string>(() => this.排序号, ref _排序号, value); }
        }
        public virtual ICollection<饰品> 饰品s { get; set; }
    }
    public class 黄金种类 : ModelBase
    {
        private string _名称;
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
        private string _排序号;
        [Display(Name = "排序号", AutoGenerateField = false)]
        public string 排序号
        {
            get { return _排序号; }
            set { Set<string>(() => this.排序号, ref _排序号, value); }
        }
        public virtual ICollection<饰品> 饰品s { get; set; }
    }
    public class 饰品类别 : ModelBase
    {
        private string _名称;
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
        private string _排序号;
        [Display(Name = "排序号", AutoGenerateField = false)]
        public string 排序号
        {
            get { return _排序号; }
            set { Set<string>(() => this.排序号, ref _排序号, value); }
        }
        public virtual ICollection<饰品类型> 饰品类型s { get; set; }

    }
    public class 饰品类型 : ModelBase
    {
        private string _名称;
        public string 名称
        {
            get { return _名称; }
            set { Set<string>(() => this.名称, ref _名称, value); }
        }
        private string _排序号;
        [Display(Name = "排序号", AutoGenerateField = false)]
        public string 排序号
        {
            get { return _排序号; }
            set { Set<string>(() => this.排序号, ref _排序号, value); }
        }

        private long _类别ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 类别ID
        {
            get { return _类别ID; }
            set { Set<long>(() => this.类别ID, ref _类别ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 饰品类别 饰品类别 { get; set; }

        public virtual ICollection<饰品> 饰品s { get; set; }

    }
    public class 饰品 : ModelBase
    {
        private string _编号;
        [Required(ErrorMessage = "必输值...")]
        public string 编号
        {
            get { return _编号; }
            set { Set<string>(() => this.编号, ref _编号, value); }
        }
        private string _条码;
        public string 条码
        {
            get { return _条码; }
            set { Set<string>(() => this.条码, ref _条码, value); }
        }
        private long _类型ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        [RangeAttribute(1, long.MaxValue)]
        public long 类型ID
        {
            get { return _类型ID; }
            set { Set<long>(() => this.类型ID, ref _类型ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public 饰品类型 饰品类型 { get; set; }
        private string _品名;
        public string 品名
        {
            get { return _品名; }
            set { Set<string>(() => this.品名, ref _品名, value); }
        }
        private long _单位ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        [RangeAttribute(1, long.MaxValue)]
        public long 单位ID
        {
            get { return _单位ID; }
            set { Set<long>(() => this.单位ID, ref _单位ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 单位 单位 { get; set; }
        private long _重量单位ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        [RangeAttribute(1, long.MaxValue)]
        public long 重量单位ID
        {
            get { return _重量单位ID; }
            set { Set<long>(() => this.重量单位ID, ref _重量单位ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 重量单位 重量单位 { get; set; }
        private decimal _单重;
        public decimal 单重
        {
            get { return _单重; }
            set { Set<decimal>(() => this.单重, ref _单重, value, ChangeColums); }
        }
        //private decimal _金重;
        //public decimal 金重
        //{
        //    get { return _金重; }
        //    set { Set<decimal>(() => this.金重, ref _金重, value); }
        //}
        //private long _黄金种类ID;
        //[Display(Name = "外键ID", AutoGenerateField = false)]
        //public long 黄金种类ID
        //{
        //    get { return _黄金种类ID; }
        //    set { Set<long>(() => this.黄金种类ID, ref _黄金种类ID, value); }
        //}
        // [Display(Name = "外键", AutoGenerateField = false)]
        //public virtual 黄金种类 黄金种类 { get; set; }
        private string _尺寸;
        public string 尺寸
        {
            get { return _尺寸; }
            set { Set<string>(() => this.尺寸, ref _尺寸, value); }
        }

        private string _柜号;
        public string 柜号
        {
            get { return _柜号; }
            set { Set<string>(() => this.柜号, ref _柜号, value); }
        }
        private long _材质ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 材质ID
        {
            get { return _材质ID; }
            set { Set<long>(() => this.材质ID, ref _材质ID, value); }
        }
     
        private 材质 _材质;
        public virtual 材质 材质
        {
            get { return _材质; }
            set { Set<材质>(() => this.材质, ref _材质, value,ChangeColums); }
        }

        private 费用计法 _工费计法;
        public 费用计法 工费计法
        {
            get { return _工费计法; }
            set { Set<费用计法>(() => this.工费计法, ref _工费计法, value); }
        }
        private decimal _成本工费;
        public decimal 成本工费
        {
            get { return _成本工费; }
            set { Set<decimal>(() => this.成本工费, ref _成本工费, value); }
        }
        private decimal _批发工费;
        public decimal 批发工费
        {
            get { return _批发工费; }
            set { Set<decimal>(() => this.批发工费, ref _批发工费, value, ChangeColums); }
        }
        private decimal _按件批发价;
        public decimal 按件批发价
        {
            get { return _按件批发价; }
            set { Set<decimal>(() => this.按件批发价, ref _按件批发价, value); }
        }
        private decimal _按重批发价;
        public decimal 按重批发价
        {
            get { return _按重批发价; }
            set { Set<decimal>(() => this.按重批发价, ref _按重批发价, value); }
        }

        private decimal _按件成本价;
        public decimal 按件成本价
        {
            get { return _按件成本价; }
            set { Set<decimal>(() => this.按件成本价, ref _按件成本价, value); }
        }
        private decimal _按重成本价;
        public decimal 按重成本价
        {
            get { return _按重成本价; }
            set { Set<decimal>(() => this.按重成本价, ref _按重成本价, value); }
        }
        private Int32 _库存上限;
        public Int32 库存上限
        {
            get { return _库存上限; }
            set { Set<Int32>(() => this.库存上限, ref _库存上限, value); }
        }
        private Int32 _库存下限;
        public Int32 库存下限
        {
            get { return _库存下限; }
            set { Set<Int32>(() => this.库存下限, ref _库存下限, value); }
        }

        private decimal _账面成本小计;
        public decimal 账面成本小计
        {
            get { return _账面成本小计; }
            set { Set<decimal>(() => this.账面成本小计, ref _账面成本小计, value); }
        }
        private Int32 _库存数量;
        public Int32 库存数量
        {
            get { return _库存数量; }
            set { Set<Int32>(() => this.库存数量, ref _库存数量, value); }
        }
        private decimal _库存重量;
        public decimal 库存重量
        {
            get { return _库存重量; }
            set { Set<decimal>(() => this.库存重量, ref _库存重量, value); }
        }

        private decimal _库存总金额;
        public decimal 库存总金额
        {
            get { return _库存总金额; }
            set { Set<decimal>(() => this.库存总金额, ref _库存总金额, value); }
        }

        public virtual ICollection<饰品提成> 饰品提成s { get; set; }


        private long? _饰品图片ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long? 饰品图片ID
        {
            get { return _饰品图片ID; }
            set { Set<long?>(() => this.饰品图片ID, ref _饰品图片ID, value); }
        }

        private 饰品图片 _饰品图片;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品图片 饰品图片
        {
            get { return _饰品图片; }
            set { Set<饰品图片>(() => this.饰品图片, ref _饰品图片, value); }
        }
        public void ChangeColums()
        {
            this.OnPropertyChanged("QtyPrice");
            this.OnPropertyChanged("WeightPrice");
        }
        public decimal QtyPrice
        {
            get
            {
                if (this.材质 == null)
                {
                    return 0M;
                }
                else
                {

                    return this.单重 * this.材质.当前价 + this.批发工费;
                }
            }
        }
        public decimal WeightPrice
        {
            get
            {
                if (this.材质 == null)
                {
                    return 0M;
                }
                else
                {
                    try
                    {
                        return (this.单重 * this.材质.当前价 + this.批发工费) / this.单重;
                    }
                    catch (Exception)
                    {
                        return 0m;
                    }
                }
            }
        }

    }
    public class 饰品图片 : ModelBase
    {
        private byte[] _图片;
        public byte[] 图片
        {
            get { return _图片; }
            set { Set<byte[]>(() => this.图片, ref _图片, value); }
        }

    }

    public class 饰品提成 : ModelBase
    {

        private long _饰品ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }

        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 饰品 饰品 { get; set; }
        private bool _是否按比例;
        public bool 是否按比例
        {
            get { return _是否按比例; }
            set { Set<bool>(() => this.是否按比例, ref _是否按比例, value); }
        }
        private decimal _比例;
        public decimal 比例
        {
            get { return _比例; }
            set { Set<decimal>(() => this.比例, ref _比例, value); }
        }
        private decimal _固定金额;
        public decimal 固定金额
        {
            get { return _固定金额; }
            set { Set<decimal>(() => this.固定金额, ref _固定金额, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

    }
    public class 会员 : ModelBase
    {
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private String _姓名;
        public String 姓名
        {
            get { return _姓名; }
            set { Set<String>(() => this.姓名, ref _姓名, value); }
        }
        private String _性别;
        public String 性别
        {
            get { return _性别; }
            set { Set<String>(() => this.性别, ref _性别, value); }
        }
        private decimal _折扣;
        public decimal 折扣
        {
            get { return _折扣; }
            set { Set<decimal>(() => this.折扣, ref _折扣, value); }
        }
        private int _初始积分;
        public int 初始积分
        {
            get { return _初始积分; }
            set { Set<int>(() => this.初始积分, ref _初始积分, value); }
        }
        private decimal _销费金额;
        public decimal 销费金额
        {
            get { return _销费金额; }
            set { Set<decimal>(() => this.销费金额, ref _销费金额, value); }
        }
        private String _手机号;
        public String 手机号
        {
            get { return _手机号; }
            set { Set<String>(() => this.手机号, ref _手机号, value); }
        }

        private String _固定电话;
        public String 固定电话
        {
            get { return _固定电话; }
            set { Set<String>(() => this.固定电话, ref _固定电话, value); }
        }
        private String _QQ;
        public String QQ
        {
            get { return _QQ; }
            set { Set<String>(() => this.QQ, ref _QQ, value); }
        }
        private String _WeiXin;
        public String WeiXin
        {
            get { return _WeiXin; }
            set { Set<String>(() => this.WeiXin, ref _WeiXin, value); }
        }
        private String _Mail;
        public String Mail
        {
            get { return _Mail; }
            set { Set<String>(() => this.Mail, ref _Mail, value); }
        }
        private bool _是否停用;
        public bool 是否停用
        {
            get { return _是否停用; }
            set { Set<bool>(() => this.是否停用, ref _是否停用, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        private Int32 _总积分;
        public Int32 总积分
        {
            get { return _总积分; }
            set { Set<Int32>(() => this.总积分, ref _总积分, value); }
        }
        private Int32 _已用积分;
        public Int32 已用积分
        {
            get { return _已用积分; }
            set { Set<Int32>(() => this.已用积分, ref _已用积分, value); }
        }

    }
    public class 供应商 : ModelBase
    {
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private String _简称;
        public String 简称
        {
            get { return _简称; }
            set { Set<String>(() => this.简称, ref _简称, value); }
        }
        private String _全称;
        public String 全称
        {
            get { return _全称; }
            set { Set<String>(() => this.全称, ref _全称, value); }
        }
        private String _联系人;
        public String 联系人
        {
            get { return _联系人; }
            set { Set<String>(() => this.联系人, ref _联系人, value); }
        }
        private String _联系电话;
        public String 联系电话
        {
            get { return _联系电话; }
            set { Set<String>(() => this.联系电话, ref _联系电话, value); }
        }
        private String _地址;
        public String 地址
        {
            get { return _地址; }
            set { Set<String>(() => this.地址, ref _地址, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

    }
    public class 销售单 : ModelBase
    {
        public 销售单()
        {
            销售单明细s = new VHObjectList<销售单明细>();
        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }

        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long? _会员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 会员ID
        {
            get { return _会员ID; }
            set { Set<long?>(() => this.会员ID, ref _会员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 会员 会员 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }
        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _总金额;
        public decimal 总金额
        {
            get { return _总金额; }
            set { Set<decimal>(() => this.总金额, ref _总金额, value); }
        }
        private decimal _已收金额;
        public decimal 已收金额
        {
            get { return _已收金额; }
            set { Set<decimal>(() => this.已收金额, ref _已收金额, value); }
        }
        private decimal _未收金额;
        public decimal 未收金额
        {
            get { return _未收金额; }
            set { Set<decimal>(() => this.未收金额, ref _未收金额, value); }
        }
        private decimal _积分系数;
        public decimal 积分系数
        {
            get { return _积分系数; }
            set { Set<decimal>(() => this.积分系数, ref _积分系数, value); }
        }
        private Int32 _本次积分;
        public Int32 本次积分
        {
            get { return _本次积分; }
            set { Set<Int32>(() => this.本次积分, ref _本次积分, value); }
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

        private ICollection<销售单明细> _销售单明细s;
        public virtual ICollection<销售单明细> 销售单明细s
        {
            get { return _销售单明细s; }
            set { Set<ICollection<销售单明细>>(() => this.销售单明细s, ref _销售单明细s, value); }
        }


    }
    public class 销售单明细 : ModelBase
    {
        private long _销售单ID;
      
        [Display(Name = "外键", AutoGenerateField = false)]
        public long 销售单ID
        {
            get { return _销售单ID; }
            set { Set<long>(() => this.销售单ID, ref _销售单ID, value); }
        }
        [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 销售单 销售单 { get; set; }
        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _饰品ID;
        [Display(Name = "外键", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        [Display(Name = "外键", AutoGenerateField = false)]
        private 饰品 _饰品;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品
        {
            get { return _饰品; }
            set { Set<饰品>(() => this.饰品, ref _饰品, value, () => { this.饰品编号 = 饰品?.编号 ?? ""; }); }
        }


        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }
        private 费用计法 _工费计法;
        public 费用计法 工费计法
        {
            get { return _工费计法; }
            set { Set<费用计法>(() => this.工费计法, ref _工费计法, value); }
        }
        private decimal _工费;
        public decimal 工费
        {
            get { return _工费; }
            set { Set<decimal>(() => this.工费, ref _工费, value); }
        }
        private decimal _销售价;
        public decimal 销售价
        {
            get { return _销售价; }
            set { Set<decimal>(() => this.销售价, ref _销售价, value); }
        }
      

        private decimal _折扣;
        public decimal 折扣
        {
            get { return _折扣; }
            set { Set<decimal>(() => this.折扣, ref _折扣, value); }
        }
        private decimal _折前价;
        public decimal 折前价
        {
            get { return _折前价; }
            set { Set<decimal>(() => this.折前价, ref _折前价, value); }
        }
        private decimal _金额;
        public decimal 金额
        {
            get => _金额;
            set => Set<decimal>(() => this.金额, ref _金额, value);
        }
     
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        #region 非映射业务字段 主要起导航作用



        private string _饰品编号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB
        [NotMapped]
        public string 饰品编号
        {
            get { return _饰品编号; }
            set { Set<string>(() => this.饰品编号, ref _饰品编号, value); }
        }
        #endregion

    }
    public class 销售退货单 : ModelBase
    {
        public 销售退货单()
        {
            销售退货单明细s = new VHObjectList<销售退货单明细>();
        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long? _会员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 会员ID
        {
            get { return _会员ID; }
            set { Set<long?>(() => this.会员ID, ref _会员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 会员 会员 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }
        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _总金额;
        public decimal 总金额
        {
            get { return _总金额; }
            set { Set<decimal>(() => this.总金额, ref _总金额, value); }
        }
        private decimal _已付金额;
        public decimal 已付金额
        {
            get { return _已付金额; }
            set { Set<decimal>(() => this.已付金额, ref _已付金额, value); }
        }
        private decimal _未付金额;
        public decimal 未付金额
        {
            get { return _未付金额; }
            set { Set<decimal>(() => this.未付金额, ref _未付金额, value); }
        }
        private decimal _积分系数;
        public decimal 积分系数
        {
            get { return _积分系数; }
            set { Set<decimal>(() => this.积分系数, ref _积分系数, value); }
        }
        private Int32 _本次积分;
        public Int32 本次积分
        {
            get { return _本次积分; }
            set { Set<Int32>(() => this.本次积分, ref _本次积分, value); }
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

        private ICollection<销售退货单明细> _销售退货单明细s;
        public virtual ICollection<销售退货单明细> 销售退货单明细s
        {
            get { return _销售退货单明细s; }
            set { Set<ICollection<销售退货单明细>>(() => this.销售退货单明细s, ref _销售退货单明细s, value); }
        }



      
    }
    public class 销售退货单明细 : ModelBase
    {
        private long _销售退货单ID;
        [Display(Name = "外键", AutoGenerateField = false)]
        public long 销售退货单ID
        {
            get { return _销售退货单ID; }
            set { Set<long>(() => this.销售退货单ID, ref _销售退货单ID, value); }
        }
        [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 销售退货单 销售退货单 { get; set; }
        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _饰品ID;
        [Display(Name = "外键", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        private 饰品 _饰品;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品
        {
            get { return _饰品; }
            set { Set<饰品>(() => this.饰品, ref _饰品, value, () => { this.饰品编号 = 饰品?.编号 ?? ""; }); }
        }



        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }
       
        private 费用计法 _工费计法;
        public 费用计法 工费计法
        {
            get { return _工费计法; }
            set { Set<费用计法>(() => this.工费计法, ref _工费计法, value); }
        }
        private decimal _工费;
        public decimal 工费
        {
            get { return _工费; }
            set { Set<decimal>(() => this.工费, ref _工费, value); }
        }
        private decimal _销售价;
        public decimal 销售价
        {
            get { return _销售价; }
            set { Set<decimal>(() => this.销售价, ref _销售价, value); }
        }
        private decimal _折扣;
        public decimal 折扣
        {
            get { return _折扣; }
            set { Set<decimal>(() => this.折扣, ref _折扣, value); }
        }
        private decimal _折前价;
        public decimal 折前价
        {
            get { return _折前价; }
            set { Set<decimal>(() => this.折前价, ref _折前价, value); }
        }
        private decimal _折后价;
        private decimal _金额;
        public decimal 金额
        {
            get => _金额;
            set => Set<decimal>(() => this.金额, ref _金额, value);
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        #region 非映射业务字段 主要起导航作用



        private string _饰品编号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB
        [NotMapped]
        public string 饰品编号
        {
            get { return _饰品编号; }
            set { Set<string>(() => this.饰品编号, ref _饰品编号, value); }
        }
        #endregion
    }
    public class 收款单 : ModelBase
    {

        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _收款日期;
        public DateTime 收款日期
        {
            get { return _收款日期; }
            set { Set<DateTime>(() => this.收款日期, ref _收款日期, value); }
        }
        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long? _会员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 会员ID
        {
            get { return _会员ID; }
            set { Set<long?>(() => this.会员ID, ref _会员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 会员 会员 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }

        private long? _销售单ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 销售单ID
        {
            get { return _销售单ID; }
            set { Set<long?>(() => this.销售单ID, ref _销售单ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 销售单 销售单 { get; set; }
        private long? _销售退货单ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 销售退货单ID
        {
            get { return _销售退货单ID; }
            set { Set<long?>(() => this.销售退货单ID, ref _销售退货单ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 销售退货单 销售退货单 { get; set; }
        private decimal _金额;
        public decimal 金额
        {
            get { return _金额; }
            set { Set<decimal>(() => this.金额, ref _金额, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

    }
    public class 库存 : ModelBase
    {
        private long _饰品ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 饰品 饰品 { get; set; }
        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }
        private decimal _金额;
        public decimal 金额
        {
            get { return _金额; }
            set { Set<decimal>(() => this.金额, ref _金额, value); }
        }
      

    }
    public class 入库单 : ModelBase
    {
        public 入库单()
        {
            this.入库单明细s = new VHObjectList<入库单明细>();

        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _分店ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        [RangeAttribute(1, long.MaxValue)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
        [Display(Name = "分店", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long? _供应商ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long? 供应商ID
        {
            get { return _供应商ID; }
            set { Set<long?>(() => this.供应商ID, ref _供应商ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 供应商 供应商 { get; set; }

        private long _操作员ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }
        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _总金额;
        public decimal 总金额
        {
            get { return _总金额; }
            set { Set<decimal>(() => this.总金额, ref _总金额, value); }
        }
        private decimal _已付金额;
        public decimal 已付金额
        {
            get { return _已付金额; }
            set { Set<decimal>(() => this.已付金额, ref _已付金额, value); }
        }
        private decimal _未付金额;
        public decimal 未付金额
        {
            get { return _未付金额; }
            set { Set<decimal>(() => this.未付金额, ref _未付金额, value); }
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        private VHObjectList<入库单明细> _入库单明细s;
       
        public virtual VHObjectList<入库单明细> 入库单明细s
        {
            get { return _入库单明细s; }
            set { Set<VHObjectList<入库单明细>>(() => this.入库单明细s, ref _入库单明细s, value); }
        }



    }
    public class 入库单明细 : ModelBase
    {

        private long _入库单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 入库单ID
        {
            get { return _入库单ID; }
            set { Set<long>(() => this.入库单ID, ref _入库单ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 入库单 入库单 { get; set; }
        private Int32 _序号;
        [DisplayFormat(DataFormatString = "000")]
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _饰品ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        private 饰品 _饰品;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品
        {
            get { return _饰品; }
            set { Set<饰品>(() => this.饰品, ref _饰品, value, () => { this.饰品编号 = 饰品?.编号 ?? ""; }); }
        }


        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        [DisplayFormat(DataFormatString = "N2", ApplyFormatInEditMode = true)]
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }

        private decimal _单价;


        [DisplayFormat(DataFormatString = "N2", ApplyFormatInEditMode = true)]
        public decimal 单价
        {
            get { return _单价; }
            set { Set<decimal>(() => this.单价, ref _单价, value); }
        }
        private 费用计法 _计价方式;
        public 费用计法 计价方式
        {
            get { return _计价方式; }
            set { Set<费用计法>(() => this.计价方式, ref _计价方式, value); }
        }


        private decimal _金额;
        [DisplayFormat(DataFormatString = "N2", ApplyFormatInEditMode = true)]
        [Editable(false)]
        public decimal 金额
        {
            get { return _金额; }
            set { Set<decimal>(() => this.金额, ref _金额, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }




        #region 非映射业务字段 主要起导航作用



        private string _饰品编号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB
        [NotMapped]
        public string 饰品编号
        {
            get { return _饰品编号; }
            set { Set<string>(() => this.饰品编号, ref _饰品编号, value); }
        }
        #endregion

    }
    [DisplayName("采购退货单")]
    public class 退库单 : ModelBase
    {
        public 退库单()
        {
            退库单明细s = new VHObjectList<退库单明细>();
        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long? _供应商ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 供应商ID
        {
            get { return _供应商ID; }
            set { Set<long?>(() => this.供应商ID, ref _供应商ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 供应商 供应商 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }
        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _总金额;
        public decimal 总金额
        {
            get { return _总金额; }
            set { Set<decimal>(() => this.总金额, ref _总金额, value); }
        }
        private decimal _已收金额;
        public decimal 已收金额
        {
            get { return _已收金额; }
            set { Set<decimal>(() => this.已收金额, ref _已收金额, value); }
        }
        private decimal _未收金额;
        public decimal 未收金额
        {
            get { return _未收金额; }
            set { Set<decimal>(() => this.未收金额, ref _未收金额, value); }
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        private ICollection<退库单明细> _退库单明细s;
        public virtual ICollection<退库单明细> 退库单明细s
        {
            get { return _退库单明细s; }
            set { Set<ICollection<退库单明细>>(() => this.退库单明细s, ref _退库单明细s, value); }
        }




    }
    public class 退库单明细 : ModelBase
    {
        private long _退库单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 退库单ID
        {
            get { return _退库单ID; }
            set { Set<long>(() => this.退库单ID, ref _退库单ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 退库单 退库单 { get; set; }
        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _入库单明细ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 入库单明细ID
        {
            get { return _入库单明细ID; }
            set { Set<long>(() => this.入库单明细ID, ref _入库单明细ID, value); }
        }

        private 入库单明细 _入库单明细;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 入库单明细 入库单明细
        {
            get { return _入库单明细; }
            set { Set<入库单明细>(() => this.入库单明细, ref _入库单明细, value); }
        }

        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }

        private Decimal _单价;
        public Decimal 单价
        {
            get { return _单价; }
            set { Set<Decimal>(() => this.单价, ref _单价, value); }
        }
        private 费用计法 _计价方式;
        public 费用计法 计价方式
        {
            get { return _计价方式; }
            set { Set<费用计法>(() => this.计价方式, ref _计价方式, value); }
        }

        private decimal _金额;
        public decimal 金额
        {
            get { return _金额; }
            set { Set<decimal>(() => this.金额, ref _金额, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }


        #region (非)映射业务字段 主要起导航作用



        private string _入库单号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB

        public string 入库单号
        {
            get { return _入库单号; }
            set { Set<string>(() => this.入库单号, ref _入库单号, value); }
        }
        #endregion
    }
    public class 盈亏单 : ModelBase
    {
        public 盈亏单()
        {
            盈亏单明细s = new VHObjectList<盈亏单明细>();
        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _分店ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        [RangeAttribute(1, long.MaxValue)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }
        private int _数量;
        public int 数量
        {
            get => _数量;
            set => Set<int>(() => this.数量, ref _数量, value);
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

        private ICollection<盈亏单明细> _盈亏单明细s;
        public virtual ICollection<盈亏单明细> 盈亏单明细s
        {
            get { return _盈亏单明细s; }
            set { Set<ICollection<盈亏单明细>>(() => this.盈亏单明细s, ref _盈亏单明细s, value); }
        }

    }
    public class 盈亏单明细 : ModelBase
    {
        private long _盈亏单id;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 盈亏单id
        {
            get { return _盈亏单id; }
            set { Set<long>(() => this.盈亏单id, ref _盈亏单id, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 盈亏单 盈亏单 { get; set; }
        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _饰品ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        private 饰品 _饰品;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品
        {
            get { return _饰品; }
            set { Set<饰品>(() => this.饰品, ref _饰品, value, () => { this.饰品编号 = 饰品?.编号 ?? ""; }); }
        }
        private decimal _盈亏金额;
        public decimal 盈亏金额
        {
            get { return _盈亏金额; }
            set { Set<decimal>(() => this.盈亏金额, ref _盈亏金额, value); }
        }
        private Int32 _盈亏数量;
        public Int32 盈亏数量
        {
            get { return _盈亏数量; }
            set { Set<Int32>(() => this.盈亏数量, ref _盈亏数量, value); }
        }
        private decimal _盈亏重量;
        public decimal 盈亏重量
        {
            get { return _盈亏重量; }
            set { Set<decimal>(() => this.盈亏重量, ref _盈亏重量, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }


        #region 非映射业务字段 主要起导航作用



        private string _饰品编号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB
        [NotMapped]
        public string 饰品编号
        {
            get { return _饰品编号; }
            set { Set<string>(() => this.饰品编号, ref _饰品编号, value); }
        }
        #endregion
    }
    public class 调拨单 : ModelBase
    {
        public 调拨单()
        {
            调拨单明细s = new VHObjectList<调拨单明细>();
        }
        private string _编号;
        public string 编号
        {
            get { return _编号; }
            set { Set<string>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _源分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 源分店ID
        {
            get { return _源分店ID; }
            set { Set<long>(() => this.源分店ID, ref _源分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 源分店 { get; set; }
        private long _目标分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 目标分店ID
        {
            get { return _目标分店ID; }
            set { Set<long>(() => this.目标分店ID, ref _目标分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 目标分店 { get; set; }
        private long? _调拨员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 调拨员ID
        {
            get { return _调拨员ID; }
            set { Set<long?>(() => this.调拨员ID, ref _调拨员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 调拨员 { get; set; }

        private long? _签收员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 签收员ID
        {
            get { return _签收员ID; }
            set { Set<long?>(() => this.签收员ID, ref _签收员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 签收员 { get; set; }
        private String _签收日期;
        public String 签收日期
        {
            get { return _签收日期; }
            set { Set<String>(() => this.签收日期, ref _签收日期, value); }
        }
        private String _签收状态;
        public String 签收状态
        {
            get { return _签收状态; }
            set { Set<String>(() => this.签收状态, ref _签收状态, value); }
        }
        private int _数量;
        public int 数量
        {
            get => _数量;
            set => Set<int>(() => this.数量, ref _数量, value);
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }


        private ICollection<调拨单明细> _调拨单明细s;
        public virtual ICollection<调拨单明细> 调拨单明细s
        {
            get { return _调拨单明细s; }
            set { Set<ICollection<调拨单明细>>(() => this.调拨单明细s, ref _调拨单明细s, value); }
        }



    }
    public class 调拨单明细 : ModelBase
    {
        private long _调拨单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 调拨单ID
        {
            get { return _调拨单ID; }
            set { Set<long>(() => this.调拨单ID, ref _调拨单ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 调拨单 调拨单 { get; set; }

        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _饰品ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        private 饰品 _饰品;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品
        {
            get { return _饰品; }
            set { Set<饰品>(() => this.饰品, ref _饰品, value, () => { this.饰品编号 = 饰品?.编号 ?? ""; }); }
        }

        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

        #region 非映射业务字段 主要起导航作用



        private string _饰品编号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB
        [NotMapped]
        public string 饰品编号
        {
            get { return _饰品编号; }
            set { Set<string>(() => this.饰品编号, ref _饰品编号, value); }
        }
        #endregion
    }
    public class 付款单 : ModelBase
    {
        public 付款单()
        {
            付款单明细s = new VHObjectList<付款单明细>();
        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long? _会员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long? 会员ID
        {
            get { return _会员ID; }
            set { Set<long?>(() => this.会员ID, ref _会员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 会员 会员 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }
        private long _供应商ID;
        [Display(Name = "外键", AutoGenerateField = false)]
        [RangeAttribute(1, int.MaxValue,ErrorMessage ="请输入供应商")]
        public long 供应商ID
        {
            get { return _供应商ID; }
            set { Set<long>(() => this.供应商ID, ref _供应商ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 供应商 供应商 { get; set; }
        private decimal _应付金额;
        public decimal 应付金额
        {
            get { return _应付金额; }
            set { Set<decimal>(() => this.应付金额, ref _应付金额, value); }
        }
        private decimal _实付金额;
        public decimal 实付金额
        {
            get { return _实付金额; }
            set { Set<decimal>(() => this.实付金额, ref _实付金额, value); }
        }

        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private ICollection<付款单明细> _付款单明细s;
        public virtual ICollection<付款单明细> 付款单明细s
        {
            get { return _付款单明细s; }
            set { Set<ICollection<付款单明细>>(() => this.付款单明细s, ref _付款单明细s, value); }
        }
    }
    public class 付款单明细 : ModelBase
    {
        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _付款单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 付款单ID
        {
            get { return _付款单ID; }
            set { Set<long>(() => this.付款单ID, ref _付款单ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 付款单 付款单 { get; set; }
        private long? _入库单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long? 入库单ID
        {
            get { return _入库单ID; }
            set { Set<long?>(() => this.入库单ID, ref _入库单ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 入库单 入库单 { get; set; }
        private long? _退库单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long? 退库单ID
        {
            get { return _退库单ID; }
            set { Set<long?>(() => this.退库单ID, ref _退库单ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 退库单 退库单 { get; set; }
        private decimal _应付金额;
        public decimal 应付金额
        {
            get { return _应付金额; }
            set { Set<decimal>(() => this.应付金额, ref _应付金额, value); }
        }
        private decimal _本次支付金额;
        public decimal 本次支付金额
        {
            get { return _本次支付金额; }
            set { Set<decimal>(() => this.本次支付金额, ref _本次支付金额, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }

        #region (非)映射业务字段 主要起导航作用



        private string _入库单号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB

        public string 入库单号
        {
            get { return _入库单号; }
            set { Set<string>(() => this.入库单号, ref _入库单号, value); }
        }

        private string _退库单号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB

        public string 退库单号
        {
            get { return _退库单号; }
            set { Set<string>(() => this.退库单号, ref _退库单号, value); }
        }
        #endregion

    }
    public class 库存出入明细 : ModelBase
    {
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private String _相关单据;
        public String 相关单据
        {
            get { return _相关单据; }
            set { Set<String>(() => this.相关单据, ref _相关单据, value); }
        }
        private long _单据ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 单据ID
        {
            get { return _单据ID; }
            set { Set<long>(() => this.单据ID, ref _单据ID, value); }
        }
        private String _单据编号;
        public String 单据编号
        {
            get { return _单据编号; }
            set { Set<String>(() => this.单据编号, ref _单据编号, value); }
        }
        private String _出入别;
        public String 出入别
        {
            get { return _出入别; }
            set { Set<String>(() => this.出入别, ref _出入别, value); }
        }
        private Int32 _数量;
        public Int32 数量
        {
            get { return _数量; }
            set { Set<Int32>(() => this.数量, ref _数量, value); }
        }
        private decimal _重量;
        public decimal 重量
        {
            get { return _重量; }
            set { Set<decimal>(() => this.重量, ref _重量, value); }
        }
        private decimal _金额;
        public decimal 金额
        {
            get { return _金额; }
            set { Set<decimal>(() => this.金额, ref _金额, value); }
        }
        private decimal _加权金额;
        public decimal 加权金额
        {
            get { return _加权金额; }
            set { Set<decimal>(() => this.加权金额, ref _加权金额, value); }
        }
        private long _分店ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long _饰品ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品 { get; set; }
    }
    public class 盘点表 : ModelBase
    {
        public 盘点表()
        {
            盘点表明细s = new VHObjectList<盘点表明细>();
        }
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private DateTime _日期;
        public DateTime 日期
        {
            get { return _日期; }
            set { Set<DateTime>(() => this.日期, ref _日期, value); }
        }
        private long _分店ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 分店ID
        {
            get { return _分店ID; }
            set { Set<long>(() => this.分店ID, ref _分店ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual 分店 分店 { get; set; }
        private long _操作员ID;
         [Display(Name = "外键", AutoGenerateField = false)]
        public long 操作员ID
        {
            get { return _操作员ID; }
            set { Set<long>(() => this.操作员ID, ref _操作员ID, value); }
        }
         [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User 操作员 { get; set; }


        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        private ICollection<盘点表明细> _盘点表明细s;
        public virtual ICollection<盘点表明细> 盘点表明细s
        {
            get { return _盘点表明细s; }
            set { Set<ICollection<盘点表明细>>(() => this.盘点表明细s, ref _盘点表明细s, value); }
        }

        private long? _盈亏单ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long? 盈亏单ID
        {
            get => _盈亏单ID;
            set => Set<long?>(() => this.盈亏单ID, ref _盈亏单ID, value);
        }
        private 盈亏单 _盈亏单;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 盈亏单 盈亏单
        {
            get => _盈亏单;
            set => Set<盈亏单>(() => this.盈亏单, ref _盈亏单, value);
        }

    }
    public class 盘点表明细 : ModelBase
    {
        private long _盘点表ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 盘点表ID
        {
            get { return _盘点表ID; }
            set { Set<long>(() => this.盘点表ID, ref _盘点表ID, value); }
        }
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 盘点表 盘点表 { get; set; }
        private Int32 _序号;
        public Int32 序号
        {
            get { return _序号; }
            set { Set<Int32>(() => this.序号, ref _序号, value); }
        }
        private long _饰品ID;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public long 饰品ID
        {
            get { return _饰品ID; }
            set { Set<long>(() => this.饰品ID, ref _饰品ID, value); }
        }
        private 饰品 _饰品;
        [Display(Name = "外键ID", AutoGenerateField = false)]
        public virtual 饰品 饰品
        {
            get { return _饰品; }
            set { Set<饰品>(() => this.饰品, ref _饰品, value, () => { this.饰品编号 = 饰品?.编号 ?? ""; }); }
        }
        private Int32 _盘点前数量;
        public Int32 盘点前数量
        {
            get { return _盘点前数量; }
            set { Set<Int32>(() => this.盘点前数量, ref _盘点前数量, value); }
        }
        private decimal _盘点前重量;
        public decimal 盘点前重量
        {
            get { return _盘点前重量; }
            set { Set<decimal>(() => this.盘点前重量, ref _盘点前重量, value); }
        }
        private decimal _盘点前金额;
        public decimal 盘点前金额
        {
            get { return _盘点前金额; }
            set { Set<decimal>(() => this.盘点前金额, ref _盘点前金额, value); }
        }
        private decimal _实盘金额;
        public decimal 实盘金额
        {
            get { return _实盘金额; }
            set { Set<decimal>(() => this.实盘金额, ref _实盘金额, value); }
        }
        private Int32 _实盘数量;
        public Int32 实盘数量
        {
            get { return _实盘数量; }
            set { Set<Int32>(() => this.实盘数量, ref _实盘数量, value); }
        }
        private decimal _实盘重量;
        public decimal 实盘重量
        {
            get { return _实盘重量; }
            set { Set<decimal>(() => this.实盘重量, ref _实盘重量, value); }
        }
        private String _备注;
        public String 备注
        {
            get { return _备注; }
            set { Set<String>(() => this.备注, ref _备注, value); }
        }
        #region 非映射业务字段 主要起导航作用



        private string _饰品编号;//20170302此字段与饰品.编号同步，是为了防止用户更改饰品.编号后保存到了DB
        [NotMapped]
        public string 饰品编号
        {
            get { return _饰品编号; }
            set { Set<string>(() => this.饰品编号, ref _饰品编号, value); }
        }
        #endregion
    }



}
