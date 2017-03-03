using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ZtxFrameWork.UI.Behaviors
{
  public  class UserControlDefaultLoad : Behavior <UserControl>
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
            System.Windows.Input.Mouse.OverrideCursor = null;
            string Token = ((dynamic)AssociatedObject.DataContext).Token;

            AssociatedObject.Tag = Token;
          //  ((dynamic)AssociatedObject.DataContext).Token = Token;
        }

    }
}
