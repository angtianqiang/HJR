using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using ZtxFrameWork.Data.Model;
using System.Windows.Input;
using System.Linq;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using System.ComponentModel;
using DevExpress.Xpf.Core;
using System.Windows.Data;
using DevExpress.Xpf.Bars;

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
        
             public object ThemeCollection
        {
            get
            {

                ICollectionView view = CollectionViewSource.GetDefaultView(Theme.Themes.Where(t =>  (t.Category== Theme.Office2010Category)).Select(t => new ThemeViewModel(t)).ToArray());
                view.GroupDescriptions.Add(new PropertyGroupDescription("Theme.Category"));
                return view;

            }
        }

        public virtual User CurrentUser { get; set; } = User.CurrentUser;
        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }
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

            //权限判断
            if (User.CurrentUser.GetUserAuthorityModuleMapping(module.ModuleTitle).Navigate==false)
            {
                MessageBoxService.ShowMessage("没有打开权限", "提示", MessageButton.OK, MessageIcon.Stop);
                return;
            } 
         
     IDocument document = DocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));

      //   IDocument document = SignleObjectDocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));
         //   IDocument document = CreateDocument(module);
        
            document.Show();
         //   Mouse.OverrideCursor = null;
        }
        IDocument CreateDocument(Module module)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var document = DocumentManagerService.CreateDocument(module.DocumentType, module.ModuleTitle, this);
            // var document = SignleObjectDocumentManagerService.CreateDocument(module.DocumentType, null, this);
            document.Title = GetModuleTitle(module);

            if (App.SystemConfigs.Where(t => t.Token == "DestroyOnClose").Single().TokenValue== "0")
            {
                document.DestroyOnClose = true;
            }
            else
            {
                document.DestroyOnClose = false;
            }
          
            return document;
        }

        protected virtual string GetModuleTitle(Module module)
        {
            return module.ModuleTitle;
        }



       

    }
  
}