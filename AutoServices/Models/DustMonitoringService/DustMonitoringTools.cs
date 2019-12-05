using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public class DustMonitoringTools
    {
        /// <summary>
        /// 判读包的类型
        /// </summary>
        public static CMDCodeEnum CheckMsgType(byte[] byteTemp)
        {
            if (byteTemp.Length == 13)
            {
                switch (byteTemp[1].ToString())
                {
                    case "1":
                        return CMDCodeEnum.Register;
                    case "2":
                        return CMDCodeEnum.RegisterResult;
                    case "3":
                        return CMDCodeEnum.NoiseUpload;
                    case "4":
                        return CMDCodeEnum.NoiseUploadResult;
                    case "5":
                        return CMDCodeEnum.DustUpload;
                    case "6":
                        return CMDCodeEnum.DustUploadResult;
                }
            }
            return CMDCodeEnum.None;
        }

        public static short bytesToShort(byte[] src, int offset)
        {
            return (short)((src[offset] & 0xFF)
                    | ((src[offset + 1] & 0xFF) << 8));
        }

        public static ResultCode GetResultCode(byte[] byteTemp)
        {
            if (byteTemp.Length == 13)
            {
                int resultCode = bytesToShort(byteTemp, 9);
                switch (resultCode)
                {
                    case 0:
                        return ResultCode.Success;
                    case 1:
                        return ResultCode.NotFinddatafram;
                    case 2:
                        return ResultCode.NoLogin;
                    case 3:
                        return ResultCode.SystemError;
                    case 4:
                        return ResultCode.DataRormateError;
                }
            }
            return ResultCode.None;
        }
    }
}
