using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Drawing;
using MyCustomLib;
using System.Threading.Tasks;

namespace MyCustomLib.Api.ImgBBApi
{
      /// <summary>
      /// Клиент для работы с сайтом imgbb.com
      /// </summary>
      public class ImgBBClient
      {
            private const string POST_URI = "https://api.imgbb.com/1/upload";

            private string _apiKey;

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="ImgBBClient"/>
            /// </summary>
            /// <param name="apiKey">Api ключ imgbb.com</param>
            public ImgBBClient(string apiKey)
            {
                  _apiKey = apiKey;
            }

            /// <summary>
            /// Загружает изображение на imgbb.com
            /// </summary>
            /// <param name="bitmap"><see cref="Bitmap"/> для загрузки</param>
            /// <param name="title">Название изображения</param>
            /// <returns><see cref="ImgBBResponse"/> с информацией о загрузке</returns>
            public ImgBBResponse UploadImage(Bitmap bitmap, string title)
            {
                  NameValueCollection variables = new NameValueCollection();

                  variables.Add("key", _apiKey);
                  variables.Add("image", bitmap.ToBase64String());
                  variables.Add("name", title);

                  return SendPostRequest(variables).ParseImgBBResponse();
            }

            /// <summary>
            /// Загружает изображение на imgbb.com
            /// </summary>
            /// <param name="image"><see cref="Image"/> для загрузки</param>
            /// <param name="title">Название изображения</param>
            /// <returns><see cref="ImgBBResponse"/> с информацией о загрузке</returns>
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
}
