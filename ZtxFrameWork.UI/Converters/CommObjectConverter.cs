using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ZtxFrameWork.UI.Converters
{
    public class CommObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string[] strs = parameter.ToString().ToLower().Split(':');
            var temp = strs[0] == value.ToString().ToLower() ? strs[1] : strs[2];
            return temp;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
