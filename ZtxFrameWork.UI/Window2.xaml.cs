using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.ViewModel;

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
            
            user =  DbFactory.Instance.CreateDbContext().Users.First();

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

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var aa = DbFactory.Instance.CreateDbContext().入库单s.Include("分店").ToList();
            this.db.ItemsSource = aa;
        }

        private void ButtonEditSettings_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModelSource.Create(() => new WindowViewModel());
        }

        private void ButtonEditSettings_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Sho");
        }

        private void view_PreviewKeyDown(object sender, KeyEventArgs e)
        {
          
         
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
    [POCOViewModel]
    public class WindowViewModel
    {
        public void Show(object str)
        {
            MessageBox.Show("Show" + SelectEntity.分店.名称 + "   " + str?.ToString()?? "");
        }

        public virtual List<入库单> list { get; set; } = DbFactory.Instance.CreateDbContext().入库单s.Include("分店").ToList();
        public virtual 入库单 SelectEntity { get; set; }

    }

     

}


