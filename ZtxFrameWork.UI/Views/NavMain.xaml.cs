using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
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
            //this.dockManager.DockItemActivated += DockManager_DockItemActivated;
            //this.dockManager.DockItemClosed += DockManager_DockItemClosed;
        }

        //private void DockManager_DockItemClosed(object sender, DevExpress.Xpf.Docking.Base.DockItemClosedEventArgs e)
        //{
        //  var doc = this.documnetGroup.Items.OrderByDescending(t => t.TabIndex).FirstOrDefault();
        //    if (doc == null) return;
        //    this.dockManager.Activate(doc);
        //}

        //DocumentPanel lastActiveDoc = null;
        //private void DockManager_DockItemActivated(object sender, DevExpress.Xpf.Docking.Base.DockItemActivatedEventArgs ea)
        //{
        //    var documentPanel = ea.Item as DocumentPanel;
        //    if (documentPanel == null) return;
        //    documentPanel.TabIndex = this.documnetGroup.Items.Max(t => t.TabIndex) + 1;
        //}

        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount >= 1)
            {
                //  MessageBox.Show(   ((StackPanel)sender).DataContext.ToString());

                Messenger.Default.Send(((StackPanel)sender).DataContext as ZtxFrameWork.Data.Model.Module);
            }
        }

        

     
    }
}
