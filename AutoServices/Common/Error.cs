using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Common
{
    /// <summary>
    /// 信息级别
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public class Level : Attribute
    {
        private Severity severity;

        /// <summary>
        /// 信息级别
        /// </summary>
        /// <param name="severity">Severity 信息级别</param>
        public Level(Severity severity)
            : base()
        {
           this.severity = severity;
        }

        /// <summary>
        /// 信息级别
        /// </summary>
        public Severity Severity
        {
            get { return severity; }
        }
    }

    /// <summary>
    ///	系统抛出错误类型
    /// </summary>
    public enum Error
    {
        /// <summary>
        /// 实体层一般性错误 
        /// </summary>		
        [Level(Severity.Error)]
        PesistentError,

        /// <summary>
        /// 数据不能为NULL
        /// </summary>
        [Level(Severity.Info)]
        NotNull,

        /// <summary>
        /// 数据不能初始化
        /// </summary>
        [Level(Severity.Info)]
        NotInitial,

        /// <summary>
        /// 已无实体对象了
        /// </summary>
        [Level(Severity.Warning)]
        NoObjectForTable,

        /// <summary>
        /// 数据库连接错误
        /// </summary>
        [Level(Severity.Error)]
        DatabaseConnectError,

        /// <summary>
        /// 数据库未处理错误
        /// </summary>
        [Level(Severity.Critical)]
        DatabaseUnknownError,

        /// <summary>
        /// 数据不唯一
        /// </summary>
        [Level(Severity.Critical)]
        NotUnique,

        /// <summary>
        /// 数据过长
        /// </summary>
        [Level(Severity.Critical)]
        DataTooLong,

        /// <summary>
        /// 字符串不能为零长度
        /// </summary>
        [Level(Severity.Critical)]
        NotAllowStringEmpty,

        /// <summary>
        /// 数据不能为空
        /// </summary>
        [Level(Severity.Critical)]
        NotAllowDataNull,

        /// <summary>
        /// 数据类型不匹配
        /// </summary>
        [Level(Severity.Critical)]
        DataTypeNotMatch,

        /// <summary>
        /// 自动产生值，不能指定
        /// </summary>
        [Level(Severity.Critical)]
        AutoValueOn,

        /// <summary>
        /// 对象更新失败，数据可能已被删除
        /// </summary>
        [Level(Severity.Critical)]
        ObjectUpdateFail,

        /// <summary>
        /// 由于约束机制，导致的错误
        /// </summary>
        [Level(Severity.Critical)]
        RestrictError,

        /// <summary>
        /// 缺少必要的属性
        /// </summary>
        [Level(Severity.Critical)]
        RequireAttribute,

        /// <summary>
        /// 未启动事务 
        /// </summary>
        [Level(Severity.Warning)]
        NoStartTrans,

        /// <summary>
        /// 目前不提供的方法、类型 
        /// </summary>
        [Level(Severity.Warning)]
        NoSupport,

        /// <summary>
        /// 目前未提供的方法数据库类型 
        /// </summary>
        [Level(Severity.Warning)]
        NoSupportDatabase,

        /// <summary>
        /// 配置文件格式错误
        /// </summary>
        [Level(Severity.Error)]
        XmlFormatException,

        /// <summary>
        /// 未发现
        /// </summary>
        [Level(Severity.Error)]
        NotFound,

        /// <summary>
        /// Xml文件错误
        /// </summary>
        [Level(Severity.Warning)]
        XmlReadError,

        /// <summary>
        /// Xml未找到
        /// </summary>
        [Level(Severity.Warning)]
        XmlNotFound,

        /// <summary>
        /// 集联信息配置错误
        /// </summary>
        [Level(Severity.Warning)]
        AssociationError,

        /// <summary>
        /// 未知错误 
        /// </summary>
        [Level(Severity.Warning)]
        Unknown
    }

    /// <summary>
    /// 错误级别  
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug,

        /// <summary>
        /// 信息
        /// </summary>
        Info,

        /// <summary>
        /// 通知
        /// </summary>
        Notice,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// 临界
        /// </summary>
        Critical,

        /// <summary>
        /// 类错误
        /// </summary>
        Unclassified
    }
}
