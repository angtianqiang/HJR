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
using ZtxFrameWork.UI.Comm.DataModel;

namespace ZtxFrameWork.UI
{
    /// <summary>
    /// BBBBBBBBBBBBBB
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser = null;
        public static List<SystemConfiguration> SystemConfigs = null;
        static IDisposable singleInstanceApplicationGuard;
        protected override void OnStartup(StartupEventArgs e)
        {
            Start(() => base.OnStartup(e), this);
        }
      
        public  void Start(Action baseStart, Application application)
        {

            Helpers.ExceptionHelper.Initialize();

         //var  db=   DbFactory.Instance.CreateDbContext();
         //   db.付款单s.Add(new 付款单() { 编号 = "000001", 日期 = DateTime.Now, 供应商ID = 1 , 会员ID=1, 操作员ID=1, 分店ID=1, 状态="N"});
         //   db.SaveChanges();


            DataDirectoryHelper.LocalPrefix = "ZtxFrameWork.UI";
            bool exit;
            singleInstanceApplicationGuard = DataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", out exit);
            if (exit)
            {
                application.Shutdown();
                return;
            }
            // this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            //加载样式文件

          //  InitSystemSet();


            SplashScreenHelper.Instance.ShowSplashScreen();
          //  base.OnStartup(e);
            application.ShutdownMode = ShutdownMode.OnExplicitShutdown;


        //    ZtxFrameWork.Data.ZtxDB.Init(DbFactory.Instance.CreateDbContext());




            LoginWindow loginWindow = new LoginWindow();


            bool? temp = loginWindow.ShowDialog();


                if (temp.HasValue && temp.Value == true)
                {
                    SplashScreenHelper.Instance.ShowSplashScreen();
             
                    ThemeManager.ApplicationThemeChanged += ThemeManager_ApplicationThemeChanged;
                    App.SystemConfigs = DbFactory.Instance.CreateDbContext().SystemConfigurations.ToList();
                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    this.MainWindow.Show();//  mainWindow.Show();
                }
                else
                {
                    App.Current.Shutdown();
                }



        }
        public void InitSystemSet()
        {
            var stylePath = System.Environment.CurrentDirectory + @"\Styles\SystemSet.xaml";
            if (!System.IO.File.Exists(stylePath))
            {
                throw new Exception("未找到相关的XAML配置文件!");
            }
           
            var resourceDictionary = new ResourceDictionary() { Source = new Uri(stylePath, UriKind.RelativeOrAbsolute) };
            //奖资源放到最前面 顺序不对会出现程序报错

            var ResourceDictionaryCollection = this.Resources.MergedDictionaries;
                if (ResourceDictionaryCollection != null)
            {
                foreach (var item in ResourceDictionaryCollection)
                {
                    resourceDictionary.MergedDictionaries.Add(item);
                }
            }

            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(resourceDictionary);


        }
        private void ThemeManager_ApplicationThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            try
            { 
                var NewName = DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName;
                NewName = DevExpress.Xpf.Core.Theme.Themes.Where(t => t.Name == NewName).FirstOrDefault().FullName;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = NewName;
              DevExpress.Xpf.Core.ApplicationThemeHelper.SaveApplicationThemeName();
             
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
        //void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        //{
        //    string errorMessage = e.Exception.Message;
        //    Exception originalException = e.Exception;
        //    while (originalException.InnerException != null)
        //    {
        //        originalException = originalException.InnerException;
        //        errorMessage += System.Environment.NewLine + originalException.Message;
        //    }
        //    MessageBox.Show(errorMessage);// + System.Environment.NewLine + e.Exception.StackTrace);

        //    //   Environment.Exit(0);
        //}

   public  static      string GetAllError (Exception e)
        {
            string errorMessage = e.Message;
            Exception originalException = e;
                while (originalException.InnerException != null)
            {
                originalException = originalException.InnerException;
                errorMessage += System.Environment.NewLine + originalException.Message;
            }
            return errorMessage;// + System.Environment.NewLine + e.Exception.StackTrace);

            //   Environment.Exit(0);
        }
    }
}
