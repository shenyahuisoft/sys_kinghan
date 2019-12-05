using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoServices.Common;
using AutoServices.Models;
using AutoServices.Models.SaveYCJCService;
using AutoServices.Services;
using CommonUtil;
using Newtonsoft.Json;

namespace AutoServices.JobTask
{
    /// <summary>
    /// 扬尘监控平台数据接入
    /// </summary>
    public class UpLoadSaveYCJCTask : BaseJob<UpLoadSaveYCJCTask>
    {
        private readonly YCJCService service;
        public UpLoadSaveYCJCTask()
        {
            service = new YCJCService();
        }
        public override void ExecuteProgramJob(SourceTaskItem sourceTask, LogTaskItem logTask)
        {
            _log.InfoFormat(DateTime.Now + Environment.NewLine + "UpLoadSaveYCJCTask 开始执行");
            Excate(sourceTask);
        }

        /// <summary>
        /// 执行
        /// DevID:|:911000011#|#Time:|:2016-06-29 12:48:33#|#HUMI:|:27#|#TEMP:|:30#|#PRE:|:150#|#WINDD:|:200#|#WINDS:|:3#|#NOISE:|:120#|#PM25:|:45#|#PM10:|:100.00#|#TSP:|:300
        /// </summary>
        private void Excate(SourceTaskItem sourceTask)
        {
            string sqlStr = "SELECT * FROM baseData WHERE ISAsync='0' OR ISAsync IS NULL";
            _log.InfoFormat(DateTime.Now + Environment.NewLine + "UpLoadSaveYCJCTask sqlStr:{0}", sqlStr);

            List<BaseDataModel> baseDataModels = new List<Models.SaveYCJCService.BaseDataModel>();

            DataTable dt = new SQLHelper(sourceTask.ConnectionString).ExecuteQuery(sqlStr, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                baseDataModels.Add(new BaseDataModel()
                {
                    ID = ConvertObject.ToInt32(dt.Rows[i]["ID"]),
                    deviceAddress = ConvertObject.ToString(dt.Rows[i]["deviceAddress"].ToString(), "-1"),
                    sensorCount = ConvertObject.ToString(dt.Rows[i]["sensorCount"].ToString(), "-1"),
                    PM25 = ConvertObject.ToString(dt.Rows[i]["PM25"].ToString(), "-1"),
                    PM10 = ConvertObject.ToString(dt.Rows[i]["PM10"].ToString(), "-1"),
                    noise = ConvertObject.ToString(dt.Rows[i]["noise"].ToString(), "-1"),
                    temperature = ConvertObject.ToString(dt.Rows[i]["temperature"].ToString(), "-1"),
                    humidity = ConvertObject.ToString(dt.Rows[i]["humidity"].ToString(), "-1"),
                    windSpeed = ConvertObject.ToString(dt.Rows[i]["windSpeed"].ToString(), "-1"),
                    windPower = ConvertObject.ToString(dt.Rows[i]["windPower"].ToString(), "-1"),
                    windDirection = ConvertObject.ToString(dt.Rows[i]["windDirection"].ToString(), "-1"),
                    dataTime = ConvertObject.ToDateTime(dt.Rows[i]["dataTime"].ToString()),
                    transTo = ConvertObject.ToString(dt.Rows[i]["transTo"].ToString(), "-1"),
                    transCode = ConvertObject.ToString(dt.Rows[i]["transCode"].ToString(), "-1")
                });
            }
            foreach (BaseDataModel item in baseDataModels)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                //获取数据源
                #region 参数
                pairs.Add(YCJCService.DevID, item.deviceAddress);//deviceAddress
                pairs.Add(YCJCService.Time, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                pairs.Add(YCJCService.HUMI, item.humidity);//humidity
                pairs.Add(YCJCService.TEMP, item.temperature);//temperature
                pairs.Add(YCJCService.PRE, "-1");
                pairs.Add(YCJCService.WINDD, item.windDirection);//windDirection
                pairs.Add(YCJCService.WINDS, item.windSpeed);//windSpeed
                pairs.Add(YCJCService.NOISE, item.noise);//noise
                pairs.Add(YCJCService.PM25, item.PM25);//PM25
                pairs.Add(YCJCService.PM10, item.PM10);//PM10
                pairs.Add(YCJCService.TSP, "-1");
                #endregion
                string resultStr = service.SaveYCJC(pairs, "Service_JZ");
                _log.InfoFormat("{0}{1}SaveYCJC 结果:{2}", DateTime.Now, Environment.NewLine, resultStr);

                ResultClass jsonObj = JsonConvert.DeserializeObject<ResultClass>(resultStr);
                if (string.IsNullOrEmpty(jsonObj.True))
                {
                    _log.InfoFormat("{0}{1}SaveYCJC 结果出现错误:{2}", DateTime.Now, Environment.NewLine, jsonObj.False);
                }
                else
                {
                    sqlStr = string.Format("UPDATE baseData SET ISAsync='1' WHERE ID = '{0}'", item.ID);
                    int result = new SQLHelper(sourceTask.ConnectionString).ExecuteNonQuery(sqlStr, CommandType.Text);
                    _log.InfoFormat("{0}{1}记录更新结果为：{2}，{3}", DateTime.Now, Environment.NewLine, sqlStr, result);
                }
                Thread.Sleep(30000);
            }

        }
    }
}
