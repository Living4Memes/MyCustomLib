using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Drawing;
using MyCustomLib;

namespace MyCustomLib.Api.ImgBBApi
{
      public class ImgBBClient
      {
            private const string POST_URI = "https://api.imgbb.com/1/upload";

            private string _apiKey;

            public ImgBBClient(string apiKey = DefaultClientSettings.DevKey)
            {
                  _apiKey = apiKey;
            }

            public ImgBBResponse UploadImage(Bitmap bitmap, string title)
            {
                  NameValueCollection variables = new NameValueCollection();

                  variables.Add("key", _apiKey);
                  variables.Add("image", bitmap.ToBase64String());
                  variables.Add("name", title);

                  return SendPostRequest(variables).ParseImgBBResponse();
            }

            public ImgBBResponse UploadImage(Image image, string title) => UploadImage(image.ToBitmap(), title);

            private string SendPostRequest(NameValueCollection parameters)
            {
                  if (!CheckConection())
                        throw new Exception("No Internet connection.");

                  string result = String.Empty;

                  using (WebClient client = new WebClient())
                  {
                        try
                        {
                              byte[] response = client.UploadValues(POST_URI, parameters);
                              result = Encoding.UTF8.GetString(response);
                        }
                        catch (Exception ex)
                        {
                              return "POST request failed. Exception: " + ex.Message;
                        }
                  }

                  return result;
            }

            private bool CheckConection()
            {
                  return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
      }

      internal static class DefaultClientSettings
      {
            public const string DevKey = "dev_key";
      }
}
