using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ptDbBase
{
    class SqliteProvider:IptBaseDb
    {
        public SqliteProvider(string connString)
        {
 
        }
        public SqliteProvider()
        {
 
        }
        int IptBaseDb.CommandTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        System.Data.IDbConnection IptBaseDb.ConnectionObject
        {
            get { throw new NotImplementedException(); }
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

        void IptBaseDb.CreateParam(System.Data.IDbCommand cmd, System.Data.DataTable dtParam, System.Data.DataRow rowValue)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.CreateParam(System.Data.IDbCommand cmd, string paramName, object paramValue)
        {
            throw new NotImplementedException();
        }

        System.Data.IDbDataParameter IptBaseDb.CreateParam(string paramName, object paramValue)
        {
            throw new NotImplementedException();
        }

        System.Data.IDbDataParameter IptBaseDb.CreateParam(string paramName, object paramValue, System.Data.ParameterDirection paraDirect)
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

        System.Data.DataTable IptBaseDb.ExecuteTable(string sqlCommand)
        {
            throw new NotImplementedException();
        }

        System.Data.DataTable IptBaseDb.ExecuteTable(string sqlCommand, string tableName)
        {
            throw new NotImplementedException();
        }

        System.Data.DataTable IptBaseDb.ExecuteTableS(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        System.Data.DataTable IptBaseDb.ExecuteTableS(string storedProcedureName, string tableName)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.CloseConnection()
        {
            throw new NotImplementedException();
        }

        System.Data.DataSet IptBaseDb.ExecuteDataSet(bool isStoredProcudure, string commandText)
        {
            throw new NotImplementedException();
        }

        System.Data.DataSet IptBaseDb.ExecuteDataSet(string commandText)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkOption(bool transaction, BulkUpdateHandler bulkUp)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkCopyTable(string sqlInsert, System.Data.IDbConnection connSelect, string sqlSelect)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkCopy(System.Data.DataTable dt)
        {
            throw new NotImplementedException();
        }

        void IptBaseDb.BulkCopy(System.Data.DataTable dt, string deleteSql, string[] primaryKeys)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
