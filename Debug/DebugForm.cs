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
            public DebugForm()
            {
                  InitializeComponent();

                  ManagedBrowser browser = new ManagedBrowser(
                        new ChromeDriver(ChromeDriverService.CreateDefaultService(DefaultPaths.ChromeDriverPath), 
                        new ChromeOptions()));
                  string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                  CookieManager manager = new CookieManager(path + @"\Living4Memes\MyCustomLib\Debug\CookieManager\");

                  Load += (s, e) =>
                  {
                        browser.Navigation.GoToUrl("https://www.riotgames.com/ru");

                        manager.Load();

                        List<OpenQA.Selenium.Cookie> cookies = manager.Cookies.Select(x => x.ToSeleniumCookie()).ToList();

                        foreach(OpenQA.Selenium.Cookie cookie in cookies)
                              browser.Options.Cookies.AddCookie(cookie);

                        manager.Cookies.ForEach(x => browser.Options.Cookies.AddCookie(x.ToSeleniumCookie()));

                        browser.Navigation.Refresh();
                        try
                        {
                              browser.FindElement(By.ClassName("_2I66LI-wCuX47s2um7O7kh")).Click();
                        }
                        catch
                        {
                              MessageBox.Show(" NO LOGIN BUTTON!");
                        }

                        browser.FindElement(By.Name("username")).SendKeys("BakaBoyyyy");
                        browser.FindElement(By.Name("password")).SendKeys("dddddddfgg1");
                        browser.FindElement(By.ClassName(("signin-checkbox"))).Click();
                        browser.FindElement(By.ClassName("mobile-button")).Click();

                        System.Threading.Tasks.Task.Delay(5000).Wait();

                        Close();
                  };

                  FormClosing += (s, e) =>
                  {
                        List<Cookie> cookies = new List<Cookie>();

                        foreach(Cookie cookie in browser.Options.Cookies.AllCookies)
                              cookies.Add(cookie);
                        
                        manager.AddCookies(
                              cookies.Select(x => x.ToNetCookie())
                              .ToArray()
                              );

                        manager.Save();
                        browser.Dispose();
                  };
            }
      }
}
