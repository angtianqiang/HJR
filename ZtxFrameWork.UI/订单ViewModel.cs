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
 public   class 订单ViewModel: SingleObjectViewModel<订单,ZtxDB,long>
    {

        public static 订单ViewModel Create()
        {
            return ViewModelSource.Create(() => new 订单ViewModel());
        }
        protected 订单ViewModel():base(DbFactory.Instance,x=>x.订单s,x=>x.ID,x=>x.订单号)
        {

        }
        //protected override bool SaveCore()
        //{
        //    Entity.客户名称 = "p0000001";
        //    return true;
        //}
    }
}
