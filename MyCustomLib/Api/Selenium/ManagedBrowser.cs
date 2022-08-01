using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyCustomLib.Api.Selenium
{

      public class ManagedBrowser : WebDriver
      {
            protected DriverService _driverService;
            protected DriverOptions _driverOptions;

            public INavigation Navigation { get => Navigate(); }
            public IOptions Options { get => Manage(); }

            public ManagedBrowser(ICommandExecutor ice, ICapabilities icap) : base(ice, icap) { }

            public ManagedBrowser(WebDriver driver) : base(driver.CommandExecutor, driver.Capabilities) { }

            ~ManagedBrowser()
            {
                  Dispose();
            }

            protected virtual void InitializeBrowser()
            {
                  
            }
      }
}
