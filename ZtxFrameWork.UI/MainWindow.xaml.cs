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
using DevExpress.Xpf.Core;
using ZtxFrameWork.UI.Helpers;

namespace ZtxFrameWork.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DevExpress.Xpf.Ribbon.DXRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s,e) => {
                SplashScreenHelper.Instance.HideSplashScreen();
                Mouse.OverrideCursor = null;

            };
            this.Closed += (s, e) => { App.Current.Shutdown(); };
        }
    }
}
