//-----------------------------------------------------------------------------------
// <copyright file="CommonExtensions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Al.vNext.Core.Entity;
using Al.vNext.Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Al.vNext.Core.Extension;

namespace Al.vNext.Core.Extension
{
    public static class CommonExtensions
    {
        /// <summary>
        /// deep clone a object used json.net
        /// </summary>
        /// <typeparam name="T">typeof object</typeparam>
        /// <param name="t">instance of object</param>
        /// <returns>a object deep clone instance</returns>
        public static T Copy<T>(this T t) where T : class
        {
            string entitySetString = JsonConvert.SerializeObject(t);
            return JsonConvert.DeserializeObject<T>(entitySetString);
        }

        /// <summary>
        /// get value by property
        /// </summary>
        /// <param name="obj">The object that have the property</param>
        /// <param name="property">property name, support the format of "A.B.C"</param>
        /// <returns>object</returns>
        public static object GetPropertyValue(this object obj, string property)
        {
            if (obj != null && !string.IsNullOrEmpty(property))
            {
                if (obj.GetType().IsFromType(typeof(IEnumerable)))
                {
                    ////(obj as ICollection).Cast<object>().ToList().ForEach(x => { x = x.GetPropertyValue(current); });
                    object[] array = (obj as IEnumerable).Cast<object>().ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = array[i].GetPropertyValue(property);
                    }

                    return array;
                }
                else
                {
                    int position = property.IndexOf('.');
                    string current = position > 0 ? property.Substring(0, position) : property;
                    PropertyInfo prop = obj.GetType().GetProperty(current);
                    if (prop != null)
                    {
                        obj = prop.GetValue(obj);
                        if (obj != null && position > 0)
                        {
                            string next = property.Substring(position + 1);
                            obj = obj.GetPropertyValue(next);
                        }

                        return obj;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 为对象的属性 property 设置 value 值
        /// </summary>
        /// <param name="obj">要设置属性的对象</param>
        /// <param name="property">属性名称</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetPropertyValue(this object obj, string property, object value)
        {
            if (obj != null && property.IsNotNullOrEmpty())
            {
                var prop = obj.GetType().GetProperty(property);
                if (prop != null)
                {
                    prop.SetValue(obj, value);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Collection ToString extension method, concatenate all collection object to a format string 
        /// </summary>
        /// <typeparam name="T">collection object type</typeparam>
        /// <param name="collection">collection object instance</param>
        /// <param name="format">object format, default is "{0}; "</param>
        /// <returns>concatenate all collection object to a format string</returns>
        public static string ToString<T>(this IEnumerable<T> collection, string format = "{0}; ")
        {
            StringBuilder resultBuilder = new StringBuilder();
            if (collection != null)
            {
                foreach (var x in collection)
                {
                    resultBuilder.Append(string.Format(format, x));
                }
            }

            string result = resultBuilder.ToString();
            if (result.IsNotNullOrEmpty())
            {
                result = result.Substring(0, result.Length - format.Length);
            }

            return result;
        }

        /// <summary>
        /// get string includes variables value from string variables expression
        /// usage (eg in user model):
        /// string value = "this is template string that contains Name: {Name} and Department: {Department.Name}";
        /// value = user.GetVariablesExpression(value);
        /// then value will be "this is template string that contains Name: Alex and Department: Huatek"
        /// </summary>
        /// <param name="obj">which object will be calculated to get value</param>
        /// <param name="value">which value expression will be calculated</param>
        /// <param name="prefix">veriable prrfox, default is "{"</param>
        /// <param name="suffix">veriable suffix, default is "}"</param>
        /// <param name="separator">veriable separator, default is "."</param>
        /// <param name="format">veriable format string, default is ""</param>
        /// <returns>string includes variables value</returns>
        public static string GetVariablesExpression(this object obj, string value, string prefix = "{", string suffix = "}", string separator = ".", string format = null)
        {
            if (!string.IsNullOrEmpty(value) && value.Contains(prefix) && value.Contains(suffix))
            {
                string exp = prefix + @"(\w+" + separator + "?)+?" + suffix;

                return Regex.Replace(
                    value,
                    exp,
                    (x) =>
                    {
                        if (x.Value.Length > prefix.Length + suffix.Length)
                        {
                            var result = obj.GetPropertyValue(x.Value.Substring(1, x.Value.Length - 2));
                            if (result != null)
                            {
                                if (result.GetType().IsFromType(typeof(DateTime)))
                                {
                                    return ((DateTime)result).ToString(format);
                                }

                                if (result.GetType().IsFromType(typeof(DateTimeOffset)))
                                {
                                    return ((DateTimeOffset)result).ToString(format);
                                }

                                if (result.GetType().IsFromType(typeof(IEnumerable)))
                                {
                                    return ((IEnumerable)result).Cast<object>().ToString(format);
                                }

                                return result.ToString();
                            }

                            return string.Empty;
                        }

                        return string.Empty;
                    },
                    RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            }

            return string.Empty;
        }

        /// <summary>
        /// current string is null or empty
        /// </summary>
        /// <param name="str">str</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// current string is not null or empty
        /// </summary>
        /// <param name="str">str</param>
        /// <returns>bool</returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// whether a collection is null or empty
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="collection">collection instance</param>
        /// <returns>deduce result</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null || !collection.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// whether a collection is not null or empty
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="collection">collection instance</param>
        /// <returns>deduce result</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection != null && collection.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// whether an entityset is null or empty
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this IEntitySet obj)
        {
            if (obj == null || obj.Id == null || obj.Id.Equals(Guid.Empty))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// whether an entityset is not null or empty
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>bool</returns>
        public static bool IsNotNullOrEmpty(this IEntitySet obj)
        {
            return !obj.IsNullOrEmpty();
        }

        /// <summary>
        /// 获取 odata 查询表达式中的枚举值表达式
        /// </summary>
        /// <param name="obj">待处理的枚举值</param>
        /// <returns>符合 odata 规范的查询表达式</returns>
        public static string ToEnumString(this object obj)
        {
            var result = obj.ToString();
            var objectType = obj.GetType();
            if (objectType.IsEnum)
            {
                result = $"{objectType.FullName}'{obj.ToString()}'";
            }

            return result;
        }

        /// <summary>
        /// 获取枚举的可显示名字
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>枚举的可显示名字</returns>
        public static string ToEnumName(this object obj)
        {
            var result = string.Empty;
            if (obj != null)
            {
                var objectType = obj.GetType();
                if (objectType.IsEnum)
                {
                    var field = objectType.GetField(obj.ToString());
                    if (field != null)
                    {
                        var display = field.GetCustomAttribute<DisplayAttribute>();
                        if (display != null)
                        {
                            result = display.Name;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从键值对中获取类型的值
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="collection">键值对集合</param>
        /// <param name="key">要获取的键</param>
        /// <param name="value">传出参数获取值</param>
        /// <returns>返回是否获取成功</returns>
        public static bool TryGetValue<T>(this IDictionary<string, object> collection, string key, out T value)
        {
            object obj;
            if (collection.TryGetValue(key, out obj) && (obj is T))
            {
                value = (T)obj;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// 获取枚举变量值的 Description 属性
        /// </summary>
        /// <param name="obj">枚举变量</param>
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
        public static string GetDescription(this object obj)
        {
            return GetDescription(obj, false);
        }

        /// <summary>
        /// 获取枚举变量值的 Description 属性
        /// </summary>
        /// <param name="obj">枚举变量</param>
        /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param>
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
        public static string GetDescription(this object obj, bool isTop)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                Type _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                if (isTop)
                {
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(_enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = _enumType.GetField(System.Enum.GetName(_enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                      fi, typeof(DescriptionAttribute));
                }

                if (dna != null && !string.IsNullOrEmpty(dna.Description))
                {
                    return dna.Description;
                }
            }
            catch
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 格式化日期，例如 2015-03-18
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>格式化后的字符串</returns>
        public static string DateFormat(this DateTime dt)
        {
            return (string.IsNullOrEmpty(dt.ToString(CultureInfo.InvariantCulture)) || dt == DateTime.MinValue) ? string.Empty : dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 格式化时间，例如 17:03:43
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>格式化后的字符串</returns>
        public static string TimeFormat(this DateTime dt)
        {
            return (string.IsNullOrEmpty(dt.ToString(CultureInfo.InvariantCulture)) || dt == DateTime.MinValue) ? string.Empty : dt.ToString("HH:mm:ss");
        }

        /// <summary>
        ///  格式化日期时间，例如 2015-03-18 17:03:43
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>格式化后的字符串</returns>
        public static string DateTimeFormat(this DateTime dt)
        {
            return (string.IsNullOrEmpty(dt.ToString(CultureInfo.InvariantCulture)) || dt == DateTime.MinValue) ? string.Empty : dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///  格式化日期时间，例如 20150318170343
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>格式化后的字符串</returns>
        public static string DateTimeNumberFormat(this DateTime dt)
        {
            return (string.IsNullOrEmpty(dt.ToString(CultureInfo.InvariantCulture)) || dt == DateTime.MinValue) ? string.Empty : dt.ToString("yyyyMMddHHmmss");
        }

        public static string ToMonthUpper(this int month)
        {
            string str = string.Empty;
            switch (month)
            {
                case 1:
                    str = "一月";
                    break;
                case 2:
                    str = "二月";
                    break;
                case 3:
                    str = "三月";
                    break;
                case 4:
                    str = "四月";
                    break;
                case 5:
                    str = "五月";
                    break;
                case 6:
                    str = "六月";
                    break;
                case 7:
                    str = "七月";
                    break;
                case 8:
                    str = "八月";
                    break;
                case 9:
                    str = "九月";
                    break;
                case 10:
                    str = "十月";
                    break;
                case 11:
                    str = "十一月";
                    break;
                case 12:
                    str = "十二月";
                    break;
                default:
                    break;
            }

            return str;
        }

        public static string ToWeekDay(this DayOfWeek weekDay)
        {
            string str = string.Empty;
            switch (weekDay)
            {
                case DayOfWeek.Monday:
                    str = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    str = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    str = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    str = "星期四";
                    break;
                case DayOfWeek.Friday:
                    str = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    str = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    str = "星期日";
                    break;
                default:
                    break;
            }

            return str;
        }

        public static ObservableCollection<T> DeepCopy<T>(this ObservableCollection<T> list) where T : ICloneable
        {
            ObservableCollection<T> newList = new ObservableCollection<T>();
            foreach (T rec in list)
            {
                newList.Add((T)rec.Clone());
            }

            return newList;
        }

        public static IList<T> DeepCopy<T>(this IList<T> list) where T : ICloneable
        {
            IList<T> newList = new List<T>();
            foreach (T rec in list)
            {
                newList.Add((T)rec.Clone());
            }

            return newList;
        }

        public static string PasswordHashed(this string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static async System.Threading.Tasks.Task<string> ReadAsStringAsync(this HttpContext context)
        {
            var initialBody = context.Request.Body; // Workaround

            context.Request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var json = System.Text.Encoding.UTF8.GetString(buffer);

            context.Request.Body = initialBody; // Workaround

            return json;
        }
    }
}
