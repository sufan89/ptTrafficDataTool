using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using ptLogManager.Log;

namespace ptDbBase
{
    /// <summary>
    /// 数据库相关配置管理
    /// </summary>
    class ptServerXml
    {
        private string diretfile = AppDomain.CurrentDomain.BaseDirectory + "\\ServerConn\\";
        private string xmlfile = AppDomain.CurrentDomain.BaseDirectory + "\\ServerConn\\" + "Server.cfg";
        /// <summary>
        /// 默认数据库连接
        /// </summary>
        private DbConnectstring _defaultDbConn = null;
        /// <summary>
        /// 默认数据库连接
        /// </summary>
        public DbConnectstring DefaultDbConn
        {
            get { return _defaultDbConn; }
        }

        public ptServerXml()
        {
            if (!Directory.Exists(diretfile))
            {
                Directory.CreateDirectory(diretfile);
            }
            InitailizeXml();
        }
        /// <summary>
        /// 初始化服务配置信息
        /// </summary>
        public void InitailizeXml()
        {
            if (!File.Exists(xmlfile))
            {
                LogManager.WriteLog("默认配置信息未配置，请先配置。");
                return;
            }
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlfile);

                #region 读取默认数据库

                XmlNode defaultNode = xmlDoc.SelectSingleNode("//Config//DefaultDb//ConnectString");
                if (defaultNode != null)
                {
                    _defaultDbConn = SetConnectstring(defaultNode);
                }
                else
                {
                    LogManager.WriteLog("默认配置信息未配置，请先配置。");
                    throw new Exception("默认配置信息未配置，请先配置。");
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("读取配置文件出错", ex);
                throw;
            }
        }
        /// <summary>
        /// 根据配置文件节点，设置配置连接信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public DbConnectstring SetConnectstring(XmlNode node)
        {
            var db = new DbConnectstring();
            db.ConnectName = node.ChildNodes[0].InnerText;
            db.DbTypeString = node.ChildNodes[1].InnerText;
            db.ServerName = node.ChildNodes[2].InnerText;
            db.DatabaseName = node.ChildNodes[3].InnerText;
            db.DbUserName = node.ChildNodes[4].InnerText;
            db.DbUserPwd = node.ChildNodes[5].InnerText;
            db.DbVersion = node.ChildNodes[6].InnerText;
            return db;
        }
        /// <summary>
        /// 设置配置文件连接节点
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public XmlNode SetXmlNode(DbConnectstring dbConn, XmlDocument xmlDoc)
        {
            XmlNode node = xmlDoc.CreateElement("ConnectString");
            XmlNode node1 = xmlDoc.CreateElement("name");
            XmlNode node2 = xmlDoc.CreateElement("dbtype");
            XmlNode node3 = xmlDoc.CreateElement("servername");
            XmlNode node4 = xmlDoc.CreateElement("dbname");
            XmlNode node5 = xmlDoc.CreateElement("username");
            XmlNode node6 = xmlDoc.CreateElement("pwd");
            XmlNode node7 = xmlDoc.CreateElement("version");
            if (dbConn != null)
            {
                node1.InnerText = dbConn.ConnectName;
                node2.InnerText = dbConn.DbTypeString;
                node3.InnerText = dbConn.ServerName;
                node4.InnerText = dbConn.DatabaseName;
                node5.InnerText = dbConn.DbUserName;
                node6.InnerText = dbConn.DbUserPwd;
                node7.InnerText = dbConn.DbVersion;
            }
            node.AppendChild(node1);
            node.AppendChild(node2);
            node.AppendChild(node3);
            node.AppendChild(node4);
            node.AppendChild(node5);
            node.AppendChild(node6);
            node.AppendChild(node7);
            return node;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="defautdb"></param>
        /// <param name="slavedb"></param>
        /// <param name="sourcedb"></param>
        public void SaveptTrafficDb(DbConnectstring defautdb, DbConnectstring slavedb, DbConnectstring sourcedb)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                XmlNode node = xmlDoc.CreateElement("Config");
                xmlDoc.AppendChild(node);

                //默认数据库
                XmlNode dbnode = xmlDoc.CreateElement("DefaultDb");
                dbnode.AppendChild(SetXmlNode(defautdb, xmlDoc));
                node.AppendChild(dbnode);

                XmlNode slavenode = xmlDoc.CreateElement("SlaveDb");
                slavenode.AppendChild(SetXmlNode(slavedb, xmlDoc));
                node.AppendChild(slavenode);

                XmlNode sourcenode = xmlDoc.CreateElement("SourceDb");
                sourcenode.AppendChild(SetXmlNode(sourcedb, xmlDoc));
                node.AppendChild(sourcenode);
                if (File.Exists(xmlfile))
                {
                    File.Delete(xmlfile);
                }
                xmlDoc.Save(xmlfile);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("数据库配置文件保存出错", ex);
                throw;
            }
        }
    }

    public class DbConnectstring
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        public string ConnectName { get; set; }

        public string DbTypeString{set;get;}
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string DbUserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string DbUserPwd { get; set; }
        /// <summary>
        /// 数据库版本，主要是针对ArcSDE
        /// </summary>
        public string DbVersion { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectString
        {
            get
            {
                if (string.IsNullOrEmpty(ServerName) || string.IsNullOrEmpty(DatabaseName))
                    return string.Empty;
                switch (DbTypeString)
                {
                    case DatabaseType.Access:
                        return ptDbConnStringProvider.GetOdbcConnectString(ServerName,DbUserPwd);
                    case DatabaseType.ArcSDE:
                        return "";
                    case DatabaseType.Oracle:
                        return "";
                    case DatabaseType.Sqlite:
                        return "";
                    case DatabaseType.SqlServer:
                        return "";
                    default:
                        return "";

                }
            }
        }
    }
}
