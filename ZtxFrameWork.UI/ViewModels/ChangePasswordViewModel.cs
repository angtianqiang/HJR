using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using ZtxFrameWork.UI.Comm.DataModel;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class ChangePasswordViewModel : IDocumentContent
    {
        public virtual string OldPassword { get; set; }
        public virtual string NewPassword { get; set; }
        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }
        #region 20170311  权限标识
        public virtual string PermissionTitle { get; set; }= "更改登录密码";//权限标识
        #endregion
        protected IDocumentOwner DocumentOwner { get; private set; }
        #region 消息管理器的令牌 20170302
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();
        #endregion

        public void Change()
        {
            if (String.IsNullOrEmpty(OldPassword))
            {
                MessageBoxService.ShowMessage("请输入原密码！", "错误",MessageButton.OK,MessageIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(NewPassword))
            {
                MessageBoxService.ShowMessage("请输入新密码！", "错误", MessageButton.OK, MessageIcon.Error);
                return;
            }
            if (Utilities.SecretUtil.md5(OldPassword) != App.CurrentUser.PassWord)
            {
                MessageBoxService.ShowMessage("原密码错误！", "错误", MessageButton.OK, MessageIcon.Error);
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            string secretPassword = Utilities.SecretUtil.md5(NewPassword);

            using (var db= DbFactory.Instance.CreateDbContext())
            {
                var user = db.Users.Where(t => t.UserName == App.CurrentUser.UserName && t.PassWord == App.CurrentUser.PassWord).FirstOrDefault();
                if (user!=null)
                {
                    user.PassWord = secretPassword;
                    db.SaveChanges();
                    App.CurrentUser.PassWord = secretPassword;
                }               
            }
            Mouse.OverrideCursor = null;

            MessageBoxService.ShowMessage("密码更改成功", "提示");

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