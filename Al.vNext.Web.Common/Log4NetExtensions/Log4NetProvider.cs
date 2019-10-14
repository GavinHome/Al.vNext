//-----------------------------------------------------------------------------------
// <copyright file="Log4NetProvider.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.IO;
using Microsoft.Extensions.Logging;

namespace Al.vNext.Web.Common.Log4NetExtensions
{
    public class Log4NetProvider : ILoggerProvider
    {
        private readonly FileInfo _fileInfo;

        public Log4NetProvider(string log4netConfigFile, bool enableScopes = false)
        {
            _fileInfo = new FileInfo(log4netConfigFile);
            EnableScopes = enableScopes;
        }

        public Log4NetProvider(bool enableScopes = false)
        {
            _fileInfo = new FileInfo("log4net.config");
            EnableScopes = enableScopes;
        }

        public bool EnableScopes { get; set; }
        
        public ILogger CreateLogger(string categoryName)
        {
            return new Log4NetLogger(categoryName, _fileInfo, EnableScopes);
        }

        public void Dispose()
        {            
        }
    }
}