using log4net;
using System;
using System.Diagnostics;
using System.IO;

namespace ZSZ.Common
{
    public class LogHelper
    {
        private static ILog _logdebug = LogManager.GetLogger("logdebug");
        private static ILog _loginfo = LogManager.GetLogger("loginfo");
        private static ILog _logwarn = LogManager.GetLogger("logwarn");
        private static ILog _logerror = LogManager.GetLogger("logerror");
        private static ILog _logfatal = LogManager.GetLogger("logfatal");

        public static bool doDebug = true;
        public static bool doInfo = true;
        public static bool doWarn = true;
        public static bool doError = true;
        public static bool doFatal = true;

        public static void SetPath(string path)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(path));
        }

        public static void Debug(string msg)
        {
            if (!doDebug)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logdebug.Debug(msg);
        }

        public static void Debug(string msg, Exception e)
        {
            if (!doDebug)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logdebug.Debug(msg, e);
        }

        public static void Info(string msg)
        {
            if (!doInfo)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _loginfo.Info(msg);
        }
        public static void Info(string msg, Exception e)
        {
            if (!doInfo)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _loginfo.Info(msg, e);
        }


        public static void Warn(string msg)
        {
            if (!doWarn)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logwarn.Warn(msg);
        }
        public static void Warn(string msg, Exception e)
        {
            if (!doWarn)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logwarn.Warn(msg, e);
        }

        public static void Error(string msg)
        {
            if (!doError)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logerror.Error(msg);
        }

        public static void Error(string msg, Exception e)
        {
            if (!doError)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logerror.Error(msg, e);
        }

        public static void Fatal(string msg)
        {
            if (!doFatal)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logfatal.Fatal(msg);
        }

        public static void Fatal(string msg, Exception e)
        {
            if (!doFatal)
            {
                return;
            }
            StackFrame sf = new StackFrame(1);
            msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);
            _logfatal.Fatal(msg, e);
        }


        /// <summary>
        /// 日志清理方法
        /// 调用方法如下
        /// DirectoryInfo folder = new DirectoryInfo("D:\\ETCP\\Log");
        /// DirectoryInfo[] paths = folder.GetDirectories();
        /// ClearLog(paths, 10);
        /// </summary>
        /// <param name="folderPaths"></param>
        /// <param name="days"></param>
        public static void ClearLog(DirectoryInfo[] folderPaths, int days)
        {
            try
            {
                foreach (var folder in folderPaths)
                {
                    DirectoryInfo[] ds = folder.GetDirectories();
                    DateTime today = DateTime.Now.Date;
                    foreach (var item in ds)
                    {
                        DateTime logTime = DateTime.Parse(item.Name);
                        if (logTime <= today.AddDays(-days))
                        {
                            item.Delete(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error("日志清理方法出错：", ex);
            }
        }

    }
}
