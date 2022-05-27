using System;
using System.Collections.Specialized;

namespace MyCustomLib.Api.PasteBinApi
{
      public class PasteBinRequestParameters
      {
            public string ApiDevKey { get; set; } = DefaultClientOptions.ApiKey;
            public PasteBinTextPrivacy TextPrivacy { get; set; } = PasteBinTextPrivacy.Public;
            public string ExpireDate { get; set; } = "N";
            public string Format { get; set; } = null;
            public string ApiUserKey { get; set; } = null;
            public string ApiUserName { get; set; } = DefaultClientOptions.Login;
            public string ApiUserPassword { get; set; } = DefaultClientOptions.Password;
            public int ApiResultsLimit { get; set; } = -1;

            public NameValueCollection AssemblePostParameteres(string text)
            {
                  if (String.IsNullOrEmpty(ApiDevKey))
                        throw new ArgumentNullException(nameof(ApiDevKey), "Key value was empty.");

                  NameValueCollection parameters = new NameValueCollection();

                  if (CheckOption(ApiDevKey))
                        parameters.Add("api_dev_key", ApiDevKey);
                  if (CheckOption(text))
                        parameters.Add("api_paste_code", text);

                  parameters.Add("api_paste_private", $"{(int)TextPrivacy}");
                  parameters.Add("api_option", "paste");

                  if (CheckOption(ExpireDate))
                        parameters.Add("api_paste_expire_date", ExpireDate);
                  if (CheckOption(Format))
                        parameters.Add("api_paste_format", Format);

                  return parameters;
            }

            public NameValueCollection AssembleListingParameteres()
            {
                  NameValueCollection result = new NameValueCollection();

                  if (CheckOption(ApiDevKey))
                        result.Add("api_dev_key", ApiDevKey);
                  if (ApiResultsLimit > 0)
                        result.Add("api_results_limit", ApiResultsLimit.ToString());
                  if (CheckOption(ApiUserKey))
                        result.Add("api_user_key", ApiUserKey);

                  result.Add("api_option", "list");

                  return result;
            }

            public NameValueCollection AssembleRawPasteParameters(string pasteKey)
            {
                  NameValueCollection result = new NameValueCollection();

                  if (CheckOption(ApiDevKey))
                        result.Add("api_dev_key", ApiDevKey);
                  if (CheckOption(pasteKey))
                        result.Add("api_paste_key", pasteKey);
                  if (CheckOption(ApiUserKey))
                        result.Add("api_user_key", ApiUserKey);

                  result.Add("api_option", "show_paste");

                  return result;
            }

            public NameValueCollection AssembleDeletePostParameters(string pasteKey)
            {
                  NameValueCollection result = new NameValueCollection();

                  if (CheckOption(ApiDevKey))
                        result.Add("api_dev_key", ApiDevKey);
                  if (CheckOption(pasteKey))
                        result.Add("api_paste_key", pasteKey);
                  if (CheckOption(ApiUserKey))
                        result.Add("api_user_key", ApiUserKey);

                  result.Add("api_option", "delete");

                  return result;
            }

            private bool CheckOption(string option)
            {
                  if (!String.IsNullOrEmpty(option))
                        return true;

                  return false;
            }
      }
}
