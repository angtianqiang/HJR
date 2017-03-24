using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.Utils;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    [POCOViewModel]
    public class SingleObjectViewModel<TEntity, TDbContext, TPrimaryKey> : ISingleObjectViewModel<TEntity, TPrimaryKey>, ISupportParameter, IDocumentContent
      where TEntity : class
where TDbContext : DbContext
    {

        object title;
        //  protected readonly Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc;
        protected readonly IDbFactory<TDbContext> dbFactory;
        protected readonly Func<TDbContext, DbSet<TEntity>> getDbSetFunc;
        protected readonly Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression;
        protected readonly Func<TEntity, object> getEntityDisplayNameFunc;
        Action<TEntity> entityInitializer;
        bool isEntityNewAndUnmodified;
        readonly Dictionary<string, IDocumentContent> lookUpViewModels = new Dictionary<string, IDocumentContent>();
        protected readonly TDbContext DB = null;
        //  readonly Uri IcoPath = null;
        /// <summary>
        /// Initializes a new instance of the SingleObjectViewModelBase class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create the unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns repository representing entities of a given type.</param>
        /// <param name="getEntityDisplayNameFunc">An optional parameter that provides a function to obtain the display text for a given entity. If ommited, the primary key value is used as a display text.</param>
        protected SingleObjectViewModel(IDbFactory<TDbContext> dbFactory, Func<TDbContext, DbSet<TEntity>> getDbSetFunc, Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression, Func<TEntity, object> getEntityDisplayNameFunc, string permissionTitle)
        {
            DB = dbFactory.CreateDbContext();

            this.dbFactory = dbFactory;
            this.getDbSetFunc = getDbSetFunc;
            this.getPrimaryKeyExpression = getPrimaryKeyExpression;
            this.getEntityDisplayNameFunc = getEntityDisplayNameFunc;
            this.PermissionTitle = permissionTitle;
            //  IcoPath= DevExpress.Utils.AssemblyHelper.GetResourceUri(typeof(DbFactory).Assembly, string.Format("Images/arpa.ico"));


            if (this.IsInDesignMode())
                this.Entity = this.DbSet.FirstOrDefault();
            else
                OnInitializeInRuntime();
        }

        /// <summary>
        /// The display text for a given entity used as a title in the corresponding view.
        /// </summary>
        /// <returns></returns>
        public object Title { get { return title; } }

        /// <summary>
        /// An entity represented by this view model.
        /// Since SingleObjectViewModelBase is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Entity { get; protected set; }

        /// <summary>
        /// Updates the Title property value and raises CanExecute changed for relevant commands.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the UpdateCommand property that can be used as a binding source in views.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public void Update()
        {
            isEntityNewAndUnmodified = false;
            UpdateTitle();
            UpdateCommands();
        }

        /// <summary>
        /// Saves changes in the underlying unit of work.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the SaveCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void Save()
        {
            SaveCore();
        }

        /// <summary>
        /// Determines whether entity has local changes that can be saved.
        /// Since SingleObjectViewModelBase is a POCO view model, this method will be used as a CanExecute callback for SaveCommand.
        /// </summary>
        public virtual bool CanSave()
        {
            if (this.IsInDesignMode()) return true;

            return Entity != null && !HasValidationErrors() && NeedSave() && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Edit;
        }

        /// <summary>
        /// Saves changes in the underlying unit of work and closes the corresponding view.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the SaveAndCloseCommand property that can be used as a binding source in views.
        /// </summary>
        [Command(CanExecuteMethodName = "CanSave")]
        public void SaveAndClose()
        {
            if (SaveCore())
                Close();
        }

        /// <summary>
        /// Saves changes in the underlying unit of work and create new entity.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the SaveAndNewCommand property that can be used as a binding source in views.
        /// </summary>
        [Command(CanExecuteMethodName = "CanSave")]
        public void SaveAndNew()
        {
            if (SaveCore())
            {
                CreateAndInitializeEntity(this.entityInitializer);
                UpdateCommands();
            }
        }

        /// <summary>
        /// Reset entity local changes.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the ResetCommand property that can be used as a binding source in views.
        /// </summary>
        [Display(Name = "Reset Changes")]
        public void Reset()
        {
            MessageResult confirmationResult = MessageBoxService.ShowMessage(CommonResources.Confirmation_Reset, CommonResources.Confirmation_Caption, MessageButton.OKCancel);
            if (confirmationResult == MessageResult.OK)
                Mouse.OverrideCursor = Cursors.Wait;
            Reload();
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Determines whether entity has local changes.
        /// Since SingleObjectViewModelBase is a POCO view model, this method will be used as a CanExecute callback for ResetCommand.
        /// </summary>
        public bool CanReset()
        {
            if (this.IsInDesignMode()) return true;

            return NeedReset();
        }
        public virtual string EntityDisplayName { get { return EntityDisplayNameHelper.GetEntityDisplayName(typeof(TEntity)); } }
        /// <summary>
        /// Deletes the entity, save changes and closes the corresponding view if confirmed by a user.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void Delete()
        {
            if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) != MessageResult.Yes)
                return;
            try
            {
                //OnBeforeEntityDeleted(PrimaryKey, Entity);
                //DbSet.Remove(Entity);
                //DB.SaveChanges();
                //TPrimaryKey primaryKeyForMessage = PrimaryKey;
                //TEntity entityForMessage = Entity;
                //Entity = null;
                //OnEntityDeleted(primaryKeyForMessage, entityForMessage);
                //Close();

                ///20170310 在新的CreateDbContext中执行，以支持事务，可在子类写关联表的操作
                using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                {
                //    DB.Entry(Entity).State = EntityState.Detached;
                 //   var newDB = dbFactory.CreateDbContext();
                 //   var newDbSet = getDbSetFunc(newDB);

                    OnBeforeEntityDeleted(DB, PrimaryKey, Entity);

                    DB.Entry(Entity).State = EntityState.Deleted;
                    DB.SaveChanges();
                    TPrimaryKey primaryKeyForMessage = PrimaryKey;
                    TEntity entityForMessage = Entity;
                    Entity = null;
                    OnEntityDeleted(DB, primaryKeyForMessage, entityForMessage);

                    ts.Complete();
                    Close();
                }
            }
            catch (DbException e)
            {
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }

        /// <summary>
        /// Determines whether the entity can be deleted.
        /// Since SingleObjectViewModelBase is a POCO view model, this method will be used as a CanExecute callback for DeleteCommand.
        /// </summary>
        public virtual bool CanDelete()
        {
            if (this.IsInDesignMode()) return true;
           
            bool temp = Entity != null && !IsNew() && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Delete;
            try
            {
                temp = temp && ((dynamic)Entity).状态 == "N";
            }
            catch { }
            return temp;
        }

        /// <summary>
        /// Closes the corresponding view.
        /// Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the CloseCommand property that can be used as a binding source in views.
        /// </summary>
        public void Close()
        {
            if (!TryClose())
                return;
            if (DocumentOwner != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                DocumentOwner.Close(this);
                Mouse.OverrideCursor = null;
            }
        }



        protected virtual bool SaveCore()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                if (HasValidationErrors())
                {
                    var errorText = Data.Common.IDataErrorInfoHelper.GetErrorText(Entity, "");
                    Mouse.OverrideCursor = null;
                    MessageBoxService.ShowMessage(errorText, CommonResources.Exception_ValidationErrorCaption, MessageButton.OK, MessageIcon.Error);                
                    return false; 
                }
                bool isNewEntity = IsNew();
                //if (!isNewEntity)
                //{
                //    ///   GetSetValueActionExpression(getPropertyExpression).Compile()
                //    //     Repository.SetPrimaryKey(Entity, PrimaryKey);
                //    //   DbSet.Update(Entity);
                //}
                //OnBeforeEntitySaved(PrimaryKey, Entity, isNewEntity);
                //DB.SaveChanges();
                //PrimaryKey = this.GetPrimaryKey(Entity);
                //LoadEntityByKey(PrimaryKey);
                //OnEntitySaved(PrimaryKey, Entity, isNewEntity);
                //Mouse.OverrideCursor = null;
                //return true;

                ///20170310 在新的CreateDbContext中执行，以支持事务，可在子类写关联表的操作
                using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                {
                   // DB.Entry(Entity).State = EntityState.Detached;
                 //   var newDB = dbFactory.CreateDbContext();
                  //  var newDbSet = getDbSetFunc(newDB);

                    OnBeforeEntitySaved(DB, PrimaryKey, Entity, isNewEntity);
                    if (isNewEntity)
                    {
                        DB.Entry(Entity).State = EntityState.Added;
                    }
                    else
                    {
                        DB.Entry(Entity).State = EntityState.Modified;
                    }

                    DB.SaveChanges();
                    PrimaryKey = this.GetPrimaryKey(Entity);
                    LoadEntityByKey(PrimaryKey);
                    OnEntitySaved(DB, PrimaryKey, Entity, isNewEntity);

                    ts.Complete();
                }
                Mouse.OverrideCursor = null;
                return true;

            }
            catch (DbException e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
                return false;
            }
            catch (DbUpdateException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);

                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
                return false;
            }
            catch (DbEntityValidationException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);

                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
                return false;
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.Message, CommonResources.Exception_UpdateErrorCaption, MessageButton.OK, MessageIcon.Error);
                return false;
            }
            //   finally { Mouse.OverrideCursor = null; }

        }

        protected virtual void OnBeforeEntitySaved(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity, bool isNewEntity) { }

        protected virtual void OnEntitySaved(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity, bool isNewEntity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, isNewEntity ? EntityMessageType.Added : EntityMessageType.Changed));
        }

        protected virtual void OnBeforeEntityDeleted(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityDeleted(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Deleted));
        }

        protected virtual void OnInitializeInRuntime()
        {
            Messenger.Default.Register<EntityMessage<TEntity, TPrimaryKey>>(this, x => OnEntityMessage(x));
            Messenger.Default.Register<SaveAllMessage>(this, x => Save());
            Messenger.Default.Register<CloseAllMessage>(this, x => OnClosing(x));
        }

        protected virtual void OnEntityMessage(EntityMessage<TEntity, TPrimaryKey> message)
        {
            if (Entity == null) return;
            if (message.MessageType == EntityMessageType.Deleted && object.Equals(message.PrimaryKey, PrimaryKey))
                Close();
        }

        protected virtual void OnEntityChanged()
        {
            if (Entity != null && this.HasPrimaryKey(Entity))
            {
                PrimaryKey = GetPrimaryKey(Entity);
                RefreshLookUpCollections(true);
            }
            Update();
        }

        protected DbSet<TEntity> DbSet { get { return getDbSetFunc(DB); } }

        protected TPrimaryKey PrimaryKey { get; private set; }

        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        protected virtual void OnParameterChanged(object parameter)
        {
            var initializer = parameter as Action<TEntity>;
            if (initializer != null)
                CreateAndInitializeEntity(initializer);
            else if (parameter is TPrimaryKey)
                LoadEntityByKey((TPrimaryKey)parameter);
            else
                Entity = null;
        }

        protected virtual TEntity CreateEntity()
        {
            TEntity newEntity = DbSet.Create();
            DbSet.Add(newEntity);
            return newEntity;
        }

        protected void Reload()
        {
            if (Entity == null || IsNew())
                CreateAndInitializeEntity(this.entityInitializer);
            else
                LoadEntityByKey(PrimaryKey);
        }

        protected void CreateAndInitializeEntity(Action<TEntity> entityInitializer)
        {

            this.entityInitializer = entityInitializer;
            var entity = CreateEntity();
            if (this.entityInitializer != null)
                this.entityInitializer(entity);
            this.RaisePropertyChanged(t => t.IsAddModel);//20170303 是否显示“保存并新建"
            Entity = entity;
            isEntityNewAndUnmodified = true;
        }

        protected void LoadEntityByKey(TPrimaryKey primaryKey)
        {

            if (Entity == null )
            {
                Entity = DbSet.Find(primaryKey);
            }
            else
            {
                DB.Entry(Entity).Reload();
                Entity = DbSet.Find(primaryKey);
            }

        }



        void UpdateTitle()
        {
            if (Entity == null)
                title = null;
            else if (IsNew())
                title = GetTitleForNewEntity();
            else
                title = GetTitle(GetState() == EntityState.Modified);
            this.RaisePropertyChanged(x => x.Title);
        }

        protected virtual void UpdateCommands()
        {
            this.RaiseCanExecuteChanged(x => x.Save());
            this.RaiseCanExecuteChanged(x => x.SaveAndClose());
            this.RaiseCanExecuteChanged(x => x.SaveAndNew());
            this.RaiseCanExecuteChanged(x => x.Delete());
            this.RaiseCanExecuteChanged(x => x.Reset());

            this.RaiseCanExecuteChanged(x => x.Confirm());
            this.RaiseCanExecuteChanged(x => x.UnConfirm());
            this.RaiseCanExecuteChanged(x => x.Audit());
            this.RaiseCanExecuteChanged(x => x.UnAudit());
            UpdateIsReadOnly();
        }

        protected IDocumentOwner DocumentOwner { get; private set; }

        protected virtual void OnDestroy()
        {
            Messenger.Default.Unregister(this);
            RefreshLookUpCollections(false);
        }

        protected virtual bool TryClose()
        {
            if (HasValidationErrors())
            {
                MessageResult warningResult = MessageBoxService.ShowMessage(CommonResources.Warning_SomeFieldsContainInvalidData, CommonResources.Warning_Caption, MessageButton.OKCancel);
                return warningResult == MessageResult.OK;
            }
            if (!NeedReset()) return true;
            MessageResult result = MessageBoxService.ShowMessage(CommonResources.Confirmation_Save, GetConfirmationMessageTitle(), MessageButton.YesNoCancel);
            if (result == MessageResult.Yes)
                return SaveCore();
            return result != MessageResult.Cancel;
        }

        protected virtual void OnClosing(CloseAllMessage message)
        {
            if (!message.Cancel)
                message.Cancel = !TryClose();
        }

        protected virtual string GetConfirmationMessageTitle()
        {
            return GetTitle();
        }

        protected bool IsNew()
        {
            return GetState() == EntityState.Added;
        }

        protected virtual bool NeedSave()
        {
            //20170208
            return DB.ChangeTracker.HasChanges();

            //if (Entity == null)
            //    return false;
            //EntityState state = GetState();
            //return state == EntityState.Modified || state == EntityState.Added;
        }

        protected virtual bool NeedReset()
        {
            return NeedSave() && !isEntityNewAndUnmodified;
        }

        protected virtual bool HasValidationErrors()
        {
            IDataErrorInfo dataErrorInfo = Entity as IDataErrorInfo;
            return dataErrorInfo != null && IDataErrorInfoHelper.HasErrors(dataErrorInfo);
        }

        string GetTitle(bool entityModified)
        {
            return GetTitle() + (entityModified ? CommonResources.Entity_Changed : string.Empty);
        }

        protected virtual string GetTitleForNewEntity()
        {
            return typeof(TEntity).Name + CommonResources.Entity_New;
        }

        protected virtual string GetTitle()
        {


            return (typeof(TEntity).Name + " - " + Convert.ToString(getEntityDisplayNameFunc != null ? getEntityDisplayNameFunc(Entity) : PrimaryKey))
   .Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }

        protected EntityState GetState()
        {
            //try
            //{
            return DB.Entry(Entity).State;
            //}
            //catch (InvalidOperationException)
            //{
            //    Repository.SetPrimaryKey(Entity, PrimaryKey);

            //    return Repository.GetState(Entity);
            //}

        }

        #region look up and detail view models
        protected virtual void RefreshLookUpCollections(bool raisePropertyChanged)
        {
            var values = lookUpViewModels.ToArray();
            lookUpViewModels.Clear();
            foreach (var item in values)
            {
                item.Value.OnDestroy();
                if (raisePropertyChanged)
                    ((IPOCOViewModel)this).RaisePropertyChanged(item.Key);
            }
        }


        #endregion

        #region ISupportParameter
        object ISupportParameter.Parameter
        {
            get { return null; }
            set { OnParameterChanged(value); }
        }
        #endregion

        #region IDocumentContent
        object IDocumentContent.Title { get { return Title; } }

        void IDocumentContent.OnClose(CancelEventArgs e)
        {
            e.Cancel = !TryClose();
        }

        void IDocumentContent.OnDestroy()
        {
            OnDestroy();
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion

        #region ISingleObjectViewModel
        TEntity ISingleObjectViewModel<TEntity, TPrimaryKey>.Entity { get { return Entity; } }

        TPrimaryKey ISingleObjectViewModel<TEntity, TPrimaryKey>.PrimaryKey { get { return PrimaryKey; } }
        #endregion

        #region 20170209




        public virtual User CurrentUser { get; set; } = User.CurrentUser;



        protected bool HasPrimaryKey(TEntity Entity)
        {
            return ExpressionHelper.GetHasValueFunctionExpression(getPrimaryKeyExpression).Compile()(Entity);
        }
        protected void SetPrimaryKey(TEntity Entity, TPrimaryKey PrimaryKey)
        {
            ExpressionHelper.GetSetValueActionExpression(getPrimaryKeyExpression).Compile()(Entity, PrimaryKey);
        }
        protected TPrimaryKey GetPrimaryKey(TEntity Entity)
        {

            return getPrimaryKeyExpression.Compile()(Entity);
        }
        #endregion
        #region 消息管理器的令牌 20170302
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();
        #endregion
        #region 20170303 添加一个字段来标识是不是新增模式，是否显示菜单项“保存并新建”
        public virtual bool IsAddModel
        {
            get { return this.entityInitializer != null; }
        }
        #endregion

        #region 20170304 打印 打印预览 报表设计

        protected virtual string GetReportPath()
        {
            return System.Environment.CurrentDirectory + @"\Reports\" + (PermissionTitle) + "Report.repx";
        }
        protected XtraReport CreateReport()
        {
            var path = GetReportPath();
            return System.IO.File.Exists(path) ? XtraReport.FromFile(path, true) : new XtraReport();

        }
        protected void SetReportDataSource(XtraReport report)
        {
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            objectDataSource.DataSource = this.Entity;
            report.DataSource = objectDataSource;
        }
        public virtual void ReportPrint()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            report.CreateDocument();
            report.Print();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportPrint()
        {
            if (this.IsInDesignMode()) return true;

            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Print;
        }
        public virtual void ReportPreview()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            report.CreateDocument();

            report.ShowPreview();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportPreview()
        {
            if (this.IsInDesignMode()) return true;

            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Preview;
        }
        public virtual void ReportDesigner()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            // report.CreateDocument();

            report.ShowDesigner();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportDesigner()
        {
            if (this.IsInDesignMode()) return true;

            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Design;
        }
        #endregion



        #region 20170310 生效审核权限实现
        public virtual void Confirm()
        {
            if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Confirm, EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) != MessageResult.Yes)
                return;
            bool IsRunOK = false;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ///20170310 在新的CreateDbContext中执行，以支持事务，可在子类写关联表的操作
                using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                {
                    var newDB = dbFactory.CreateDbContext();
                    var newDbSet = getDbSetFunc(newDB);
                    var newEntity = newDbSet.Find(this.GetPrimaryKey(Entity));
                    if (((dynamic)newEntity).状态 != "N")
                    {
                        throw new Exception("当前状态不可操作，可能其它用户已更改了单据状态！");
                    }
                    OnBeforeEntityConfirmed(newDB, PrimaryKey, newEntity);                   
                    ((dynamic)newEntity).状态 = "Y";
                    newDB.Entry(newEntity).State = EntityState.Modified;
                    newDB.SaveChanges();
                 //   PrimaryKey = this.GetPrimaryKey(newEntity);
                   // LoadEntityByKey(PrimaryKey);
                    OnEntityEntityConfirmed(newDB, PrimaryKey, newEntity);
                    IsRunOK = true;
                    ts.Complete();

                }
                LoadEntityByKey(PrimaryKey);
                UpdateCommands();
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage("生效成功！");
              
            }
            catch (DbException e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);

            }
            catch (DbUpdateException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (DbEntityValidationException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.Message, CommonResources.Exception_UpdateErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            finally
            {

                //if (IsRunOK == false) LoadEntityByKey(this.GetPrimaryKey(Entity));
            }
        }
        public virtual bool CanConfirm()
        {
            if (this.IsInDesignMode())
            {
                return true;
            }
            bool temp = !DB.ChangeTracker.HasChanges() && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Confirm;
            try
            {
                temp = temp && ((dynamic)Entity).状态 == "N";
            }
            catch { }
            return temp;
        }
        public virtual void UnConfirm()
        {
            if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_UnConfirm, EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) != MessageResult.Yes)
                return;
            bool IsRunOK = false;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ///20170310 在新的CreateDbContext中执行，以支持事务，可在子类写关联表的操作
                using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                {
                    var newDB = dbFactory.CreateDbContext();
                    var newDbSet = getDbSetFunc(newDB);
                    var newEntity = newDbSet.Find(this.GetPrimaryKey(Entity));
                    if (((dynamic)newEntity).状态 != "Y")
                    {
                        throw new Exception("当前状态不可操作，可能其它用户已更改了单据状态！");
                    }
                    OnBeforeEntityUnConfirmed(newDB, PrimaryKey, newEntity);
                    ((dynamic)newEntity).状态 = "N";
                    newDB.Entry(newEntity).State = EntityState.Modified;
                    newDB.SaveChanges();
                  //  PrimaryKey = this.GetPrimaryKey(newEntity);
                 //   LoadEntityByKey(PrimaryKey);
                    OnEntityEntityUnConfirmed(newDB, PrimaryKey, newEntity);
                    IsRunOK = true;
                    ts.Complete();
                }
                LoadEntityByKey(PrimaryKey);
                UpdateCommands();
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage("失效成功！");
              
            }
            catch (DbException e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);

            }
            catch (DbUpdateException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (DbEntityValidationException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.Message, CommonResources.Exception_UpdateErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            finally
            {

                //if (IsRunOK == false) LoadEntityByKey(this.GetPrimaryKey(Entity));
            }
        }
        public virtual bool CanUnConfirm()
        {
            if (this.IsInDesignMode()) return true;

            bool temp = !DB.ChangeTracker.HasChanges() && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Confirm;
            try
            {
                temp = temp && ((dynamic)Entity).状态 == "Y";
            }
            catch { }
            return temp;
        }
        public virtual void Audit()
        {
            if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Audit, EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) != MessageResult.Yes)
                return;
            bool IsRunOK = false;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ///20170310 在新的CreateDbContext中执行，以支持事务，可在子类写关联表的操作
                using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                {
                    var newDB = dbFactory.CreateDbContext();
                    var newDbSet = getDbSetFunc(newDB);
                    var newEntity = newDbSet.Find(this.GetPrimaryKey(Entity));
                    if (((dynamic)newEntity).状态 != "Y")
                    {
                        throw new Exception("当前状态不可操作，可能其它用户已更改了单据状态！");
                    }
                    OnBeforeEntityAudited(newDB, PrimaryKey, newEntity);
                    ((dynamic)newEntity).状态 = "Z";
                    newDB.Entry(newEntity).State = EntityState.Modified;
                    newDB.SaveChanges();
                   // PrimaryKey = this.GetPrimaryKey(newEntity);
                 //   LoadEntityByKey(PrimaryKey);
                    OnEntityEntityAudited(newDB, PrimaryKey, newEntity);
                    IsRunOK = true;
                    ts.Complete();
                }
                LoadEntityByKey(PrimaryKey);
                UpdateCommands();
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage("审核成功！");
               
            }
            catch (DbException e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);

            }
            catch (DbUpdateException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (DbEntityValidationException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.Message, CommonResources.Exception_UpdateErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            finally
            {

                //if (IsRunOK == false) LoadEntityByKey(this.GetPrimaryKey(Entity));
            }
        }
        public virtual bool CanAudit()
        {
            if (this.IsInDesignMode()) return true;

            bool temp = !DB.ChangeTracker.HasChanges() && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Confirm;
            try
            {
                temp = temp && ((dynamic)Entity).状态 == "Y";
            }
            catch { }
            return temp;
        }
        public virtual void UnAudit()
        {
            if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_UnAudit, EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) != MessageResult.Yes)
                return;
            bool IsRunOK = false;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ///20170310 在新的CreateDbContext中执行，以支持事务，可在子类写关联表的操作
                using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                {
                    var newDB = dbFactory.CreateDbContext();
                    var newDbSet = getDbSetFunc(newDB);
                    var newEntity = newDbSet.Find(this.GetPrimaryKey(Entity));
                    if (((dynamic)newEntity).状态 != "Z")
                    {
                        throw new Exception("当前状态不可操作，可能其它用户已更改了单据状态！");
                    }
                    OnBeforeEntityUnAudited(newDB, PrimaryKey, newEntity);
                    ((dynamic)newEntity).状态 = "Y";
                    newDB.Entry(newEntity).State = EntityState.Modified;
                    newDB.SaveChanges();
                   // PrimaryKey = this.GetPrimaryKey(newEntity);
                  //  LoadEntityByKey(PrimaryKey);
                    OnEntityEntityUnAudited(newDB, PrimaryKey, newEntity);
                    IsRunOK = true;
                    ts.Complete();
                }
                LoadEntityByKey(PrimaryKey);
                UpdateCommands();
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage("取消审核成功！");
              
            }
            catch (DbException e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);

            }
            catch (DbUpdateException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (DbEntityValidationException e)
            {
                Mouse.OverrideCursor = null;
                var dbExprtion = Comm.DataModel.DbExceptionsConverter.Convert(e);
                MessageBoxService.ShowMessage(dbExprtion.ErrorMessage, dbExprtion.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                MessageBoxService.ShowMessage(e.Message, CommonResources.Exception_UpdateErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            finally
            {

                //if (IsRunOK == false) LoadEntityByKey(this.GetPrimaryKey(Entity));
            }
        }
        public virtual bool CanUnAudit()
        {
            if (this.IsInDesignMode()) return true;

            bool temp = !DB.ChangeTracker.HasChanges() && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Confirm;
            try
            {
                temp = temp && ((dynamic)Entity).状态 == "Z";
            }
            catch { }
            return temp;
        }


        protected virtual void OnBeforeEntityConfirmed(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityEntityConfirmed(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Changed));
        }
        protected virtual void OnBeforeEntityUnConfirmed(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityEntityUnConfirmed(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Changed));
        }
        protected virtual void OnBeforeEntityAudited(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityEntityAudited(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Changed));
        }
        protected virtual void OnBeforeEntityUnAudited(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityEntityUnAudited(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Changed));
        }

        #endregion

        #region 20170311  权限标识
        public virtual string PermissionTitle { get; set; }//权限标识
        #endregion
        #region 20170318 从子类抽象

        public virtual bool IsReadOnly { get; set; }//根据单据的生效状态和权限判断是否可编辑
        public virtual void UpdateIsReadOnly()
        {
            try
            {
                if (((dynamic)Entity).状态 == "N")
                {
                    IsReadOnly = false;
                }
                else
                {
                    IsReadOnly = true;
                }
            }
            catch
            { }

        }

        protected IDocumentManagerService QueryListManagerService { get { return this.GetRequiredService<IDocumentManagerService>("QueryListDocumentManagerService"); } }
        //colleciton页面的服务  用于打开其它的页面，比如在入库单界面上导航到收款单上
        protected virtual IDocumentManagerService GetDocumentManagerService() {

            //return this.GetRequiredService<IDocumentManagerService>("SignleObjectDocumentManagerService");

            var tokenValue = App.SystemConfigs.Where(t => t.Token == "ViewOpenMode").Single().TokenValue;
            return tokenValue == "0" ? this.GetService<IDocumentManagerService>("SignleObjectDocumentManagerService") : this.GetService<IDocumentManagerService>("TabbedDocumentManagerService");
        }


        #endregion

      
    }
}
