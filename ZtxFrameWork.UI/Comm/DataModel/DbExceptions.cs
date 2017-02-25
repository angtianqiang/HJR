using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.UI.Comm.DataModel
{
    /// <summary>
    /// The database-independent exception used in Data Layer and View Model Layer to handle database errors.
    /// 使用的数据库异常数据层和视图模型层来处理数据库错误
    /// </summary>
    public class DbException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the DbRepository class.
        /// </summary>
        /// <param name="errorMessage">An error message text.</param>
        /// <param name="errorCaption">An error message caption text.</param>
        /// <param name="innerException">An underlying exception.</param>
        public DbException(string errorMessage, string errorCaption, Exception innerException)
            : base(innerException.Message, innerException)
        {
            ErrorMessage = errorMessage;
            ErrorCaption = errorCaption;
        }

        /// <summary>The error message text.</summary>
        public string ErrorMessage { get; private set; }

        /// <summary>The error message caption text.</summary>
        public string ErrorCaption { get; private set; }
    }
}
