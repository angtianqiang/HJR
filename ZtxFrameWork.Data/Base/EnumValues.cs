using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data
{
    public enum DirtyState : short
    {
        UnChanged=0,
       Added=1,
        Deleted=2,
        Modified=3
    }


    public enum EnableState :int  // short  枚举必须是INT类型，不然PetaPoco转换时会报错
    {
        可用=0,
         停用=1
    }
}
