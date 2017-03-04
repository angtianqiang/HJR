using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZtxFrameWork.UI.Comm.DataModel;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    /// <summary>
    /// The base class for POCO view models exposing a read-only collection of entities of a given type. 
    /// This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public partial class ReadOnlyCollectionViewModel<TEntity, TDbContext, TPrimaryKey> : ReadOnlyCollectionViewModel<TEntity, TEntity, TDbContext, TPrimaryKey>
       where TEntity : class
    
        where TDbContext : DbContext
    {

        /// <summary>
        /// Creates a new instance of ReadOnlyCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        public static ReadOnlyCollectionViewModel<TEntity, TDbContext, TPrimaryKey> CreateReadOnlyCollectionViewModel(
              IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
        {
            return ViewModelSource.Create(() => new ReadOnlyCollectionViewModel<TEntity, TDbContext, TPrimaryKey>(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression));
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyCollectionViewModel class.
        /// This constructor is declared protected to avoid an undesired instantiation of the PeekCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">An optional parameter that provides a LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data.</param>
        protected ReadOnlyCollectionViewModel(
             IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression):base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression)
        {
        }
    }

    /// <summary>
    /// The base class for POCO view models exposing a read-only collection of entities of a given type. 
    /// This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public partial class ReadOnlyCollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey> : ReadOnlyCollectionViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey>
         where TEntity : class
        where TProjection : class
        where TDbContext : DbContext
    {

        /// <summary>
        /// Creates a new instance of ReadOnlyCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        public static ReadOnlyCollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey> CreateReadOnlyProjectionCollectionViewModel(
             IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
        {
            return ViewModelSource.Create(() => new ReadOnlyCollectionViewModel<TEntity, TProjection, TDbContext, TPrimaryKey>(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression));
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyCollectionViewModel class.
        /// This constructor is declared protected to avoid an undesired instantiation of the PeekCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        protected ReadOnlyCollectionViewModel(
            IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
         : base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression)
        {
        }
    }

    /// <summary>
    /// The base class for POCO view models exposing a read-only collection of entities of a given type. 
    /// It is not recommended to inherit directly from this class. Use the ReadOnlyCollectionViewModel class instead.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    [POCOViewModel]
    public abstract class ReadOnlyCollectionViewModelBase<TEntity, TProjection, TDbContext, TPrimaryKey> : EntitiesViewModel<TEntity, TProjection, TDbContext, TPrimaryKey> //, ISupportParameter, IDocumentContent
         where TEntity : class
        where TProjection : class
        where TDbContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the ReadOnlyCollectionViewModelBase class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        protected ReadOnlyCollectionViewModelBase(
             IDbFactory<TDbContext> dbFactory,
            Func<TDbContext, DbSet<TEntity>> getDbSetFunc,
            Func<IQueryable<TEntity>, IQueryable<TProjection>> projection,
             Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
         : base(dbFactory, getDbSetFunc, projection, getPrimaryKeyExpression)
        {
        }

        /// <summary>
        /// The selected enity.
        /// Since ReadOnlyCollectionViewModelBase is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        /// </summary>
        public virtual TProjection SelectedEntity { get; set; }

        /// <summary>
        /// The lambda expression used to filter which entities will be loaded locally from the unit of work.
        /// Since ReadOnlyCollectionViewModelBase is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        /// </summary>
        public virtual Expression<Func<TEntity, bool>> FilterExpression { get; set; }

        /// <summary>
        /// Reloads entities.
        /// Since CollectionViewModelBase is a POCO view model, an instance of this class will also expose the RefreshCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void Refresh()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            LoadEntities(false);
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Determines whether entities can be reloaded.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for RefreshCommand.
        /// </summary>
        public bool CanRefresh()
        {
            return !IsLoading;
        }

        protected override void OnEntitiesAssigned(Func<TProjection> getSelectedEntityCallback)
        {
            base.OnEntitiesAssigned(getSelectedEntityCallback);
            #region 20161222
            if (Entities == null || Entities.Count == 0)
            {
                SelectedEntity = null;
                return;
            }
            #endregion
            SelectedEntity = getSelectedEntityCallback() ?? Entities.FirstOrDefault();
        }

        protected override Func<TProjection> GetSelectedEntityCallback()
        {
            int selectedItemIndex = -1;
            if (SelectedEntity!=null)
            {
                selectedItemIndex = Entities.IndexOf(SelectedEntity);
            }
     
            return () => (selectedItemIndex >= 0 && selectedItemIndex < Entities.Count) ? Entities[selectedItemIndex] : null;
        }

        protected override void OnIsLoadingChanged()
        {
            base.OnIsLoadingChanged();
            this.RaiseCanExecuteChanged(x => x.Refresh());
        }

        protected virtual void OnSelectedEntityChanged() { }

        protected virtual void OnFilterExpressionChanged()
        {
            //20161223 ilterExpression更改后暂时不更新
            if (IsLoaded || IsLoading)
                LoadEntities(true);
        }

        protected override Expression<Func<TEntity, bool>> GetFilterExpression()
        {
            return FilterExpression;
        }

        //#region 20161213 在DEV基础上扩展新功能
        //[ServiceProperty(Key = "CommMessageBox")]
        //public virtual IMessageBoxService MessageBoxService { get { return null; } }
        //[ServiceProperty(Key = "FindDialogWindow")]
        //public virtual IDialogService DialogService { get { return null; } }



        //public EntityExpressionBuilder<TEntity> EntityExpressionViewModel { get; set; }

        //protected MoudleStatus MoudleCurrnetStatus { get; set; } = MoudleStatus.None;




        //public virtual void Find()
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;
        //    DialogService.ShowDialog(null, ContentTitle + "【查询】", EntityExpressionViewModel);
        //    if (EntityExpressionViewModel.DialogResult == true)
        //    {
        //        this.FilterExpression = EntityExpressionViewModel.AdvancedExpression;
        //        LoadEntities(false);

        //        MoudleCurrnetStatus = MoudleStatus.Find;
        //    }
        //}
        //public virtual bool CanFind()
        //{
        //    return MoudleCurrnetStatus == MoudleStatus.None;
        //}
        //public virtual void Back()
        //{
        //    ResetEntities();
        //    MoudleCurrnetStatus = MoudleStatus.None;
        //}
        //public virtual bool CanBack()
        //{

        //    return MoudleCurrnetStatus != MoudleStatus.None;
        //}

        //protected virtual void OnParameterChanged(object parameter)
        //{
        //    ContentTitle = parameter.ToString();
        //}

        //#region ISupportParameter
        //object ISupportParameter.Parameter
        //{
        //    get { return null; }
        //    set { OnParameterChanged(value); }
        //}
        //#endregion

        //#region IDocumentContent
        //object IDocumentContent.Title { get { return ContentTitle; } }

        //void IDocumentContent.OnClose(CancelEventArgs e)
        //{
        //    //   e.Cancel = !TryClose();
        //    e.Cancel = false;
        //}

        //void IDocumentContent.OnDestroy()
        //{
        //    OnDestroy();
        //}

        //IDocumentOwner IDocumentContent.DocumentOwner
        //{
        //    get { return DocumentOwner; }
        //    set { DocumentOwner = value; }
        //}
        //protected IDocumentOwner DocumentOwner { get; private set; }
        //#endregion

        //#endregion


      


    }
}
