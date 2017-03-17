﻿using DevExpress.Xpf.Grid;
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
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.UI.Views
{
    /// <summary>
    /// Interaction logic for UserAuthorityModuleMappingCollectionView.xaml
    /// </summary>
    public partial class UserAuthorityModuleMappingCollectionView : UserControl
    {
        public UserAuthorityModuleMappingCollectionView()
        {
            InitializeComponent();
            this.Loaded += UserAuthorityModuleMappingCollectionView_Loaded;

            this.grid.AutoGeneratedColumns += delegate  { View.BestFitColumns(); };
        }
        public VHObjectList<UserAuthorityModuleMapping> dbContext { get; set; }


        private void UserAuthorityModuleMappingCollectionView_Loaded(object sender, RoutedEventArgs e)
        {
            dbContext = (this.DataContext as UI.ViewModels.UserAuthorityModuleMappingCollectionViewModel).Entities;
           
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //foreach (var item in View.GetSelectedCells())
            //{
            //    if (item.Column.FieldType.Name == typeof(bool).Name)
            //    {

            //        grid.SetCellValue(item.RowHandle, item.Column, true);
            //    }
            //}
          //  try
            {
                foreach (var item in View.GetSelectedCells())
                {
                    if (item.Column.FieldType.Name == typeof(bool).Name)
                    {
                        var id = Convert.ToInt32(grid.GetCellValue(item.RowHandle, "ID"));
                        var currentItem = dbContext.Where(t => t.ID == id).First();
                        var proppertyInfo = new UserAuthorityModuleMapping().GetType().GetProperty("AuthorityModule");
                        var proppertyInfo2 = new AuthorityModule().GetType().GetProperty(item.Column.FieldName);
                        AuthorityModule temp = (AuthorityModule)proppertyInfo.GetValue(currentItem, null);
                        bool temp2 = (bool)proppertyInfo2.GetValue(temp, null);
                        grid.SetCellValue(item.RowHandle, item.Column, true && temp2);//如果这个权限未启用，就不设计为TRUE
                    }
                }
            }
         //   catch (Exception EX)
            {


            }



            grid.RefreshData();


        }
        private void BarButtonItem_ItemClick1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            foreach (var item in View.GetSelectedCells())
            {
                if (item.Column.FieldType.Name == typeof(bool).Name)
                {

                    grid.SetCellValue(item.RowHandle, item.Column, false);
                }
            }
            grid.RefreshData();
        }
    }
}