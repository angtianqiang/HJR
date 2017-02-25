using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.UI.Comm.DataModel
{
    /// <summary>
    /// Provides methods to convert Entity Framework exceptions to database-independent exceptions used in Data Layer and View Model Layer.
    /// 提供方法来转换中使用实体框架异常数据库异常数据层和视图层模型
    /// </summary>
    public static class DbExceptionsConverter
    {

        /// <summary>
        /// Converts System.Data.Entity.Infrastructure.DbUpdateException exception to database-independent DbException exception used in Data Layer and View Model Layer.
        /// 转换数据库System.Data.Entity.Infrastructure.DbUpdateException例外DbException异常用于数据层和视图模型层
        /// </summary>
        /// <param name="exception">Exception to convert.</param>
        public static DbException Convert(DbUpdateException exception)
        {
            Exception originalException = exception;
            while (originalException.InnerException != null)
            {
                originalException = originalException.InnerException;
            }
            return new DbException(originalException.Message, CommonResources.Exception_UpdateErrorCaption, exception);
        }

        /// <summary>
        /// Converts System.Data.Entity.Validation.DbEntityValidationException exception to database-independent DbException exception used in Data Layer and View Model Layer.
        /// 转换数据库System.Data.Entity.Validation.DbEntityValidationException例外DbException异常用于数据层和视图模型层
        /// </summary>
        /// <param name="exception">Exception to convert.</param>
        public static DbException Convert(DbEntityValidationException exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DbEntityValidationResult validationResult in exception.EntityValidationErrors)
            {
                foreach (DbValidationError error in validationResult.ValidationErrors)
                {
                    if (stringBuilder.Length > 0)
                        stringBuilder.AppendLine();
                    stringBuilder.Append(error.PropertyName + ": " + error.ErrorMessage);
                }
            }
            return new DbException(stringBuilder.ToString(), CommonResources.Exception_ValidationErrorCaption, exception);
        }
    }
}
