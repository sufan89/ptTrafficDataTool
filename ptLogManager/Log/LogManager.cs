using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.IO;

namespace ptLogManager.Log
{
    public class LogManager
    {
        private static string logfilePath = AppDomain.CurrentDomain.BaseDirectory + "\\LOG";

        private static string info = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// 系统发布程序信息
        /// </summary>
        public static string SysInfomation { set { info = value; } get { return info; } }
        /// <summary>
        /// 日志管理
        /// </summary>
        static LogManager()
        {

        }
        /// <summary>
        /// 是否调试状态
        /// </summary>
        public static bool IsDebug = false;
        /// <summary>
        /// 写日记
        /// </summary>
        /// <param name="content"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteLog(string content)
        {
            try
            {
                if (!Directory.Exists(logfilePath)) Directory.CreateDirectory(logfilePath);
                string filename = string.Format("{0}/{1}Log.txt", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    if (!string.IsNullOrEmpty(SysInfomation))
                    {
                        sw.WriteLine(SysInfomation);
                    }
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine(content);
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 写系统错误日志
        /// </summary>
        /// <param name="title">功能模块名称</param>
        /// <param name="ex">错误异常</param>
        /// <example>
        ///     <remarks>本事例演示如何写错误日志</remarks>
        ///         <code>
        ///           //功能模块名称，ex 错误操作失败
        ///           //调用以下方法,将自定义信息写入系统日志中
        ///           LogManage.Instance.WriteLog("测试模块异常", ex);
        ///         </code>
        ///    <value>无</value>
        /// </example>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteLog(string title, Exception ex)
        {
            try
            {

                if (!Directory.Exists(logfilePath)) Directory.CreateDirectory(logfilePath);
                string filename = string.Format("{0}/{1}Log.txt", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    if (!string.IsNullOrEmpty(SysInfomation))
                    {
                        sw.WriteLine(SysInfomation);
                    }
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine(title);
                    sw.WriteLine(ex.Message);
                    sw.WriteLine(ex.StackTrace);
                    if (ex.InnerException != null)
                        sw.WriteLine(ex.InnerException);
                    sw.WriteLine(ex.Source);
                    sw.WriteLine();
                }
            }
            catch (Exception)
            {

            }

        }
        #region Debug  日志

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteDebug(string fileName, string title, string content)
        {
            if (!IsDebug) return;
            
            try
            {
                string jsonpath ;
                if (fileName.Contains("通知"))
                {
                    jsonpath =
                    string.Format("{0}\\{1}调试信息\\通知", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    jsonpath =
                    string.Format("{0}\\{1}调试信息", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                }
                if (!Directory.Exists(jsonpath))
                    Directory.CreateDirectory(jsonpath);
                string filename = string.Format("{0}/{1}.txt"
                    , jsonpath,  fileName);
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    sw.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine(title);
                    sw.WriteLine(content);
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteDebugSql(string title, string content)
        {
            if (!IsDebug) return;

            try
            {
                string jsonpath = string.Format("{0}\\{1}调试信息", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(jsonpath))
                    Directory.CreateDirectory(jsonpath);
                string filename = string.Format("{0}/存储过程_时间{1}.txt"
                    , jsonpath, DateTime.Now.ToString("HHmm"));
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    sw.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                   if(!string.IsNullOrEmpty(title))
                    sw.WriteLine(title);
                    sw.WriteLine(content);
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 写测试日记
        /// </summary>
        /// <param name="content"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteDebug(string content)
        {
            if (!IsDebug) return;

            try
            {

                string jsonpath = string.Format("{0}\\{1}调试信息", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(jsonpath))
                    Directory.CreateDirectory(jsonpath);
                string filename = string.Format("{0}/{1}_系统调试.txt", jsonpath, DateTime.Now.ToString("yyyy-MM-dd"));
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(content);
                    sw.WriteLine();
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contont"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteDebugTime(string contont)
        {
            if (!IsDebug) return;
#if DEBUG
            try
            {
                
                 
                string jsonpath = string.Format("{0}\\{1}调试信息", logfilePath, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(jsonpath))
                    Directory.CreateDirectory(jsonpath);
                string filename = string.Format("{0}/{1}_调试时间.txt", jsonpath, DateTime.Now.ToString("yyyy-MM-dd"));
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    sw.Write(contont.PadRight(30));
                    sw.Write(DateTime.Now.ToString("yyyymmdd HH:MM:ss,fff"));
                    sw.WriteLine();
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }
#endif
        }
      
        #endregion
    }
}
