using DevExpress.Mvvm;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZtxFrameWork.UI.Views
{
    /// <summary>
    /// Interaction logic for NavMain.xaml
    /// </summary>
    public partial class NavMain : UserControl
    {
        public NavMain()
        {
            InitializeComponent();
        }
        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount > 1)
            {
                //  MessageBox.Show(   ((StackPanel)sender).DataContext.ToString());

                Messenger.Default.Send(((StackPanel)sender).DataContext as ZtxFrameWork.Data.Model.Module);
            }
        }
    }
}
