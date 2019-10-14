//-----------------------------------------------------------------------------------
// <copyright file="GuardUtils.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Al.vNext.Core.Attributes;

namespace Al.vNext.Core.Utility
{
    public static class GuardUtils
    {
        public static void NotNull<T>([ValidatedNotNull] this T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
