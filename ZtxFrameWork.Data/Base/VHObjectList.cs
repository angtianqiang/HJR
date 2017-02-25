using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Specialized;
namespace ZtxFrameWork.Data
{
  public  class VHObjectList<T>:ObservableCollection<T> where T:VHObject
  {
      //20140530 由于父类的PropertyChanged事件的可访问性为properct,故重写
      public event PropertyChangedEventHandler IsDirtyPropertyChanged;
      protected  void OnIsDirtyPropertyChanged(PropertyChangedEventArgs e)
      {
          if (IsDirtyPropertyChanged!=null)
          {
              IsDirtyPropertyChanged(this, e);
          }
      }

      private ObservableCollection<T> _pendingDeletedItems;
      public ObservableCollection<T> PendingDeletedItems
      {
          get
          {
              if (_pendingDeletedItems==null)
              {
                  _pendingDeletedItems = new ObservableCollection<T>();
              }
              return _pendingDeletedItems;
          }
      }
      public VHObjectList()
      {
          this.CollectionChanged += VHObjectList_CollectionChanged;
         
      }

      void VHObjectList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
      {
          if (e.Action==NotifyCollectionChangedAction.Remove)
          {
              if (e!=null && e.OldItems.Count>0)
              {
                  IList items = e.OldItems;
                  VHObject temp=null;
                  foreach (T item in items)
                  {
                      temp = (VHObject)item ;
                      if (temp.DirtyState!=DirtyState.Added) //新添加的数据在数据库中没有存在，可以直接删除
                      {
                          temp.DirtyState = DirtyState.Deleted;
                          PendingDeletedItems.Add((T)temp);
                      }
                  }
              }
          }
          else if (e.Action == NotifyCollectionChangedAction.Add)
          {
              if (e != null && e.NewItems.Count > 0)
              {
                  IList items = e.NewItems;
                  VHObject temp = null;
                  foreach (T item in items)
                  {
                      temp = (VHObject)item;
                    temp.PropertyChanged += (objectSender, objecte) =>
                    {
                        this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsDirty"));
                        this.OnIsDirtyPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsDirty"));
                    };

                     /// temp.PropertyChanged += temp_PropertyChanged;
                  }
              }

          }

          this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsDirty"));
          this.OnIsDirtyPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsDirty"));
      }

      void temp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
      {
        
      }

     

      public IEnumerator<T> GetAllDataEnumerator()
      {
          List<T> temp = new List<T>(this);
          foreach (T item in PendingDeletedItems)
          {
              temp.Add(item);
          }
          return temp.GetEnumerator();
      }
      
      public IList<T> GetAllDataList()
      {
          List<T> temp = new List<T>(this);
          foreach (T item in PendingDeletedItems)
          {
              temp.Add(item);
          }
          return temp;
      }
      /// <summary>
      /// 提交更改
      /// </summary>
      public void AcceTChanges()
      {
          this.PendingDeletedItems.Clear();
          foreach (T item in this)
          {
              ((VHObject)item).MakeClean();
          }
         this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("IsDirty"));
      }
      /// <summary>
      /// 是否有更改为的数据
      /// </summary>
      public virtual bool IsDirty
      {
          get
          {
              bool isDirty = false;

              isDirty = (this._pendingDeletedItems != null && this._pendingDeletedItems.Count > 0);

              if (!isDirty )
              {
                  foreach (IVHObject item in this)
                  {
                      if (item != null)
                      {
                          isDirty = item.IsDirty;
                          if (isDirty)
                              break;
                      }
                  }
              }

              return isDirty;
          }
      }


        #region 20161212  添加List构造

        public VHObjectList(List<T> list)
            : base((list != null) ? new List<T>(list.Count) : list)
        {
           
            CopyFrom(list);
            this.CollectionChanged += VHObjectList_CollectionChanged;
        }
        public VHObjectList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            CopyFrom(collection);
            this.CollectionChanged += VHObjectList_CollectionChanged;
        }

        private void CopyFrom(IEnumerable<T> collection)
        {
            IList<T> items = Items;
            if (collection != null && items != null)
            {
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        items.Add(enumerator.Current);
                    }
                }
            }
        }

        #endregion

    }
}
