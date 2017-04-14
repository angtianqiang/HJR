using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data
{
  public  interface IVHObject:INotifyPropertyChanged
    {
        #region Properties

        string UniqueId { get; set; }
     
        bool PropertyChangeEventDisabled { get; set; }
     
        DirtyState DirtyState { get; set; }
     
        bool IsDirty { get; }
       
        object ClientTag { get; set; }
        #endregion
        #region Methods
        bool SerializeToFile();
        bool SerializeToFile(string outputDirectory);
        string SerializeToXml();
        MemoryStream SerializeToMemoryStream();
     //   T CreateAClone<T>();
        void MakeClean();
        void NotifyPropertyChanged(string propertyName);


     
        #endregion
    }
}
