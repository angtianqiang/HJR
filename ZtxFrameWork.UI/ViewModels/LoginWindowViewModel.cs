using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using ZtxFrameWork.Data.Model;
using System.Linq;
using System.Windows.Input;
using System.Data.Entity;
using ZtxFrameWork.UI.Comm.DataModel;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Collections.Generic;

namespace ZtxFrameWork.UI.ViewModels
{
    [POCOViewModel]
    public class LoginWindowViewModel
    {
        public string Token { get; set; }
        public virtual string UserName { get; set; }// = "F";
        public virtual string PassWord { get; set; }// = "050301";
        public virtual long Current分店ID { get; set; }
        public virtual List<分店> 分店Source { get; set; }
        public LoginWindowViewModel()
        {
            Init();
        }
        public  void Init()
        {
                分店Source =  DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToList();
            
        }
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
            if (Current分店ID<=0)
            {
                Messenger.Default.Send<string>("请选择登录的分店！", "Error" + Token);
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            var db = DbFactory.Instance.CreateDbContext();

            #region 20170329对EF的MAP进行缓存 提高运行效率  http://www.cnblogs.com/dudu/p/entity-framework-warm-up.html

         
            var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingCollection.GenerateViews(new System.Collections.Generic.List<EdmSchemaError>());


            #endregion
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            var passWord = ZtxFrameWork.Utilities.SecretUtil.md5(PassWord);
       
            User userInfo = db.Users.Where(x => x.UserName == UserName && x.PassWord == passWord&& x.IsFrozen==false).FirstOrDefault();
            if (userInfo == null)
            {
                Mouse.OverrideCursor = null;
                Messenger.Default.Send<string>("用户名或密码错误！", "Error" + Token);
                return;
            }
            //取权线

            //同步数据库服务器时间
            userInfo.UserAuthorityModuleMappings =new Data.VHObjectList<UserAuthorityModuleMapping>( db.UserAuthorityModuleMappings.Include(t=>t.AuthorityModule).Where(t => t.UserID == userInfo.ID).AsEnumerable());
            DateTime dt = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").First();
            Utilities.LocalTimeSync.SyncTime(dt);
            //比较时间一致性 
            //  if (ConvertEx.ToCharYYYYMMDDHHMM(DateTime.Now) != ConvertEx.ToCharYYYYMMDDHHMM(dt))
            //     LocalTimeSync.SyncTime(dt);//同步服务器的时间 

            App.CurrentUser = User.CurrentUser = userInfo;
            App.Current分店 = 分店Source.First(T=>T.ID==Current分店ID);
            userInfo.LoginCount++;
            userInfo.LastLoginDate = dt;
            db.SaveChanges();
            db.Dispose();
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