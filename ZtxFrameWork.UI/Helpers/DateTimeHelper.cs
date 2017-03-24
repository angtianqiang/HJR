using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.UI.Helpers
{
  public  class DateTimeHelper
    {
        public static DateTime GetCurrentMonthFistDay()
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, 1);
        }
        public static DateTime GetCurrentMonthCurrentDay()
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }
    }
}
