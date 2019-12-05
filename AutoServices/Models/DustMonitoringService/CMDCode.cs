using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{

    public enum CMDCodeEnum
    {
        /// <summary>
        /// 注册登记
        /// </summary>
        Register,

        /// <summary>
        /// 注册登记
        /// </summary>
        RegisterResult,

        /// <summary>
        /// 噪声数据上报
        /// </summary>
        NoiseUpload,

        /// <summary>
        /// 噪声数据上报
        /// </summary>
        NoiseUploadResult,

        /// <summary>
        /// 扬尘数据上报
        /// </summary>
        DustUpload,

        /// <summary>
        /// 扬尘数据上报
        /// </summary>
        DustUploadResult,

        /// <summary>
        /// 未知
        /// </summary>
        None
    }


    /// <summary>
    /// 控制码
    /// </summary>
    public class CMDCode
    {
        /// <summary>
        /// 注册登记
        /// </summary>
        public static string Register = "01";

        /// <summary>
        /// 注册登记应答
        /// </summary>
        public static string RegisterResult = "02";

        /// <summary>
        /// 噪声数据上报
        /// </summary>
        public static string NoiseUpload = "03";

        /// <summary>
        /// 噪声数据上报应答
        /// </summary>
        public static string NoiseUploadResult = "04";

        /// <summary>
        /// 扬尘数据上报
        /// </summary>
        public static string DustUpload = "05";

        /// <summary>
        /// 扬尘数据上报应答
        /// </summary>
        public static string DustUploadResult = "06";
    }
}
