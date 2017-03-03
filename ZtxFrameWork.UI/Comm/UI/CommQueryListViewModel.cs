using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
namespace ZtxFrameWork.UI.Comm.UI
{
    [POCOViewModel]
    public class CommQueryListViewModel
    {
        
   protected ICurrentWindowService CurrentWindowService { get { return this.GetService<ICurrentWindowService>(); } }



        public virtual string  Title { get; set; }
        public virtual List<dynamic> Entities { get; set; }
        public virtual dynamic SelectEntity { get; set; }
        public virtual bool IsSelect { get; set; } = false;
        public void Select(dynamic entity)
        {
            IsSelect = true;
            CurrentWindowService.Close();
        }
    }
}