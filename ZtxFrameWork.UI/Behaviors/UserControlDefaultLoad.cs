using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZtxFrameWork.UI.Behaviors
{
  public  class UserControlDefaultLoad : Behavior <UserControl>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }


        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }
        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ////string vmName = $"ZtxFrameWork.UI.ViewModels.{((UserControl)sender).GetType().Name.Replace("View","")}ViewModel";
            ////    ((UserControl)sender).DataContext = DevExpress.Mvvm.Native.ViewModelSourceHelper.Create(Type.GetType(vmName));

            //System.Windows.Input.Mouse.OverrideCursor = null;

            //string Token = ((dynamic)AssociatedObject.DataContext).Token;

            //AssociatedObject.Tag = Token;
            if (((UserControl)sender).DataContext !=null)
            {
                System.Windows.Input.Mouse.OverrideCursor = null;
                string Token1 = ((dynamic)AssociatedObject.DataContext).Token;
                AssociatedObject.Tag = Token1;
                return;
            }
           
            System.Threading.Tasks.Task<object> task1 = System.Threading.Tasks.Task.Factory.StartNew(delegate {

             string vmName = $"ZtxFrameWork.UI.ViewModels.{((UserControl)sender).GetType().Name.Replace("View","")}ViewModel";
                System.Diagnostics.Debug.WriteLine($"task1(): { System.Threading.Thread.CurrentThread.ManagedThreadId}");              
                return  DevExpress.Mvvm.Native.ViewModelSourceHelper.Create(Type.GetType(vmName));
            });
            task1.ContinueWith(delegate {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"t  task1.ContinueWith(): { System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    ((UserControl)sender).DataContext = task1.Result;
                    System.Windows.Input.Mouse.OverrideCursor = null;

                    string Token1 = ((dynamic)AssociatedObject.DataContext).Token;

                    AssociatedObject.Tag = Token1;
                }));

            });
        

        }

    }
}
