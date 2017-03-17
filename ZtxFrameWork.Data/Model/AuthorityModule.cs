using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.Data.Model
{
    [Table("sys_AuthorityModules")]
    public class AuthorityModule : ModelBase
    {
        public AuthorityModule()
        {
            UserAuthorityModuleMappings = new VHObjectList<UserAuthorityModuleMapping>();
        }
        private string _viewTitle;
        [Display(Name = "页面名称", AutoGenerateField = true)]
        public string ViewTitle
        {
            get { return _viewTitle; }
            set { Set<string>(() => this.ViewTitle, ref _viewTitle, value); }
        }

        private string _des;
        [Display(Name = "描述", AutoGenerateField = true)]
        public string Des
        {
            get { return _des; }
            set { Set<string>(() => this.Des, ref _des, value); }
        }
        private string _category;
        [Display(Name = "模块组", AutoGenerateField = true)]
        public string Category
        {
            get { return _category; }
            set { Set<string>(() => this.Category, ref _category, value); }
        }
        private string _remark;
        [Display(Name = "备注", AutoGenerateField = true)]
        public string Remark
        {
            get { return _remark; }
            set { Set<string>(() => this.Remark, ref _remark, value); }
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

        public virtual VHObjectList<UserAuthorityModuleMapping> UserAuthorityModuleMappings { get; set; }
    }
}
