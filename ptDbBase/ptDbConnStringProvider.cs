using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ptDbBase
{
   public class ptDbConnStringProvider
    {
        #region 设置数据库连接字符串


        /// <summary>
        /// 设置SQLCE数据库连接字符串
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="databasePwd"></param>
        public static string GetSqlCeConnetString(string databaseName, string databasePwd)
        {
            if (string.IsNullOrEmpty(databasePwd))
            {
                return "Data Source=" +
                        string.Format(@"{0}\{1}",
                                      Path.GetDirectoryName(
                                          Assembly.GetExecutingAssembly().GetName().CodeBase),
                                      databaseName)
                    ;
            }
            else
            {
                return "Data Source=" +
                       string.Format(@"{0}\{1};password={2}",
                                     Path.GetDirectoryName(
                                         Assembly.GetExecutingAssembly().GetName().CodeBase),
                                     databaseName,
                                     databasePwd)
                    ;
            }
        }
        /// <summary>
        /// 设置Oracle数据库连接字符串
        /// </summary>
        /// <param name="serverIp"></param>
        /// <param name="databaseName"></param>
        /// <param name="userId"></param>
        /// <param name="userPwd"></param>
        public static string GetOracleConnectString(string serverIp, string databaseName, string userId, string userPwd)
        {
            return string.Format("SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={1})));uid={2};pwd={3};Unicode=true;",
                                 serverIp, databaseName, userId, userPwd);
        }
        /// <summary>
        /// 设置Oracle数据库连接字符串
        /// </summary>
        /// <param name="serverIp"></param>
        /// <param name="databaseName"></param>
        /// <param name="userId"></param>
        /// <param name="userPwd"></param>
        /// <param name="ipPort"></param>
        public static string GetOracleConnectString(string serverIp, string databaseName, string userId, string userPwd, string ipPort)
        {
            return string.Format("SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={4}))(CONNECT_DATA=(SERVICE_NAME={1})));uid={2};pwd={3};Unicode=true;",
                                 serverIp, databaseName, userId, userPwd, ipPort);
        }
        /// <summary>
        /// 设置SqlServer数据库连接字符串
        /// </summary>
        /// <param name="serverIp"></param>
        /// <param name="databaseName"></param>
        /// <param name="userId"></param>
        /// <param name="userPwd"></param>
        /// <param name="isWindowsLogin"></param>
        public static string GetSqlServerConnectString(string serverIp, string databaseName, string userId, string userPwd, bool isWindowsLogin)
        {
            if (isWindowsLogin)
            {
                return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;",
                                     serverIp, databaseName);
            }
            else
            {
                return string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
                                     serverIp, databaseName, userId, userPwd);
            }

        }
       /// <summary>
       /// 获取MDB连接字符串
       /// </summary>
       /// <param name="mdbFileName"></param>
       /// <param name="userPwd"></param>
       /// <returns></returns>
        public static string GetOdbcConnectString(string mdbFileName,string userPwd="")
        {
            if (string.IsNullOrEmpty(userPwd))
            {
                return string.Format("provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", mdbFileName);
            }
            else
            {
                return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=True;Jet OLEDB:Database password={1}",
                    mdbFileName, userPwd);
            }
        }
        #endregion 
    }
}
