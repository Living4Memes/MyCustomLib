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
                  //browser.Navigation.GoToUrl("https://demo.guru99.com/test/cookie/selenium_aut.php");
                  browser.Navigation.GoToUrl("http://riotgames.com");
                  //manager.Load();
                  browser.LoadCookies(manager.Cookies.Select(x => x.ToSeleniumCookie()).ToList());
                  //browser.Navigation.Refresh();

                  //try
                  //{
                  //      browser.FindElement(By.Name("username")).SendKeys("abc123");
                  //      browser.FindElement(By.Name("password")).SendKeys("123xyz");
                  //      browser.FindElement(By.Name("submit")).Click();
                  //}
                  //catch
                  //{
                  //      MessageBox.Show("Cant login");
                  //}
            }

            private void SaveCookies()
            {
                  manager.AddCookies(browser.Options.Cookies.AllCookies.Select(x => x.ToNetCookie()).ToArray());
            }

            private void customButton1_Click(object sender, EventArgs e)
            {
                  SaveCookies();
            }
      }
}
