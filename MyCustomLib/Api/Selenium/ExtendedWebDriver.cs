using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomLib.Api.Selenium
{
      internal class ExtendedWebDriver : WebDriver, INotifyPropertyChanged
      {
            private string url;
            public new string Url
            {
                  get => url; 
                  set
                  {
                        url = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Url)));
                  }
            }

            protected ExtendedWebDriver(ICommandExecutor executor, ICapabilities capabilities) : base(executor, capabilities)
            {
            }

            public event PropertyChangedEventHandler PropertyChanged;
      }
}
