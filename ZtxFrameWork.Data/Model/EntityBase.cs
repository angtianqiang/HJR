using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data.Model
{
    public class Entity : VHObject,IEntity, IDbContextLink
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
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss", ApplyFormatInEditMode = true, NullDisplayText = "")]
        [Display(Name = "建立时间", AutoGenerateField = false, Description = "")]
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
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss", ApplyFormatInEditMode = true, NullDisplayText = "")]
        [Display(Name = "最后更改时间", AutoGenerateField = false, Description = "")]
        public DateTime? ModifiedOn
        {
            get { return _modifiedOn; }
            set { Set<DateTime?>(() => this.ModifiedOn, ref _modifiedOn, value); }
        }


        public virtual void OnSaving()
        {
           // Debugger.IsAttached 20170411 用这个应该也可以

            if (User.CurrentUser == null)//数据迁时会调用
            {
                User.CurrentUser = new User() { DispalyName = "system" };
            }

            //  var currentDateTime=   new ZtxDB().Database.SqlQuery<DateTime>("SELECT GETDATE()").First();
            var currentDateTime = DateTime.Now;

            switch (this.DirtyState)
            {
                case DirtyState.UnChanged:
                    break;
                case DirtyState.Added:
                    this.CreateBy = this.ModifiedBy = User.CurrentUser.DispalyName;
                    this.CreateOn = this.ModifiedOn = currentDateTime;
                    AddDbOperatorLog();
                    break;
                case DirtyState.Deleted:
                    AddDbOperatorLog();
                    break;
                case DirtyState.Modified:
                    this.ModifiedBy = User.CurrentUser.DispalyName;
                    this.ModifiedOn = currentDateTime;
                    AddDbOperatorLog();
                    break;
                default:
                    break;
            }
            //实体CUD日志记录

        }  
        public virtual void OnLoaded() { }
        private void AddDbOperatorLog()
        {
          
                ((ZtxDB)this.DbContext).DbOperatorLogs.Add(
               new DbOperatorLog()
               {
                   EntityName = GetEntityDisplayName(),
                   EntityID = ID,
                   DirtyState = DirtyState.ToString(),
                   CreateBy = CreateBy,
                   CreateOn = CreateOn.Value,
                   ModifiedBy = ModifiedBy,
                   ModifiedOn = ModifiedOn.Value,
                   KeyValue = GetOperatorLogColumnValue(),
                //   UniqueId = UniqueId,
                   OperatorOn = DateTime.Now,
                   OperatorBy = User.CurrentUser.DispalyName
               }
               );
         
           
        }

        private string GetOperatorLogColumnValue()
        {
            if (this is IBillEntity)
            {
                return ((IBillEntity)this).编号;
            }
            string temp = string.Empty;

            Type type = this.GetType();

            Attribute[] atrs = { new  OperatorLogAttribute()};

            PropertyDescriptorCollection list = TypeDescriptor.GetProperties(type, atrs);
         
            if (list != null && list.Count > 0)
            {
                temp= list[0].GetValue(this).ToString();
            }
          
            return temp;
        }
        private string GetEntityDisplayName()
        {
            Type type = this.GetType();
            DisplayNameAttribute attribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(type, typeof(DisplayNameAttribute));

            return attribute != null && attribute != DisplayNameAttribute.Default ? attribute.DisplayName : type.Name;
       
        }

        #region IDbContextLink实现
        [Display(AutoGenerateField = false)]
        [NotMapped]
        public DbContext DbContext { get; set; }
        #endregion
    }
    public class BillEntity : Entity, IBillEntity
    {
        private String _编号;
        public String 编号
        {
            get { return _编号; }
            set { Set<String>(() => this.编号, ref _编号, value); }
        }
        private String _状态;
        public String 状态
        {
            get { return _状态; }
            set { Set<String>(() => this.状态, ref _状态, value); }
        }
    }

  
    [AttributeUsage(AttributeTargets.Property)]
    public class OperatorLogAttribute : Attribute
    {

    }
    public interface IDbContextLink
    {
        DbContext DbContext { get; set; }
    }

    public interface IEntity:IDbContextLink
    {
         long ID { get; set; }  
         string CreateBy { get; set; }
         DateTime? CreateOn { get; set; }
         string ModifiedBy { get; set; }
         DateTime? ModifiedOn { get; set; }
        void OnSaving();
        void OnLoaded();
    }
 public   interface IBillEntity:IEntity
    {
        String 编号 { get; set; }
        String 状态 { get; set; }
    }
}
