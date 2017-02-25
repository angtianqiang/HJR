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
using DevExpress.Xpf.Core;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.ViewModel;
using ZtxFrameWork.Data;
using ZtxFrameWork.UI.Comm.DataModel;
using DevExpress.Mvvm;

namespace ZtxFrameWork.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : DXWindow
    {
   /// CollectionViewModel<订单, ZtxDB, long> list = CollectionViewModel<订单, ZtxDB, long>.CreateCollectionViewModel(DbFactory.Instance, x => x.订单s, x => x, x => x.ID);
        public MainWindow1()
        {
            InitializeComponent();
           // this.grid.DataContext = list;
            //   list.FilterExpression = x => true;

            //   this.grid.ItemsSource = DbFactory.Instance.CreateDbContext().订单明细s.ToList();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            //   this.grid.ItemsSource = list.Entities;
         //   list.Refresh();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
         //   ZtxDB db = new ZtxDB();
         //var item=   db.订单明细s.Add(new 订单明细() { 订单ID=9, 序号 = 1001, 数量 = 1000, 产品ID = 12 });
         //   db.SaveChanges();
         //   Messenger.Default.Send(new EntityMessage<订单明细, long>(item.ID,  EntityMessageType.Added ));
        }
        //protected virtual void OnEntitySaved(TPrimaryKey primaryKey, TEntity entity, bool isNewEntity)
        //{
        //    Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, isNewEntity ? EntityMessageType.Added : EntityMessageType.Changed));
        //}

        //protected virtual void OnBeforeEntityDeleted(TPrimaryKey primaryKey, TEntity entity) { }

        //protected virtual void OnEntityDeleted(TPrimaryKey primaryKey, TEntity entity)
        //{
        //    Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Deleted));
        //}
    }
}
