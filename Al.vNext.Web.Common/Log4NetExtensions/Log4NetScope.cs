//-----------------------------------------------------------------------------------
// <copyright file="Log4NetScope.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading;

namespace Al.vNext.Web.Common.Log4NetExtensions
{
    public class Log4NetScope
    {
        private static AsyncLocal<Log4NetScope> _value = new AsyncLocal<Log4NetScope>();
        private readonly string _name;
        private readonly object _state;

        public Log4NetScope(string name, object state)
        {
            _name = name;
            _state = state;
        }

        public static Log4NetScope Current
        {
            get { return _value.Value; }
            set { _value.Value = value; }
        }

        public Log4NetScope Parent { get; private set; }

        public static IDisposable Push(string name, object state)
        {
            var temp = Current;
            Current = new Log4NetScope(name, state);
            Current.Parent = temp;

            return new DisposableScope();
        }

        public override string ToString()
        {
            return _state?.ToString();
        }

        private sealed class DisposableScope : IDisposable
        {
            public void Dispose()
            {
                Current = Current.Parent;
            }
        }
    }
}
