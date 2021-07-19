using Common.Log4;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class Executer
    {
        /// <summary>
        /// 异步执行(Asyn)
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="skipException"></param>
        public static void TryRunByAsyn(Action fun, bool skipException = true)
        {
            TryRunSkipExceptoin(() => fun.BeginInvoke(null, null), skipException);
        }

        /// <summary>
        ///  异步执行(Task)
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="skipException"></param>
        public static void TryRunByTask(Action fun, bool skipException = true)
        {
            Task task = Task.Factory.StartNew(() => TryRunSkipExceptoin(fun, skipException));
        }


        /// <summary>
        /// 执行(Thread)
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="skipException"></param>
        public static void TryRunByThread(Action fun, bool skipException = true)
        {
            Thread thread = new Thread(new ThreadStart(fun.Invoke))
            {
                IsBackground = true
            };
            if (!skipException)
            {
                thread.Start();
            }
            else
            {
                try
                {
                    thread.Start();
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// 执行(ThreadPool)
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="skipException"></param>
        public static void TryRunByThreadPool(Action fun, bool skipException = true)
        {
            ThreadPool.QueueUserWorkItem(obj => TryRunSkipExceptoin(fun, skipException));
        }



        public static void TryRunLogExceptioin(Action fun, string message = "")
        {
            try
            {
                fun();
            }
            catch (Exception exception)
            {
                LogService.ILogger.Warn($"{ message} + 错误详情：{exception.Message}");
            }
        }


        public static void TryRunSkipExceptoin(Action fun, bool skipException = true)
        {
            if (!skipException)
            {
                fun();
            }
            else
            {
                try
                {
                    fun();
                }
                catch (Exception exception)
                {
                    LogService.ILogger.Error(exception.Message, exception);
                }
            }
        }


        public static void TryRunThrowExceptioin(Action fun, string message = "")
        {
            try
            {
                fun();
            }
            catch (Exception exception)
            {
                throw new ApplicationException($"{message}，请尝试重新运行/重启程序，或者联系技术支持。{Environment.NewLine}错误详情：{exception.Message}", exception);
            }
        }






    }
}
