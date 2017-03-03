using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.UI.Converters
{
    public class MoudleInfoToImageConerter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var A =(ModuleInfo) Enum.Parse(typeof(ModuleInfo), value.ToString());
            if (A == ModuleInfo.ModuleGroup)
            {
                return DevExpress.Utils.AssemblyHelper.GetResourceUri(typeof(MoudleInfoToImageConerter).Assembly, string.Format("Images/moudleGroup.png"));
                //  return "../Images/Status/a.png";
                return "";
            }
            else if (A == ModuleInfo.MoudleSubGroup)
            {
                return DevExpress.Utils.AssemblyHelper.GetResourceUri(typeof(MoudleInfoToImageConerter).Assembly, string.Format("Images/moudleGroup.png"));

                //  return "../Images/Status/b.png";
            }
            else if (A == ModuleInfo.MoudleAction)
            {
                return DevExpress.Utils.AssemblyHelper.GetResourceUri(typeof(MoudleInfoToImageConerter).Assembly, string.Format("Images/moudle.png"));

                //  return "../Images/Status/c.png";
            }
            else
            {
                return "";// "..Images/Status/d.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
