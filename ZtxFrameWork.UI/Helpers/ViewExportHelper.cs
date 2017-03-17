using DevExpress.Export;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ZtxFrameWork.UI.Helpers
{
  public  class ViewExportHelper
    {
        protected readonly DataViewBase view;
        public ViewExportHelper(DataViewBase view)
        {
            this.view = view;
        }
        public virtual void ExportToXlsx()
        {
            string fileName = GetFileName(new XlsxExportOptions());
            Export((file, options) => view.ExportToXlsx(file, options), fileName, new XlsxExportOptionsEx());
        }
        public virtual void ExportToXls()
        {
            string fileName = GetFileName(new XlsExportOptions());
            Export((file, options) => view.ExportToXls(file, options), fileName, new XlsExportOptionsEx());
        }
        public virtual void ExportToCsv()
        {
            string fileName = GetFileName(new CsvExportOptions());
            Export((file, options) => view.ExportToCsv(file, options), fileName, new CsvExportOptionsEx());
        }
        protected void Export<T>(Action<string, T> exportToFile, string fileName, T options) where T : ExportOptionsBase
        {
            if (string.IsNullOrEmpty(fileName))
                return;
            try
            {
                ExportCore(exportToFile, fileName, options);
            }
            catch (Exception e)
            {
                DXMessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        protected virtual void SubscribeProgressEvents<T>(T options)
        {
            ((IDataAwareExportOptions)options).ExportProgress += ExportProgress;
        }
        protected virtual void UnsubscribeProgressEvents<T>(T options)
        {
            ((IDataAwareExportOptions)options).ExportProgress -= ExportProgress;
        }
        protected virtual void ExportCore<T>(Action<string, T> exportToFile, string fileName, T options) where T : ExportOptionsBase
        {
            DXSplashScreen.Show<SplashWindow>();
            SubscribeProgressEvents<T>(options);
            try
            {
                exportToFile(fileName, options);
            }
            finally
            {
                UnsubscribeProgressEvents<T>(options);
                if (DXSplashScreen.IsActive)
                    DXSplashScreen.Close();
            }
            if (ShouldOpenExportedFile())
                ProcessLaunchHelper.StartProcess(fileName, false);
        }
        protected void ExportProgress(ProgressChangedEventArgs ea)
        {
            if (!DXSplashScreen.IsActive) return;
            DXSplashScreen.Progress(ea.ProgressPercentage);
        }
        protected static string GetFileName(ExportOptionsBase options)
        {
            return GetFileName(ExportOptionsControllerBase.GetControllerByOptions(options));
        }
        static string GetFileName(ExportOptionsControllerBase controller)
        {
            SaveFileDialog dlg = CreateSaveFileDialog(controller);
            if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
                return FileHelper.SetValidExtension(dlg.FileName, controller.FileExtensions[0], controller.FileExtensions);
            else
                return string.Empty;
        }
        static SaveFileDialog CreateSaveFileDialog(ExportOptionsControllerBase controller)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title);
            dlg.ValidateNames = true;
            dlg.FileName = PrintPreviewOptions.DefaultFileNameDefault;
            dlg.Filter = controller.Filter;
            return dlg;
        }
        protected static bool ShouldOpenExportedFile()
        {
            return DXMessageBox.Show(
                PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestion),
                PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestionCaption),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
    public class ExportHelper : ViewExportHelper
    {
        PrintableControlLink link;
        public ExportHelper(DataViewBase view)
            : base(view)
        {
            this.link = new PrintableControlLink(view);
        }
        public void DoExport(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Xls:
                    ExportToXls();
                    break;
                case ExportFormat.Xlsx:
                    ExportToXlsx();
                    break;
                case ExportFormat.Pdf:
                    ExportToPdf();
                    break;
                case ExportFormat.Htm:
                    ExportToHtml();
                    break;
                case ExportFormat.Mht:
                    ExportToMht();
                    break;
                case ExportFormat.Rtf:
                    ExportToRtf();
                    break;
                case ExportFormat.Txt:
                    ExportToTxt();
                    break;
                case ExportFormat.Image:
                    ExportToImage();
                    break;
                case ExportFormat.Xps:
                    ExportToXps();
                    break;
            }
        }
        public void ExportToHtml()
        {
            string fileName = GetFileName(new HtmlExportOptions());
            Export((file, options) => link.ExportToHtml(file, options), fileName, new HtmlExportOptions());
        }
        public void ExportToPdf()
        {
            string fileName = GetFileName(new PdfExportOptions());
            Export((file, options) => link.ExportToPdf(file, options), fileName, new PdfExportOptions());
        }
        public void ExportToMht()
        {
            string fileName = GetFileName(new MhtExportOptions());
            Export((file, options) => link.ExportToMht(file, options), fileName, new MhtExportOptions());
        }
        public void ExportToRtf()
        {
            string fileName = GetFileName(new RtfExportOptions());
            Export((file, options) => link.ExportToRtf(file, options), fileName, new RtfExportOptions());
        }
        public void ExportToTxt()
        {
            string fileName = GetFileName(new TextExportOptions());
            Export((file, options) => link.ExportToText(file, options), fileName, new TextExportOptions());
        }
        public void ExportToImage()
        {
            string fileName = GetFileName(new ImageExportOptions());
            Export((file, options) => link.ExportToImage(file, options), fileName, new ImageExportOptions());
        }
        public void ExportToXps()
        {
            string fileName = GetFileName(new XpsExportOptions());
            Export((file, options) => link.ExportToXps(file, options), fileName, new XpsExportOptions());
        }
        public override void ExportToXlsx()
        {
            string fileName = GetFileName(new XlsxExportOptions());
            Export((file, options) => link.ExportToXlsx(file, options), fileName, new XlsxExportOptions());
        }
        public override void ExportToXls()
        {
            string fileName = GetFileName(new XlsExportOptions());
            Export((file, options) => link.ExportToXls(file, options), fileName, new XlsExportOptions());
        }
        public override void ExportToCsv()
        {
            string fileName = GetFileName(new CsvExportOptions());
            Export((file, options) => link.ExportToCsv(file, options), fileName, new CsvExportOptions());
        }
        protected override void SubscribeProgressEvents<T>(T options)
        {
            link.PrintingSystem.ProgressReflector.PositionChanged += OnExportProgress;
            link.PrintingSystem.AfterBuildPages += OnAfterBuildPages;
        }
        void OnAfterBuildPages(object sender, EventArgs e)
        {
            DXSplashScreen.Close();
        }
        void OnExportProgress(object sender, EventArgs e)
        {
            ExportProgress(new ProgressChangedEventArgs(link.PrintingSystem.ProgressReflector.Position, null));
        }
        protected override void UnsubscribeProgressEvents<T>(T options)
        {
            link.PrintingSystem.ProgressReflector.PositionChanged -= OnExportProgress;
            link.PrintingSystem.AfterBuildPages -= OnAfterBuildPages;
        }
    }

}
