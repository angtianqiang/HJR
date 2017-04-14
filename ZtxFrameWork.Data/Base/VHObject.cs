using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ZtxFrameWork.Data
{
    [Serializable]


  
    public class VHObject : IVHObject,IDataErrorInfo
    {
        #region Fields
        private string _uniqueId = string.Empty;
        protected bool _propertyChangedEventDisabled = false;
        protected object _entityLocker = new object();
        protected object _tag = null;
        private DirtyState _dirtyState;
        #endregion
        #region Properties
         [Display(AutoGenerateField = false)]
       [NotMapped]
        public string UniqueId { get { return this._uniqueId; } set { this._uniqueId = value; } }
          [Display(AutoGenerateField = false)]
        [NotMapped]
        public virtual bool PropertyChangeEventDisabled { get { return this._propertyChangedEventDisabled; } set { this._propertyChangedEventDisabled = value; } }
          [Display(AutoGenerateField = false)]
        [NotMapped]
        protected bool HasPropertyChangeListener
        {
            get { return this.PropertyChanged != null; }
        }
         [Display(AutoGenerateField = false)]
        [NotMapped]
        public virtual DirtyState DirtyState
        {
            get { return this._dirtyState; }
            set
            {
                bool valueChaned = !(this._dirtyState == value);
                this._dirtyState = value;
                if (valueChaned)
                {
                    this.NotifyPropertyChanged("DirtyState");
                    // this.NotifyPropertyChanged("IsPendingAdd");
                    //  this.NotifyPropertyChanged("IsPendingDelete");
                    this.NotifyPropertyChanged("IsDirty");

                }
            }
        }
          [Display(AutoGenerateField = false)]
        [NotMapped]
        public virtual object ClientTag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                this.NotifyPropertyChanged("ClientTag");
            }
        }
        public bool SerializeToFile()
        {
            throw new NotImplementedException();
        }

        public bool SerializeToFile(string outputDirectory)
        {
            throw new NotImplementedException();
        }

        public string SerializeToXml()
        {
            throw new NotImplementedException();
        }

        public MemoryStream SerializeToMemoryStream()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Constructor
        public VHObject()
            : this(DirtyState.UnChanged)
        {

        }
        public VHObject(DirtyState dirtyState) : this(false)
        {
            this._dirtyState = dirtyState;
        }
        public VHObject(bool propertyChangeEventDisabled)
            : base()
        {
            this._uniqueId = Helper.GetUniqueId();
            this._propertyChangedEventDisabled = propertyChangeEventDisabled;
        }
        #endregion
        #region Override

        #endregion
        #region IPSObject Members
        protected virtual bool Equals(VHObject obj)
        {
            if (obj != null)
            {
                return (string.Equals(this.UniqueId, obj.UniqueId));
            }
            return base.Equals(obj);
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as VHObject);
        }
        public override int GetHashCode()
        {
            if (String.IsNullOrEmpty(this.UniqueId))
                return Int32.MinValue;
            else
                return this.UniqueId.GetHashCode();
        }
        public virtual void MakeClean()
        {
            this.DirtyState = DirtyState.UnChanged;
        }
        #endregion
        #region Protected Methods
        protected virtual bool Set<T>(string propertyName, ref T propertyField, T newValue, Action changedCallback=null)
        {
            T oldValue = propertyField;
            propertyField = newValue;
            //20161212  如果字段是VHObjectList
         

            bool valuesDifferent = !object.Equals(oldValue, newValue);
            if (valuesDifferent && (!this.PropertyChangeEventDisabled || this.HasPropertyChangeListener))
            {
                if (!this.PropertyChangeEventDisabled && (this.DirtyState == DirtyState.UnChanged))
                {
                    //20131116注：这里更改后把DirtyState改为Modified，如果实类已是ADDED的话就不用更改
                //   if (propertyName!="DirtyState" &&propertyName!="ClientTag")//20131118注:加上一个判断,以免更改这个个属性是，会把DirtyState设置为Modified，比如客户端设置为Added后，程序却变成Modified了
                    {
                        this.DirtyState = DirtyState.Modified;
                    }
                  
                  //  this.OnPropertyChanged(propertyName, oldValue, propertyField);
                }
                this.OnPropertyChanged(propertyName, oldValue, propertyField);
                if (changedCallback!=null)
                {
                    changedCallback();
                }
            }
            return valuesDifferent;

        }
        protected virtual bool Set<T>(Expression<Func<T>> propertyExpression, ref T propertyField, T newValue, Action changedCallback = null)
        {
            string propertyName = GetPropertyName<T>(propertyExpression);
            return Set<T>(propertyName, ref propertyField, newValue,changedCallback);
        }


        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }
    
        #region 20160617在开发工单的打印次数里增加，更改通知，但不改DirtyState属性
        protected virtual void RaisePropertyChanged<T>(
         Expression<Func<T>> propertyExpression)

        {
            string propertyName = GetPropertyName<T>(propertyExpression);
            OnPropertyChanged(propertyName);


        }
        #endregion
        #endregion
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.OnPropertyChanged(propertyName);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propertyName);
            this.OnPropertyChanged(propertyChangedEventArgs);
        }
        protected virtual void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            PropertyNotificationEventArgs propertyNotificationEventArgs = new PropertyNotificationEventArgs(propertyName, oldValue, newValue);
            this.OnPropertyChanged(propertyNotificationEventArgs);
        }
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
            if (propertyChangedEventHandler != null)
            {
                propertyChangedEventHandler(this, propertyChangedEventArgs);
            }
        }
        #endregion
        #region IValidateToNavigate Members
         [Display(AutoGenerateField = false)]
        public virtual bool IsDirty
        {
            get { return (this.DirtyState != DirtyState.UnChanged); }
        }
        #endregion
        #region Factory Members



        #endregion
        #region IDataErrorInfo实现
         [Display(AutoGenerateField = false)]
       
        public string Error
        {
            get { return null; }
        }
      

        public string this[string columnName]
        {
            get { return this.ValidateProperty(this.GetType(), columnName); }
        }
        #endregion
       
    }
}
