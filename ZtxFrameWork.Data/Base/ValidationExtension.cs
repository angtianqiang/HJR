using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
namespace ZtxFrameWork.Data
{
    public static class ValidationExtension
    {
        /// <summary>
        /// 扩展方法:验证属性是否合格
        /// </summary>
        /// <typeparam name="metadatatype">要验证的数据类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static string ValidateProperty(this VHObject obj, Type metadatatype, string propertyName)
        {

            if (string.IsNullOrEmpty(propertyName))
            {
                return string.Empty;
            }
            if (propertyName.Contains("."))//20170228 如果是一个关系类的属性，如订单.分店
            {
                return string.Empty;
            }
            var targetType = obj.GetType();
            if (targetType != metadatatype)
            {
                TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(targetType, metadatatype), targetType);
            }
            var propertyValue = targetType.GetProperty(propertyName).GetValue(obj, null);
            var validationContext = new ValidationContext(obj, null, null);
            validationContext.MemberName = propertyName;
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, validationContext, validationResults);
            if (validationResults.Count > 0)
            {
                return validationResults.First().ErrorMessage;
            }
            return string.Empty;
        }



        /// <summary>
        /// 扩展方法：验证属性值
        /// </summary>
        /// <typeparam name="metadataType">要验证的数据类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="errors">错误信息</param>
        /// <returns></returns>
        public static bool IsPropertyValid(this VHObject obj,Type metadataType, object propertyValue, string propertyName,
             ref Dictionary<string, string> errors)
        {
            TypeDescriptor.AddProviderTransparent(
                new AssociatedMetadataTypeTypeDescriptionProvider(obj.GetType(), metadataType), obj.GetType());
            var validationContent = new ValidationContext(obj, null, null);
            validationContent.MemberName = propertyName;
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, validationContent, validationResults);
            if (validationResults.Count > 0 && errors == null)
                errors = new Dictionary<string, string>(validationResults.Count);
            foreach (var validationResult in validationResults)
            {
                if (!errors.ContainsKey(validationResult.MemberNames.First()))
                    errors.Add(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
            if (validationResults.Count > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 扩展方法：类实例是否通过验证
        /// </summary>
        /// <typeparam name="metadataType">要验证的数据类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="errors">错误信息</param>
        /// <returns></returns>
        public static bool Valid(this VHObject obj,Type metadataType, ref Dictionary<string, string> errors)
        {
            bool result = true;
            Type t = obj.GetType();
            PropertyInfo[] propertyInfos = t.GetProperties();
            try
            {
                foreach (PropertyInfo pi in propertyInfos)
                {
                    string name = pi.Name;
                    //vhobject对象的属情不用验证
                    if (name == "Error" || name == "Item" || name == "UniqueId" || name == "PropertyChangeEventDisabled" || name == "HasPropertyChangeListener" || name == "DirtyState" || name == "ClientTag")
                        continue;
                    object value1 = pi.GetValue(obj, null);
                    Dictionary<string, string> propertyErrors = new Dictionary<string, string>();
                    if (!obj.IsPropertyValid(metadataType,value1, name, ref propertyErrors))
                    {
                        result = false;
                        foreach (var item in propertyErrors)
                        {
                            if (!errors.ContainsKey(item.Key))
                            {
                                errors.Add(item.Key, item.Value);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                string aaaa = ex.Message;
            }
            return result;

        }
        /// <summary>
        /// 辅助方法,把消息转成字符串
        /// </summary>
        /// <param name="errors">消息</param>
        /// <returns></returns>
        public static string ErrorToString(this Dictionary<string, string> errors)
        {
            if (errors == null || errors.Count == 0)
                return "";
            StringBuilder str = new StringBuilder();
            foreach (var item in errors)
            {
                str.Append(item.Value);
                str.Append(Environment.NewLine);
            }
            str.Remove(str.Length - 1, 1);
            return str.ToString();
        }

    }
}
