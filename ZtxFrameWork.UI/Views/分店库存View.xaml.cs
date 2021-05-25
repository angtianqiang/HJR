using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZtxFrameWork.UI.Views
{
    /// <summary>
    /// Interaction logic for 分店库存View.xaml
    /// </summary>
    public partial class 分店库存View : UserControl
    {
        public 分店库存View()
        {
            InitializeComponent();
           // this.Loaded +=delegate{ this.grid.ExpandAllGroups(); };
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            ZtxFrameWork.Data.Model.库存 AFA = this.grid.GetFocusedRow() as ZtxFrameWork.Data.Model.库存;
            if (AFA == null) return;

            string ID = AFA.饰品.编号;

            //       var temp = this.grid.GetFocusedRowCellValue("ID") as string;
            var window = new Window() { Width = 600, Height = 500, WindowStyle = WindowStyle.SingleBorderWindow, ShowActivated = true, Title = "浏览图片" };
            var ImageViewer = new Controls.ImageViewer() { Margin = new Thickness(0) };
            ImageViewer.ImageSource = new BitmapImage(new Uri($"{App.HttpPath}/{ID.ToString()}.png"));
            window.Content = ImageViewer;
            window.ShowDialog();

        }
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;
            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }
    }
}
