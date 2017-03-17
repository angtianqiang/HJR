using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.Data.Model;
using System.Linq;
using System.Windows.Input;
using System.Data.Entity;
using ZtxFrameWork.UI.Comm.DataModel;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class LoginWindowViewModel
    {
        public string Token { get; set; }
        public virtual string UserName { get; set; } = "F";
        public virtual string PassWord { get; set; } = "050301";
        public void Login()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                Messenger.Default.Send<string>("用户名不能为空！", "Error" + Token);
                return;
            }
            if (string.IsNullOrEmpty(PassWord))
            {
                Messenger.Default.Send<string>("用户密码不能为空！", "Error" + Token);
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            var db = DbFactory.Instance.CreateDbContext();
            var passWord = ZtxFrameWork.Utilities.SecretUtil.md5(PassWord);
       
            User userInfo = db.Users.Include(t=>t.UserAuthorityModuleMappings).Where(x => x.UserName == UserName && x.PassWord == passWord&& x.IsFrozen==false).FirstOrDefault();
            if (userInfo == null)
            {
                Mouse.OverrideCursor = null;
                Messenger.Default.Send<string>("用户名或密码错误！", "Error" + Token);
                return;
            }
            //同步数据库服务器时间

            DateTime dt = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").First();
            Utilities.LocalTimeSync.SyncTime(dt);
            //比较时间一致性 
            //  if (ConvertEx.ToCharYYYYMMDDHHMM(DateTime.Now) != ConvertEx.ToCharYYYYMMDDHHMM(dt))
            //     LocalTimeSync.SyncTime(dt);//同步服务器的时间 

            App.CurrentUser = User.CurrentUser = userInfo;
            userInfo.LoginCount++;
            userInfo.LastLoginDate = dt;
            db.SaveChanges();

            //    string sqlWhere = string.Format("where M.[UserID]='{0}'", userInfo.UserID);
            //  App.MenuAuthorityMappings = new Bll.MenuAuthorityMapping().GetMenuAuthorityMappingsList(sqlWhere).ToList();
            Mouse.OverrideCursor = null;

            Messenger.Default.Send<string>("Confirm", "Confirm" + Token);
        }
        public void Cancel()
        {
            Messenger.Default.Send<string>("Canel", "Cancel" + Token);
        }
    }
}