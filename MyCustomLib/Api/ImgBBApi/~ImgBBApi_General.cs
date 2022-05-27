using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomLib.Api.ImgBBApi
{
      public struct ImgBBResponse
      {
            public string RawImgBBResponse { get; set; }
            public ImgBBResponse_GeneralData Data { get; set; }
            public bool Success { get; set; }
            public int Status { get; set; }
      }

      public struct ImgBBResponse_GeneralData
      {
            public string ID { get; set; }
            public string Title { get; set; }
            public string URL_Viewer { get; set; }
            public string URL { get; set; }
            public string Display_URL { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public string Size { get; set; }
            public string Time { get; set; }
            public string Expiration { get; set; }
            public ImgBBResponse_ImageInfo Image { get; set; }
            public ImgBBResponse_ImageInfo Thumb { get; set; }
            public ImgBBResponse_ImageInfo Medium { get; set; }
            public string Delete_URL { get; set; }
      }

      public struct ImgBBResponse_ImageInfo
      {
            public string FileName { get; set; }
            public string Name { get; set; }
            public string Mime { get; set; }
            public string Extension { get; set; }
            public string URL { get; set; }
      }      

      static class Extensions
      {
            public static ImgBBResponse ParseImgBBResponse(this string str)
            {
                  return ResponseParser.Parse(str);
            }
      }
}
