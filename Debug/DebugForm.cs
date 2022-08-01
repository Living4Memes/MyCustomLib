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
                        manager.Load();

                        List<OpenQA.Selenium.Cookie> cookies = manager.Cookies.Select(x => x.ToSeleniumCookie()).ToList();

                        foreach(OpenQA.Selenium.Cookie cookie in cookies.Where(x => x.Domain.StartsWith("www")))
                              browser.Options.Cookies.AddCookie(cookie);

                        manager.Cookies.ForEach(x => browser.Options.Cookies.AddCookie(x.ToSeleniumCookie()));

                        browser.Navigation.GoToUrl("https://www.riotgames.com/ru");

                        browser.FindElement(By.Id("login")).Click();

                        browser.FindElement(By.Name("username")).SendKeys("BakaBoyyyy");
                        browser.FindElement(By.Name("password")).SendKeys("ddddddddfgg1");
                        browser.FindElement(By.CssSelector("body > div:nth-child(3) > div > div > div.grid.grid-direction__row.grid-page-web__content > div > div > div.grid.grid-align-center.grid-justify-space-between.grid-fill.grid-direction__column.grid-panel-web__content.grid-panel__content > div > div > div > div:nth-child(4) > div.mobile-checkbox.signin-checkbox > label > input[type=checkbox]")).Click();
                        browser.FindElement(By.ClassName("mobile-button")).Click();

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
