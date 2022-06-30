using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomLib.Api.PasteBinApi
{
      /// <summary>
      /// Содержит информацию об ответе pastebin.com
      /// </summary>
      public struct PasteInfo
      {
            /// <summary>
            /// Содержит ответ pastebin.com в XML формате
            /// </summary>
            public string RawPasteInfo { get; internal set; }
            /// <summary>
            /// Ключ paste
            /// </summary>
            public string Key { get; internal set; }
            /// <summary>
            /// Дата загрузки paste
            /// </summary>
            public string Date { get; internal set; }
            /// <summary>
            /// Название paste
            /// </summary>
            public string Title { get; internal set; }
            /// <summary>
            /// Размер paste
            /// </summary>
            public string Size { get; internal set; }
            /// <summary>
            /// Информация об уровне доступа к paste
            /// </summary>
            public string Private { get; internal set; }
            public string FormatLong { get; internal set; }
            public string FormatShort { get; internal set; }
            /// <summary>
            /// Ссылка на paste
            /// </summary>
            public string Url { get; internal set; }
            /// <summary>
            /// Количество просмотров paste
            /// </summary>
            public string Hits { get; internal set; }
            /// <summary>
            /// Для вывода информации
            /// </summary>
            /// <returns> Возвращает параметры построчно</returns>
            public override string ToString()
            {
                  return $"Paste Key: {Key}\nDate: {Date}\nTitle: {Title}\nSize: {Size}\nPrivate: {Private}\nFormatLong: {FormatLong}\nFormatShort: {FormatShort}\nUrl: {Url}\nHits: {Hits}\n";
            }
      }

      /// <summary>
      /// Тип авторизации при загрузке paste
      /// </summary>
      public enum PasteBinLoginType
      {
            /// <summary>
            /// Режим гостя
            /// </summary>
            Guest,
            /// <summary>
            /// Режим авторизации. Требуется логин и пароль при использовании <see cref="PasteBinClient"/>
            /// </summary>
            User
      }

      /// <summary>
      /// Тип доступа к paste
      /// </summary>
      public enum PasteBinTextPrivacy : int
      {
            /// <summary>
            /// Открытый доступ
            /// </summary>
            Public,
            /// <summary>
            /// Не указан
            /// </summary>
            Unlisted,
            /// <summary>
            /// Доступ только для публикующего. Требуется логин и пароль при использовании <see cref="PasteBinClient"/>
            /// </summary>
            Private
      }

      internal static class Extensions
      {
            public static string DebugView(this NameValueCollection nvc)
            {
                  return string.Join("\r\n", nvc.AllKeys.Select(key => $"[{key}]: [{nvc[key]}]"));
            }

            public static List<PasteInfo> ParsePasteBinResponse(this string str)
            {
                  return PastesListParser.Parse(str);
            }
      }
}
