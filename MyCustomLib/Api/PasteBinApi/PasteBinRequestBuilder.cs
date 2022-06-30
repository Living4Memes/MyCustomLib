using System;
using System.Collections.Specialized;

namespace MyCustomLib.Api.PasteBinApi
{
      /// <summary>
      /// Содержит параметры запросов для pastebin.com
      /// </summary>
      public struct PasteBinRequestParameters : IPasteBinPostParameters, IPasteBinDeleteParameters, IPasteBinListingParameters, IPasteBinRawPasteParameters
      {
            /// <summary>
            /// Api ключ pastebin.com
            /// </summary>
            public string ApiDevKey { get; set; }
            /// <summary>
            /// Тип доступа к paste
            /// </summary>
            public PasteBinTextPrivacy TextPrivacy { get; set; }
            /// <summary>
            /// Дата автоудаления paste
            /// </summary>
            public string ExpireDate { get; set; }
            /// <summary>
            /// Стиль текста (для цветного форматирования синтаксиса на сайте)
            /// </summary>
            public string Format { get; set; }
            /// <summary>
            /// Одноразовый ключ для публикации paste с доступом <see cref="PasteBinTextPrivacy.Private"/>
            /// </summary>
            public string ApiUserKey { get; set; }
            /// <summary>
            /// Имя пользователя для авторизации
            /// </summary>
            public string ApiUserName { get; set; }
            /// <summary>
            /// Пароль для авторизации
            /// </summary>
            public string ApiUserPassword { get; set; }
            /// <summary>
            /// Ограничение по количеству результатов выполнения GET запроса
            /// </summary>
            public int ApiResultsLimit { get; set; }
      }

      internal interface IPasteBinMainParameters
      {
            string ApiDevKey { get; set; }
      }

      internal interface IPasteBinPostParameters : IPasteBinMainParameters
      {
            PasteBinTextPrivacy TextPrivacy { get; set; }
            string ExpireDate { get; set; }
            string Format { get; set; }
      }

      internal interface IPasteBinListingParameters : IPasteBinMainParameters
      {
            int ApiResultsLimit { get; set; }
            string ApiUserKey { get; set; }
      }

      internal interface IPasteBinRawPasteParameters : IPasteBinMainParameters
      {
            string ApiUserKey { get; set; }
      }

      internal interface IPasteBinDeleteParameters : IPasteBinMainParameters
      { 
            string ApiUserKey { get; set; }
      }

      internal static class PasteBinRequestBuilder
      {
            public static NameValueCollection BuildPost(IPasteBinPostParameters postParameters, string pasteText)
            {
                  if (String.IsNullOrEmpty(postParameters.ApiDevKey))
                        throw new ArgumentNullException(nameof(postParameters.ApiDevKey), "DevKey value was empty.");

                  NameValueCollection parameters = new NameValueCollection();
                  parameters.Add("api_dev_key", postParameters.ApiDevKey);
                  parameters.Add("api_paste_code", pasteText);

                  parameters.Add("api_paste_private", $"{(int)postParameters.TextPrivacy}");
                  parameters.Add("api_option", "paste");
                  parameters.Add("api_paste_expire_date", postParameters.ExpireDate);
                  parameters.Add("api_paste_format", postParameters.Format);

                  return parameters;
            }

            public static NameValueCollection BuildListing(IPasteBinListingParameters listingParameters)
            {
                  NameValueCollection result = new NameValueCollection();

                  result.Add("api_dev_key", listingParameters.ApiDevKey);
                  if (listingParameters.ApiResultsLimit > 0)
                        result.Add("api_results_limit", listingParameters.ApiResultsLimit.ToString());
                  result.Add("api_user_key", listingParameters.ApiUserKey);

                  result.Add("api_option", "list");

                  return result;
            }

            public static NameValueCollection BuildRawPaste(IPasteBinRawPasteParameters rawPasteParameters, string pasteKey)
            {
                  NameValueCollection result = new NameValueCollection();

                  result.Add("api_dev_key", rawPasteParameters.ApiDevKey);
                  result.Add("api_paste_key", pasteKey);
                  result.Add("api_user_key", rawPasteParameters.ApiUserKey);

                  result.Add("api_option", "show_paste");

                  return result;
            }

            public static NameValueCollection BuildDelete(IPasteBinDeleteParameters deleteParamteres, string pasteKey)
            {
                  NameValueCollection result = new NameValueCollection();

                  result.Add("api_dev_key", deleteParamteres.ApiDevKey);
                  result.Add("api_paste_key", pasteKey);
                  result.Add("api_user_key", deleteParamteres.ApiUserKey);

                  result.Add("api_option", "delete");

                  return result;
            }
      }
}
