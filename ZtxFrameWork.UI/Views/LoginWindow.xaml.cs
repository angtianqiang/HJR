using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZtxFrameWork.UI.Helpers;
using ZtxFrameWork.UI.ViewModels;

namespace ZtxFrameWork.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        public LoginWindow()
        {
            InitializeComponent();


            this.Token = this.GetHashCode().ToString();
            ((LoginWindowViewModel)this.DataContext).Token = this.Token;



            this.Loaded += (s, e) =>
            {


                Messenger.Default.Register<string>(this, "Cancel" + Token, m =>
                {
                    this.DialogResult = false;
                });
                Messenger.Default.Register<string>(this, "Confirm" + Token, m =>
                {
                    this.DialogResult = true;
                    Mouse.OverrideCursor = Cursors.Wait;
                });
                Messenger.Default.Register<string>(this, "Error" + Token, m => {
                    DXMessageBox.Show(this, m, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                });

                this.txtUserName.Focus();

                WindowInteropHelper windHelper = new WindowInteropHelper(this);
                SplashScreenHelper.Instance.HideSplashScreen();
                //20140717  加入SplashScreen窗体后发现登录窗口显示后不能自动得到焦点，
                //因此用API得到
                SetForegroundWindow(windHelper.Handle);
            };
            this.Closed += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();

            }

        }
        public string Token { get; set; }


    }
}
