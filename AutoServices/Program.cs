using System;
using System.IO;
using AutoServices.Services;
using Topshelf;

namespace AutoServices
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            HostFactory.Run(x =>
            {
                x.UseLog4Net();

                //x.Service<ServiceRunner>();
                x.Service(settings => new ServiceRunner(), s =>
                {
                    //s.BeforeStartingService(hsc => hsc.CancelStart());在服务启动的时候
                    //s.AfterStartingService(hsc => { started = true; });//在服务启动后调用的回调
                });

                x.RunAsLocalSystem();

                x.SetDescription("sys_kinghan作业");
                x.SetDisplayName("sys_kinghan作业服务");
                x.SetServiceName("sys_kinghanServices");
                x.EnablePauseAndContinue();

            });

            ////////////////////////////////////
            //TBJDataUploadService tBJDataUploadService = new TBJDataUploadService();
            //tBJDataUploadService.UploadData(null);

        }
    }
}
