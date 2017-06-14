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
    /// Interaction logic for 销售单View.xaml
    /// </summary>
    public partial class 销售单View : UserControl
    {
        public 销售单View()
        {
            InitializeComponent();
            //this.Loaded += (s, e) => { Mouse.OverrideCursor = null; };
            this.Details.CellValueChanging += Details_CellValueChanging;
            this.Details.CellValueChanged += Details_CellValueChanged;
            this.Details.ShownEditor += (s, e) => { Dispatcher.BeginInvoke(new Action(() => Details.ActiveEditor?.SelectAll())); };
            this.Loaded += 销售单View_Loaded;
          
        }

        private void 销售单View_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "选中数据" + this.Tag.ToString(), m =>
            {
                //    this.Details.SelectCell(this.Details.FocusedRowHandle,this.DetailGrid.Columns["数量"]);


                this.Details.MoveNextCell();
                this.Details.MoveNextCell();
                this.Details.MoveNextCell();
                this.Details.MoveNextCell();
                this.Details.MoveNextCell();
                this.Details.ShowEditor();
               Dispatcher.BeginInvoke(new Action(() => Details.ActiveEditor?.SelectAll()));
                //this.DetailGrid.BeginSelection();
                //this.DetailGrid.UnselectAll();
                //this.Details.SelectCell(this.Details.FocusedRowHandle, this.DetailGrid.Columns["数量"]);
                //this.DetailGrid.EndSelection();

            });
        }

        private void Details_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
             
                case "数量":
                    if (e.Value.ToString() != e.OldValue.ToString())
                    {

                        Messenger.Default.Send<string>("", "数量更改后" + this.Tag.ToString());
                    }
                    break;
             
                default:
                    break;
            }
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
                case "饰品编号":
                    Messenger.Default.Send<string>("", "饰品编号更改" + this.Tag.ToString());
                    break;
                case "数量":
                    Messenger.Default.Send<string>("", "数量更改" + this.Tag.ToString());
                    break;
                case "重量":
                case "单价":
                case "工费计法":
                case "销售价":
                case "折扣":
                    Messenger.Default.Send<string>("", "更新金额" + this.Tag.ToString());
                    break;
                default:
                    break;
            }
        }
    }
}
