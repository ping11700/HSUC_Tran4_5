using System;
using System.Diagnostics;

namespace Common
{
    public class UAssert
    {
        //使用异常代替断言
        public static bool UseException { get; set; }

        public static void Assert(bool condition)
        {
#if DEBUG
#if !DisableAssert
            if (UseException)
            {
                if (!condition)
                    throw new AssertFailedException();
            }
            else
            {
                Trace.Assert(condition);
            }
#endif
#endif
        }
        public static void Assert(bool condition, string message)
        {
#if DEBUG

#if !DisableAssert
            if (UseException)
            {
                if (!condition)
                    throw new AssertFailedException(message);
            }
            else
            {
                Trace.Assert(condition, message);
            }
#endif
#endif

        }
        public static void Assert(bool condition, string message, string detailMessage)
        {
#if DEBUG

#if !DisableAssert
            if (UseException)
            {
                if (!condition)
                    throw new AssertFailedException(message + detailMessage);
            }
            else
            {
                Trace.Assert(condition, message, detailMessage);
            }
#endif
#endif

        }
    }

    public class AssertFailedException : Exception
    {
        public AssertFailedException(string msg = "")
            : base(msg)
        {
        }
    }
}
