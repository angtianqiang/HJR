using DevExpress.Xpf.Core;
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

namespace ZtxFrameWork.UI.Helpers
{
    public partial class SplashWindow : Window, ISplashScreen
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        public void CloseSplashScreen()
        {
            this.Close();
        }

        public void Progress(double value)
        {

        }

        public void SetProgressState(bool isIndeterminate)
        {

        }
    }
}
