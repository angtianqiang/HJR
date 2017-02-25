﻿using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using ZtxFrameWork.Data.Model;
using System.Windows.Input;
using System.Linq;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;


namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class NavMainViewModel
    {
        public static NavMainViewModel Create()
        {
            return DevExpress.Mvvm.POCO.ViewModelSource.Create(() => new NavMainViewModel());
        }
        protected NavMainViewModel()
        {
            if (!this.IsInDesignMode())
                OnInitializeInRuntime();
            Messenger.Default.Register<Module>(this, x => Show(x));
        

        }

        public virtual User CurrentUser { get; set; } = User.CurrentUser;

        IDocumentManagerService SignleObjectDocumentManagerService { get { return this.GetService<IDocumentManagerService>("SignleObjectDocumentManagerService"); } }

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>("TabbedDocumentManagerService"); } }
        public virtual void OnInitializeInRuntime()
        {
            var db = DbFactory.Instance.CreateDbContext();

            //   var ztx = new ZtxFrameWorkDB();

            Modules = new ObservableCollection<Module>(db.Modules.Where(t => t.ParentID == null).OrderBy(x => x.SortNo).ToList());
            foreach (var item in Modules)
            {
                InitAndSort(item);
            }
        }
        //加载并排序
        public void InitAndSort(Module item)
        {
            if (item.ChildModules.Count == 0)
            {
                return;
            }
            item.ChildModules = item.ChildModules.OrderBy(x => x.SortNo).ToList();
            foreach (var subItem in item.ChildModules)
            {
                InitAndSort(subItem);
            }
        }

        public virtual ObservableCollection<Module> Modules { get; set; }

        public void Show(Module module)
        {

            if (module == null || DocumentManagerService == null || string.IsNullOrEmpty(module.DocumentType)|| module.ModuleInfo!= ModuleInfo.MoudleAction)
                return;
            Mouse.OverrideCursor = Cursors.Wait;
            // IDocument document = DocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));

            // IDocument document = SignleObjectDocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));
            IDocument document = CreateDocument(module);
            Mouse.OverrideCursor = null;
            document.Show();

        }
        IDocument CreateDocument(Module module)
        {
            var document = DocumentManagerService.CreateDocument(module.DocumentType, module.ModuleTitle, this);
            // var document = SignleObjectDocumentManagerService.CreateDocument(module.DocumentType, null, this);
            document.Title = GetModuleTitle(module);
            document.DestroyOnClose = false;
            return document;
        }

        protected virtual string GetModuleTitle(Module module)
        {
            return module.ModuleTitle;
        }



       

    }
  
}