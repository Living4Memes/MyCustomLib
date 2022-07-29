using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyCustomLib.Api.Selenium
{

      public abstract class ManagedBrowser
      {
            public event EmptyEventHandler Started;

            protected WebDriver _browser;
            protected DriverService _driverService;
            protected DriverOptions _driverOptions;

            public ManagedBrowser()
            {
                  //Options = new List<string>();
            }

            public INavigation Navigate()
            {
                  return _browser.Navigate();
            }

            protected virtual void InitializeBrowser()
            {
                  
            }
      }
}
