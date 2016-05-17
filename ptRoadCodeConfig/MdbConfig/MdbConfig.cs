using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ptRoadCodeConfig
{
    class MdbConfig:IConfig
    {
        OleDbConnection m_DbConn = null;

        public MdbConfig()
        {
            InitializeConfig();
        }

        public void InitializeConfig()
        {
            string StrProvider = string.Format("provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", FilePathEntity.m_DbConfigFile);
            try
            {
                if (File.Exists(FilePathEntity.m_DbConfigFile))
                {
                    m_DbConn = new OleDbConnection(StrProvider);
                }
            }
            catch (Exception ex)
            {
                m_DbConn = null;
            }
        }

        public void GetLayerConfig()
        {
            throw new NotImplementedException();
        }

        public void GetCodeRuleConfig()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开启数据库连接
        /// </summary>
        /// <returns></returns>
        private bool OpenConn()
        {
            bool flag = false;
            if (m_DbConn != null && m_DbConn.State == ConnectionState.Closed)
            {
                try
                {
                    m_DbConn.Open();
                    flag = true;
                    return flag;
                }
                catch (Exception ex)
                {
                    return flag;
                }
            }
            return flag;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        private bool CloseConn()
        {
            bool flag = false;
            if (m_DbConn != null && m_DbConn.State != ConnectionState.Closed)
            {
                try
                {
                    m_DbConn.Close();
                    flag = true;
                    return flag;
                }
                catch (Exception ex)
                {
                    return flag;
                }
            }
            return flag;
        }
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetTableByName(string TableName,string StrWhere="")
        {
            DataTable dt = new DataTable();
            try
            {
                string StrSql = "";
                if (string.IsNullOrEmpty(StrWhere))
                {
                    StrSql = string.Format("select * from {0}", TableName);
                }
                else
                {
                    StrSql = string.Format("select * from {0} where {1}", TableName, StrWhere);
                }
                if (OpenConn())
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    adapter.SelectCommand = new OleDbCommand(StrSql, m_DbConn);
                    adapter.Fill(dt);
                }

            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                CloseConn();
            }
            return dt;
        }
    }
}
