using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ptDbBase
{
   public class OdbcProvider:IptBaseDb
    {
       public int CommandTimeout
       {
           get;
           set;
       }

        public IDbConnection ConnectionObject
        {
            get { throw new NotImplementedException(); }
        }

        public bool TestConnection(ref string msg)
        {
            throw new NotImplementedException();
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public void ReSetConnectionString(string connString)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void CreateParam(System.Data.IDbCommand cmd, System.Data.DataTable dtParam, System.Data.DataRow rowValue)
        {
            throw new NotImplementedException();
        }

        public void CreateParam(System.Data.IDbCommand cmd, string paramName, object paramValue)
        {
            throw new NotImplementedException();
        }

        public System.Data.IDbDataParameter CreateParam(string paramName, object paramValue)
        {
            throw new NotImplementedException();
        }

        public System.Data.IDbDataParameter CreateParam(string paramName, object paramValue, System.Data.ParameterDirection paraDirect)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sqlCommand, bool transcation)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQueryS(string storedProcedureName, bool transcation)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQueryS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string[] sqlCommands, bool transcation)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        public void ExecuteReader(string sqlCommand, ReaderDelegateHandler dbReader)
        {
            throw new NotImplementedException();
        }

        public void ExecuteReader<T>(string sqlCommand, IList<T> list) where T : new()
        {
            throw new NotImplementedException();
        }

        public void ExecuteReaderS(string storedProcedureName, ReaderDelegateHandler dbReader)
        {
            throw new NotImplementedException();
        }

        public void ExecuteReaderS<T>(string storedProcedureName, IList<T> list) where T : new()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExecuteTable(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExecuteTable(string sqlCommand, string tableName)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExecuteTableS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExecuteTableS(string storedProcedureName, string tableName)
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet ExecuteDataSet(bool isStoredProcudure, string commandText)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet ExecuteDataSet(string commandText)
        {
            throw new NotImplementedException();
        }

        public void BulkOption(bool transaction, BulkUpdateHandler bulkUp)
        {
            throw new NotImplementedException();
        }

        public void BulkCopyTable(string sqlInsert, System.Data.IDbConnection connSelect, string sqlSelect)
        {
            throw new NotImplementedException();
        }

        public void BulkCopy(System.Data.DataTable dt)
        {
            throw new NotImplementedException();
        }

        public void BulkCopy(System.Data.DataTable dt, string deleteSql, string[] primaryKeys)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
