using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace MyCustomLib.Api.PasteBinApi
{
      public class PasteBinClient
      {
            private const string PASTE_URI = "https://pastebin.com/api/api_post.php";
            private const string LOGIN_URI = "https://pastebin.com/api/api_login.php";

            private PasteBinLoginType _loginType;

            public PasteBinClient(PasteBinLoginType loginType = PasteBinLoginType.Guest)
            {
                  _loginType = loginType;
            }

            public string CreatePaste(PasteBinRequestParameters options, string pasteName, string pasteText)
            {
                  if (String.IsNullOrEmpty(pasteText))
                        throw new ArgumentNullException(nameof(pasteText), "Text was empty.");

                  NameValueCollection parameters = options.AssemblePostParameteres(pasteText);

                  string userKey = Login(options);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        return userKey;
                  else
                        options.ApiUserKey = userKey;

                  parameters.Add("api_user_key", options.ApiUserKey);
                  parameters.Add("api_paste_name", pasteName);

                  string response = SendPostRequest(parameters);

                  return response;
            }

            public List<PasteInfo> GetUserPastes(PasteBinRequestParameters options)
            {
                  string userKey = Login(options);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        throw new Exception("User key was not correct value!");
                  else
                        options.ApiUserKey = userKey;

                  NameValueCollection parameters = new NameValueCollection();

                  if (!String.IsNullOrEmpty(options.ApiDevKey) && !String.IsNullOrEmpty(options.ApiUserKey) && options.ApiResultsLimit > 0)
                        parameters = options.AssembleListingParameteres();
                  else
                        throw new Exception("Wrong input parameters!");

                  return SendPostRequest(parameters).ParsePasteBinResponse();
            }

            public string GetPasteText(PasteBinRequestParameters options, string pasteKey)
            {
                  string userKey = Login(options);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        return userKey;
                  else
                        options.ApiUserKey = userKey;

                  NameValueCollection parameters = new NameValueCollection();

                  if (!String.IsNullOrEmpty(options.ApiDevKey) && !String.IsNullOrEmpty(options.ApiUserKey) && options.ApiResultsLimit > 0)
                        parameters = options.AssembleRawPasteParameters(pasteKey);
                  else
                        return "Wrong input parameteres!";

                  return SendPostRequest(parameters);
            }

            public string DeletePaste(PasteBinRequestParameters options, string pasteKey)
            {
                  string userKey = Login(options);
                  if (userKey.LastIndexOf('.') == userKey.Length - 1)
                        return userKey;
                  else
                        options.ApiUserKey = userKey;

                  NameValueCollection parameters = new NameValueCollection();

                  if (!String.IsNullOrEmpty(options.ApiDevKey) && !String.IsNullOrEmpty(options.ApiUserKey) && options.ApiResultsLimit > 0)
                        parameters = options.AssembleDeletePostParameters(pasteKey);
                  else
                        return "Wrong input parameteres!";

                  return SendPostRequest(parameters);
            }

            public void DeleteAllPastes(PasteBinRequestParameters options)
            {
                  GetUserPastes(options).Select(x => x.Key).ToList().ForEach(x => DeletePaste(options, x));
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
                  if (_loginType == PasteBinLoginType.User && String.IsNullOrEmpty(options.ApiUserName) && String.IsNullOrEmpty(options.ApiUserPassword))
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

      internal static class DefaultClientOptions
      {
            public const string ApiKey = "api_key";
            public const string Login = "login";
            public const string Password = "password";
      }
}
