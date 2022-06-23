using MyCustomLib.Controls;
using MyCustomLib.Api.ImgBBApi;
using System.Collections.Generic;

namespace Debug
{
      public partial class DebugForm : CustomForm
      {
            public DebugForm()
            {
                  InitializeComponent();

                  ImgBBClient client = new ImgBBClient("79389fec6db7ccceb614d0c8bcda4bca");

                  client.UploadImage(Properties.Resources.DFX, "Api test");
            }

      }
}
