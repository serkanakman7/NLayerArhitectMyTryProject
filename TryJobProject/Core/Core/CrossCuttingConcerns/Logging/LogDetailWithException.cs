using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException: LogDetail
    {
        public string ExceptionMessage { get; set; }

        public LogDetailWithException():base()
        {
            ExceptionMessage= string.Empty;
        }

        public LogDetailWithException(string fullName, string methodName, List<LogParameter> logParameters, string user,string exceptionMessage):base(fullName,methodName,logParameters,user)
        {
            ExceptionMessage = exceptionMessage;
        }



    }
}
