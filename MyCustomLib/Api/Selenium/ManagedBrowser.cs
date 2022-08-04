using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace MyCustomLib.Api.Selenium
{

      public class ManagedBrowser : IWebDriver, IDisposable
      {
            private EventFiringWebDriver _driver;
            private Dictionary<string, CookieCollection> _allCookies;

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
                  _allCookies = new Dictionary<string, CookieCollection>();

                  InitializeEvents();
                  Manage();
                  Navigate();
            }

            public void Close() => _driver.Close();
            public void Quit() => _driver.Quit();
            public IOptions Manage() => _driver.Manage();
            public INavigation Navigate() => _driver.Navigate();
            public ITargetLocator SwitchTo() => _driver.SwitchTo();
            public IWebElement FindElement(By by) => _driver.FindElement(by);
            public ReadOnlyCollection<IWebElement> FindElements(By by) => _driver.FindElements(by);
            public void Dispose()
            {
                  _driver.Quit();
                  _driver.Dispose();
            }

            public void LoadCookies(List<OpenQA.Selenium.Cookie> cookies)
            {
                  cookies.Where(x => Url.Contains(x.Domain))
                        .ToList()
                        .ForEach(x => Options.Cookies.AddCookie(x));
            }

            private void InitializeEvents()
            {
                  _driver.Navigating += (s, e) =>
                  {
                        string host = new Uri(Url).Host;
                        if(_allCookies.Keys.Contains(host))
                        {
                              foreach (OpenQA.Selenium.Cookie cookie in Options.Cookies.AllCookies)
                                    _allCookies[host].Add(cookie.ToNetCookie());

                              Options.Cookies.DeleteAllCookies();

                              foreach (System.Net.Cookie cookie in _allCookies[host])
                                    Options.Cookies.AddCookie(cookie.ToSeleniumCookie());
                        }
                        else
                              _allCookies.Add(host, new CookieCollection());
                  };
            }
      }
}
