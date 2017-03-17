using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.Data;
using ZtxFrameWork.UI.Comm.DataModel;
using System.ComponentModel;
using System.Windows.Input;
using ZtxFrameWork.Data.Model;
using System.Linq;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm;
using DevExpress.XtraReports.UI;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class UserAuthorityModuleMappingCollectionViewModel : IDocumentContent
    {
        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        protected IDocumentOwner DocumentOwner { get; private set; }
        #region 消息管理器的令牌 20170302
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();

        #endregion
        public virtual string PermissionTitle { get; set; } = "用户权限管理";//权限标识
        public virtual string  Title { get; set; }
        public virtual VHObjectList<UserAuthorityModuleMapping> Entities { get; set; }
        public ZtxFrameWork.Data.ZtxDB DB { get; set; }


        public static UserAuthorityModuleMappingCollectionViewModel Create()
        {
            return ViewModelSource.Create(() => new UserAuthorityModuleMappingCollectionViewModel());
        }
        protected UserAuthorityModuleMappingCollectionViewModel()
        {
            DB = DbFactory.Instance.CreateDbContext();
            Entities = new VHObjectList<UserAuthorityModuleMapping>(DB.UserAuthorityModuleMappings.Where(t=>t.User.IsFrozen==false).AsEnumerable());
        }
        public virtual void Save()
        {
            if (DB.ChangeTracker.HasChanges())
            {
                Mouse.OverrideCursor = Cursors.Wait;

                DB.SaveChanges();
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage("保存成功", "提示");
            }
            else
            {
                MessageBoxService.ShowMessage("没有可保存的内容", "提示");
            }

        }
        public virtual bool CanSave()
        {
            return DB.ChangeTracker.HasChanges();
        }
        public virtual void Close()
        {
            if (!TryClose())
                return;
            if (DocumentOwner != null)
                Mouse.OverrideCursor = Cursors.Wait;

            DocumentOwner.Close(this);
            Mouse.OverrideCursor = null;

        }
        protected virtual bool TryClose()
        {
            if (DB.ChangeTracker.HasChanges())
            {
                MessageResult result = MessageBoxService.ShowMessage(CommonResources.Confirmation_Save, CommonResources.Confirmation_Caption, MessageButton.YesNoCancel);
                if (result == MessageResult.Yes)
                {
                    Save();
                    return true;
                }

                return result != MessageResult.Cancel;
            }
            return true;
        }
            #region IDocumentContent
            object IDocumentContent.Title { get { return Title; } }

            void IDocumentContent.OnClose(CancelEventArgs e) {
                e.Cancel = !TryClose();

            }

            void IDocumentContent.OnDestroy()
        {

            }

            IDocumentOwner IDocumentContent.DocumentOwner
        {
                get { return DocumentOwner; }
                set { DocumentOwner = value; }
            }
        #endregion


        #region 20170311 打印 打印预览 报表设计

        protected virtual string GetReportPath()
        {
            return System.Environment.CurrentDirectory + @"\Reports\" + (PermissionTitle) + "Report.repx";
        }
        protected XtraReport CreateReport()
        {
            var path = GetReportPath();
            return System.IO.File.Exists(path) ? XtraReport.FromFile(path, true) : new XtraReport();

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
    }
}