using MyCustomLib.Controls;
using MyCustomLib.Api.PasteBinApi;

namespace Debug
{
      public partial class DebugForm : CustomForm
      {
            public DebugForm()
            {
                  InitializeComponent();

                  PasteBinClient client = new PasteBinClient(PasteBinLoginType.User);
                  PasteBinRequestParameters requestParameters = new PasteBinRequestParameters()
                  {
                        ApiDevKey = "xrY0PFPfWPpPCxI2omEgMneDgF_sh-_7",
                        ApiUserName = "living4memes",
                        ApiUserPassword = "DsnfCj9gFvdddddddfgg",
                        ApiResultsLimit = 5,
                        ApiUserKey = "user_key",
                        ExpireDate = "N",
                        Format = "csharp",
                        TextPrivacy = PasteBinTextPrivacy.Private
                  };

                  label1.Text = client.CreatePaste(requestParameters, "test_api", "Hello world!");
                  //client.DeleteAllPastes(requestParameters);
            }
      }
}
