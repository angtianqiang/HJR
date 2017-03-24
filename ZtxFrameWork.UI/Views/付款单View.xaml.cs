using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
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

namespace ZtxFrameWork.UI.Views
{
    /// <summary>
    /// Interaction logic for 付款单View.xaml
    /// </summary>
    public partial class 付款单View : UserControl
    {
        public 付款单View()
        {
            InitializeComponent();
            //this.Loaded += (s, e) => { Mouse.OverrideCursor = null; };
            this.Details.CellValueChanging += Details_CellValueChanging;
            this.Details.ShownEditor += (s, e) => { Dispatcher.BeginInvoke(new Action(() => Details.ActiveEditor?.SelectAll())); };
        }

        private void Details_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            TableView view = sender as TableView;
            //   Messenger.Default.Send<String>("", "TableView_CellValueChanging" + Token);
            view.PostEditor();

            //   if (e.Value == null) return;

            //if (e.Column.FieldName == "饰品编号")
            //{
            //    Messenger.Default.Send<string>("", "饰品编号更改" + this.Tag.ToString());

            //}

            switch (e.Column.FieldName)
            {
                case "入库单号":
                    Messenger.Default.Send<string>("", "入库单号更改" + this.Tag.ToString());
                    break;
                case "退库单号":
                    Messenger.Default.Send<string>("", "退库单号更改" + this.Tag.ToString());
                    break;              
                case "本次支付金额":
                    Messenger.Default.Send<string>("", "本次支付金额更改" + this.Tag.ToString());
                    break;
                default:
                    break; 
            }
        }
    }
}
