using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace MyCustomLib.Api.PasteBinApi
{
      /// <summary>
      /// Клиент для работы с сайтом pastebin.com
      /// </summary>
      public sealed class PasteBinClient
      {
            private const string PASTE_URI = "https://pastebin.com/api/api_post.php";
            private const string LOGIN_URI = "https://pastebin.com/api/api_login.php";

            private PasteBinRequestParameters _parameters;

            /// <summary>
            /// Параметры клиента
            /// </summary>
            public PasteBinRequestParameters Parameters { get => _parameters; set => _parameters = value; }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="PasteBinClient"/>
            /// </summary>
            /// <param name="parameters">Параметры клиента</param>
            public PasteBinClient(PasteBinRequestParameters parameters)
            {
                  Parameters = parameters;
            }

            /// <summary>
            /// Создание нового paste
            /// </summary>
            /// <param name="pasteName">Название paste</param>
            /// <param name="pasteText">Основной текст для загрузки</param>
            /// <returns>Информация о выполненном запросе</returns>
            public string CreatePaste(string pasteName, string pasteText)
            {
                  if (String.IsNullOrEmpty(pasteText))
                        throw new ArgumentNullException(nameof(pasteText), "Text was empty.");

                  NameValueCollection parameters = PasteBinRequestBuilder.BuildPost(Parameters, pasteText);

                  string userKey = Login(Parameters);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        return userKey;
                  else
                        _parameters.ApiUserKey = userKey;

                  parameters.Add("api_user_key", Parameters.ApiUserKey);
                  parameters.Add("api_paste_name", pasteName);

                  string response = SendPostRequest(parameters);

                  return response;
            }

            /// <summary>
            /// Получение всех paste'ов пользователя
            /// </summary>
            /// <returns>Список с <see cref="PasteInfo"/> всех paste'ов</returns>
            /// <remarks>Требуется логин и пароль в <see cref="Parameters"/></remarks>
            public List<PasteInfo> GetUserPastes()
            {
                  string userKey = Login(Parameters);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        throw new Exception("User key was not correct value!");
                  else
                        _parameters.ApiUserKey = userKey;

                  NameValueCollection parameters = new NameValueCollection();

                  if (!String.IsNullOrEmpty(Parameters.ApiDevKey) && !String.IsNullOrEmpty(Parameters.ApiUserKey) && Parameters.ApiResultsLimit > 0)
                        parameters = PasteBinRequestBuilder.BuildListing(Parameters);
                  else
                        throw new Exception("Wrong input parameters!");

                  return SendPostRequest(parameters).ParsePasteBinResponse();
            }

            /// <summary>
            /// Получает текст paste по его ключу
            /// </summary>
            /// <param name="pasteKey">Ключ paste</param>
            /// <returns>Текст найденного paste</returns>
            public string GetPasteText(string pasteKey)
            {
                  string userKey = Login(Parameters);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        return userKey;
                  else
                        _parameters.ApiUserKey = userKey;

                  NameValueCollection parameters = new NameValueCollection();

                  if (!String.IsNullOrEmpty(Parameters.ApiDevKey) && !String.IsNullOrEmpty(Parameters.ApiUserKey) && Parameters.ApiResultsLimit > 0)
                        parameters = PasteBinRequestBuilder.BuildPost(Parameters, pasteKey);
                  else
                        return "Wrong input parameteres!";

                  return SendPostRequest(parameters);
            }

            /// <summary>
            /// Удаляет paste по его ключу
            /// </summary>
            /// <param name="pasteKey">Ключ paste</param>
            /// <returns>Информация о выполненном запросе</returns>
            /// <remarks>Требуется логин и пароль в <see cref="Parameters"/></remarks>
            public string DeletePaste(string pasteKey)
            {
                  string userKey = Login(Parameters);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        return userKey;
                  else
                        _parameters.ApiUserKey = userKey;

                  NameValueCollection parameters = new NameValueCollection();

                  if (!String.IsNullOrEmpty(Parameters.ApiDevKey) && !String.IsNullOrEmpty(Parameters.ApiUserKey) && Parameters.ApiResultsLimit > 0)
                        parameters = PasteBinRequestBuilder.BuildDelete(Parameters, pasteKey);
                  else
                        return "Wrong input parameteres!";

                  return SendPostRequest(parameters);
            }

            /// <summary>
            /// Удаляет все paste'ы пользователя
            /// </summary>
            /// <remarks>Требуется логин и пароль в <see cref="Parameters"/></remarks>
            public void DeleteAllPastes()
            {
                  GetUserPastes().Select(x => x.Key).ToList().ForEach(x => DeletePaste(x));
            }

            private string SendPostRequest(NameValueCollection parameters)
            {
                  if (!CheckConection())
                        throw new Exception("No Internet connection.");

                  string result = String.Empty;

                  using (WebClient client = new WebClient())
                  {
                        try
                        {
                              byte[] response = client.UploadValues(PASTE_URI, parameters);
                              result = Encoding.UTF8.GetString(response);
                        }
                        catch (Exception ex)
                        {
                              return "POST request failed. Exception: " + ex.Message;
                        }
                  }

                  return result;
            }

            private string Login(PasteBinRequestParameters options)
            {
                  if (Parameters.TextPrivacy == PasteBinTextPrivacy.Private && String.IsNullOrEmpty(options.ApiUserName) && String.IsNullOrEmpty(options.ApiUserPassword))
                        return "Login error. Uploading aborted.";

                  using (WebClient client = new WebClient())
                  {
                        NameValueCollection loginData = new NameValueCollection();

                        loginData.Add("api_dev_key", options.ApiDevKey);
                        loginData.Add("api_user_name", options.ApiUserName);
                        loginData.Add("api_user_password", options.ApiUserPassword);

                        try
                        {
                              byte[] response = client.UploadValues(LOGIN_URI, loginData);
                              string apiUserKey = Encoding.UTF8.GetString(response);

                              if (!apiUserKey.Contains("Bad API request"))
                              {
                                    return apiUserKey;
                              }
                        }
                        catch (Exception ex)
                        {
                              return "Logging error. Exception: " + ex.Message;
                        }
                  }

                  return String.Empty;
            }

            private bool CheckConection()
            {
                  return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
      }
}
