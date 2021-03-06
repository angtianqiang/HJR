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

namespace ZtxFrameWork.UI.Views
{
    /// <summary>
    /// Interaction logic for 饰品View.xaml
    /// </summary>
    public partial class 饰品View : UserControl
    {
        public 饰品View()
        {
            InitializeComponent();
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (imageEdit.EditValue == null)
            //{
            //    return;
            //}
            //byte[] temp = imageEdit.EditValue as byte[];

            ////     BitmapSource image=
            //var window = new Window() { Width = 600, Height = 500, WindowStyle = WindowStyle.SingleBorderWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner, ShowActivated = true, Title = "浏览图片" };
            //var ImageViewer = new Controls.ImageViewer() { Margin = new Thickness(0) };
            //ImageViewer.ImageSource = ByteArrayToBitmapImage(temp);
            //window.Content = ImageViewer;
            //window.ShowDialog();


            if (imageEdit.Source == null)
            {
                return;
            }
        

            //     BitmapSource image=
            var window = new Window() { Width = 600, Height = 500, WindowStyle = WindowStyle.SingleBorderWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner, ShowActivated = true, Title = "浏览图片" };
            var ImageViewer = new Controls.ImageViewer() { Margin = new Thickness(0) };
            ImageViewer.ImageSource = imageEdit.Source as BitmapSource;
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
