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

       private static DbConnectstring _DbConnect = null;

       static void ptDbBaseFactory()
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
               _DbConnect = xml.DefaultDbConn;
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
                   return GetNewOdbcServer();
               case DatabaseType.ArcSDE:
                   return GetNewSdeServer();
               case DatabaseType.Oracle:
                   return GetNewOracleServer();
               case DatabaseType.Sqlite:
                   return GetNewSqliteServer();
               case DatabaseType.SqlServer:
                   return GetNewSqlServer();
               default:
                   return GetNewSqlServer();
           }
       }
       public static IptBaseDb GetNewDbServer(string dbtype, string connString)
       {
           if (string.IsNullOrEmpty(dbtype))
           {
               switch (dbtype)
               {
                   case DatabaseType.Access:
                       return GetNewOdbcServer(connString);
                   case DatabaseType.ArcSDE:
                       return GetNewSdeServer(connString);
                   case DatabaseType.Oracle:
                       return GetNewOracleServer(connString);
                   case DatabaseType.Sqlite:
                       return GetNewSqliteServer(connString);
                   case DatabaseType.SqlServer:
                       return GetNewSqlServer(connString);
                   default:
                       return GetNewOdbcServer(connString);
               }
           }
           else
           {
               return GetNewDbServer();
           }
       }
       /// <summary>
       /// 创建SQLServer连接对象
       /// </summary>
       /// <returns></returns>
       private static IptBaseDb GetNewSqlServer()
       {
           return new SqlServerProvider();
       }
       private static IptBaseDb GetNewSqlServer(string connString)
       {
           return new SqlServerProvider(connString);
       }
       /// <summary>
       /// 新建MDB数据库连接对象
       /// </summary>
       /// <returns></returns>
       private static IptBaseDb GetNewOdbcServer()
       {
           return new OdbcProvider();
       }
       private static IptBaseDb GetNewOdbcServer(string connString)
       {
           return new OdbcProvider(connString);
       }
       /// <summary>
       /// 新建Oracle数据库连接对象
       /// </summary>
       /// <returns></returns>
       private static IptBaseDb GetNewOracleServer()
       {
           return new OracleServerProvider();
       }
       private static IptBaseDb GetNewOracleServer(string connString)
       {
           return new OracleServerProvider(connString);
       }
       /// <summary>
       /// 新建Sqlite数据库连接对象
       /// </summary>
       /// <returns></returns>
       private static IptBaseDb GetNewSqliteServer()
       {
           return new SqliteProvider();
       }
       private static IptBaseDb GetNewSqliteServer(string connString)
       {
           return new SqliteProvider(connString);
       }
       /// <summary>
       /// 新建SDE连接对象
       /// </summary>
       /// <returns></returns>
       private static IptBaseDb GetNewSdeServer()
       {
           return new SdeServerProvider();
       }
       private static IptBaseDb GetNewSdeServer(string connString)
       {
           return new SdeServerProvider(connString);
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
