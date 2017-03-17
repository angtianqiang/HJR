using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
namespace ZtxFrameWork.UI.Controls
{
    /// <summary>
    /// Interaction logic for CollectionMenu.xaml
    /// </summary>
    public partial class CollectionMenu : UserControl
    {
        public CollectionMenu()
        {
            InitializeComponent();
        }





        public DataViewBase DataView
        {
            get { return (DataViewBase)GetValue(DataViewProperty); }
            set { SetValue(DataViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataViewProperty =
            DependencyProperty.Register("DataView", typeof(DataViewBase), typeof(CollectionMenu), new PropertyMetadata(null));




    }
}
