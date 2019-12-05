using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Common
{
    /// <summary>
    /// 持久层异常抛出
    /// </summary>
    public sealed class PersistenceLayerException : Exception
    {

        #region 变量

        /// <summary>
        /// 
        /// </summary>
        private Error m_Error;

        /// <summary>
        /// 
        /// </summary>
        private Exception m_ErrorSource;

        /// <summary>
        /// 
        /// </summary>
        private string m_ErrorMessage;

        #endregion


        #region 构造函数

        /// <summary>
        ///	生成异常实例
        /// </summary>
        /// <param name="message">信息</param>
        public PersistenceLayerException(string message)
            : base(message)
        {
            m_ErrorMessage = message;
            m_Error = Error.Unknown;
        }

        /// <summary>
        ///	生成异常实例
        /// </summary>
        /// <param name="e">异常实例</param>
        public PersistenceLayerException(Exception e)
            : base(e.Message)
        {
            m_Error = Error.Unknown;
            m_ErrorSource = e;
            m_ErrorMessage = e.Message;
        }

        /// <summary>
        ///	生成异常实例
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="error">代码</param>
        public PersistenceLayerException(string message, Error error)
            : base(message)
        {
            m_ErrorMessage = message;
            m_Error = error;
        }

        #endregion


        #region 属性

        /// <summary>
        ///	返回错误代码
        /// </summary>
        public Error ErrorType
        {
            get { return m_Error; }
        }

        /// <summary>				
        ///	获取错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return m_ErrorMessage; }
        }

        /// <summary>
        /// 错误级别
        /// </summary>
        public Severity Severity
        {
            get { return GetSeverity(m_Error); }
        }

        /// <summary>
        /// 获取错误级别
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        static private Severity GetSeverity(Error error)
        {
            Attribute[] attr = (Attribute[])error.GetType().GetCustomAttributes(typeof(Level), true);
            if (attr != null && attr.Length > 0 && attr[0] is Level)
                return (attr[0] as Level).Severity;
            return Severity.Unclassified;
        }

        /// <summary>
        /// 错误源信息
        /// </summary>
        public Exception ErrorSource
        {
            get { return m_ErrorSource; }
        }

        #endregion

    }
}
