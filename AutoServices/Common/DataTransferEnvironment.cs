using AutoServices.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutoServices.Common
{


    /// <summary>
    /// 数据库任务
    /// </summary>
    public class DataTransferEnvironment
    {

        #region 变量

        /// <summary>
        /// ApplicationConfig中数据库映射的集合
        /// </summary>
        private Hashtable m_DatabaseMaps;

        /// <summary>
        /// 
        /// </summary>
        private string m_DataTransferConfigFile;
        #endregion


        //配置文件
        private static DataTransferEnvironment Instance = null;
        public static DataTransferEnvironment GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataTransferEnvironment();
            }
            return Instance;
        }
        private DataTransferEnvironment()
        {
            this.m_DataTransferConfigFile = System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, System.AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"))
                + "DataTransfer.xml";

            LoadConfigInformation();
        }


        #region 获取ApplicationConfig中DataSource信息

        /// <summary>
        /// 获取ApplicationConfig中DataSource信息
        /// </summary>
        private void LoadConfigInformation()
        {
            XmlDocument oXmlDocument = new XmlDocument();
            DatabaseCollection = new Dictionary<string, Tuple<string, string, string>>();
            //Assert.VerifyNotEquals(databaseXmlFile, "", Error.PesistentError, "请设置配置文件");
            try
            {
                oXmlDocument.Load(this.m_DataTransferConfigFile);
                XmlNodeReader oXmlReader = new XmlNodeReader(oXmlDocument);

                //加载数据源
                while (oXmlReader.Read())
                {
                    if (oXmlReader.NodeType != XmlNodeType.Element) continue;
                    if (oXmlReader.Name.ToLower() == "datasource")
                    {
                        DatabaseCollection.Add(oXmlReader.GetAttribute("name"), GetConnectionStr(oXmlReader));
                    }
                }

                //加载Tasks
                System.Xml.XmlNode root = oXmlDocument.DocumentElement;
                XmlNodeList xmlNodeList = root.SelectNodes("dataTransfer/task");
                LoadTask(xmlNodeList);

                //加载WebServiceUrls
                XmlNodeList xmlNodeListUrls = root.SelectNodes("WebServiceUrls/url");
                LoadWebServiceUrl(xmlNodeListUrls);
            }
            catch (PersistenceLayerException pException)
            {
                throw pException;
            }
            catch (Exception e)
            {
                string strErr = "错误：读取类映射文件" + this.m_DataTransferConfigFile + "发生错误，请确认你的文件路径及格式！" + e.Message;
                //Assert.Fail(Error.XmlReadError, strErr);
            }
        }

        public void LoadWebServiceUrl(XmlNodeList xmlNodeList)
        {
            WebServiceUrls = new List<KeyValuePair<string, string>>();
            foreach (XmlNode item in xmlNodeList)
            {
                string enabled = item.Attributes["enabled"] == null ? "1" : item.Attributes["enabled"].Value;
                if (enabled != "1")
                {
                    continue;
                }
                string key = item.Attributes["key"] == null ? "1" : item.Attributes["key"].Value;
                string value = item.Attributes["value"] == null ? "1" : item.Attributes["value"].Value;
                if ((!string.IsNullOrEmpty(key)) && (!string.IsNullOrEmpty(value)))
                {
                    WebServiceUrls.Add(new KeyValuePair<string, string>(key, value));
                }
            }
        }

        /// <summary>
        /// 加载任务
        /// </summary>
        private void LoadTask(XmlNodeList xmlNodeList)
        {


            TaskModels = new List<TaskModel>();

            foreach (XmlNode item in xmlNodeList)
            {
                TaskModel taskModel = new TaskModel();
                taskModel.Enabled = item.Attributes["enabled"] == null ? "1" : item.Attributes["enabled"].InnerText;
                if (taskModel.Enabled != "1")
                {
                    continue;
                }

                taskModel.Name = item.Attributes["name"] == null ? "" : item.Attributes["name"].InnerText;
                taskModel.Type = item.Attributes["type"] == null ? "program" : item.Attributes["type"].InnerText;

                XmlNodeList parameterItemList = item.SelectNodes("parameter");
                foreach (XmlNode parameterItem in parameterItemList)
                {
                    string name = parameterItem.Attributes["name"] == null ? "" : parameterItem.Attributes["name"].InnerText;
                    if (name == "plan")
                    {
                        string appDomainPath = System.AppDomain.CurrentDomain.BaseDirectory;
                        taskModel.PlanTask = new PlanTaskItem()
                        {
                            ClassName = parameterItem.Attributes["classname"] == null ? "" : parameterItem.Attributes["classname"].InnerText,
                            //DllName = parameterItem.Attributes["dllname"] == null ? "" :
                            //appDomainPath.Substring(0, appDomainPath.IndexOf("bin"))
                            //+ "dll\\" + parameterItem.Attributes["dllname"].InnerText,
                            Frequency = parameterItem.Attributes["frequency"] == null ? "" : parameterItem.Attributes["frequency"].InnerText,
                            MonthDay = parameterItem.Attributes["monthday"] == null ? "" : parameterItem.Attributes["monthday"].InnerText,
                            Time = parameterItem.Attributes["time"] == null ? "" : parameterItem.Attributes["time"].InnerText,
                            WeekDay = parameterItem.Attributes["weekday"] == null ? "" : parameterItem.Attributes["weekday"].InnerText
                        };
                    }
                    else if (name == "source")
                    {
                        taskModel.SourceTask = new SourceTaskItem()
                        {
                            DataSource = parameterItem.Attributes["datasource"] == null ? "" : parameterItem.Attributes["datasource"].InnerText,
                            TableName = parameterItem.Attributes["tablename"] == null ? "" : parameterItem.Attributes["tablename"].InnerText,
                            Query = parameterItem.Attributes["query"] == null ? "" : parameterItem.Attributes["query"].InnerText,
                            ProcName = parameterItem.Attributes["procname"] == null ? "" : parameterItem.Attributes["procname"].InnerText,
                            CommandtType = parameterItem.Attributes["commandttype"] == null ? "" : parameterItem.Attributes["commandttype"].InnerText
                        };
                    }
                    else if (name == "target")
                    {
                        taskModel.TargetTask = new TargetTaskItem()
                        {
                            DataSource = parameterItem.Attributes["datasource"] == null ? "" : parameterItem.Attributes["datasource"].InnerText,
                            TableName = parameterItem.Attributes["tablename"] == null ? "" : parameterItem.Attributes["tablename"].InnerText,
                            Increment = parameterItem.Attributes["increment"] == null ? "" : parameterItem.Attributes["increment"].InnerText,
                        };
                    }
                    else if (name == "log")
                    {
                        taskModel.LogTask = new LogTaskItem()
                        {
                            DataSource = parameterItem.Attributes["datasource"] == null ? "" : parameterItem.Attributes["datasource"].InnerText,
                            TableName = parameterItem.Attributes["tablename"] == null ? "" : parameterItem.Attributes["tablename"].InnerText
                        };
                    }
                }
                TaskModels.Add(taskModel);
            }
        }

        /// <summary>
        /// 生成IPersistenceProvider对象(子方法)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Tuple<string, string, string> GetConnectionStr(XmlNodeReader node)
        {
            //<dataSource name="REP" type="SQLServer">
            //  <parameter name="Data Source" value="." />
            //  <parameter name="Initial Catalog" value="REP" />
            //  <parameter name="User ID" value="sa" />
            //  <parameter name="Password" value="Fde6HlO4RfcbFvb5GO6qKA==" />
            //  <parameter name="Encrypt" value="" />
            //</dataSource>

            string dbName = node.GetAttribute("name");
            string dbType = node.GetAttribute("type");

            string connectionString = "";
            if (dbName != null)
            {
                if (dbType == null || dbType == "")
                {
                    //Assert.Fail(Error.NoSupportDatabase);
                }
                //else
                //{
                //    string dbClassName = GetDbClassName(dbType);

                //    try
                //    {
                //        rdb = (IPersistenceProvider)this.GetType().Assembly.CreateInstance(dbClassName);
                //        if (rdb == null) throw new Exception();
                //    }
                //    catch
                //    {
                //        //Assert.Fail(Error.DatabaseConnectError);
                //    }
                //}
                //rdb.Name = dbName;
                //this.m_DatabaseMaps.Add(dbName, new DatabaseMap(dbName));
                int i = node.Depth;

                string pName = "";
                string pValue = "";

                string strPassword = "";
                //是否为加密信息
                bool blnEncrypt = false;
                //加密类
                string strEncryptClass = "";

                while (node.Read() && node.Depth > i)
                {
                    //取数据配置信息
                    if ((node.NodeType == XmlNodeType.Element) && (node.Name == "parameter"))
                    {
                        pName = node.GetAttribute("name");
                        pValue = node.GetAttribute("value");
                        if ((pName != null) && (pValue != null))
                        {
                            if (pName.ToLower() == "password")
                            {
                                strPassword = pValue;
                            }
                            else if (pName.ToLower() == "encrypt")
                            {
                                blnEncrypt = true;
                                strEncryptClass = pValue;
                            }
                            else
                            {
                                connectionString += pName + "=" + pValue + ";";
                            }
                        }
                    }
                }
                //如果为加密状态，则进行解密
                if (blnEncrypt)
                {
                    pValue = strPassword;
                    ////如果没有设置解密函数，则使用默认解密函数
                    //if (strEncryptClass.Equals(""))
                    //{
                    //    BaseEncryptClass baseEncry = new BaseEncryptClass();
                    //    pValue = baseEncry.Decrypt(strPassword);
                    //}
                    //else
                    //{
                    //    //使用自定义解密码函数
                    //    try
                    //    {
                    //        BaseEncryptClass baseEncry = (BaseEncryptClass)LoadType(strEncryptClass).Assembly.CreateInstance(strEncryptClass);
                    //        pValue = baseEncry.Decrypt(strPassword);
                    //    }
                    //    catch (Exception)
                    //    {
                    //        //Assert.Fail("装载自定义解密函数" + strEncryptClass + "出错，请检查解密函数是否存在！！！");
                    //    }
                    //}
                    connectionString += "password=" + pValue + ";";
                }
            }
            return new Tuple<string, string, string>(dbName, dbType, connectionString);
        }
        #endregion


        #region 静态方法

        /// <summary>
        /// 根据数据类型取操作类
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string GetDbClassName(string dbType)
        {
            string dbClassName = "";
            switch (dbType)
            {
                case "SQLServer":
                case "MSSqlServer":
                    dbClassName = "DatabaseLayer.Provider.SQLServer.SQLServerProvider";
                    break;
                case "MSAccess":
                case "Excel":
                    dbClassName = "DatabaseLayer.Provider.Excel.ExcelProvider";
                    break;
                case "Oracle":
                case "MySQL":
                    dbClassName = "DatabaseLayer.Provider.MySQL.MySQLProvider";
                    break;
                default:
                    //Assert.Fail(Error.NoSupportDatabase);
                    break;
            }
            return dbClassName;
        }

        /// <summary>
        /// 根据类型装载对象
        /// </summary>
        /// <param name="TypeName">类型名</param>
        /// <returns></returns>
        public static Type LoadType(string className)
        {
            if (className == null) return null;
            className = className.Trim();
            Type type = null;
            //获得相应的 命名空间
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            if (assembly.GetType(className) != null) return assembly.GetType(className);
            type = System.Reflection.Emit.TypeBuilder.GetType(className);
            if (type != null) return type;

            string[] nameSpaces = className.Split('.');
            string strTemp = "";
            for (int i = 0; i < nameSpaces.Length; i++)
            {
                if (strTemp != "") strTemp += ".";
                strTemp += nameSpaces[i];
                assembly = null;
                try
                {
                    assembly = Assembly.Load(strTemp);
                    type = assembly.GetType(className);
                    if (type != null) return type;
                }
                catch (Exception)
                {
                    //throw new ApplicationBaseException("装载类型出错",ex); 
                }
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 数据任务配置文件
        /// </summary>
        public string DataTransferConfigFile
        {
            get { return this.m_DataTransferConfigFile; }
            set { this.m_DataTransferConfigFile = value; }
        }

        /// <summary>
        /// 任务列表 
        /// </summary>
        public List<TaskModel> TaskModels { get; set; }

        public List<KeyValuePair<string, string>> WebServiceUrls { get; set; }

        /// <summary>
        /// 数据库连接池
        /// </summary>
        public IDictionary<string, Tuple<string, string, string>> DatabaseCollection;
    }
}
