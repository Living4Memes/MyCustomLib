using MyCustomLib.Controls;
using MyCustomLib.Api.WinApi;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Debug
{
      public partial class DebugForm : CustomForm
      {
            public DebugForm()
            {
                  InitializeComponent();

                  GlobalKeyboardHook gkh = new GlobalKeyboardHook();

                  gkh.Filtered = false;

                  gkh.KeyDown += (s, e) => richTextBox1.Text += e.KeyCode.ToString();
            }

      }
}
