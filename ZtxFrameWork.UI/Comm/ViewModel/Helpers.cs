using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel;

namespace ZtxFrameWork.UI.Comm.ViewModel
{
    static class EntityDisplayNameHelper
    {
        public static string GetEntityDisplayName(Type type)
        {
            var attribute = (DisplayNameAttribute)AttributesHelper.GetAttributes(type)[typeof(DisplayNameAttribute)];
            return attribute != null && attribute != DisplayNameAttribute.Default ? attribute.DisplayName : type.Name;
        }
    }
}
