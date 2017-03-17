using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZtxFrameWork.UI.Behaviors
{
    /// <summary>
    /// 20170308为通用的查义框注册相关事件
    /// </summary>
    public class EntityExpressionUserControlDefaultLoad : Behavior<UserControl>
    {
        UserControl AssociatedTextBox { get { return AssociatedObject; } }

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

        
            string Token = ((dynamic)AssociatedObject.DataContext).Token;

            AssociatedObject.Tag = Token;


            DXDialogWindow win =
                Utils.TreeHelper.TryFindParent<DXDialogWindow>(AssociatedObject);
            if (win != null)
            {



                Messenger.Default.Register<string>(win, "Cancel" + Token, m =>
                {

                    win.DialogResult = false;
                });
                Messenger.Default.Register<string>(win, "Confirm" + Token, m =>
                {
                    win.DialogResult = true;
                });
                Messenger.Default.Register<string>(win, "Clear" + Token, m =>
                {
                    var currentFilterControl = win.FindName("FilterControl");
                    if (currentFilterControl != null)
                    {
                        ((DevExpress.Xpf.Editors.Filtering.FilterControl)currentFilterControl).RootNode.SubNodes.Clear();

                    }
                });
                //};

                win.Activated += delegate
                {
                    Mouse.OverrideCursor = null;
                };
                win.Closed += delegate
                {
                    Messenger.Default.Unregister(win);

                };
            }
        }

    }
}
