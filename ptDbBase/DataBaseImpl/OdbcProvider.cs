using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace ptDbBase
{
   public class OdbcProvider:IptBaseDb
    {
       public OdbcProvider(string connString)
       {
           CommandTimeout = 300;
           m_DbConn = new OleDbConnection(connString);
       }
       public OdbcProvider()
       {
           CommandTimeout = 300;
       }
       private  OleDbConnection m_DbConn = null;

       public int CommandTimeout
       {
           get;
           set;
       }
       private IDbConnection odbcConnection;

       IDbConnection IptBaseDb.ConnectionObject
       {
           get { return odbcConnection; }
       }

        bool IptBaseDb.TestConnection(ref string msg)
        {
            throw new NotImplementedException();
        }

        string IptBaseDb.GetConnectionString()
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.ReSetConnectionString(string connString)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.Save()
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.CreateParam(IDbCommand cmd, DataTable dtParam, DataRow rowValue)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.CreateParam(IDbCommand cmd, string paramName, object paramValue)
        {
            throw new NotImplementedException();
        }

        IDbDataParameter IptBaseDb.CreateParam(string paramName, object paramValue)
        {
            throw new NotImplementedException();
        }

        IDbDataParameter IptBaseDb.CreateParam(string paramName, object paramValue, ParameterDirection paraDirect)
        {
            throw new NotImplementedException();
        }

        int IptBaseDb.ExecuteNonQuery(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        int IptBaseDb.ExecuteNonQuery(string sqlCommand, bool transcation)
        {
            throw new NotImplementedException();
        }

        int IptBaseDb.ExecuteNonQueryS(string storedProcedureName, bool transcation)
        {
            throw new NotImplementedException();
        }

        int IptBaseDb.ExecuteNonQueryS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        int IptBaseDb.ExecuteNonQuery(string[] sqlCommands, bool transcation)
        {
            throw new NotImplementedException();
        }

        object IptBaseDb.ExecuteScalar(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        object IptBaseDb.ExecuteScalarS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.ExecuteReader(string sqlCommand, ReaderDelegateHandler dbReader)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.ExecuteReader<T>(string sqlCommand, IList<T> list)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.ExecuteReaderS(string storedProcedureName, ReaderDelegateHandler dbReader)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.ExecuteReaderS<T>(string storedProcedureName, IList<T> list)
        {
            throw new NotImplementedException();
        }

        DataTable IptBaseDb.ExecuteTable(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        DataTable IptBaseDb.ExecuteTable(string sqlCommand, string tableName)
        {
            throw new NotImplementedException();
        }

        DataTable IptBaseDb.ExecuteTableS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        DataTable IptBaseDb.ExecuteTableS(string storedProcedureName, string tableName)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.CloseConnection()
        {
            throw new NotImplementedException();
        }

        DataSet IptBaseDb.ExecuteDataSet(bool isStoredProcudure, string commandText)
        {
            throw new NotImplementedException();
        }

        DataSet IptBaseDb.ExecuteDataSet(string commandText)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkOption(bool transaction, BulkUpdateHandler bulkUp)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkCopyTable(string sqlInsert, IDbConnection connSelect, string sqlSelect)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkCopy(DataTable dt)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkCopy(DataTable dt, string deleteSql, string[] primaryKeys)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
