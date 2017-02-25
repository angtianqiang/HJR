using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data
{
  public static  class Helper
    {
      public static string GetUniqueId()
      {
          return DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + (Guid.NewGuid().ToString("N"));
      }
    }
}
