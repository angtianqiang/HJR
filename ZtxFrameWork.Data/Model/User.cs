using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.Data.Model
{
    [Table("sys_Users")]
 public   class User:VHObject
    {
        public static User CurrentUser = null;
        public  UserAuthorityModuleMapping GetUserAuthorityModuleMapping(string ViewTitle)
        {
        return  UserAuthorityModuleMappings.Where(t => t.AuthorityModule.ViewTitle == ViewTitle).FirstOrDefault();
           
        }
           
        public User()
        {
            UserAuthorityModuleMappings = new VHObjectList<UserAuthorityModuleMapping>();
        }
        private long id;
        public long ID
        {
            get { return id;  }
            set { Set<long>(() => this.ID, ref id, value); }
        }
        private string userName;

        [DisplayName("用户代码")]
        [Required(ErrorMessage = "用户代码为必输项")]
        public string UserName
        {
            get { return userName; }
            set { Set<string>(() => this.UserName, ref userName, value); }
        }

        private string passWord;
    
        [Display(Name = "用户密码", AutoGenerateField = true)]
        [ReadOnly(true)]
        [Required]
        [DataType(DataType.Password)]
        public string PassWord
        {
            get { return passWord; }
            set { Set<string>(() => this.PassWord, ref passWord, value); }
        }

        private string department;
    
        [DisplayName("用户部门")]
        [Required(ErrorMessage = "用户部门为必输项")]
        public string Department
        {
            get { return department; }
            set { Set<string>(() => this.Department, ref department, value); }
        }
        private string displayName;
    
        [DisplayName("用户姓名")]
        [Required(ErrorMessage = "用户姓名为必输项")]
        public string DispalyName
        {
            get { return displayName; }
            set { Set<string>(() => this.DispalyName, ref displayName, value); }
        }
        private bool isFrozen;
    
        [DisplayName("用否停用")]
        public bool IsFrozen
        {
            get { return isFrozen; }
            set { Set<bool>(() => this.IsFrozen, ref isFrozen, value); }
        }
        private int loginCount;
    
        [Display(Name = "登陆次数", Description = "dfsdfsadf")]
        [ReadOnly(true)]
        public int LoginCount
        {
            get { return loginCount; }
            set { Set<int>(() => this.LoginCount, ref loginCount, value); }
        }

        private DateTime? lastLoginDate;
    
        [DisplayName("最后登录时间"), DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "yyyy-MM-dd HH:mm")]
        [ReadOnly(true)]
        public DateTime? LastLoginDate
        {
            get { return lastLoginDate; }
            set { Set<DateTime?>(() => this.LastLoginDate, ref lastLoginDate, value); }
        }


        public override string ToString()
        {
            return "xxxxds" + displayName;
        }


        public virtual VHObjectList<UserAuthorityModuleMapping> UserAuthorityModuleMappings { get; set; }
    }
}
