using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace ZtxFrameWork.UI.Behaviors
{
    public class CheckMultipleBehavior : Behavior<GridColumn>
    {
        public GridColumn CurrentIsCheckedColumn;
        public IList Collection;
        public TableView View;
        public GridControl Grid;
        public int TrueCount = 0;
        public bool InCodeCheck;
        public bool InGeneralCheck;

        public static readonly DependencyProperty IsCheckedColumnProperty = DependencyProperty.RegisterAttached("IsCheckedColumn", typeof(bool?), typeof(CheckMultipleBehavior), new PropertyMetadata(null));

        public static void SetIsCheckedColumn(UIElement element, bool? value)
        {
            element.SetValue(IsCheckedColumnProperty, value);
        }
        public static bool? GetIsCheckedColumn(UIElement element)
        {
            return (bool?)element.GetValue(IsCheckedColumnProperty);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            CurrentIsCheckedColumn = this.AssociatedObject as GridColumn;

            CurrentIsCheckedColumn.HeaderTemplate = CreateTemplate();

            CurrentIsCheckedColumn.Loaded += column_Loaded;
        }

        DataTemplate CreateTemplate()
        {
            string xamlTemplate = "<DataTemplate><dxe:CheckEdit Content = \"" + CurrentIsCheckedColumn.FieldName + "\" EditValue=\"{Binding Path=DataContext.(local:CheckMultipleBehavior.IsCheckedColumn), RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=dxg:GridColumnHeader}}\"/></DataTemplate>";
            var xaml = xamlTemplate;

            var context = new ParserContext();

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("dxe", "http://schemas.devexpress.com/winfx/2008/xaml/editors");
            context.XmlnsDictionary.Add("dxg", "http://schemas.devexpress.com/winfx/2008/xaml/grid");
            context.XmlnsDictionary.Add("local", "clr-namespace:ZtxFrameWork.UI.Behaviors;assembly=ZtxFrameWork.UI");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }

        void column_Loaded(object sender, RoutedEventArgs e)
        {
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(CheckMultipleBehavior.IsCheckedColumnProperty, typeof(CheckMultipleBehavior));

            dpd.AddValueChanged(CurrentIsCheckedColumn, IsCheckedColumn_Chenged);

            View = CurrentIsCheckedColumn.View as TableView;

            Grid = View.Grid as GridControl;

            Collection = Grid.ItemsSource as IList;

            foreach (Object item in Collection)
            {
                INotifyPropertyChanged iCurrentItem = item as INotifyPropertyChanged;

                if (iCurrentItem != null)
                {
                    iCurrentItem.PropertyChanged += iItem_PropertyChanged;
                }
            }

            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                if ((bool)Grid.GetCellValue(i, CurrentIsCheckedColumn) == true)
                {
                    TrueCount++;
                }
            }

            GeneralCheckBoxSetter();

            INotifyCollectionChanged iCollection = Collection as INotifyCollectionChanged;
            iCollection.CollectionChanged += iCollection_CollectionChanged;

            View.CellValueChanging += View_CellValueChanging;
        }

        void View_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            View.CommitEditing();
        }

        void IsCheckedColumn_Chenged(object sender, EventArgs e)
        {
            if (InGeneralCheck == false)
            {
                InCodeCheck = true;
                var value = CurrentIsCheckedColumn.GetValue(IsCheckedColumnProperty) as bool?;

                if (value == true)
                {
                    TrueCount = Grid.VisibleRowCount;
                    for (int i = 0; i < Grid.VisibleRowCount; i++)
                    {
                       
                        Grid.SetCellValue(i, CurrentIsCheckedColumn, true);
                    }
                }
                else if (value == false)
                {
                    TrueCount = 0;
                    for (int i = 0; i < Grid.VisibleRowCount; i++)
                    {
                        Grid.SetCellValue(i, CurrentIsCheckedColumn, false);
                    }
                }
                InCodeCheck = false;
            }
        }

        void iCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var propertyName = CurrentIsCheckedColumn.FieldName;

            object checkValue = null;

            if (e.NewItems != null)
            {
                var item = e.NewItems[0] as Object;

                checkValue = TypeDescriptor.GetProperties(item)[propertyName].GetValue(item);

                INotifyPropertyChanged iNewItem = item as INotifyPropertyChanged;

                if (iNewItem != null)
                {
                    iNewItem.PropertyChanged += iItem_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                var oldItem = e.OldItems[0] as Object;

                checkValue = TypeDescriptor.GetProperties(oldItem)[propertyName].GetValue(oldItem);

                INotifyPropertyChanged iOldItem = oldItem as INotifyPropertyChanged;

                if (iOldItem != null)
                {
                    iOldItem.PropertyChanged -= iItem_PropertyChanged;
                }
            }

            if (checkValue != null)
            {
                if ((Boolean)checkValue == true)
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                        TrueCount++;
                    else TrueCount--;
                }
            }

            GeneralCheckBoxSetter();
        }

        void iItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (InCodeCheck == false)
            {
                var checkValue = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);

                if (checkValue.GetType() == typeof(Boolean))
                {
                    if ((Boolean)checkValue == true)
                    {
                        TrueCount++;
                    }
                    else if ((Boolean)checkValue == false)
                    {
                        TrueCount--;
                    }
                }
                GeneralCheckBoxSetter();
            }
        }

        public void GeneralCheckBoxSetter()
        {
            InGeneralCheck = true;

            if (TrueCount == Grid.VisibleRowCount)
            {
                CurrentIsCheckedColumn.SetValue(IsCheckedColumnProperty, true);
            }
            else if (TrueCount == 0)
            {
                CurrentIsCheckedColumn.SetValue(IsCheckedColumnProperty, false);
            }
            else
            {
                CurrentIsCheckedColumn.SetValue(IsCheckedColumnProperty, null);
            }

            InGeneralCheck = false;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}