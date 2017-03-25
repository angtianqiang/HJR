using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ZtxFrameWork.Data.Model
{
    [Table("sys_Modules")]
    [DisplayName("系统模块")]
    public class Module : ModelBase
    {
        [Required]
        [Display(Name = "模块名称")]
        public string ModuleTitle { get; set; }
      



        private String _documentType;
        [Display(Name = "模块页面")]

        public String DocumentType
        {
            get { return _documentType; }
            set { Set<String>(() => this.DocumentType, ref _documentType, value); }
        }


        [Display(Name = "模块图标")]
        public string ImageName { get; set; }
        [Display(Name = "模块类别")]
        [Required]
        public ModuleInfo ModuleInfo { get; set; }
        [Display(Name = "上级模块")]
        public long? ParentID { get; set; }
        [Display(Name = "排序号")]
        public virtual string SortNo { get; set; }
        public virtual Module Parent { get; set; }
        [ForeignKey("ParentID")]
        public virtual ICollection<Module> ChildModules { get; set; }
        public override string ToString()
        {
            return ModuleTitle;
        }

    }
}
