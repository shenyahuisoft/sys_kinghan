using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Models;
using CommonUtil;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using Newtonsoft.Json;
using Topshelf.Logging;

namespace AutoServices.Services
{
    public class HttpMethod
    {

        #region 请求方法

        /// <summary>
        /// 工人信息上传
        /// {"success":false,"resultCode":0,"msg":"校验码错误，请检查！","obj":null}
        /// {"success":true,"resultCode":0,"msg":"工人数据推送成功！","obj":[{"workerName":"沈亚辉","idCardNum":"419090909090909090","msg":"当前工人的施工许可证号无法查询到相应的工地信息，请核实后再试；","success":false}]}
        /// </summary>
        /// <returns></returns>
        public string SendWorkerInfo(List<WorkerInfo> workerInfos)
        {
            string rquestUrl = "manage/build/sendWorkerInfo";

            List<Dictionary<string, dynamic>> requestParams = new List<Dictionary<string, dynamic>>();

            foreach (var item in workerInfos)
            {
                Dictionary<string, dynamic> requestParam = new Dictionary<string, dynamic>();
                requestParam.Add("name", item.name);//工人姓名
                requestParam.Add("idCardNum", item.idCardNum);//身份证号
                requestParam.Add("gender", item.gender);//性别 0女 1男
                requestParam.Add("constructionPermitNum", item.constructionPermitNum);//工人所在工地的施工许可证号
                requestParam.Add("bankCardNum", item.bankCardNum);//工人银行卡号
                requestParam.Add("attenNum", item.attenNum);//考勤编号
                requestParam.Add("nation", item.nation);//民族
                requestParam.Add("headPicBase64", item.headPicBase64);//工人头像图片base64数据 否
                requestParam.Add("nativePlace", item.nativePlace);//工人籍贯  否
                requestParam.Add("phone", item.phone);//工人手机号
                requestParam.Add("workState", item.workState);//工人进退场状态（是否离职）， 0 已退场 1 已进场
                requestParam.Add("entranceTime", item.entranceTime);//进场时间（格式：yyyy-MM-dd）

                requestParams.Add(requestParam);
            }
            return SendMethod(requestParams, rquestUrl);
        }

        /// <summary>
        /// 工人考勤数据上传
        /// {"success":false,"resultCode":0,"msg":"校验码错误，请检查！","obj":null}
        /// {"success":true,"resultCode":0,"msg":"考勤数据已推送！","obj":[{"constructionPermitNum":"99999999","projectName":"郑州第一工地","msg":"未查询到施工许可证号对应的工地信息，请联系平台管理员！","success":false}]}
        /// </summary>
        /// <returns></returns>
        public string SendAttendanceInfo(AttendanceInfo attendanceInfo, LogWriter _log, string connectionString)
        {
            //List<WorkerAttenInfo> workerAttenInfos = new List<WorkerAttenInfo>();
            ////初始化考勤数据
            //workerAttenInfos.Add(new WorkerAttenInfo()
            //{
            //    attenCode = "0001",//工人考勤编号
            //    attenTime = "2019-08-08 12:30:12",//考勤时间
            //    idCardNum = "410222199009090998",//工人身份证号 
            //    workerName = "张三",//工人姓名
            //    inoutType = 1,//进出标识
            //    photoBase64 = "",
            //});

            int dataCount = 50;
            string result = "";
            string sqlStr = "";
            StringBuilder sqlWheresb = new StringBuilder();
            List<WorkerAttenInfo> postData = new List<WorkerAttenInfo>();
            for (int i = 0; i < attendanceInfo.WorkerAttenInfos.Count; i += dataCount)
            {
                postData = attendanceInfo.WorkerAttenInfos.Skip(i).Take(dataCount).ToList();
                if (postData.Count == 0)
                {
                    break;
                }
                string rquestUrl = "manage/build/sendAttendanceInfo";
                List<Dictionary<string, dynamic>> requestParams = new List<Dictionary<string, dynamic>>();
                Dictionary<string, dynamic> requestParam = new Dictionary<string, dynamic>();
                requestParam.Add("projectName", attendanceInfo.projectName);//工地名称
                requestParam.Add("constructionPermitNum", attendanceInfo.constructionPermitNum);//工地施工许可证号
                requestParam.Add("workerAttenInfo", postData.Select(_item => new WorkerAttenInfoBase
                {
                    workerName = _item.workerName,
                    idCardNum = _item.idCardNum,
                    inoutType = _item.inoutType,
                    attenTime = _item.attenTime,
                    photoBase64 = _item.photoBase64,
                    attenCode = _item.attenCode
                }).ToList());//该工地下的考勤数据集合
                requestParams.Add(requestParam);

                result = SendMethod(requestParams, rquestUrl);

                _log.InfoFormat("AttendanceInfoSyscTask result:{0}", result);
                dynamic resultObj = JsonConvert.DeserializeObject<dynamic>(result);

                //更新上传的考勤数据
                if (resultObj.resultCode == 0)
                {
                    sqlWheresb.Clear();
                    for (int j = 0; j < postData.Count; j++)
                    {
                        sqlWheresb.AppendFormat("'{0}'", postData[j].recordID);
                        if (j < postData.Count - 1)
                        {
                            sqlWheresb.Append(" ,");
                        }
                    }

                    sqlStr = string.Format("UPDATE record SET ISAsync = '1' WHERE ISAsync = '0' OR ISAsync IS NULL AND RecordID IN ({0})", sqlWheresb.ToString());
                    int resultint = new SQLHelper(connectionString).ExecuteNonQuery(sqlStr, CommandType.Text);

                    _log.InfoFormat("AttendanceInfoSyscTask resultint:{0}", resultint);
                }
                else
                {
                    _log.InfoFormat("AttendanceInfoSyscTask 返回结果异常：{0}", result);
                    break;
                }

            }
            return sqlStr;

        }

        #endregion

        #region private

        private static HttpHelper http = new HttpHelper();

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="rquestUrl"></param>
        /// <returns></returns>
        private string SendMethod(List<Dictionary<string, dynamic>> requestParams, string rquestUrl)
        {
            string postStr = JsonConvert.SerializeObject(requestParams, Formatting.None);

            CookieCollection _cookieCollection = new CookieCollection();
            HttpItem item = new HttpItem()
            {
                URL = string.Format("{0}/{1}", RequestHelper.HostUrl, rquestUrl),
                Method = "POST",//URL     可选项 默认为Get
                ContentType = "application/json",//application/json、application/x-www-form-urlencoded
                KeepAlive = true,
                Postdata = postStr, //RequestHelper.GetPostData(requestParams)
                ResultCookieType = ResultCookieType.CookieCollection,
                CookieCollection = _cookieCollection,
                PostEncoding = Encoding.UTF8,
                Encoding = Encoding.UTF8
            };
            //请求的返回值对象
            item.Header.Add("Authorization", getAuthorization());
            HttpResult result = http.GetHtml(item);
            return result.Html;
        }

        /// <summary>
        /// 获取加密后的值
        /// </summary>
        /// <returns></returns>
        private string getAuthorization()
        {
            string authorizationStr = RequestHelper.UserName + RequestHelper.Password + DateTime.Now.ToString("yyyy-MM-dd");
            return EncryptTools.EncryptMD5(authorizationStr);
        }

        #endregion
    }

    public class resultObj
    {
        public bool success { get; set; }

        public int resultCode { get; set; }

        public string msg { get; set; }
    }
}
