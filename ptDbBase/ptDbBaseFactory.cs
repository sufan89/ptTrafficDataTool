using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ptLogManager.Log;

namespace ptDbBase
{
   public class ptDbBaseFactory
    {
       /// <summary>
       /// 数据库连接字符串
       /// </summary>
       private static string _connstring = string.Empty;
       /// <summary>
       /// 默认的数据库类型
       /// </summary>
       private static string currentDbType = DatabaseType.Access;

       static ptDbBaseFactory()
        {
            InitializeDefault();

        }
       /// <summary>
       /// 加载默认配置
       /// </summary>
       private static void InitializeDefault()
       {
           ptServerXml xml = new ptServerXml();
           if (xml.DefaultDbConn != null)
           {
               currentDbType = xml.DefaultDbConn.DbTypeString;
               _connstring = xml.DefaultDbConn.ConnectString;
           }
           LogManager.WriteLog(DateTime.Now.ToString());
       }
       /// <summary>
       /// 通用数据库提供服务（后台动态配置数据库）
       /// </summary>
       /// <returns></returns>
       public static IptBaseDb GetNewDbServer()
       {
           switch (currentDbType)
           {
               case DatabaseType.Access:
                   return GetNewSqlServer();
               case DatabaseType.ArcSDE:
                   return GetNewOracleServer();
               case DatabaseType.Oracle:
                   return;
               case DatabaseType.Sqlite:
                   return;
               case DatabaseType.SqlServer:
                   break;
               default:
                   return GetNewSqlServer();
           }
       }
       private static IptBaseDb GetNewSqlServer()
       {
           return new SqlServerProvider();
       }
       private static IptBaseDb GetNewOdbcServer()
       {
           return new OdbcProvider();
       }

    }
    /// <summary>
    /// 数据库类型
    /// </summary>
   public class DatabaseType
   {
       /// <summary>
       /// SQL Server 数据库
       /// </summary>
       public const string SqlServer = "sqlserver";
       /// <summary>
       /// Oracle 数据库
       /// </summary>
       public const string Oracle = "oracle";
       /// <summary>
       /// Sqlite 数据库
       /// </summary>
       public const string Sqlite = "sqlite";
       /// <summary>
       /// Access数据库
       /// </summary>
       public const string Access = "access";
       /// <summary>
       /// SDE连接
       /// </summary>
       public const string ArcSDE = "arcsde";
   }
}
