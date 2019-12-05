using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Common;
using AutoServices.Models;
using AutoServices.Models.SaveYCJCService;
using AutoServices.Services;
using CommonUtil;

namespace AutoServices.JobTask
{
    public class TBJDataUploadTask : BaseJob<TBJDataUploadTask>
    {
        public override void ExecuteProgramJob(SourceTaskItem sourceTask, LogTaskItem logTask)
        {
            _log.InfoFormat(DateTime.Now + Environment.NewLine + "TBJDataUploadTask 开始执行");
            Excate(sourceTask);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Excate(SourceTaskItem sourceTask)
        {
            string sqlStr = @"
                SELECT
	                ISNULL(TransToType.longitude,'0') AS longitude, ISNULL(TransToType.latitude,'0') AS latitude, C.*
                FROM
                (
	                SELECT 
		                *
	                FROM
	                (
	                SELECT ROW_NUMBER() OVER(PARTITION BY deviceAddress ORDER BY dataTime DESC) AS RowNumber, *FROM
	                (
		                SELECT
			                *
		                FROM  baseData

		                WHERE deviceAddress IN(SELECT deviceAddress FROM TransToType WHERE transTo1 = '9')
	                ) AS A
	                ) AS B WHERE RowNumber = 1 -- AND deviceAddress ='11004'  '11018' '11036'
                ) AS C LEFT JOIN TransToType ON C.deviceAddress = TransToType.deviceAddress  
                ";


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
                baseDataModel.sensorCount = ConvertObject.ToString(item["sensorCount"], "");
                baseDataModel.PM25 = ConvertObject.ToString(item["PM25"], "");
                baseDataModel.PM10 = ConvertObject.ToString(item["PM10"], "");
                baseDataModel.noise = ConvertObject.ToString(item["noise"], "");
                baseDataModel.temperature = ConvertObject.ToString(item["temperature"], "");
                baseDataModel.humidity = ConvertObject.ToString(item["humidity"], "");
                baseDataModel.windSpeed = ConvertObject.ToString(item["windSpeed"], "");
                baseDataModel.windPower = ConvertObject.ToString(item["windPower"], "");
                baseDataModel.windDirection = ConvertObject.ToString(item["windDirection"], "");
                baseDataModel.dataTime = ConvertObject.ToDateTime(item["dataTime"]);
                baseDataModel.transTo = ConvertObject.ToString(item["transTo"], "");
                baseDataModel.transCode = ConvertObject.ToString(item["transCode"], "");

                baseDataModel.longitude = ConvertObject.ToString(item["longitude"], "");
                baseDataModel.latitude = ConvertObject.ToString(item["latitude"], "");


                //十分钟之内的数据才会统计
                if (baseDataModel.dataTime >= now.AddMinutes(-10))
                {
                    dataList.Add(baseDataModel);
                }
            }
            foreach (BaseDataModel item in dataList)
            {
                TBJDataUploadService _TBJDataUploadService = new TBJDataUploadService();
                _TBJDataUploadService.UploadData(item);
                //sqlStr = string.Format("UPDATE baseData SET ISAsync='1' WHERE ID = '{0}'", baseDataModel.ID);
                //int result = new SQLHelper(sourceTask.ConnectionString).ExecuteNonQuery(sqlStr, CommandType.Text);
                //_log.InfoFormat("{0}{1}记录更新结果为：{2}，{3}", DateTime.Now, Environment.NewLine, sqlStr, result);
                //Thread.Sleep(1000);
            }
        }
    }
}
