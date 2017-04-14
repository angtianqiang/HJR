using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data.Model
{
    [Table("sys_DbOperatorLogs")]
    [DisplayName("数据存储操作日志")]
    public class DbOperatorLog
    {
        [Display(Name = "ID", AutoGenerateField = false)]
        public long ID { get; set; }
        [DisplayName("操作对象")]
        public string EntityName { get; set; }
        [DisplayName("对象编号")]
        public long EntityID { get; set; }
        [DisplayName("关键字")]
        public string KeyValue { get; set; }
        [DisplayName("操作类别")]
        public string DirtyState { get; set; }
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss", ApplyFormatInEditMode = true, NullDisplayText = "")]
        [DisplayName("操作时间")]
        public DateTime OperatorOn { get; set; }
        [DisplayName("操作人员")]
        public string OperatorBy { get; set; }
        [DisplayName("建立人")]
        public string CreateBy { get; set; }
        [DisplayName("建立时间")]
        public DateTime CreateOn { get; set; }
        [DisplayName("最后更改人")]
        public string ModifiedBy { get; set; }
        [DisplayName("最后更改时间")]
        public DateTime ModifiedOn { get; set; }
        //[DisplayName("标识")]
        //public string UniqueId { get; set; }
    }
    [Table("sys_BillStateChangeLogs")]
    [DisplayName("单据状态更改日志")]
    public class BillStateChangeLog
    {
        [Display(Name = "ID", AutoGenerateField = false)]
        public long ID { get; set; }
        [DisplayName("单据名")]
        public string BillName { get; set; }
        [DisplayName("单据ID")]
        public long BillID { get; set; }
        [DisplayName("单据编号")]
        public string BillNum { get; set; }
        [DisplayName("更改前状态")]
        public string SourceState { get; set; }
        [DisplayName("更改后状态")]
        public string TargetState { get; set; }
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss", ApplyFormatInEditMode = true, NullDisplayText = "")]
        [DisplayName("更改时间")]
        public DateTime ChangeOn { get; set; }
        [DisplayName("更改人员")]
        public string ChangeBy { get; set; }
        //[DisplayName("标识")]
        //public string UniqueId { get; set; }
    }

}
