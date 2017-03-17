using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.Data.Model
{
    [Table("sys_SystemConfigurations")]
  public  class SystemConfiguration : ModelBase
    {
        private String  _token;
        [Display(Name = "标识", AutoGenerateField = true)]
        public String  Token
        {
            get { return _token; }
            set { Set<String >(() => this.Token, ref _token, value); }
        }
        private string  _category;
        [Display(Name = "类别", AutoGenerateField = true)]
        public string  Category
        {
            get { return _category; }
            set { Set<string >(() => this.Category, ref _category, value); }
        }
        private string _tokenValue;
        [Display(Name = "参数值", AutoGenerateField = true)]
        public string TokenValue
        {
            get { return _tokenValue; }
            set { Set<string>(() => this.TokenValue, ref _tokenValue, value); }
        }
        private string _des;
        [Display(Name = "配置描述", AutoGenerateField = true)]
        public string Des
        {
            get { return _des; }
            set { Set<string>(() => this.Des, ref _des, value); }
        }
        private string _reamrk;
        [Display(Name = "备注", AutoGenerateField = true)]
        public string Remark
        {
            get { return _reamrk; }
            set { Set<string>(() => this.Remark, ref _reamrk, value); }
        }

    }
}
