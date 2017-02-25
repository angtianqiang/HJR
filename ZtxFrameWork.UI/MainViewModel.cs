using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.Data;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.DataModel;
using ZtxFrameWork.UI.Comm.ViewModel;

namespace ZtxFrameWork.UI
{
   public  class MainViewModel :  CollectionViewModel<订单, ZtxDB, long>
    {
        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel():base(DbFactory.Instance, x => x.订单s, x => x, x => x.ID)
        {

        }
    }
}
