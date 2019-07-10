using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Interfaces.IoC;
using Dinazor.Core.Interfaces.Plugin;
using Dinazor.Core.IoC.Module;
using Dinazor.Core.Plugin;

namespace Dinazor.Core.Common.Extensions
{
    public static class Extensions
    {

        #region PluginSourceListExtensions

        public static void AddFolder(this PluginSourceList list, string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            list.Add(new FolderPluginSource(folder, searchOption));
        }

        public static void AddTypeList(this PluginSourceList list, params Type[] moduleTypes)
        {
            list.Add(new PluginTypeListSource(moduleTypes));
        }

        #endregion

        #region Datetime

        public static string GetFormattedDatetime(this DateTime? dateTime)
        {

            if (dateTime.HasValue)
            {
                StringBuilder builder = new StringBuilder();

                var day = dateTime.Value.Day;
                var month = dateTime.Value.Month;
                var year = dateTime.Value.Year;
                if (day < 10)
                {
                    builder.Append("0").Append(day);
                }
                else
                {
                    builder.Append(day);
                }
                builder.Append("/");
                if (month < 10)
                {
                    builder.Append("0").Append(month);
                }
                else
                {
                    builder.Append(month);
                }
                builder.Append("/").Append(year);
                return builder.ToString();
            }
            return null;
        }

        public static string GetFormattedDatetimeWithTime(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                StringBuilder builder = new StringBuilder();

                var day = dateTime.Value.Day;
                var month = dateTime.Value.Month;
                var year = dateTime.Value.Year;
                if (day < 10)
                {
                    builder.Append("0").Append(day);
                }
                else
                {
                    builder.Append(day);
                }
                builder.Append("/");
                if (month < 10)
                {
                    builder.Append("0").Append(month);
                }
                else
                {
                    builder.Append(month);
                }
                builder.Append("/").Append(year).Append(" ");

                if (dateTime.Value.Hour < 10)
                {
                    builder.Append("0").Append(dateTime.Value.Hour);
                }
                else
                {
                    builder.Append(dateTime.Value.Hour);
                }
                builder.Append(":");

                if (dateTime.Value.Minute < 10)
                {
                    builder.Append("0").Append(dateTime.Value.Minute);
                }
                else
                {
                    builder.Append(dateTime.Value.Minute);
                }

                builder.Append(":");

                if (dateTime.Value.Second < 10)
                {
                    builder.Append("0").Append(dateTime.Value.Second);
                }
                else
                {
                    builder.Append(dateTime.Value.Second);
                }


                return builder.ToString();
            }
            return null;
        }

        public static DateTime? SetFormattedDatetime(this string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr) || !dateStr.Contains('/') || dateStr.Split('/').Length != 3)
            {
                return null;
            }

            var date = dateStr.Split('/');
            return new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        }


        #endregion

        #region Exception

        public static string GetAllMessages(this System.Exception exception)
        {

            var messages = exception.FromHierarchy(ex => ex.InnerException)
                .Select(ex => ex.Message);
            return string.Join(Environment.NewLine, messages);
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem)
           where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem,
           Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        #endregion

        #region
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, lifeStyle);
            return true;
        }
        #endregion

        #region List
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();
            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }
            return sorted;
        }

        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);
            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found! Item: " + item);
                }
            }
            else
            {
                visited[item] = true;
                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }
                visited[item] = false;
                sorted.Add(item);
            }
        }

        #endregion

        #region Plugin
        public static List<Type> GetModulesWithAllDependencies(this IPluginSource plugInSource)
        {
            return plugInSource
                .GetModules()
                .SelectMany(DinazorModule.FindDependedModuleTypesRecursivelyIncludingGivenModule)
                .Distinct()
                .ToList();
        }
        #endregion

        #region Dictionary

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }

        #endregion

        #region Collection
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
        #endregion

        public static object GetValue(this object curr, string attributeName)
        {
            if (curr == null)
            {
                return null;
            }
            var attribute = curr.GetType().GetProperty(attributeName);
            if (attribute == null)
            {
                return null;
            }
            var value = attribute.GetValue(curr, null);
            return value;
        }

        public static bool SetValue(this object source, string attribute, object value)
        {
            if (source == null)
            {
                return false;
            }
            var property = source.GetType().GetProperty(attribute);
            if (property == null)
            {
                return false;
            }
            try
            {
                property.SetValue(source, Convert.ChangeType(value, property.PropertyType), null);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void CopyFrom<TSource>(this TSource curr, TSource source)
          where TSource : IEntity<long>
        {
            if (curr == null || source == null) return;

            var props = curr.GetType().GetProperties();
            foreach (var prop in props)
            {
                // TODO prop.GetValue(source, null); denenecek
                bool isEntity = prop.PropertyType.GetInterfaces().Any(l => l == typeof(IEntity));
                var value = source.GetType().GetProperty(prop.Name).GetValue(source, null);
                if (value == null)
                {
                    prop.SetValue(curr, null, null);
                }
                else
                {
                    if (isEntity && ((IEntity)value).Id > 0) continue;
                    try
                    {

                        value = Convert.ChangeType(value, prop.PropertyType);
                    }
                    catch
                    {
                        // ignored
                    }
                    prop.SetValue(curr, value, null);
                }

            }
        }

    }
}

