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
    /// BBBBBBBBBBBBBB
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser = null;
        protected override void OnStartup(StartupEventArgs e)
        {
    //   new DXWindow1().ShowDialog();

            //new Window2().ShowDialog();
            //        return;



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
                    ThemeManager.ApplicationThemeChanged += ThemeManager_ApplicationThemeChanged;
                      
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

        private void ThemeManager_ApplicationThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            try
            {
                var NewName = DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName;
                NewName = DevExpress.Xpf.Core.Theme.Themes.Where(t => t.Name == NewName).FirstOrDefault().FullName;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = NewName;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
            //220170304更新界面皮肤后要设置以下代码，作用于Report.ShowPreview，DEV内部用的是winform,且皮肤的名字不一样
            //wpf:office2010blue winform:office 2010 bule
          //  var NewName = DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName;
          //  NewName= DevExpress.Xpf.Core.Theme.Themes.Where(t => t.Name == NewName).FirstOrDefault().FullName;


            //switch (NewName)
            //{
            //    case "Office2007Black":
            //        NewName = "Office 2007 Black";
            //        break;
            //    case "Office2007Blue":
            //        NewName = "Office 2007 Blue";
            //        break;
            //    case "Office2007Silver":
            //        NewName = "Office 2007 Silver";
            //        break;
            //    case "Office2010Black":
            //        NewName = "Office 2010 Black";
            //        break;
            //    case "Office2010Blue":
            //        NewName = "Office 2010 Blue";
            //        break;
            //    case "Office2010Silver":
            //        NewName = "Office 2010 Silver";
            //        break;
            //    case "MetropolisLight":
            //        NewName = "Metropolis Light";
            //        break;
            //    case "MetropolisDark":
            //        NewName = "Metropolis Dark";
            //        break;
            //    case "LightGray":
            //        NewName = "Light Gray";
            //        break;               

            //    default:
            //        break;
            //}
            //   DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(NewName);
         //   DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = NewName;

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

         //   Environment.Exit(0);
        }

    

     
    }
}
