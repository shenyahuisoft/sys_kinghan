using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using DatabaseLayer;
using System.Reflection;

namespace Entity
{
    public class ErrorLog
    {

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="e"></param>
        public static void Write(Exception ex)
        {
            Write(ex, "", "");
        }

        public static void Write(Exception ex, string remark)
        {
            Write(ex, "", remark);
        }

        /// <summary>
        /// 写错误记录
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="staffid"></param>
        /// <param name="remark"></param>
        public static void Write(Exception ex, string staffid, string remark)
        {
            //b_Log_Error obj = new b_Log_Error();
            //obj.ErrorDate = CommonUtil.ConvertDateTime.ToDateString();
            //obj.StaffID = staffid;
            //obj.Message = (ex.Message == null) ? "Null" : ex.Message;
            //obj.Source = (ex.Source == null) ? "Null" : ex.Source;
            //obj.StackTrace = (ex.StackTrace == null) ? "Null" : ex.StackTrace;
            //obj.Remark = remark;
            //new b_Log_ErrorManager().InsEntityObject(obj);
        }

        /// <summary>
        /// 写错误备注
        /// </summary>
        /// <param name="remark"></param>
        public static void Write(string remark)
        {
            //b_Log_Error obj = new b_Log_Error();
            //obj.ErrorDate = CommonUtil.ConvertDateTime.ToDateString();
            //obj.Remark = remark;
            //new b_Log_ErrorManager().InsEntityObject(obj);
        }

        /// <summary>
        /// 写日志备注
        /// </summary>
        /// <param name="remark"></param>
        public static void WriteLog(string remark)
        {
            WriteLog(remark, "LOG");
        }

        /// <summary>
        /// 写日志备注
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="status"></param>
        public static void WriteLog(string remark, string status)
        {
            //b_Log_Error obj = new b_Log_Error();
            //obj.ErrorDate = CommonUtil.ConvertDateTime.ToDateString();
            //obj.Remark = remark;
            //obj.Status = status;
            //new b_Log_ErrorManager().InsEntityObject(obj);
        }

    }
}
