using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.Data.Model
{
    [Table("sys_UserAuthorityModuleMappings")]
    public  class UserAuthorityModuleMapping : ModelBase
    {
        private long _userID;
        [Display(Name = "外键", AutoGenerateField = false)]
        public long UserID
        {
            get { return _userID; }
            set { Set<long>(() => this.UserID, ref _userID, value); }
        }
        private long _authorityModuleID;
        [Display(Name = "外键", AutoGenerateField = false)]
        public long AuthorityModuleID
        {
            get { return _authorityModuleID; }
            set { Set<long>(() => this.AuthorityModuleID, ref _authorityModuleID, value); }
        }


        private User _user;
        [Display(Name = "外键", AutoGenerateField = false)]
        public virtual User User
        {
            get { return _user; }
            set { Set<User>(() => this.User, ref _user, value); }
        }

        private AuthorityModule _authorityModule;
        [Display(Name = "外键", AutoGenerateField = false)]
        public virtual AuthorityModule AuthorityModule
        {
            get { return _authorityModule; }
            set { Set<AuthorityModule>(() => this.AuthorityModule, ref _authorityModule, value); }
        }


        private bool _navigate;
        [Display(Name = "导航", AutoGenerateField = true)]
        public bool Navigate
        {
            get { return _navigate; }
            set { Set<bool>(() => this.Navigate, ref _navigate, value); }
        }
        private bool _add;
        [Display(Name = "新建", AutoGenerateField = true)]
        public bool Add
        {
            get { return _add; }
            set { Set<bool>(() => this.Add, ref _add, value); }
        }
        private bool _edit;
        [Display(Name = "编辑", AutoGenerateField = true)]
        public bool Edit
        {
            get { return _edit; }
            set { Set<bool>(() => this.Edit, ref _edit, value); }
        }
        private bool _delete;
        [Display(Name = "删除", AutoGenerateField = true)]
        public bool Delete
        {
            get { return _delete; }
            set { Set<bool>(() => this.Delete, ref _delete, value); }
        }
        private bool _export;
        [Display(Name = "导出", AutoGenerateField = true)]
        public bool Export
        {
            get { return _export; }
            set { Set<bool>(() => this.Export, ref _export, value); }
        }
        private bool _print;
        [Display(Name = "打印", AutoGenerateField = true)]
        public bool Print
        {
            get { return _print; }
            set { Set<bool>(() => this.Print, ref _print, value); }
        }
        private bool _preview;
        [Display(Name = "预览", AutoGenerateField = true)]
        public bool Preview
        {
            get { return _preview; }
            set { Set<bool>(() => this.Preview, ref _preview, value); }
        }
        private bool _design;
        [Display(Name = "设计", AutoGenerateField = true)]
        public bool Design
        {
            get { return _design; }
            set { Set<bool>(() => this.Design, ref _design, value); }
        }
        private bool _confirm;
        [Display(Name = "生效", AutoGenerateField = true)]
        public bool Confirm
        {
            get { return _confirm; }
            set { Set<bool>(() => this.Confirm, ref _confirm, value); }
        }

        private bool _unConfirm;
        [Display(Name = "失效", AutoGenerateField = true)]
        public bool UnConfirm
        {
            get { return _unConfirm; }
            set { Set<bool>(() => this.UnConfirm, ref _unConfirm, value); }
        }
        private bool _audit;
        [Display(Name = "审核", AutoGenerateField = true)]
        public bool Audit
        {
            get { return _audit; }
            set { Set<bool>(() => this.Audit, ref _audit, value); }
        }
        private bool _unAudit;
        [Display(Name = "反审", AutoGenerateField = true)]
        public bool UnAudit
        {
            get { return _unAudit; }
            set { Set<bool>(() => this.UnAudit, ref _unAudit, value); }
        }
    }
}
