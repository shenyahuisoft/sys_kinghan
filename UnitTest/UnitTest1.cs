using System;
using System.Collections.Generic;
using System.Text;
using AutoServices.Common;
using AutoServices.Models;
using AutoServices.Models.MonitoringPollutants;
using AutoServices.Models.SaveYCJCService;
using AutoServices.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSendWorkerInfo()
        {
            new HttpMethod().SendWorkerInfo(new List<WorkerInfo>());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestSendAttendanceInfo()
        {
            new HttpMethod().SendAttendanceInfo(new AttendanceInfo(), null, null);
        }

        [TestMethod]
        public void Test()
        {
            string ss = @"E:\百年\sourceCode\sys_kinghan\AutoServices\bin\Debug\";

            string sd = ss.Substring(0, ss.IndexOf(@"bin\"));
        }

        [TestMethod]
        public void ReadJsonTest()
        {
            string jsonStr = "{\"true\":\"0\"}";

            ResultClass jsonObj = JsonConvert.DeserializeObject<ResultClass>(jsonStr);

            string strTrue = jsonObj.True;

            string strFalse = jsonObj.False;
        }

        [TestMethod]
        public void ToCRC16()
        {
            //string inputStr = @"QN=20160801085857223;ST=32;CN=1062;PW=100000;MN=010000A8900016F000169DC0;Flag=5;CP=&&RtdInterval=30&&";

            //int length = inputStr.Length;
            //string result = CRC.ToCRC16(inputStr, Encoding.ASCII, false);

            //Console.WriteLine(result);
            BaseDataModel _baseDataModel = new BaseDataModel() {
                dataTime=DateTime.Now,
                deviceAddress="11056",
                humidity="12",
                ID=12,
                latitude= "114.213452",
                longitude= "34.232157",
                noise="12",
                PM10="12",
                PM25="12",
                sensorCount="12",
                temperature="12",
                transCode="12",
                transTo="12",
                TSP="12",
                windDirection="12",
                windPower="12",
                windSpeed="12"
            };

            RequstModel requstModel = new RequstModel();
            string resultStr = requstModel.ToString(new DataModel()
            {
                QN = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                ST = "39",
                CN = "2011",
                PW = "123456",
                MN = "11004",
                Flag = "5",
                CP = string.Format("&&DataTime={0};" +
                "a01001-Rtd={1},a01001-Flag=N;" +
                "a01002-Rtd={2},a01002-Flag=N;" +
                "a01006-Rtd={3},a01006-Flag=N;" +
                "a01007-Rtd={4},a01007-Flag=N;" +
                "a01008-Rtd={5},a01008-Flag=N;" +
                "a34001-Rtd={6},a34001-Flag=N;" +
                "a34002-Rtd={7},a34002-Flag=N;" +
                "a34004-Rtd={8},a34004-Flag=N;" +
                "La-Rtd={9},La-Flag=N;" +
                "longitude={10};latitude={11};&&",
                DateTime.Now.ToString("yyyyMMddhhmmss"),
                _baseDataModel.temperature,     //温度 摄氏度
                _baseDataModel.humidity,     //湿度 %
                0,     //气压 千帕
                _baseDataModel.windSpeed,     //风速 米/秒
                _baseDataModel.windDirection,     //风向 角度
                _baseDataModel.TSP,     //总悬浮颗粒物 TSP
                _baseDataModel.PM10,     //可吸入颗粒物 PM10
                _baseDataModel.PM25,     //细微颗粒物 PM2.5
                _baseDataModel.noise,     //噪声 分贝
                _baseDataModel.longitude,
                _baseDataModel.latitude
                ),
            });

            Console.WriteLine(resultStr);

        }

        [TestMethod]
        public void UploadData()
        {
            //MonitoringPollutantsService monitoringPollutantsService = new MonitoringPollutantsService();
            //monitoringPollutantsService.UploadNoiseData();


        }


    }


}
