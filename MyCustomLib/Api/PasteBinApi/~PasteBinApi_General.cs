using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomLib.Api.PasteBinApi
{
      public struct PasteInfo
      {
            public string RawPasteInfo { get; set; }
            public string Key { get; set; }
            public string Date { get; set; }
            public string Title { get; set; }
            public string Size { get; set; }
            public string Private { get; set; }
            public string FormatLong { get; set; }
            public string FormatShort { get; set; }
            public string Url { get; set; }
            public string Hits { get; set; }

            public override string ToString()
            {
                  return $"Paste Key: {Key}\nDate: {Date}\nTitle: {Title}\nSize: {Size}\nPrivate: {Private}\nFormatLong: {FormatLong}\nFormatShort: {FormatShort}\nUrl: {Url}\nHits: {Hits}\n";
            }
      }

      public enum PasteBinLoginType
      {
            Guest,
            User
      }

      public enum PasteBinTextPrivacy : int
      {
            Public,
            Unlisted,
            Private
      }

      static class Extensions
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
