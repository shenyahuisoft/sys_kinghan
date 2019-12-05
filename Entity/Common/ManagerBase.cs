using System;
using System.Configuration;
using System.Collections;
using System.Data;
using DatabaseLayer;

namespace Entity
{
    /// <summary>
    /// 系统业务基础类
    /// </summary>
    public abstract class ManagerBase
    {
        private SystemEnvironment m_systemEnvironment;
        //private string m_AppConfigFile;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ManagerBase()
        {
            this.m_systemEnvironment = SystemEnvironment.Instance;
            DatabaseLayer.PersistenConfig.Instance().ApplicationContextFile = m_systemEnvironment.ConfigFile;
            try
            {
                DatabaseLayer.PersistenConfig.Instance().Initialize();
            }
            catch (DatabaseLayer.PersistenceLayerException ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// SystemEnvironment
        /// </summary>
        public SystemEnvironment SystemEnvironment
        {
            get { return this.m_systemEnvironment; }
        }
    }
}