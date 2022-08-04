using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyCustomLib.Api.Selenium
{

      public class ManagedBrowser : IWebDriver, IDisposable
      {
            public delegate void WebSiteHandler(string url, CookieCollection cookies);
            public event WebSiteHandler UrlChanged;

            private ExtendedWebDriver _driver;

            public IOptions Options { get => Manage(); }
            public INavigation Navigation { get => Navigate(); }
            public string Url { get => _driver.Url; set => _driver.Url = value; }
            public string Title => _driver.Title;
            public string PageSource => _driver.PageSource;
            public string CurrentWindowHandle => _driver.CurrentWindowHandle;
            public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

            public ManagedBrowser(WebDriver driver) { _driver = (ExtendedWebDriver)driver; Initialize();  Manage(); Navigate(); }

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

            private void Initialize()
            {
                  _driver.PropertyChanged += (s, e) =>
                  {
                        if(e.PropertyName == nameof(_driver.Url))
                        {

                        }
                  };
            }
            
            private CookieCollection GetCookiesForDomain(string url)
            {
                  return null;
            }
      }
}
