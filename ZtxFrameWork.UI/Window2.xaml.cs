using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.UI
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        User user = null;
        public Window2()
        {
            InitializeComponent();
            user = new ZtxDB().Users.First();

           // user = new User();
            this.DataContext = user;
            user.UserName = "DFDF";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            user.UserName = "CHANGE";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string[] path = System.IO.Directory.GetFiles(@"C:\Users\angti\Desktop\www\新建文件夹");
            foreach (var item in path)
            {
                System.IO.File.Move(item, item.Replace("Bluegrid.Jewel.Module.EmbeddedReports.zh_CN.", ""));
                System.IO.File.Delete(item);
            }
        }
    }
}
