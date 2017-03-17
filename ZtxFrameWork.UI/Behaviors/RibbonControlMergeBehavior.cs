using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Ribbon;
using System.Windows;

namespace ZtxFrameWork.UI.Behaviors
{
    public class RibbonControlMergeBehavior : Behavior<RibbonControl>
    {



        public object ChildRibbonControl
        {
            get { return (object)GetValue(ChildRibbonControlProperty); }
            set { SetValue(ChildRibbonControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChildRibbonControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildRibbonControlProperty =
            DependencyProperty.Register("ChildRibbonControl", typeof(object), typeof(RibbonControlMergeBehavior), new PropertyMetadata(null, PropertyChanged));


        static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            //if (e.NewValue != null && e.NewValue is RibbonControl)
            //{
            //    RibbonControl Main = ((RibbonControlMergeBehavior)d).AssociatedObject;   
            //    Main.Merge(e.NewValue as RibbonControl);
            //}
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (ChildRibbonControl != null && ChildRibbonControl is RibbonControl)
            {
                this.AssociatedObject.Merge(ChildRibbonControl as RibbonControl);
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }

    }
}
