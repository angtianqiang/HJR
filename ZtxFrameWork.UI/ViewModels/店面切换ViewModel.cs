using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using System.Collections.Generic;
using ZtxFrameWork.Data.Model;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
  public class 店面切换ViewModel :  IDocumentContent
    {
        public 店面切换ViewModel()
        {
      //      if (this.IsInDesignMode())
            {


                Init();
            }

        }
        
     
        public void Init()
        {
            分店Source = Helpers.CacheHelper.分店Source;

        }

        public virtual long Current分店ID { get; set; } = App.Current分店.ID;
        public virtual long Target分店ID { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }
        public virtual string PermissionTitle { get; set; } = "店面切换";//权限标识

        protected IDocumentOwner DocumentOwner { get; private set; }
   
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();
        public void Switch()
        {
            if (Target分店ID <= 0)
            {
                MessageBoxService.ShowMessage("请选择切换店面", "提示", MessageButton.OK, MessageIcon.Error);
                return;
            }
            if (Target分店ID == Current分店ID)
            {
                MessageBoxService.ShowMessage("源店面与目标店面相同", "提示", MessageButton.OK, MessageIcon.Error);
                return;
            }

            App.Current分店 = 分店Source.First(T => T.ID == Target分店ID);
            Messenger.Default.Send<string>("", "分店变更");
            MessageBoxService.ShowMessage("店面切换成功!", "提示", MessageButton.OK, MessageIcon.Information);
                

        }
        public virtual void Close()
        {
            if (DocumentOwner != null)
                Mouse.OverrideCursor = Cursors.Wait;

            DocumentOwner.Close(this);
            Mouse.OverrideCursor = null;

        }
        #region IDocumentContent
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