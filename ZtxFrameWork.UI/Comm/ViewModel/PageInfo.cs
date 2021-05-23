using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    [Serializable]
    public class PageInfo : BindableBase
    {


        protected bool Set<T>(Expression<Func<T>> expression, ref T storage, T value)
        {
            return SetProperty<T>(ref storage, value, expression);
        }
        protected bool Set<T>(Expression<Func<T>> expression, ref T storage, T value, Action changedCallback)
        {
            return SetProperty<T>(ref storage, value, expression, changedCallback);
        }

        public static int ConfigPageSize = 0;

        public PageInfo()
        {
            if (this.IsInDesignMode())
            {
                PageSize = 50;
            }
            else
            {
                if (ConfigPageSize == 0)
                {

                    //  ConfigPageSize = Convert.ToInt32(ZtxFrameWork.Utilities.XMLHelper.GetValue(System.Environment.CurrentDirectory + "\\config.xml", "//resources/items/item", "PageSize"));

                    ConfigPageSize = App.PageSize;
                }
                PageSize = PageInfo.ConfigPageSize;
            }

            PageIndex = 1;
            //  SortStr = ""; 必输
            //   IsSortDesc = false;

            //  Dist = false;
            PageCount = 1;
            ItemCount = 1;
        }

        private int _pageSize;
        public int PageSize
        {
            get
            {

                return _pageSize;
            }
            set { Set<int>(() => this.PageSize, ref _pageSize, value); }
        }
        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { Set<int>(() => this.PageIndex, ref _pageIndex, value, () => RaisePropertiesChanged(GetPropertyName(() => this.PageCountStr))); }
        }
        private int _itemCount;
        public int ItemCount
        {
            get { return _itemCount; }
            set { Set<int>(() => this.ItemCount, ref _itemCount, value, () => RaisePropertiesChanged(GetPropertyName(() => this.ItemCountStr))); }
        }


        private int _pageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { Set<int>(() => this.PageCount, ref _pageCount, value, () => RaisePropertiesChanged(GetPropertyName(() => this.PageCountStr))); }
        }



        public string PageCountStr { get => $"{this.PageIndex}/{this.PageCount} 页"; }
        public string ItemCountStr { get => $"共 {this.ItemCount} 条记录"; }

        #region UI操作



        private bool _canMoveFistPage;
        public bool CanMoveFistPage
        {
            get { return _canMoveFistPage; }
            set { Set<bool>(() => this.CanMoveFistPage, ref _canMoveFistPage, value); }
        }
        private bool _canMovePrvPage;
        public bool CanMovePrvPage
        {
            get { return _canMovePrvPage; }
            set { Set<bool>(() => this.CanMovePrvPage, ref _canMovePrvPage, value); }
        }
        private bool _canMoveNextPage;
        public bool CanMoveNextPage
        {
            get { return _canMoveNextPage; }
            set { Set<bool>(() => this.CanMoveNextPage, ref _canMoveNextPage, value); }
        }

        private bool _canMoveLastPage;
        public bool CanMoveLastPage
        {
            get { return _canMoveLastPage; }
            set { Set<bool>(() => this.CanMoveLastPage, ref _canMoveLastPage, value); }
        }

        private bool _canGo;
        public bool CanGo
        {
            get { return _canGo; }
            set { Set<bool>(() => this.CanGo, ref _canGo, value); }
        }


        public void Go()
        {
            this.Bind();
        }
        public void MoveFirstPage()
        {
            this.PageIndex = 1;
            this.Bind();
        }
        public void MovePrvPage()
        {
            this.PageIndex -= 1;
            if (this.PageIndex <= 0)
            {
                this.PageIndex = 1;
            }
            this.Bind();
        }

        public void MoveNextPage()
        {
            this.PageIndex += 1;
            if (this.PageIndex > this._pageCount)
            {
                this.PageIndex = this._pageCount;
            }
            this.Bind();
        }
        public void MoveLastPage()
        {
            this.PageIndex = this._pageCount;
            this.Bind();
        }


        private void Bind()
        {
            if (this.EventPaging != null)
            {
                this.EventPaging(new EventArgs());

            }
        }
        public void Refresh()
        {
            //201805015
            this.PageCount = this.ItemCount % this.PageSize == 0 ? this.ItemCount / this.PageSize : this.ItemCount / this.PageSize + 1;
            //201800621 可能会是0，加一句处理0值
            if (this.PageIndex == 0)
            {
                this.PageIndex = 1;
            }

            if (this.PageIndex > this._pageCount)
            {
                this.PageIndex = this._pageCount;
            }

            if (this.PageCount > 1)
            {
                this.CanGo = true;
            }
            else
            {
                this.CanGo = false;
            }
            if (this.PageIndex == 1)
            {
                this.CanMovePrvPage = false;
                this.CanMoveFistPage = false;
            }
            else
            {
                this.CanMovePrvPage = true;
                this.CanMoveFistPage = true;
            }
            if (this.PageIndex == this.PageCount)
            {
                this.CanMoveLastPage = false;
                this.CanMoveNextPage = false;
            }
            else
            {
                this.CanMoveNextPage = true;
                this.CanMoveLastPage = true;
            }
            if (this.ItemCount == 0)
            {
                this.CanMoveFistPage = false;
                this.CanMovePrvPage = false;
                this.CanMoveNextPage = false;
                this.CanMoveLastPage = false;
            }
        }

        #endregion



        public delegate void EventArgsHandler(EventArgs e);
        public event EventArgsHandler EventPaging;

    }
}
