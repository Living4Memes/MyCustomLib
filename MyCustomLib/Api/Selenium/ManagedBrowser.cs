using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyCustomLib.Web;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace MyCustomLib.Api.Selenium
{
      public class ManagedBrowser : IDisposable
      {
            private EventFiringWebDriver _driver;
            private ManagedCookieCollection _allCookies;

            public IOptions Options { get => Manage(); }
            public INavigation Navigation { get => Navigate(); }
            public string Url { get => _driver.Url; set => _driver.Url = value; }
            public string Title => _driver.Title;
            public string PageSource => _driver.PageSource;
            public string CurrentWindowHandle => _driver.CurrentWindowHandle;
            public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

            public ManagedBrowser(IWebDriver driver)
            {
                  _driver = new EventFiringWebDriver(driver);
                  _allCookies = new ManagedCookieCollection();

                  InitializeEvents();
                  Manage();
                  Navigate();
            }

            public void Close() => _driver.Close();
            public void Quit() => _driver.Quit();
            public ITargetLocator SwitchTo() => _driver.SwitchTo();
            public IWebElement FindElement(By by) => _driver.FindElement(by);
            public ReadOnlyCollection<IWebElement> FindElements(By by) => _driver.FindElements(by);
            public void Dispose()
            {
                  _driver.Quit();
                  _driver.Dispose();
            }
            private IOptions Manage() => _driver.Manage();
            private INavigation Navigate() => _driver.Navigate();

            public void LoadCookies(List<OpenQA.Selenium.Cookie> cookies)
            {
                  cookies.Where(x => Url.Contains(x.Domain))
                        .ToList()
                        .ForEach(x => Options.Cookies.AddCookie(x));
            }

            private void InitializeEvents()
            {
                  _driver.Navigated += (s, e) => ManageCookies();
                  _driver.ElementClicked += (s, e) => ManageCookies();
            }

            private void ManageCookies()
            {
                  string host = new Uri(Url).Host;
                  if (_allCookies.Hosts.Contains(host))
                  {
                        foreach (OpenQA.Selenium.Cookie cookie in Options.Cookies.AllCookies)
                              _allCookies[host].Add(cookie.ToNetCookie());

                        Options.Cookies.DeleteAllCookies();

                        foreach (System.Net.Cookie cookie in _allCookies[host])
                              Options.Cookies.AddCookie(cookie.ToSeleniumCookie());
                  }
                  else
                        _allCookies.Add(host, new CookieCollection());
            }
            
      }
}
