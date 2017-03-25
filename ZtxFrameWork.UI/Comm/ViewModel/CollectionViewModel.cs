using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.Utils;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    /// <summary>
    /// The base class for a POCO view models exposing a colection of entities of a given type and CRUD operations against these entities.
    /// This is a partial class that provides extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public partial class CollectionViewModel<TEntity, TDbContext, TPrimaryKey> : CollectionViewModel<TEntity, TEntity, TDbContext, TPrimaryKey>
        where TEntity : class
        where TDbContext : DbContext
    {

        /// <summary>
        /// Creates a new instance of CollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        public static CollectionViewModel<TEntity, TDbContext, TPrimaryKey> CreateCollectionViewModel(
               IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression,
            Action<TEntity> newEntityInitializer = null,
   bool ignoreSelectEntityMessage = false, string permissionTitle = "")
        {
            return ViewModelSource.Create(() => new CollectionViewModel<TEntity, TDbContext, TPrimaryKey>(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression, newEntityInitializer, ignoreSelectEntityMessage,permissionTitle));
        }

        /// <summary>
        /// Initializes a new instance of the CollectionViewModel class.
        /// This constructor is declared protected to avoid an undesired instantiation of the CollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        protected CollectionViewModel(
              IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression,
            Action<TEntity> newEntityInitializer = null,
   bool ignoreSelectEntityMessage = false,string permissionTitle=""
            ) : base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression, newEntityInitializer, ignoreSelectEntityMessage,permissionTitle)
        {
        }
    }

    /// <summary>
    /// The base class for a POCO view models exposing a collection of entities of a given type and CRUD operations against these entities. 
    /// This is a partial class that provides extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public partial class CollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey> : CollectionViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey>
       where TEntity : class
        where TProjection : class
        where TDbContext : DbContext
    {

        /// <summary>
        /// Creates a new instance of CollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        public static CollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey> CreateProjectionCollectionViewModel(
          IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression,
            Action<TEntity> newEntityInitializer = null,
            bool ignoreSelectEntityMessage = false, string permissionTitle = "")
        {
            return ViewModelSource.Create(() => new CollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey>(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression, newEntityInitializer, ignoreSelectEntityMessage,permissionTitle));
        }

        /// <summary>
        /// Initializes a new instance of the CollectionViewModel class.
        /// This constructor is declared protected to avoid an undesired instantiation of the CollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        protected CollectionViewModel(
            IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression,
            Action<TEntity> newEntityInitializer = null,
            bool ignoreSelectEntityMessage = false, string permissionTitle = ""
            ) : base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression, newEntityInitializer, ignoreSelectEntityMessage,permissionTitle)
        {
        }
    }

    /// <summary>
    /// The base class for POCO view models exposing a collection of entities of a given type and CRUD operations against these entities.
    /// It is not recommended to inherit directly from this class. Use the CollectionViewModel class instead.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public abstract class CollectionViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey> : ReadOnlyCollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey>
        where TEntity : class
        where TProjection : class
        where TDbContext : DbContext
    {

        EntitiesChangeTracker<TPrimaryKey> ChangeTrackerWithKey { get { return (EntitiesChangeTracker<TPrimaryKey>)ChangeTracker; } }
        readonly Action<TEntity> newEntityInitializer;
        // IRepository<TEntity, TPrimaryKey> Repository { get { return (IRepository<TEntity, TPrimaryKey>)ReadOnlyRepository; } }

        /// <summary>
        /// Initializes a new instance of the CollectionViewModelBase class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        /// <param name="newEntityInitializer">A function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="ignoreSelectEntityMessage">A parameter used to specify whether the selected entity should be managed by PeekCollectionViewModel.</param>
        protected CollectionViewModelBase(
            IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression,
            Action<TEntity> newEntityInitializer,
            bool ignoreSelectEntityMessage, string permissionTitle 
            ) : base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression)
        {
            VerifyProjectionType();
            this.newEntityInitializer = newEntityInitializer;
            this.ignoreSelectEntityMessage = ignoreSelectEntityMessage;
            PermissionTitle = permissionTitle;
            if (!this.IsInDesignMode())
                RegisterSelectEntityMessage();
        }

        /// <summary>
        /// Creates and shows a document that contains a single object view model for new entity.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the NewCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void New()
        {
          Mouse.OverrideCursor = Cursors.Wait; 
            GetDocumentManagerService().ShowNewEntityDocument(this, newEntityInitializer);
        }
       
 public virtual bool CanNew()
        {
            if (this.IsInDesignMode()) return true;

            return User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle ).Add; }
        /// <summary>
        /// Creates and shows a document that contains a single object view model for the existing entity.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the EditCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">Entity to edit.</param>
        public virtual void Edit(TProjection projectionEntity)
        {
            if (this.IsDetached(projectionEntity))
                return;
            Mouse.OverrideCursor = Cursors.Wait;
            TPrimaryKey primaryKey = this.GetProjectionPrimaryKey(projectionEntity);
            int index = Entities.IndexOf(projectionEntity);
            projectionEntity = ChangeTrackerWithKey.FindActualProjectionByKey(primaryKey);
            if (index >= 0)
            {
                if (projectionEntity == null)
                    Entities.RemoveAt(index);
                else
                    Entities[index] = projectionEntity;
            }
            if (projectionEntity == null)
            {
                DestroyDocument(GetDocumentManagerService().FindEntityDocument<TEntity, TPrimaryKey>(primaryKey));
                return;
            }
            GetDocumentManagerService().ShowExistingEntityDocument<TEntity, TPrimaryKey>(this, primaryKey);
        }

        /// <summary>
        /// Determines whether an entity can be edited.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for EditCommand.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual bool CanEdit(TProjection projectionEntity)
        {
            if (this.IsInDesignMode()) return true;

            return projectionEntity != null && !IsLoading && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Edit;
        }

        /// <summary>
        /// Deletes a given entity from the repository and saves changes if confirmed by the user.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual void Delete(TProjection projectionEntity)
        {
            if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, typeof(TEntity).Name), CommonResources.Confirmation_Caption, MessageButton.YesNo) != MessageResult.Yes)
                return;
            try
            {
                //Entities.Remove(projectionEntity);
                //TPrimaryKey primaryKey = this.GetProjectionPrimaryKey(projectionEntity);
                //TEntity entity = this.ReadOnlyDbSet.Find(primaryKey);
                //if (entity != null)
                //{
                //    OnBeforeEntityDeleted(primaryKey, entity);
                //    this.ReadOnlyDbSet.Remove(entity);
                //  this.DB.SaveChanges();
                //    OnEntityDeleted(primaryKey, entity);
                //}



                Entities.Remove(projectionEntity);
                TPrimaryKey primaryKey = this.GetProjectionPrimaryKey(projectionEntity);
                var newDB = dbFactory.CreateDbContext();
                var newDbSet = getDbSetFunc(newDB);

                TEntity entity = newDbSet.Find(primaryKey);
                if (entity != null)
                {
                    using (var ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required))
                    {
                       
                        OnBeforeEntityDeleted(newDB,primaryKey, entity);

                        newDB.Entry(entity).State = EntityState.Deleted;
                        newDB.SaveChanges();
                        OnEntityDeleted(newDB,primaryKey, entity);

                        ts.Complete();
                    }
                      
                }


            }
            catch (DbException e)
            {
                Refresh();
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception e)
            {
                Refresh();
                MessageBoxService.ShowMessage(App.GetAllError(e), "错误", MessageButton.OK, MessageIcon.Error);
            }
        }

        /// <summary>
        /// Determines whether an entity can be deleted.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for DeleteCommand.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual bool CanDelete(TProjection projectionEntity)
        {
            if (this.IsInDesignMode()) return true;

            bool temp = projectionEntity != null && !IsLoading && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Delete;
            try
            {
                temp = temp && ((dynamic)projectionEntity).状态 == "N";
            }
            catch { }
            return temp;
        }

        /// <summary>
        /// Saves the given entity.
        /// Since CollectionViewModelBase is a POCO view model, the instance of this class will also expose the SaveCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to save.</param>
        [Display(AutoGenerateField = false)]
        public virtual void Save(TProjection projectionEntity)
        {
            TPrimaryKey primaryKey = this.GetProjectionPrimaryKey(projectionEntity);
            TEntity entity = this.ReadOnlyDbSet.Find(primaryKey);
            if (typeof(TProjection) != typeof(TEntity))
                ApplyProjectionPropertiesToEntity(projectionEntity, entity);
            try
            {
                OnBeforeEntitySaved(primaryKey, entity);
                this.DB.SaveChanges();
                OnEntitySaved(primaryKey, entity);
            }
            catch (DbException e)
            {
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }

        /// <summary>
        /// Determines whether entity local changes can be saved.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for SaveCommand.
        /// </summary>
        /// <param name="projectionEntity">An entity to save.</param>
        public virtual bool CanSave(TProjection projectionEntity)
        {
            if (this.IsInDesignMode()) return true;

            return projectionEntity != null && !IsLoading && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Edit;
        }

        /// <summary>
        /// Notifies that SelectedEntity has been changed by raising the PropertyChanged event.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the UpdateSelectedEntityCommand property that can be used as a binding source in views.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual void UpdateSelectedEntity()
        {
            this.RaisePropertyChanged(x => x.SelectedEntity);
        }

        /// <summary>
        /// Closes the corresponding view.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the CloseCommand property that can be used as a binding source in views.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public void Close()
        {
            if (DocumentOwner != null)
                Mouse.OverrideCursor = Cursors.Wait;

            DocumentOwner.Close(this);
            Mouse.OverrideCursor = null;

        }

        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        protected virtual IDocumentManagerService GetDocumentManagerService()
        {



            var tokenValue = App.SystemConfigs.Where(t => t.Token == "ViewOpenMode").Single().TokenValue;
            return tokenValue == "0" ? this.GetService<IDocumentManagerService>("SignleObjectDocumentManagerService") : this.GetService<IDocumentManagerService>("TabbedDocumentManagerService");
        }
        protected virtual void OnBeforeEntityDeleted(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityDeleted(TDbContext dbContext, TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Deleted));
        }

        protected override Func<TProjection> GetSelectedEntityCallback()
        {
            var entity = SelectedEntity;
            return () => FindLocalProjectionWithSameKey(entity);
        }

        TProjection FindLocalProjectionWithSameKey(TProjection projectionEntity)
        {
            bool primaryKeyAvailable = projectionEntity != null && this.ProjectionHasPrimaryKey(projectionEntity);
            return primaryKeyAvailable ? ChangeTrackerWithKey.FindLocalProjectionByKey(this.GetProjectionPrimaryKey(projectionEntity)) : null;
        }

        protected virtual void OnBeforeEntitySaved(TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntitySaved(TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Changed));
        }

        protected virtual void ApplyProjectionPropertiesToEntity(TProjection projectionEntity, TEntity entity)
        {
            throw new NotImplementedException("Override this method in the collection view model class and apply projection properties to the entity so that it can be correctly saved by unit of work.");
        }

        protected override void OnSelectedEntityChanged()
        {
            base.OnSelectedEntityChanged();
            UpdateCommands();
        }

        protected override void RestoreSelectedEntity(TProjection existingProjectionEntity, TProjection newProjectionEntity)
        {
            base.RestoreSelectedEntity(existingProjectionEntity, newProjectionEntity);
            if (ReferenceEquals(SelectedEntity, existingProjectionEntity))
                SelectedEntity = newProjectionEntity;
        }

        protected override void OnIsLoadingChanged()
        {
            base.OnIsLoadingChanged();
            UpdateCommands();
            if (!IsLoading)
                RequestSelectedEntity();
        }

        void UpdateCommands()
        {
            TProjection projectionEntity = null;
            this.RaiseCanExecuteChanged(x => x.Edit(projectionEntity));
            this.RaiseCanExecuteChanged(x => x.Delete(projectionEntity));
            this.RaiseCanExecuteChanged(x => x.Save(projectionEntity));
        }

        protected void DestroyDocument(IDocument document)
        {
            if (document != null)
                document.Close();
        }

        //protected IRepository<TEntity, TPrimaryKey> CreateRepository()
        //{
        //    return (IRepository<TEntity, TPrimaryKey>)CreateReadOnlyRepository();
        //}

        protected override IEntitiesChangeTracker CreateEntitiesChangeTracker()
        {
            return new EntitiesChangeTracker<TPrimaryKey>(this);
        }

        void VerifyProjectionType()
        {
            string primaryKeyPropertyName = this.GetPrimaryKeyPropertyName();
            if (TypeDescriptor.GetProperties(typeof(TProjection))[primaryKeyPropertyName] == null)
                throw new ArgumentException(string.Format("Projection type {0} should have primary key property {1}", typeof(TProjection).Name, primaryKeyPropertyName), "TProjection");
        }

        #region SelectEntityMessage
        protected class SelectEntityMessage
        {
            public SelectEntityMessage(TPrimaryKey primaryKey)
            {
                PrimaryKey = primaryKey;
            }
            public TPrimaryKey PrimaryKey { get; private set; }
        }

        protected class SelectedEntityRequest { }

        readonly bool ignoreSelectEntityMessage;

        void RegisterSelectEntityMessage()
        {
            if (!ignoreSelectEntityMessage)
                Messenger.Default.Register<SelectEntityMessage>(this, x => OnSelectEntityMessage(x));
        }

        void RequestSelectedEntity()
        {
            if (!ignoreSelectEntityMessage)
                Messenger.Default.Send(new SelectedEntityRequest());
        }

        void OnSelectEntityMessage(SelectEntityMessage message)
        {
            if (!IsLoaded)
                return;
            var projectionEntity = ChangeTrackerWithKey.FindActualProjectionByKey(message.PrimaryKey);
            if (projectionEntity == null)
            {
                FilterExpression = null;
                projectionEntity = ChangeTrackerWithKey.FindActualProjectionByKey(message.PrimaryKey);
            }
            SelectedEntity = projectionEntity;
        }
        #endregion



        #region 20170227生成最新的单号
        static public string GetNewCode(string prefix, IDbFactory<TDbContext> dbFactory,
              Func<TDbContext, DbSet<TEntity>> getDbSetFunc, Expression<Func<TEntity, string>> getCodeKeyExpression)
        {
            string str = prefix + DateTime.Now.ToString("yyyyMMdd");
            var columnName = ExpressionHelper.GetPropertyName(getCodeKeyExpression);
            var fun = getCodeKeyExpression.Compile();
            int k = 1;
            //   Expression<Func<Expression<Func<TEntity, string>>, bool>> dd = null;
            PropertyInfo propertyInfo = typeof(TEntity).GetProperty(columnName);
            //try
            //{
            var dbSet = getDbSetFunc(dbFactory.CreateDbContext());
            var list = ETForStartsWith<TEntity>(dbSet, str, propertyInfo).ToList();
            if (list != null && list.Count > 0)
            {
                k = list.Select(t => Convert.ToInt32(fun(t).Substring(10, 3))).Max() + 1;
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return str + k.ToString().PadLeft(3, '0');

        }


        static public IQueryable<TEntity> ETForStartsWith<TEntity>(DbSet<TEntity> query, string propertyValue, PropertyInfo propertyInfo) where TEntity : class
        {
            ParameterExpression e = Expression.Parameter(typeof(TEntity), "e");
            MemberExpression m = Expression.MakeMemberAccess(e, propertyInfo);
            ConstantExpression c = Expression.Constant(propertyValue, typeof(string));
            MethodInfo mi = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            Expression call = Expression.Call(m, mi, c);

            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(call, e);
            return query.Where(lambda);
        }
        #endregion


        #region 消息管理器的令牌 20170302
        public virtual string Token { get; set; } = Guid.NewGuid().ToString();
        #endregion


        #region 20170304 打印 打印预览 报表设计
         

        protected virtual string GetReportPath()
        {
            return System.Environment.CurrentDirectory + @"\Reports\" + PermissionTitle  + "Collection" + "Report.repx";
        }
        protected XtraReport CreateReport()
        {
            var path = GetReportPath();

            XtraReport newReport = System.IO.File.Exists(path) ? XtraReport.FromFile(path, true) : new XtraReport();
            newReport.DisplayName = PermissionTitle + "Collection"+ "Report.repx";
            return newReport;

        }
        protected void SetReportDataSource(XtraReport report)
        {
            report.DataSource = this.Entities;
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

            return !IsLoading && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Print;
        }
        public virtual void ReportPreview()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var report = CreateReport();
            SetReportDataSource(report);
            report.CreateDocument();

            //DevExpress.LookAndFeel.UserLookAndFeel defaultLF = new DevExpress.LookAndFeel.UserLookAndFeel(null);
            //defaultLF.SkinName = "MetropolisDark";
            //defaultLF.Style= DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //defaultLF.SetDefaultStyle();

            // DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName;
            //  DevExpress.Skins.SkinManager.EnableFormSkins();
            //DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            //report.ShowPreview(defaultLF);
            // DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = true;
            //   DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName;
            report.ShowPreview();
            Mouse.OverrideCursor = null;
        }
        public virtual bool CanReportPreview()
        {
            if (this.IsInDesignMode()) return true;

            return !IsLoading&& User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Preview;
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

            return !IsLoading&& User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Design;
        }
        #endregion

        #region 20170311  权限标识
        public virtual string PermissionTitle { get; set; }//权限标识
        #endregion
        #region 20170315 表格导出

       

        [Command(CanExecuteMethodName = "CanExport")]
        public virtual void   ExportToXls(DevExpress.Xpf.Grid.DataViewBase view)
        {
            if (view == null) return;
            new Helpers.ExportHelper(view).ExportToXls();
        }
        [Command(CanExecuteMethodName = "CanExport")]
        public virtual void ExportToXlsx(DevExpress.Xpf.Grid.DataViewBase view)
        {
            if (view == null) return;
            new Helpers.ExportHelper(view).ExportToXlsx();
        }
        [Command(CanExecuteMethodName = "CanExport")]
        public virtual void ExportToPdf(DevExpress.Xpf.Grid.DataViewBase view)
        {
            if (view == null) return;
            new Helpers.ExportHelper(view).ExportToPdf();
        }
        [Command(CanExecuteMethodName = "CanExport")]
        public virtual void ExportToImage(DevExpress.Xpf.Grid.DataViewBase view)
        {
            if (view == null) return;
            new Helpers.ExportHelper(view).ExportToImage();
        }
        public virtual bool CanExport(DevExpress.Xpf.Grid.DataViewBase view)
        {
            if (this.IsInDesignMode()) return true;

            return !IsLoading && User.CurrentUser.GetUserAuthorityModuleMapping(PermissionTitle).Export;
        }
        #endregion

    }

    /// <summary>
    /// Provides the extension methods that are used to implement the IDocumentManagerService interface.
    /// </summary>
    public static class DocumentManagerServiceExtensions
    {

        /// <summary>
        /// Creates and shows a document containing a single object view model for the existing entity.
        /// </summary>
        /// <param name="documentManagerService">An instance of the IDocumentManager interface used to create and show the document.</param>
        /// <param name="parentViewModel">An object that is passed to the view model of the created view.</param>
        /// <param name="primaryKey">An entity primary key.</param>
        public static void ShowExistingEntityDocument<TEntity, TPrimaryKey>(this IDocumentManagerService documentManagerService, object parentViewModel, TPrimaryKey primaryKey)
        {
            IDocument document = FindEntityDocument<TEntity, TPrimaryKey>(documentManagerService, primaryKey) ?? CreateDocument<TEntity>(documentManagerService, primaryKey, parentViewModel);
            if (document != null)
                document.Show();
        }

        /// <summary>
        /// Creates and shows a document containing a single object view model for new entity.
        /// </summary>
        /// <param name="documentManagerService">An instance of the IDocumentManager interface used to create and show the document.</param>
        /// <param name="parentViewModel">An object that is passed to the view model of the created view.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function that initializes a new entity.</param>
        public static void ShowNewEntityDocument<TEntity>(this IDocumentManagerService documentManagerService, object parentViewModel, Action<TEntity> newEntityInitializer = null)
        {
            IDocument document = CreateDocument<TEntity>(documentManagerService, newEntityInitializer != null ? newEntityInitializer : x => DefaultEntityInitializer(x), parentViewModel);
            if (document != null)
                document.Show();
        }

        /// <summary>
        /// Searches for a document that contains a single object view model editing entity with a specified primary key.
        /// </summary>
        /// <param name="documentManagerService">An instance of the IDocumentManager interface used to find a document.</param>
        /// <param name="primaryKey">An entity primary key.</param>
        public static IDocument FindEntityDocument<TEntity, TPrimaryKey>(this IDocumentManagerService documentManagerService, TPrimaryKey primaryKey)
        {
            if (documentManagerService == null)
                return null;
            foreach (IDocument document in documentManagerService.Documents)
            {
                ISingleObjectViewModel<TEntity, TPrimaryKey> entityViewModel = document.Content as ISingleObjectViewModel<TEntity, TPrimaryKey>;
                if (entityViewModel != null && object.Equals(entityViewModel.PrimaryKey, primaryKey))
                    return document;
            }
            return null;
        }

        static void DefaultEntityInitializer<TEntity>(TEntity entity) { }

        static IDocument CreateDocument<TEntity>(IDocumentManagerService documentManagerService, object parameter, object parentViewModel)
        {
            if (documentManagerService == null)
                return null;
          var document = documentManagerService.CreateDocument(typeof(TEntity).Name + "View", parameter, parentViewModel);
            if (App.SystemConfigs.Where(t => t.Token == "DestroyOnClose").Single().TokenValue == "1")
            {
                document.DestroyOnClose = true;
            }
            return document;
        }




    }
}
