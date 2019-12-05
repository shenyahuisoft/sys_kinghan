using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Web;

namespace Entity
{
    /// <summary>
    /// 系统环境信息类
    /// </summary>
    public class SystemEnvironment
    {
        private string m_ConfigFile;            //配置文件
        private ResourceInfo m_ResourceInfo;    //资源信息类
        private AuthorizationObject m_AuthorizationObject;    //证书资源对象
        private string m_MapPath;//站点路径    
        private ReviewederObject m_ReviewederObject;//审批人列表

        /// <summary>
        /// 构造函数
        /// </summary>
        public static readonly SystemEnvironment Instance = new SystemEnvironment();

        public static string _MapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用 
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    //strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\'); 
                    strPath = strPath.TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf(@"bin\")), strPath);
            }
        }
        private SystemEnvironment()
        {
            string processname = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower();
            //this.m_ConfigFile = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "ApplicationConfig.xml";
            this.m_ConfigFile = _MapPath("/App_Config/ApplicationConfig.xml");

            this.m_ResourceInfo = new ResourceInfo(m_ConfigFile);
            this.m_AuthorizationObject = GetAuthorizationObject(this.m_ConfigFile);
            //this.m_MapPath = System.Configuration.ConfigurationManager.AppSettings["MapPath"].ToString();
            this.m_ReviewederObject = GetReviewederObject(this.m_ConfigFile);
            this.m_MapPath = _MapPath("");
        }

        public ReviewederObject ReviewederObject
        {
            get
            {
                return this.m_ReviewederObject;
            }
        }

        /// <summary>
        /// 系统配置文件
        /// </summary>
        public string ConfigFile
        {
            get { return this.m_ConfigFile; }
            set { this.m_ConfigFile = value; }
        }

        public string MapPath
        {
            get { return this.m_MapPath; }
        }

        /// <summary>
        /// 默认数据资源
        /// </summary>
        public string DefaultDataSource
        {
            get
            {
                if (this.m_AuthorizationObject.DefaultDataSource == "")
                {
                    return "sqlsource";
                }
                else
                {
                    return this.m_AuthorizationObject.DefaultDataSource;
                }
            }
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnStr
        {
            get
            {
                System.Xml.XmlDocument xmldom = new System.Xml.XmlDocument();
                xmldom.Load(this.m_ConfigFile);
                System.Xml.XmlNode root = xmldom.DocumentElement;
                string defaultDS = root.SelectSingleNode("ResourceInfo/DefaultDataSource").InnerText;
                System.Xml.XmlNode node = root.SelectSingleNode("PersistenceInfo/dataSource[@name=\"" + defaultDS + "\"]");
                string uid = node.SelectSingleNode("parameter[@name=\"User ID\"]").Attributes["value"].Value;
                string pwd = node.SelectSingleNode("parameter[@name=\"Password\"]").Attributes["value"].Value;
                string db = node.SelectSingleNode("parameter[@name=\"Initial Catalog\"]").Attributes["value"].Value;
                return string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
                    this.AuthorizationObject.DataSource,
                    db,
                    uid,
                    EncryptTools.Decrypt(pwd));
            }
        }
        /// <summary>
        /// 资源信息类
        /// </summary>
        public ResourceInfo ResourceInfo
        {
            get { return this.m_ResourceInfo; }
        }

        /// <summary>
        /// 证书对象
        /// </summary>
        public AuthorizationObject AuthorizationObject
        {
            get { return this.m_AuthorizationObject; }
        }


        /// <summary>
        /// 获取权限对象
        /// </summary>
        /// <param name="applicationConfigFile"></param>
        /// <returns></returns>
        private AuthorizationObject GetAuthorizationObject(string applicationConfigFile)
        {
            System.Xml.XmlDocument xmldom = new System.Xml.XmlDocument();
            xmldom.Load(applicationConfigFile);
            System.Xml.XmlNode root = xmldom.DocumentElement;

            try
            {
                AuthorizationObject obj = new AuthorizationObject();
                obj.DefaultDataSource = root.SelectSingleNode("ResourceInfo/DefaultDataSource").InnerText;
                obj.DevelopeCompany = root.SelectSingleNode("ResourceInfo/DevelopeCompany").InnerText;
                obj.ReleaseDate = root.SelectSingleNode("ResourceInfo/ReleaseDate").InnerText;
                obj.ExpiryDate = root.SelectSingleNode("ResourceInfo/ExpiryDate").InnerText;
                obj.Version = root.SelectSingleNode("ResourceInfo/Version").InnerText;
                return obj;
            }
            catch { }
            return new AuthorizationObject();
        }

        /// <summary>
        /// 获取审批人列表
        /// </summary>
        /// <param name="applicationConfigFile"></param>
        /// <returns></returns>
        private ReviewederObject GetReviewederObject(string applicationConfigFile)
        {
            System.Xml.XmlDocument xmldom = new System.Xml.XmlDocument();
            xmldom.Load(applicationConfigFile);
            System.Xml.XmlNode root = xmldom.DocumentElement;
            System.Xml.XmlNode node = root.SelectSingleNode("ResourceInfo");
            try
            {
                ReviewederObject obj = new ReviewederObject();

                obj.LuoJun = new StaffObj()
                {
                    StaffID = node.SelectSingleNode("Revieweder[@name=\"LuoJun\"]").Attributes["staffid"].Value,
                    StaffName = node.SelectSingleNode("Revieweder[@name=\"LuoJun\"]").Attributes["staffname"].Value
                };
                obj.LiLong = new StaffObj()
                {
                    StaffID = node.SelectSingleNode("Revieweder[@name=\"LiLong\"]").Attributes["staffid"].Value,
                    StaffName = node.SelectSingleNode("Revieweder[@name=\"LiLong\"]").Attributes["staffname"].Value
                };
                return obj;
            }
            catch { }
            return new ReviewederObject();
        }
    }
}
