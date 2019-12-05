using System;					//系统
using System.Xml;				//系统.XML
using System.IO;				//系统.IO
using System.Globalization;     //系统.全球化
using System.Collections;

namespace Entity
{
    /// <summary> 
    /// 资源信息类
    /// </summary>
    /// <namespace>MyEmpire.Person.Business</namespace>
    /// <version>1.0.0.0</version>
    public class ResourceInfo
    {
        #region	定义

        #region 常量
        /// <summary>
        /// 资源信息节点名称
        /// </summary>
        private const string RESOURCEINFO = "ResourceInfo";
        #endregion

        #region 对象
        /// <summary>
        /// 资源节点
        /// </summary>
        private XmlNode m_ResourceNode = null;
        /// <summary>
        /// 资源表
        /// </summary>
        Hashtable m_Resourcetable = new Hashtable();
        #endregion

        #endregion

        #region	构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="applicationConfigFile" >
        /// 应用程序配置文件
        /// </param>
        /// <exception cref="SystemEnvironmentException">
        /// 系统环境错误
        /// </exception>		
        public ResourceInfo(string applicationConfigFile)
        {
            try
            {
                this.m_ResourceNode = this.m_GetResourceInfoNode(applicationConfigFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region IResourceInfo 成员

        #region Get String
        /// <summary>
        /// 根据指定编码取得相应字符串信息
        /// </summary>
        /// <param name="id">
        /// string：指定编码
        /// </param>
        /// <returns>
        /// string：字符串信息
        /// </returns>
        /// <developdate from="2004.10.11" to="2004.10.11"/>
        public string GetString(string id)
        {
            //--------------------------------------------------------------------------------
            if (id == null)
            {
                return null;
            }
            if (id.Trim().Length == 0)
            {
                return null;
            }
            //--------判断是否为用户自定义资源信息--------------------------------------------
            if (m_Resourcetable[id] != null)
            {
                return m_Resourcetable[id].ToString();
            }
            //--------------------------------------------------------------------------------
            if (this.m_ResourceNode == null)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            for (int i = 0; i <= this.m_ResourceNode.ChildNodes.Count - 1; i++)
            {
                if (this.m_ResourceNode.ChildNodes[i].Name == id)
                {
                    return this.m_ResourceNode.ChildNodes[i].InnerText;
                }
            }
            return null;
        }

        #endregion

        #region SetResource
        /// <summary>
        /// 设定指定编码值
        /// </summary>
        /// <param name="id">编码</param>
        /// <param name="Value">编码值</param>
        public void SetResource(string id, string Value)
        {
            //--------------------------------------------------------------------------------
            if (id == null)
            {
                return;
            }

            //--------判断是否为用户自定义资源信息--------------------------------------------
            if (m_Resourcetable[id] != null)
            {
                this.m_Resourcetable.Remove(id);
            }
            this.m_Resourcetable.Add(id, Value);
            //--------------------------------------------------------------------------------
        }

        #endregion

        #region Get Int
        /// <summary>
        /// 根据指定编码取得相应整数信息
        /// </summary>
        /// <param name="id">
        /// string：指定编码
        /// </param>
        /// <returns>
        /// int：整数信息
        /// </returns>
        public int GetInt(string id)
        {
            return Convert.ToInt32(this.GetString(id), CultureInfo.InvariantCulture);
        }
        #endregion

        #endregion

        #region m_GetResourceInfoNode
        /// <summary>
        /// 获得资源节点
        /// </summary>
        /// <param name="file">
        /// string：配置文件名
        /// </param>
        /// <exception cref="SystemEnvironmentException">
        /// 系统环境错误
        /// </exception>
        ///	<returns>
        ///	XmlNode：配置节点
        ///	</returns> 
        /// <developdate from="2004.10.11" to="2004.10.11"/>
        /// <developer></developer>
        private XmlNode m_GetResourceInfoNode(string file)
        {
            //--------------------------------------------------------------------------------
            if (file == null)
            {
                return null;
            }
            if (file.Trim().Length == 0)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            //文件是否存在
            if (File.Exists(file) == false)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            //载入文件
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(file);
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            //--------------------------------------------------------------------------------
            //获得文档节点
            XmlElement element = document.DocumentElement;
            XmlNodeList nodeList = null;
            //--------------------------------------------------------------------------------
            //节点
            nodeList = element.GetElementsByTagName(RESOURCEINFO);
            if (nodeList.Count != 1)
            {
                element = null;
                nodeList = null;
                return null;
            }
            //--------------------------------------------------------------------------------
            element = null;
            if (nodeList.Item(0).HasChildNodes == false)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            return nodeList.Item(0);
        }
        #endregion

    }
}
