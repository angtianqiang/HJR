using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections.ObjectModel;
using ZtxFrameWork.Data.Model;

namespace efTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //          TestDb db = new TestDb();
            //          OrderItem orderItem = db.OrderItems.Create();
            //          orderItem.PO = "po";
            //          orderItem.ProductID = 1;
            //db.OrderItems.Add(orderItem);
            //          // var a = db.OrderItems.Local.Count();


            //          //   var tt=   db.OrderItems.Local.Where(t => t.PO == "po").First();

            //          //   this.Title = tt.PO + "  " + tt.Product.Des;

            //          this.Title = orderItem.Product.Des;

            var aa = new TestDb().OrderItems.Include(X => X.Product).Include(x => x.Product.Color).ToList();
            this.dataGrid.ItemsSource = list;

            this.dataGrid1.ItemsSource = lsit2;
        }
        ObservableCollection<OrderItem> list = new ObservableCollection<OrderItem>();
        TestDb db = new TestDb();
        private void button_Click(object sender, RoutedEventArgs e)
        {

         
            OrderItem orderItem = db.OrderItems.Create();
            orderItem.PO = "po";
            orderItem.ProductID = 1;
            db.OrderItems.Add(orderItem);
            list.Add(orderItem);
            orderItem.ProductID = 6;
            db.Entry(orderItem).Reference(l => l.Product).Load();
          //  db.Entry(orderItem.Product).Reference(l => l.Color).Load();
            //  db.Products.Load();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in list)
            {
                if (db.Entry(item).Reference(l => l.Product).IsLoaded == false)
                {
                    db.Entry(item).Reference(l => l.Product).Load();
                }
              
            }
            this.dataGrid.Items.Refresh();
        }
        ObservableCollection<dynamic> lsit2 = new  ObservableCollection<dynamic>();
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            lsit2.Add(new  { Name = "aaaa", FullName = "bbbb",FDF="FDF" });
            lsit2.Add(new { Name = "2aaaa", FullName = "2bbbb" });
            this.dataGrid1.Items.Refresh();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lsit2.Count; i++)
            {
                var item = lsit2[i];
                if (item.Name=="aaaa")
                {
                    lsit2.Remove(item);
                }
            }
           // this.dataGrid1.Items.Refresh();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ZtxFrameWork.Data.ZtxDB db = new ZtxFrameWork.Data.ZtxDB();
            订单 order = new 订单()
            {
                客户名称 = "abb",
                订单号 = "po001",
                订单明细 = new ZtxFrameWork.Data.VHObjectList<订单明细> {
                    new 订单明细() { 序号 = 1, 数量 = 100, 产品 = new 产品 { 番号 = "001", 规格 = "hbb3*1", 颜色 = new 颜色 { 颜色内容 = "1.color" } } },
                     new 订单明细() { 序号 = 2, 数量 = 300, 产品 = new 产品 { 番号 = "002", 规格 = "hbb3*2", 颜色 = new 颜色 { 颜色内容 = "2.color" } } },
                      new 订单明细() { 序号 = 3, 数量 = 300, 产品 = new 产品 { 番号 = "003", 规格 = "hbb3*3", 颜色 = new 颜色 { 颜色内容 = "3.color" } } }
                }
            };
            db.订单s.Add(order);
            db.SaveChanges();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {

            ZtxFrameWork.Data.ZtxDB db = new ZtxFrameWork.Data.ZtxDB();
            订单 order = db.订单s.Where(t => t.客户名称 == "abb").FirstOrDefault();
            order.订单明细[0].产品.颜色.颜色内容 = order.订单明细[0].产品.颜色.颜色内容 + "ooooooo";
            this.Title = db.Entry(order).State.ToString();
            var aa= db.Entry(order.订单明细[0].产品.颜色).State.ToString();
          var  a=  db.ChangeTracker.HasChanges();
            db.SaveChanges();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            ZtxFrameWork.Data.ZtxDB db = new ZtxFrameWork.Data.ZtxDB();
            订单 order = db.订单s.Where(t => t.客户名称 == "abb").FirstOrDefault();
            db.订单明细s.RemoveRange(order.订单明细);
            db.订单s.Remove(order);
            db.SaveChanges();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {

            ZtxFrameWork.Data.ZtxDB db = new ZtxFrameWork.Data.ZtxDB();
            订单 order = new 订单()
            {
                客户名称 = "abb",
                订单号 = "po001",
                订单明细 = new ZtxFrameWork.Data.VHObjectList<订单明细> {
                    new 订单明细() { 序号 = 1, 数量 = 100, 产品ID=21 }, 
                    new 订单明细() { 序号 = 1, 数量 = 200, 产品ID=21 }, 
                    new 订单明细() { 序号 = 1, 数量 = 300, 产品ID=21 },
              
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
                }
            };
            db.订单s.Add(order);
            db.SaveChanges();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            ZtxFrameWork.Data.ZtxDB db = new ZtxFrameWork.Data.ZtxDB();
            订单 order = db.订单s.Where(t => t.客户名称 == "abb").FirstOrDefault();
            // order.订单明细[0].序号 = 1001;
            var item = order.订单明细[2];
           db.订单明细s.Remove(item);
            order.订单明细.Remove(item);

            db.SaveChanges();
        }
    }
}
