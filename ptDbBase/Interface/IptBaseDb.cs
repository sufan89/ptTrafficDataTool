using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ptDbBase
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
   public interface IptBaseDb:IDisposable
    {
        /// <summary>
        /// 脚本执行的超时时间
        /// </summary>
        int CommandTimeout { get; set; }
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        IDbConnection ConnectionObject { get; }
       /// <summary>
       /// 测试连接
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
        bool TestConnection(ref string msg);
        /// <summary>
        /// 取得当前连接串
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();
        /// <summary>
        /// 取得当前连接串
        /// </summary>
        /// <returns></returns>
        void ReSetConnectionString(string connString);
        /// <summary>
        /// 提交当前的保存(开启事务情况下使用)
        /// </summary>
        void Save();
        /// <summary>
        /// 创建参数方法
        /// </summary>
        /// <param name="cmd">sql对象命令</param>
        /// <param name="dtParam">参数表格</param>
        /// <param name="rowValue">赋值行</param>
        /// <returns></returns>
        void CreateParam(IDbCommand cmd, DataTable dtParam, DataRow rowValue);
        /// <summary>
        /// 创建参数方法
        /// </summary>
        /// <param name="cmd">sql对象命令</param>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">赋值</param>
        /// <returns></returns>
        void CreateParam(IDbCommand cmd, string paramName, object paramValue);
        /// <summary>
        /// 创建参数方法(默认方法，BulkOption方法禁用)
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        IDbDataParameter CreateParam(string paramName, object paramValue);
        /// <summary>
        /// 创建参数方法
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="paraDirect">参数是只可输入、只可输出、双向还是存储过程返回值参数</param>
        IDbDataParameter CreateParam(string paramName, object paramValue, ParameterDirection paraDirect);
        /// <summary>
        /// 执行普通SQL命令(此功能默认支持带事务)
        /// </summary>
        /// <param name="sqlCommand">Sql命令</param>
        /// <returns>受影响的记录数</returns>
        int ExecuteNonQuery(string sqlCommand);
        /// <summary>
        /// 执行普通SQL命令(此功能支持带事务)
        /// </summary>
        /// <param name="sqlCommand">Sql命令</param>
        /// <param name="transcation"></param>
        /// <returns>受影响的记录数</returns>
        int ExecuteNonQuery(string sqlCommand, bool transcation);
        /// <summary>
        /// 执行存储过程(此功能支持带事务)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="transcation"></param>
        /// <returns>受影响的记录数</returns>
        int ExecuteNonQueryS(string storedProcedureName, bool transcation);
        /// <summary>
        /// 执行存储过程(此功能默认支持带事务)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <returns>受影响的记录数</returns>
        int ExecuteNonQueryS(string storedProcedureName);
        /// <summary>
        /// 执行批量SQL命令(此功能支持带事务)
        /// </summary>
        /// <param name="sqlCommands">Sql命令</param>
        /// <param name="transcation"></param>
        /// <returns>受影响的记录数</returns>
        int ExecuteNonQuery(string[] sqlCommands, bool transcation);
        /// <summary>
        /// 执行普通SQL命令(不支持事务，主要用来读取数据)
        /// </summary>
        /// <param name="sqlCommand">Sql命令</param>
        /// <returns>返回第一行第一列数据</returns>
        object ExecuteScalar(string sqlCommand);
        /// <summary>
        /// 执行存储过程名称(不支持事务，主要用来读取数据)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <returns>返回第一行第一列数据</returns>
        object ExecuteScalarS(string storedProcedureName);
        /// <summary>
        /// 执行普通SQL,带有Reader读取功能(不支持事务，主要用来读取数据)
        /// 推荐此方法,效率最快
        /// </summary>
        /// <param name="sqlCommand">Sql命令</param>
        /// <param name="dbReader">数据绑定代理方法</param>
        void ExecuteReader(string sqlCommand, ReaderDelegateHandler dbReader);
        /// <summary>
        /// 执行普通SQL,带有Reader读取功能(不支持事务，主要用来读取数据)
        /// </summary>
        /// <param name="sqlCommand">Sql命令</</param>
        /// <param name="list">实体集合</param>
        void ExecuteReader<T>(string sqlCommand, IList<T> list)
            where T : new();
        /// <summary>
        /// 执行存储过程名称,带有Reader读取功能(不支持事务，主要用来读取数据)
        /// 推荐此方法,效率最快
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="dbReader">数据绑定代理方法</param>
        void ExecuteReaderS(string storedProcedureName, ReaderDelegateHandler dbReader);
        /// <summary>
        /// 执行存储过程名称,带有Reader读取功能(不支持事务，主要用来读取数据)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</</param>
        /// <param name="list">实体集合</param>
        void ExecuteReaderS<T>(string storedProcedureName, IList<T> list)
            where T : new();
        /// <summary>
        /// 执行SQL,返回一个Table(不支持事务，主要用来读取数据)
        /// </summary>
        /// <param name="sqlCommand">SQL命令</param>
        /// <returns>返回表数据，默认表名("Table1")</returns>
        DataTable ExecuteTable(string sqlCommand);
        /// <summary>
        /// 执行SQL,返回一个Table(不支持事务，主要用来读取数据)
        /// <param name="sqlCommand">SQL命令</param>
        /// <param name="tableName">表名</param>
        /// </summary>
        /// <returns>返回表数据</returns>
        DataTable ExecuteTable(string sqlCommand, string tableName);
        /// <summary>
        /// 执行存储过程名称,返回一个Table(不支持事务，主要用来读取数据)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <returns>返回表数据，默认表名("Table1")</returns>
        DataTable ExecuteTableS(string storedProcedureName);
        /// <summary>
        /// 执行存储过程名称,返回一个Table(不支持事务，主要用来读取数据)
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="tableName">表名</param>
        /// </summary>
        /// <returns>返回表数据</returns>
        DataTable ExecuteTableS(string storedProcedureName, string tableName);
        /// <summary>
        /// 关闭连接 
        /// </summary>
        void CloseConnection();
        /// <summary>
        /// 执行sql(语句/存储过程)返回dataset
        /// </summary>
        /// <param name="isStoredProcudure"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(bool isStoredProcudure, string commandText);
        /// <summary>
        /// 执行sql 返回dataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText);

        #region 批量录入
        /// <summary>
        /// 高效批量操作（添加参数注意）
        /// 如果使用参数，则需要传输 cmd进入
        ///  void CreateParam(IDbCommand cmd, string paramName, object paramValue);
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="bulkUp"></param>
        void BulkOption(bool transaction, BulkUpdateHandler bulkUp);
        /// <summary>
        /// 数据库表数据批量导入
        /// </summary>
        /// <param name="sqlInsert">目标数据库插入数据脚本</param>
        /// <param name="connSelect">源数据库连接</param>
        /// <param name="sqlSelect">源数据库查询脚本</param>
        void BulkCopyTable(string sqlInsert, IDbConnection connSelect, string sqlSelect);
        /// <summary>
        /// 批量插入数据
        /// <param name="dt">要插入的表</param>
        /// </summary>
        void BulkCopy(DataTable dt);
        /// <summary>
        /// 批量插入数据（用于增量更新，但无法更新已删除的数据）
        /// </summary>
        /// <param name="dt">要插入的表</param>
        /// <param name="deleteSql">清除原表数据sql</param>
        /// <param name="primaryKeys">按表主键进行清除</param>
        void BulkCopy(DataTable dt, string deleteSql, string[] primaryKeys);
        #endregion

    }
   /// <summary>
   /// DataReader代理执行
   /// </summary>
   /// <param name="reader">读取器</param>
   public delegate void ReaderDelegateHandler(IDataReader reader);

   /// <summary>
   /// 批量操作委托
   /// </summary>
   /// <param name="cmd"></param>
   public delegate void BulkUpdateHandler(IDbCommand cmd);
}
