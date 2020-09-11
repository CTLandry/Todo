using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Todo.Infrastructure.Exceptions
{

    /// <summary>
    /// Provides methods for reporting errors to the registered app center app
    /// </summary>
    public static class ErrorTracker
    {
        public static void ReportError(Exception execption, IDictionary<string, string> errorData)
        {
            LoggingExeceptionInformation(execption);
            Microsoft.AppCenter.Crashes.Crashes.TrackError(execption, errorData);
        }

        public static void ReportError(Exception execption)
        {
            LoggingExeceptionInformation(execption);
            Microsoft.AppCenter.Crashes.Crashes.TrackError(execption);
        }

        private static void LoggingExeceptionInformation(Exception execption)
        {
            execption.Data.Add("Line Number", GetExceptionLineNumber(execption));
            execption.Data.Add("Method Name", GetExceptionMethodName(execption));
            execption.Data.Add("Class Name", GetExceptionClassName(execption));
            
        }

        private static int GetExceptionLineNumber(Exception execption)
        {

            var stackTrace = new StackTrace(execption, true);
            // Get the top stack frame
            var frame = stackTrace.GetFrame(stackTrace.FrameCount - 1);

            // Get the line number from the stack frame
            return frame.GetFileLineNumber();
        }

        private static string GetExceptionMethodName(Exception execption)
        {
            return new StackTrace(execption).GetFrames().Select(function => function.GetMethod()).First(method => method.Module.Assembly == Assembly.GetExecutingAssembly()).Name;
        }

        private static string GetExceptionClassName(Exception execption)
        {
            return new StackTrace(execption).GetFrames().Select(function => function.GetMethod()).First(method => method.Module.Assembly == Assembly.GetExecutingAssembly()).ReflectedType.FullName;
        }
    }
}
