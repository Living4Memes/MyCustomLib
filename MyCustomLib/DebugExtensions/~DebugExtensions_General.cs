using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using MyCustomLib;

namespace MyCustomLib.DebugExtensions
{
      public static class Extensions
      {
            // Для удобства заколовок с типом коллекции
            static string GeArraytDebugHeader(this object obj)
            {
                  return $"Debug view. {obj.GetType()}\n\n";
            }

            // Обработка NameValue ==================================================
            public static string DebugView(this NameValueCollection collection)
            {
                  return collection.GeArraytDebugHeader() + String.Join("\r\n", collection.AllKeys.Select(key => $"{key} => {collection[key].ToString()}"));
            }

            // Обработка Dictionary ===================================================
            public static string DebugView(this Dictionary<string, string> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<string, int> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<int, string> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<int, int> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<string, double> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<double, string> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<double, double> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<int, double> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<double, int> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<string, float> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<float, string> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<float, float> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<int, float> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<float, int> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<double, float> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            public static string DebugView(this Dictionary<float, double> dictionary)
            {
                  return dictionary.GeArraytDebugHeader() + String.Join("\t\n", dictionary.Keys.Select(key => $"{key} => {dictionary[key]}"));
            }

            // Обработка массива ======================================================
            public static string DebugVIew(this int[] array)
            {
                  return array.GeArraytDebugHeader() + String.Join(" | ", array);
            }

            public static string DebugVIew(this string[] array)
            {
                  return array.GeArraytDebugHeader() + String.Join(" | ", array);
            }

            public static string DebugVIew(this double[] array)
            {
                  return array.GeArraytDebugHeader() + String.Join(" | ", array);
            }

            public static string DebugVIew(this float[] array)
            {
                  return array.GeArraytDebugHeader() + String.Join(" | ", array);
            }

            // Обработка двумерного массива ===========================================
            public static string DebugVIew(this int[,] array)
            {
                  return array.GeArraytDebugHeader() + array.TableView();
            }

            public static string DebugVIew(this string[,] array)
            {
                  return array.GeArraytDebugHeader() + array.TableView();
            }

            public static string DebugVIew(this double[,] array)
            {
                  return array.GeArraytDebugHeader() + array.TableView();
            }

            public static string DebugVIew(this float[,] array)
            {
                  return array.GeArraytDebugHeader() + array.TableView();
            }

            // Обработка List =========================================================
            public static string DebugView(this List<int> list)
            {
                  return GeArraytDebugHeader(list) + String.Join(" | ", list);
            }

            public static string DebugView(this List<string> list)
            {
                  return GeArraytDebugHeader(list) + String.Join(" | ", list);
            }

            public static string DebugView(this List<double> list)
            {
                  return GeArraytDebugHeader(list) + String.Join(" | ", list);
            }

            public static string DebugView(this List<float> list)
            {
                  return GeArraytDebugHeader(list) + String.Join(" | ", list);
            }

            // Обработка ArrayList ====================================================
            public static string DebugView(this ArrayList list)
            {
                  return list.GeArraytDebugHeader() + String.Join("\t\n", list);
            }
      }
}
