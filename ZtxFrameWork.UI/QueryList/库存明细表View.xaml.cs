﻿using System;
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

namespace ZtxFrameWork.UI.QueryList
{
    /// <summary>
    /// Interaction logic for 库存明细表View.xaml
    /// </summary>
    public partial class 库存明细表View : UserControl
    {
        public 库存明细表View()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //byte[] temp = this.grid.GetFocusedRowCellValue("饰品.饰品图片.图片") as byte[];
            //     var a = this.grid.GetFocusedRowCellValue("饰品.饰品图片.图片");
            //if (temp == null)
            //{
            //    return;
            //}
            //    byte[] temp = imageEdit.EditValue as byte[];

            //     BitmapSource image=

            var AFA = this.grid.GetFocusedRow();
            if (AFA == null) return;
           
                string ID = ((dynamic)AFA).编号;
          
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
