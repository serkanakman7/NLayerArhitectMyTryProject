﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; }
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
        public string User { get; set; }

        public LogDetail()
        {
            FullName = string.Empty;
            MethodName = string.Empty;
            User = string.Empty;
            LogParameters = new List<LogParameter>();
        }

        public LogDetail(string fullName, string methodName, List<LogParameter> logParameters, string user)
        {
            FullName = fullName;
            MethodName = methodName;
            LogParameters = logParameters;
            User = user;
        }

    }
}
