using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServices.SaveYCJCService01;

namespace AutoServices.Models.SaveYCJCService
{
    public class SaveYCJCServiceUrlModel
    {
        public SaveYCJCServiceUrlModel(string url, SaveYCJCServicePortType saveYCJCServicePortType)
        {
            Url = url;
            SaveYCJCServicePortType = saveYCJCServicePortType;
        }
        public string Url { get; set; }

        public SaveYCJCServicePortType SaveYCJCServicePortType { get; set; }
    }
}
