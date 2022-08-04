using MyCustomLib.Api.Selenium;
using MyCustomLib.Controls;
using MyCustomLib.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Debug
{
      public partial class DebugForm : CustomForm
      {
            private ManagedBrowser browser = new ManagedBrowser(
                        new ChromeDriver(ChromeDriverService.CreateDefaultService(DefaultPaths.ChromeDriverPath),
                        new ChromeOptions()));
            private static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            private CookieManager manager = new CookieManager(path + @"\Living4Memes\MyCustomLib\Debug\CookieManager\");

            public DebugForm()
            {
                  InitializeComponent();

                  DoStuff();
            }

            private void DoStuff()
            {
                  // L: abc123
                  // P: 123xyz
            }

            private void SaveCookies()
            {

            }

            private void customButton1_Click(object sender, EventArgs e)
            {
                  SaveCookies();
            }
      }
}
