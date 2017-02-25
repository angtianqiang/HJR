using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Helpers;
using ZtxFrameWork.UI.Views;

namespace ZtxFrameWork.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser = null;
        protected override void OnStartup(StartupEventArgs e)
        {
     //  new Window2().ShowDialog();
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            SplashScreenHelper.Instance.ShowSplashScreen();
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;


      ZtxFrameWork.Data.ZtxDB.Init(new Data.ZtxDB());




            LoginWindow loginWindow = new LoginWindow();


            bool? temp = loginWindow.ShowDialog();

            try
            {

                if (temp.HasValue && temp.Value == true)
                {
                    SplashScreenHelper.Instance.ShowSplashScreen();
                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    this.MainWindow.Show();//  mainWindow.Show();
                }
                else
                {
                    App.Current.Shutdown();
                }

            }
            catch (Exception ex)
            {
                //    MyLog.error("未知错误", ex);
                MessageBox.Show(ex.Message + "  " + ex.StackTrace + "  " + ex.InnerException.Message);
            }


        }
        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }
        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = e.Exception.Message;
            Exception originalException = e.Exception;
            while (originalException.InnerException != null)
            {
                originalException = originalException.InnerException;
                errorMessage += System.Environment.NewLine + originalException.Message;
            }
            MessageBox.Show(errorMessage);// + System.Environment.NewLine + e.Exception.StackTrace);
          
          //  App.Current.Shutdown();
        }

    

     
    }
}
