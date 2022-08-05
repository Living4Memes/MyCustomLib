using System;
using System.Runtime.InteropServices;

namespace MyCustomLib.Api.WinApi
{
      public static class WinApiProbider
      {
            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint MapVirtualKey(uint uCode, uint uMapType);
      }
}
