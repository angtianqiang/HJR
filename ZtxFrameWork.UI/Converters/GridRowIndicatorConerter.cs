using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ZtxFrameWork.UI.Converters
{
    public class GridRowIndicatorConerter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int rowHandle = (int)value;
                return (rowHandle < 0) ? string.Empty : (rowHandle + 1).ToString();
            }
            catch (Exception rsx)
            {

                throw;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
