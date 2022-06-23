using System;
using System.Collections.Specialized;

namespace MyCustomLib.Api.PasteBinApi
{

      public struct PasteBinRequestParameters : IPasteBinPostParameters, IPasteBinDeleteParameters, IPasteBinListingParameters, IPasteBinRawPasteParameters
      {
            public string ApiDevKey { get; set; }
            public PasteBinTextPrivacy TextPrivacy { get; set; }
            public string ExpireDate { get; set; }
            public string Format { get; set; }
            public string ApiUserKey { get; set; }
            public string ApiUserName { get; set; }
            public string ApiUserPassword { get; set; }
            public int ApiResultsLimit { get; set; }
      }

      public interface IPasteBinMainParameters
      {
            string ApiDevKey { get; set; }
      }

      public interface IPasteBinPostParameters : IPasteBinMainParameters
      {
            PasteBinTextPrivacy TextPrivacy { get; set; }
            string ExpireDate { get; set; }
            string Format { get; set; }
      }

      public interface IPasteBinListingParameters : IPasteBinMainParameters
      {
            int ApiResultsLimit { get; set; }
            string ApiUserKey { get; set; }
      }

      public interface IPasteBinRawPasteParameters : IPasteBinMainParameters
      {
            string ApiUserKey { get; set; }
      }

      public interface IPasteBinDeleteParameters : IPasteBinMainParameters
      { 
            string ApiUserKey { get; set; }
      }

      public static class PasteBinRequestBuilder
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
