using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.UI.Interactivity;

using System.Windows;
using DevExpress.Xpf.Grid;
using System.Windows.Controls;

namespace ZtxFrameWork.UI.Behaviors
{
  public  class GridControlLayoutLoad: Behavior<GridControl>
    {


        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(string), typeof(GridControlLayoutLoad), new PropertyMetadata(""));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

       

        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            GridControl gd = sender as GridControl;
            string key = Key;
            string path = $"{System.Environment.CurrentDirectory}\\UserLayout\\";
            string file = $"{key}.xaml";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            if (System.IO.File.Exists(path + file))
            {
                gd.RestoreLayoutFromXml(path + file);
            }




            MenuItem item1 = new MenuItem() { Header = "保存自定义布局" };
            item1.Click += delegate {
                gd.SaveLayoutToXml(path + file);
            };
            MenuItem item2 = new MenuItem() { Header = "清除自定义布局" };
            item2.Click += delegate {
                if (System.IO.File.Exists(path + file))
                {
                    System.IO.File.Delete(path + file);
                }

            };

            MenuItem item3 = new MenuItem() { Header = "显示列布局窗口" };
            item3.Click += delegate {
                gd.View.IsColumnChooserVisible = true;

            };
            MenuItem item4 = new MenuItem() { Header = "隐藏列布局窗口" };
            item4.Click += delegate {
                gd.View.IsColumnChooserVisible = false;

            };


            ContextMenu menu = new ContextMenu();
            menu.Items.Add(item1);
            menu.Items.Add(item2);
            menu.Items.Add(item3);
            menu.Items.Add(item4);

            gd.ContextMenu = menu;
        }
    }
}
