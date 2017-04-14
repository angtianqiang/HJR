using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using System.Data.Entity;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.Utils;
using System.Windows.Input;
using System.Data.Entity.Infrastructure;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    /// <summary>
    /// The base class for POCO view models exposing a collection of entities of the given type.
    /// This is a partial class that provides an extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// 少的基类视图模型暴露一个给定类型的实体的集合。
    ///这是一个部分的类提供了一个扩展点添加自定义属性,命令和覆盖方法无需修改自动生成的代码。
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type. 存储库实体类型 </typeparam>
    /// <typeparam name="TProjection">A projection entity type. 一个投影的实体类型。</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.一个工作单元类型。</typeparam>
    public abstract partial class EntitiesViewModel<TEntity, TProjection, TDbContext, TPrimaryKey> :
        EntitiesViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey>
        where TEntity : class
        where TProjection : class
        where TDbContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the EntitiesViewModel class.
        /// EntitiesViewModel类的初始化一个新的实例
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance. 工厂用于创建一个工作单元实例</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.一个函数,返回一个库代表给定类型的实体</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.
        ///  LINQ功能用于自定义查询实体。参数,例如,可以用于分类数据对预测数据和/或一个自定义类型不匹配库实体类型</param>
        protected EntitiesViewModel(
            IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
            : base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression)
        {
        }
    }

    /// <summary>
    /// The base class for a POCO view models exposing a collection of entities of the given type.
    /// It is not recommended to inherit directly from this class. Use the EntitiesViewModel class instead.
    /// 少的基类视图模型暴露一个给定类型的实体的集合。
    /// 不建议直接从这个类继承。使用EntitiesViewModel类。
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    [POCOViewModel]
    public abstract class EntitiesViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey> : IEntitiesViewModel<TProjection>
        where TEntity : class
        where TProjection : class
        where TDbContext : DbContext
    {

        #region inner classes
        protected interface IEntitiesChangeTracker
        {
            void RegisterMessageHandler();
            void UnregisterMessageHandler();
        }

        protected class EntitiesChangeTracker<TPrimaryKey> : IEntitiesChangeTracker
        {

            readonly EntitiesViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey> owner;
            ObservableCollection<TProjection> Entities { get { return owner.Entities; } }
         //   IRepository<TEntity, TPrimaryKey> Repository { get { return (IRepository<TEntity, TPrimaryKey>)owner.ReadOnlyRepository; } }

            public EntitiesChangeTracker(EntitiesViewModelBase<TEntity, TProjection, TDbContext,TPrimaryKey> owner)
            {
                this.owner = owner;
            }

            void IEntitiesChangeTracker.RegisterMessageHandler()
            {
                Messenger.Default.Register<EntityMessage<TEntity, TPrimaryKey>>(this, x => OnMessage(x));
            }

            void IEntitiesChangeTracker.UnregisterMessageHandler()
            {
                Messenger.Default.Unregister(this);
            }

            public TProjection FindLocalProjectionByKey(TPrimaryKey primaryKey)
            {
                var primaryKeyEqualsExpression = owner.GetProjectionPrimaryKeyEqualsExpression( primaryKey);
                return Entities.AsQueryable().FirstOrDefault(primaryKeyEqualsExpression);
            }

            public TProjection FindActualProjectionByKey(TPrimaryKey primaryKey)
            {
                var projectionEntity = owner.FindActualProjectionByKey(owner.Projection, primaryKey);
                if (projectionEntity != null && ExpressionHelper.IsFitEntity(owner.ReadOnlyDbSet.Find(primaryKey), owner.GetFilterExpression()))
                {
                    owner.OnEntitiesLoaded(owner.DB, new TProjection[] { projectionEntity });
                    return projectionEntity;
                }
                return null;
            }

            void OnMessage(EntityMessage<TEntity, TPrimaryKey> message)
            {
                if (!owner.IsLoaded)
                    return;
                switch (message.MessageType)
                {
                    case EntityMessageType.Added:
                        OnEntityAdded(message.PrimaryKey);
                        break;
                    case EntityMessageType.Changed:
                        OnEntityChanged(message.PrimaryKey);
                        break;
                    case EntityMessageType.Deleted:
                        OnEntityDeleted(message.PrimaryKey);
                        break;
                }
            }

            void OnEntityAdded(TPrimaryKey primaryKey)
            {
                var projectionEntity = FindActualProjectionByKey(primaryKey);
                if (projectionEntity != null)
                    Entities.Add(projectionEntity);
            }

            void OnEntityChanged(TPrimaryKey primaryKey)
            {
                var existingProjectionEntity = FindLocalProjectionByKey(primaryKey);
                var projectionEntity = FindActualProjectionByKey(primaryKey);
                if (projectionEntity == null)
                {
                    Entities.Remove(existingProjectionEntity);
                    return;
                }
                if (existingProjectionEntity != null)
                {
                    Entities[Entities.IndexOf(existingProjectionEntity)] = projectionEntity;
                    owner.RestoreSelectedEntity(existingProjectionEntity, projectionEntity);
                    return;
                }
                OnEntityAdded(primaryKey);
            }

            void OnEntityDeleted(TPrimaryKey primaryKey)
            {
                Entities.Remove(FindLocalProjectionByKey(primaryKey));
            }
        }
        #endregion

        ObservableCollection<TProjection> entities = new ObservableCollection<TProjection>();
        CancellationTokenSource loadCancellationTokenSource;
        //protected readonly IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory;
        //protected readonly Func<TUnitOfWork, IReadOnlyRepository<TEntity>> getRepositoryFunc;
        //protected Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> Projection { get; private set; }

        protected readonly IDbFactory<TDbContext> dbFactory;
        protected readonly Func<TDbContext, DbSet<TEntity>> getDbSetFunc;
        protected Func<IQueryable<TEntity>, IQueryable<TProjection>> Projection { get; private set; }
        protected Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression { get; }

        /// <summary>
        /// Initializes a new instance of the EntitiesViewModelBase class. EntitiesViewModelBase类的初始化一个新的实例
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        protected EntitiesViewModelBase(
              IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
           
        {
            //this.unitOfWorkFactory = unitOfWorkFactory;
            //this.getRepositoryFunc = getRepositoryFunc;
            //this.Projection = projection;
            this.dbFactory = dbFactory;
            this.getDbSetFunc = getDbSetFunc;
            this.Projection = projection;
            this.getPrimaryKeyExpression = getPrimaryKeyExpression;
            this.ChangeTracker = CreateEntitiesChangeTracker();
            if (!this.IsInDesignMode())
                OnInitializeInRuntime();
        }

        /// <summary>
        /// Used to check whether entities are currently being loaded in the background. The property can be used to show the progress indicator.
        /// 用于检查是否实体目前正在在后台加载。属性可以用来显示进度指示器
        /// </summary>
        public virtual bool IsLoading { get; protected set; }

        /// <summary>
        /// The collection of entities loaded from the unit of work.
        /// 从工作单元加载实体的集合
        /// </summary>
 
        public ObservableCollection<TProjection> Entities
        {
            get
            {
                if (!IsLoaded)

                    LoadEntities(false);
                return entities;
                
            }
        }

        protected IEntitiesChangeTracker ChangeTracker { get; private set; }

      protected DbSet<TEntity> ReadOnlyDbSet { get; private set; }
        protected TDbContext DB { get; private set; }

        protected bool IsLoaded { get { return ReadOnlyDbSet != null; } }

        protected void LoadEntities(bool forceLoad)
        {
            if (forceLoad)
            {
                if (loadCancellationTokenSource != null)
                    loadCancellationTokenSource.Cancel();
            }
            else if (IsLoading)
            {
                return;
            }
            loadCancellationTokenSource = LoadCore();
        }

        void CancelLoading()
        {
            if (loadCancellationTokenSource != null)
                loadCancellationTokenSource.Cancel();
            IsLoading = false;
        }

        CancellationTokenSource LoadCore()
        {
          //  Mouse.OverrideCursor = Cursors.Wait;
            IsLoading = true;
            var cancellationTokenSource = new CancellationTokenSource();
            var selectedEntityCallback = GetSelectedEntityCallback();
            Task.Factory.StartNew(() =>
            {
                //var repository = CreateReadOnlyRepository();
                //var entities = new ObservableCollection<TProjection>(repository.GetFilteredEntities(GetFilterExpression(), Projection));
                //OnEntitiesLoaded(GetUnitOfWork(repository), entities);
                //return new Tuple<IReadOnlyRepository<TEntity>, ObservableCollection<TProjection>>(repository, entities);
                var db = CreateDbContext();
                var dbset = getDbSetFunc(db);
                ObservableCollection<TProjection> entities = null;
                if (this.IsInDesignMode())
                {
                     entities = new ObservableCollection<TProjection>(dbset.GetFilteredEntities(GetFilterExpression(), Projection).Take(2));

                }
                else
                {
                    entities = new ObservableCollection<TProjection>(dbset.GetFilteredEntities(GetFilterExpression(), Projection));

                }
                OnEntitiesLoaded(this.DB, entities);
                return new Tuple<TDbContext, DbSet<TEntity>, ObservableCollection<TProjection>>(db,dbset, entities);





            }).ContinueWith(x =>
            {
                if (!x.IsFaulted)
                {
                    DB = x.Result.Item1;
                    ReadOnlyDbSet = x.Result.Item2;
                    entities = x.Result.Item3;
                    this.RaisePropertyChanged(y => y.Entities);
                    OnEntitiesAssigned(selectedEntityCallback);
                }
                IsLoading = false;
           //     Mouse.OverrideCursor = null;
            }, cancellationTokenSource.Token, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return cancellationTokenSource;
        }

        //static TUnitOfWork GetUnitOfWork(IReadOnlyRepository<TEntity> repository)
        //{
        //    return (TUnitOfWork)repository.UnitOfWork;
        //}

        protected virtual void OnEntitiesLoaded(TDbContext Db, IEnumerable<TProjection> entities)
        {
        }

        protected virtual void OnEntitiesAssigned(Func<TProjection> getSelectedEntityCallback)
        {
        }

        protected virtual Func<TProjection> GetSelectedEntityCallback()
        {
            return null;
        }

        protected virtual void RestoreSelectedEntity(TProjection existingProjectionEntity, TProjection projectionEntity)
        {
        }

        protected virtual Expression<Func<TEntity, bool>> GetFilterExpression()
        {
            return null;
        }

        protected virtual void OnInitializeInRuntime()
        {
            if (ChangeTracker != null)
                ChangeTracker.RegisterMessageHandler();
        }

        protected virtual void OnDestroy()
        {
            CancelLoading();
            if (ChangeTracker != null)
                ChangeTracker.UnregisterMessageHandler();
        }

        protected virtual void OnIsLoadingChanged()
        {
        }

        //protected IReadOnlyRepository<TEntity> CreateReadOnlyRepository()
        //{
        //    return getRepositoryFunc(CreateUnitOfWork());
        //}

        //protected TUnitOfWork CreateUnitOfWork()
        //{
        //    return unitOfWorkFactory.CreateUnitOfWork();
        //}
        //Func<TDbContext, DbSet<TEntity>> getDbSetFunc,

        protected DbSet<TEntity> CreateDbSet()
        {
            return getDbSetFunc(CreateDbContext());
        }
        protected TDbContext CreateDbContext()
        {
            //  this.DB= dbFactory.CreateDbContext();
            // return this.DB;
          return  dbFactory.CreateDbContext();
        }
        protected virtual IEntitiesChangeTracker CreateEntitiesChangeTracker()
        {
            return null;
        }

        protected IDocumentOwner DocumentOwner { get; private set; }

        #region IDocumentContent
        object IDocumentContent.Title { get { return null; } }

        void IDocumentContent.OnClose(CancelEventArgs e) { }

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

        #region IEntitiesViewModel
        ObservableCollection<TProjection> IEntitiesViewModel<TProjection>.Entities { get { return Entities; } }

        bool IEntitiesViewModel<TProjection>.IsLoading { get { return IsLoading; } }
        #endregion
        public  Expression<Func<TEntity, bool>> GetPrimaryKeyEqualsExpression(TPrimaryKey primaryKey) 
        {
            return ExpressionHelper.GetValueEqualsExpression(this.getPrimaryKeyExpression, primaryKey);
        }
        public  Expression<Func<TEntity, bool>> GetPrimaryKeyEqualsExpressio( TPrimaryKey primaryKey)
        {
            return ExpressionHelper.GetValueEqualsExpression(this.getPrimaryKeyExpression, primaryKey);
        }
        public  string GetPrimaryKeyPropertyName() 
        {
            return ExpressionHelper.GetPropertyName(this.getPrimaryKeyExpression);
        }
        /// <summary>
        /// Builds a lambda expression that compares an entity primary key with the given constant value.
        /// 构建一个lambda表达式,比较一个实体主键与给定的常数值
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="primaryKey">A value to compare with the entity primary key.</param>
        public  Expression<Func<TProjection, bool>> GetProjectionPrimaryKeyEqualsExpression(TPrimaryKey primaryKey) 
        {
            return GetProjectionValue(primaryKey,
                (TPrimaryKey x) => this.GetPrimaryKeyEqualsExpression(x),
                (TPrimaryKey x) => GetProjectionPrimaryKeyEqualsExpressionCore( x));
        }

        public  TPrimaryKey GetProjectionPrimaryKey( TProjection projectionEntity) 
        {
            return GetProjectionValue(projectionEntity,
                (TEntity x) => this.getPrimaryKeyExpression.Compile()(x),
                (TProjection x) => (TPrimaryKey)TypeDescriptor.GetProperties(typeof(TProjection))[this.GetPrimaryKeyPropertyName()].GetValue(x));
        }


        public  TProjection FindActualProjectionByKey( Func<IQueryable<TEntity>, IQueryable<TProjection>> projection, TPrimaryKey primaryKey) 
        {
            var primaryKeyEqualsExpression = GetProjectionPrimaryKeyEqualsExpression( primaryKey);

            //   var result = ReadOnlyDbSet.GetFilteredEntities(null, projection).Where(primaryKeyEqualsExpression).Take(1).ToArray().FirstOrDefault();


            var objectContext = ((IObjectContextAdapter)this.DB).ObjectContext;
            var objectSet = objectContext.CreateObjectSet<TEntity>();
            var oldMergeOption = objectSet.MergeOption;
            objectSet.MergeOption = System.Data.Entity.Core.Objects.MergeOption.OverwriteChanges;
           var result = objectSet.GetFilteredEntities(null, projection).Where(primaryKeyEqualsExpression).Take(1).ToArray().FirstOrDefault();
            objectSet.MergeOption = oldMergeOption;
            //  20170412 更改这里减少一次查询
            //return GetProjectionValue(result,
            //    (TEntity x) => x != null ? ReloadCore(x) : null,
            //(TProjection x) => x);
            return GetProjectionValue(result,
               (TEntity x) => x,
           (TProjection x) => x);
        }
        protected virtual TEntity ReloadCore(TEntity entity)
        {
         
                DB.Entry(entity).Reload();
      
            return ReadOnlyDbSet.Find(this.getPrimaryKeyExpression.Compile()(entity));
        }
        static TProjectionResult GetProjectionValue<TEntity, TProjection, TEntityResult, TProjectionResult>(TProjection value, Func<TEntity, TEntityResult> entityFunc, Func<TProjection, TProjectionResult> projectionFunc)
        {
            if (typeof(TEntity) != typeof(TProjection) || typeof(TEntityResult) != typeof(TProjectionResult))
                return projectionFunc(value);
            return (TProjectionResult)(object)entityFunc((TEntity)(object)value);
        }

         Expression<Func<TProjection, bool>> GetProjectionPrimaryKeyEqualsExpressionCore( TPrimaryKey primaryKey)
        {
            var parameter = Expression.Parameter(typeof(TProjection));
            var keyExpression = Expression.Lambda<Func<TProjection, TPrimaryKey>>(Expression.Property(parameter, this.GetPrimaryKeyPropertyName()), parameter);
            return ExpressionHelper.GetValueEqualsExpression(keyExpression, primaryKey);
        }

        public  bool IsDetached( TProjection projectionEntity) 
        {
            return GetProjectionValue(projectionEntity,
                (TEntity x) => this.DB.Entry(x).State == EntityState.Detached,
                (TProjection x) => false);
        }
        public  bool ProjectionHasPrimaryKey(TProjection projectionEntity)
        {
            return GetProjectionValue(projectionEntity,
                (TEntity x) => ExpressionHelper.GetHasValueFunctionExpression(this.getPrimaryKeyExpression).Compile()(x),
            (TProjection x) => true);
        }

    }

    /// <summary>
    /// The base interface for view models exposing a collection of entities of the given type. 
    /// 基本界面视图模型暴露一个给定类型的实体的集合
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    public interface IEntitiesViewModel<TEntity> : IDocumentContent where TEntity : class
    {

        /// <summary>
        /// The loaded collection of entities.
        /// </summary>
        ObservableCollection<TEntity> Entities { get; }

        /// <summary>
        /// Used to check whether entities are currently being loaded in the background. The property can be used to show the progress indicator.
        ///用于检查是否实体目前正在在后台加载。属性可以用来显示进度指示器
        /// </summary>
        bool IsLoading { get; }
    }
}
