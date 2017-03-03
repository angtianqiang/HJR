using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data.Model
{
 public   class ModelBase:VHObject
    {
        private long id;
        [Display(Name = "ID", AutoGenerateField = false, Description = "")]
        public long ID
        {
            get { return id; }
            set { Set<long>(() => this.ID, ref id, value); }
        }


        private string _createBy;
        [Display(Name = "建立人", AutoGenerateField = false, Description = "")]
        public string CreateBy
        {
            get { return _createBy; }
            set { Set<string>(() => this.CreateBy, ref _createBy, value); }
        }

        private DateTime? _createOn;
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss", ApplyFormatInEditMode =true, NullDisplayText ="")]
        [Display(Name = "建立时间",  AutoGenerateField = false, Description = "")]
        public DateTime? CreateOn
        {
            get { return _createOn; }
            set { Set<DateTime?>(() => this.CreateOn, ref _createOn, value); }
        }
        private string _modifiedBy;
        [Display(Name = "最后更改人", AutoGenerateField = false, Description = "")]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { Set<string>(() => this.ModifiedBy, ref _modifiedBy, value); }
        }
        private DateTime? _modifiedOn;
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss", ApplyFormatInEditMode =true, NullDisplayText ="")]
        [Display(Name = "最后更改时间", AutoGenerateField = false, Description = "")]
        public DateTime? ModifiedOn
        {
            get { return _modifiedOn; }
            set { Set<DateTime?>(() => this.ModifiedOn, ref _modifiedOn, value); }
        }


        public override void OnSaving()
        {
            base.OnSaving();

            if (User.CurrentUser==null)//数据迁时会调用
            {
                User.CurrentUser = new User() { DispalyName = "系统生成" };
            } 
        
            //  var currentDateTime=   new ZtxDB().Database.SqlQuery<DateTime>("SELECT GETDATE()").First();
            var currentDateTime = DateTime.Now;

            switch (this.DirtyState)
            {
                case DirtyState.UnChanged:
                    break;
                case DirtyState.Added:
                    this.CreateBy =this.ModifiedBy= User.CurrentUser.DispalyName;
                    this.CreateOn =this.ModifiedOn= currentDateTime;
                    break;
                case DirtyState.Deleted:
                    break;
                case DirtyState.Modified:
                      this.ModifiedBy = User.CurrentUser.DispalyName;
                      this.ModifiedOn = currentDateTime;
                    break;
                default:
                    break;
            }
        }
    }
}
