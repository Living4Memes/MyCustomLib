using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCustomLib.Api.PasteBinApi
{
      public static class PastesListParser
      {
            public static List<PasteInfo> Parse(string input)
            {
                  List<string> parameterArray = new List<string>(input.Split('\n'));

                  if (parameterArray.Count > 3)
                  {
                        List<PasteInfo> pastes = new List<PasteInfo>();

                        while (parameterArray.Where(x => x.StartsWith("\t<paste_key>")).Count() > 0)
                        {
                              PasteInfo currentPaste = new PasteInfo();

                              currentPaste.Key = parameterArray.First(x => x.StartsWith("\t<paste_key>"));
                              parameterArray.Remove(currentPaste.Key);
                              currentPaste.Key = currentPaste.Key.Replace("\t<paste_key>", "").Replace("</paste_key>", "");

                              currentPaste.Date = parameterArray.First(x => x.StartsWith("\t<paste_date>"));
                              parameterArray.Remove(currentPaste.Date);
                              currentPaste.Date = currentPaste.Date.Replace("\t<paste_date>", "").Replace("</paste_date>", "");

                              currentPaste.Title = parameterArray.First(x => x.StartsWith("\t<paste_title>"));
                              parameterArray.Remove(currentPaste.Title);
                              currentPaste.Title = currentPaste.Title.Replace("\t<paste_title>", "").Replace("</paste_title>", "");

                              currentPaste.Size = parameterArray.First(x => x.StartsWith("\t<paste_size>"));
                              parameterArray.Remove(currentPaste.Size);
                              currentPaste.Size = currentPaste.Size.Replace("\t<paste_size>", "").Replace("</paste_size>", "");

                              currentPaste.Date = parameterArray.First(x => x.StartsWith("\t<paste_expire_date>"));
                              parameterArray.Remove(currentPaste.Date);
                              currentPaste.Date = currentPaste.Date.Replace("\t<paste_expire_date>", "").Replace("</paste_expire_date>", "");

                              currentPaste.Private = parameterArray.First(x => x.StartsWith("\t<paste_private>"));
                              parameterArray.Remove(currentPaste.Private);
                              currentPaste.Private = currentPaste.Private.Replace("\t<paste_private>", "").Replace("</paste_private>", "");

                              currentPaste.FormatLong = parameterArray.First(x => x.StartsWith("\t<paste_format_long>"));
                              parameterArray.Remove(currentPaste.FormatLong);
                              currentPaste.FormatLong = currentPaste.FormatLong.Replace("\t<paste_format_long>", "").Replace("</paste_format_long>", "");

                              currentPaste.FormatShort = parameterArray.First(x => x.StartsWith("\t<paste_format_short>"));
                              parameterArray.Remove(currentPaste.FormatShort);
                              currentPaste.FormatShort = currentPaste.FormatShort.Replace("\t<paste_format_short>", "").Replace("</paste_format_short>", "");

                              currentPaste.Url = parameterArray.First(x => x.StartsWith("\t<paste_url>"));
                              parameterArray.Remove(currentPaste.Url);
                              currentPaste.Url = currentPaste.Url.Replace("\t<paste_url>", "").Replace("</paste_url>", "");

                              currentPaste.Hits = parameterArray.First(x => x.StartsWith("\t<paste_hits>"));
                              parameterArray.Remove(currentPaste.Hits);
                              currentPaste.Hits = currentPaste.Hits.Replace("\t<paste_hits>", "").Replace("</paste_hits>", "");

                              currentPaste.RawPasteInfo = input;

                              pastes.Add(currentPaste);
                        }

                        return pastes;
                  }
                  else if (input == "No pastes found.")
                        return new List<PasteInfo>();
                  else
                        throw new Exception("Wrong parsing string input.");
            }
      }
}
