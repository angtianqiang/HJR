using DevExpress.Data.Filtering;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ZtxFrameWork.Data;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    [POCOViewModel]
    public class EntityExpressionBuilder<TEntity> where TEntity : VHObject
    {

        public static EntityExpressionBuilder<TEntity> Create(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            return ViewModelSource.Create(() => new EntityExpressionBuilder<TEntity>(hiddenProperties, additionalProperties));
        }

        protected EntityExpressionBuilder(IEnumerable<string> hiddenProperties, IEnumerable<string> additionalProperties)
        {
            Token = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + (Guid.NewGuid().ToString("N"));
            this.DialogResult = false;
            AdvancedCriteria = CriteriaOperator.TryParse("");
            EntityType = typeof(TEntity);
            HiddenProperties = hiddenProperties;
            AdditionalProperties = additionalProperties;
            SetLoadData();
        }
        public virtual Expression<Func<TEntity, bool>> AdvancedExpression { get; set; }
        public virtual CriteriaOperator AdvancedCriteria { get; set; }
        //[ServiceProperty(Key = "CommMessageBox")]
        //public virtual IMessageBoxService MessageBoxService { get { return null; } }

        public virtual CriteriaOperator FilterCriteria { get; set; }


        public bool DialogResult { get; set; }
        #region 消息管理器的令牌 20170302
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();
        #endregion
        public Type EntityType { get; private set; }
        public IEnumerable<string> HiddenProperties { get; private set; }
        public IEnumerable<string> AdditionalProperties { get; private set; }
        public IMessageBoxService MessageBoxService
        {
            get { return ((DynamicQueryCollectionViewModel<TEntity>)((ISupportParentViewModel)this).ParentViewModel).MessageBoxService; }
        }


        public void Confirm()
        {
            if (this.DataValidate())
            {
                
                this.DialogResult = true;
                Messenger.Default.Send<string>("Confirm", "Confirm" + Token);
                this.BuilderExpression();
            }
        }

        public void Cancel()
        {
            this.DialogResult = false;
            Messenger.Default.Send<string>("Canel", "Cancel" + Token);
        }
        public void Clear()
        {
            AdvancedCriteria = CriteriaOperator.TryParse("");
            SetLoadData();
            // this.DialogResult = false;
            Messenger.Default.Send<string>("Clear", "Clear" + Token);
        }

        protected virtual bool DataValidate()
        { return true; }
        protected virtual void BuilderExpression()
        { }
        protected virtual void SetLoadData()
        { }
    }
}
