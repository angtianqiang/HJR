using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data.Model
{
    public enum ModuleInfo : short
    {
        [Display(Name = "模块组")]
        ModuleGroup,
        [Display(Name = "模块明细组")]
        MoudleSubGroup,
        [Display(Name = "模块")]
        MoudleAction
    }

    public enum 工费计法
    {
        [Display(Name = "按件", AutoGenerateField = true, Description = "",Order =1)]
        按件,
        [Display(Name = "按重", AutoGenerateField = true, Description = "", Order = 1)]
        按重
    }
}
