using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ZtxFrameWork.UI.Utils
{
    public class TableViewButtonEditEnterSupportHelper
    {
        public static readonly DependencyProperty IsRegisterProperty = DependencyProperty.RegisterAttached("IsRegister", typeof(bool), typeof(ButtonEdit), new FrameworkPropertyMetadata(IsRegisterPropertyChanged));
        public static void SetIsRegister(TableView element, bool value)
        {
            element.SetValue(IsRegisterProperty, value);
        }
        public static bool GetIsRegister(TableView element)
        {
            return (bool)element.GetValue(IsRegisterProperty);
        }

        private static void IsRegisterPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            TableView editor = source as TableView;
            if (Convert.ToBoolean(e.NewValue) == true)
            {
                editor.PreviewKeyDown += editor_PreviewKeyDown;
            }

        }

        static void editor_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
          TableView DetailView = sender as TableView;
            if (DetailView.ActiveEditor == null)
            {
                return;
            }

            if (e.Key == Key.Enter && DetailView.ActiveEditor.IsEditorActive)
            {

                if (DetailView.ActiveEditor is ButtonEdit)
                {
                    ButtonEdit F = DetailView.ActiveEditor as ButtonEdit;
                    var d = F.Buttons.Count;


                    //  F.Template.FindName("PART_Item",
                    //ButtonInfo bi = F.Buttons[0] as ButtonInfo;
                    //bi.Command.Execute(DetailView.ActiveEditor.EditValue);




                    //F.RaiseEvent(new RoutedEventArgs(ButtonEdit.DefaultButtonClickEvent,F));

                    //bb.RaiseEvent(new RoutedEventArgs(ButtonEdit.DefaultButtonClickEvent,bb));

                    var method = F.GetType().GetMethod("OnDefaultButtonClick",
             BindingFlags.NonPublic | BindingFlags.Instance);

                    if (method != null)
                    {
                        method.Invoke(F, new object[] { F, new RoutedEventArgs(ButtonEdit.DefaultButtonClickEvent, F) });
                    }

                }
            }
        }
    }
}