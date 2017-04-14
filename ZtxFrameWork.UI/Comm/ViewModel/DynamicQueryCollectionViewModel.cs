using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    [POCOViewModel]
    public class DynamicQueryCollectionViewModel<TEntity> : IDocumentContent  where TEntity : VHObject 
    {

        [ServiceProperty(Key = "CommMessageBox")]
        public virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(Key = "FindDialogWindow")]
        public virtual IDialogService DialogService { get { return null; } }
        protected IDocumentOwner DocumentOwner { get; private set; }

        public virtual string PermissionTitle { get; set; }//权限标识
       
        public virtual List<dynamic> Entities { get; set; }
        public EntityExpressionBuilder<TEntity> EntityExpressionViewModel { get; set; }
        public virtual string EntityExpressionViewName { get; set; }
        #region 消息管理器的令牌 20170302
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();
        #endregion
        public virtual IEnumerable<string> HiddenProperties { get; protected set; }
        public virtual IEnumerable<string> AdditionalProperties { get; protected set; }

        public DynamicQueryCollectionViewModel(string permissionTitle)
        {
            PermissionTitle = permissionTitle;
        }
        protected virtual  List<dynamic> GetNewData(Expression<Func<TEntity, bool>> AdvancedExpression)
        {
            return null;
        }
        public virtual void Find()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            DialogService.ShowDialog(null, PermissionTitle + "【查询】", EntityExpressionViewModel);
            if (EntityExpressionViewModel.DialogResult == true)
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Entities =  GetNewData( EntityExpressionViewModel.AdvancedExpression);
                Mouse.OverrideCursor = null;


            }
        }
   
        public virtual void Close()
        {
            if (DocumentOwner != null)
                Mouse.OverrideCursor = Cursors.Wait;

            DocumentOwner.Close(this);
            Mouse.OverrideCursor = null;

        }

        #region 20170311 打印 打印预览 报表设计

        protected virtual string GetReportPath()
        {
            return System.Environment.CurrentDirectory + @"\Reports\" + (PermissionTitle) + "Report.repx";
        }
        protected XtraReport CreateReport()
        {
            var path = GetReportPath();

            XtraReport newReport = System.IO.File.Exists(path) ? XtraReport.FromFile(path, true) : new XtraReport();
            newReport.DisplayName = PermissionTitle + "Report.repx";
            return newReport;

        }
        protected void SetReportDataSource(XtraReport report)
        {
          //  DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
         //   objectDataSource.DataSource = this.Entity;
            report.DataSource = Entities;
        }
        public virtual void ReportPrint()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            report.CreateDocument();
            report.Print();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportPrint()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Print;
        }
        public virtual void ReportPreview()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            report.CreateDocument();

            report.ShowPreview();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportPreview()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Preview;
        }
        public virtual void ReportDesigner()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            // report.CreateDocument();

            report.ShowDesigner();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportDesigner()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Design;
        }
        #endregion
# region IDocumentContent
        object IDocumentContent.Title { get { return null; } }

        void IDocumentContent.OnClose(CancelEventArgs e) { }

        void IDocumentContent.OnDestroy()
        {
          
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion

    }
}
