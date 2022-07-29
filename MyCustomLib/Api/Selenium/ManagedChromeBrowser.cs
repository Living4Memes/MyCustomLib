using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace MyCustomLib.Api.Selenium
{
      public class ManagedChromeBrowser : ManagedBrowser
      {
            protected new ChromeDriver _browser;
            protected new ChromeDriverService _driverService;
            protected new ChromeOptions _driverOptions;

            public ManagedChromeBrowser() : base()
            {
                  _driverOptions = new ChromeOptions();
                  _driverService = ChromeDriverService.CreateDefaultService();

                  _browser = new ChromeDriver(_driverService, _driverOptions);
                  
            }
      }
}
