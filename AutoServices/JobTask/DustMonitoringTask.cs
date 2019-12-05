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

namespace AutoServices.JobTask
{
    public class DustMonitoringTask : BaseJob<DustMonitoringTask>
    {
        public override void ExecuteProgramJob(SourceTaskItem sourceTask, LogTaskItem logTask)
        {
            string sqlStr = @"
                SELECT 
                    *
                FROM
                (
                SELECT ROW_NUMBER() OVER(PARTITION BY deviceAddress ORDER BY dataTime DESC) AS RowNumber, *FROM
                (
                    SELECT
                        *
                    FROM  baseData

                    WHERE deviceAddress IN(SELECT deviceAddress FROM TransToType WHERE transTo = '12')
                ) AS A
                ) AS B WHERE RowNumber = 1 AND deviceAddress ='11056'";


            //_log.InfoFormat(DateTime.Now + Environment.NewLine + "DustMonitoringTask sqlStr:{0}", sqlStr);

            
            DataTable dt = new SQLHelper(sourceTask.ConnectionString).ExecuteQuery(sqlStr, CommandType.Text);

            DateTime now = DateTime.Now;
            _log.InfoFormat(DateTime.Now + Environment.NewLine + "DustMonitoringTask sqlStr:{0}", dt.Rows.Count);

            List<BaseDataModel> dataList = new List<BaseDataModel>();
            foreach (DataRow item in dt.Rows)
            {
                BaseDataModel baseDataModel = new BaseDataModel();
                baseDataModel.ID = ConvertObject.ToInt32(item["ID"]);
                baseDataModel.deviceAddress = ConvertObject.ToString(item["deviceAddress"]); ;
                baseDataModel.sensorCount = ConvertObject.ToString(item["sensorCount"], "0");
                baseDataModel.PM25 = ConvertObject.ToString(item["PM25"], "0");
                baseDataModel.PM10 = ConvertObject.ToString(item["PM10"], "0");
                baseDataModel.noise = ConvertObject.ToString(item["noise"], "0");
                baseDataModel.temperature = ConvertObject.ToString(item["temperature"], "0");
                baseDataModel.humidity = ConvertObject.ToString(item["humidity"], "0");
                baseDataModel.windSpeed = ConvertObject.ToString(item["windSpeed"], "0");
                baseDataModel.windPower = ConvertObject.ToString(item["windPower"], "0");
                baseDataModel.windDirection = ConvertObject.ToString(item["windDirection"], "0");
                baseDataModel.dataTime = ConvertObject.ToDateTime(item["dataTime"]);
                baseDataModel.transTo = ConvertObject.ToString(item["transTo"], "0");
                baseDataModel.transCode = ConvertObject.ToString(item["transCode"], "0");

                //十分钟之内的数据才会统计
                if (baseDataModel.dataTime >= now.AddMinutes(-10))
                {
                    dataList.Add(baseDataModel);
                }
            }
            foreach (BaseDataModel item in dataList)
            {
                DustMonitoringService dustMonitoringService = new DustMonitoringService();
                dustMonitoringService.Regester(item);
                while (true)
                {
                    if (dustMonitoringService._client.State == Cowboy.Sockets.TcpSocketConnectionState.Closed)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                //sqlStr = string.Format("UPDATE baseData SET ISAsync='1' WHERE ID = '{0}'", baseDataModel.ID);
                //int result = new SQLHelper(sourceTask.ConnectionString).ExecuteNonQuery(sqlStr, CommandType.Text);
                //_log.InfoFormat("{0}{1}记录更新结果为：{2}，{3}", DateTime.Now, Environment.NewLine, sqlStr, result);
                //Thread.Sleep(1000);
            }

        }
    }
}
