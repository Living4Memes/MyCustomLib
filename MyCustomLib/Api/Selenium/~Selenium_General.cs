using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyCustomLib.Api.Selenium
{
      public static class DefaultPaths
      {
            public const string ChromeDriverPath = @"C:\Users\vladk\source\repos\Living4Memes\MyCustomLib\packages\Selenium.WebDriver.ChromeDriver.103.0.5060.13400\driver\win32";
      }

      public static class Extensions
      {
            public static System.Net.Cookie ToNetCookie(this OpenQA.Selenium.Cookie cookie)
            {
                  DateTime dt;

                  if (cookie.Expiry == null)
                        dt = DateTime.MinValue;
                  else
                        dt = (DateTime)cookie.Expiry;

                  return new System.Net.Cookie()
                  {
                        Name = cookie.Name,
                        Value = cookie.Value,
                        Domain = cookie.Domain,
                        Path = cookie.Path,
                        Expires = dt,
                        Secure = (bool)cookie.Secure,
                        Comment = (string)cookie.SameSite
                  };
            }

            public static OpenQA.Selenium.Cookie ToSeleniumCookie(this System.Net.Cookie cookie)
            {
                  DateTime? dt;

                  if (cookie.Expires.ToString() == DateTime.MinValue.ToString())
                        dt = null;
                  else
                        dt = cookie.Expires;

                  return new OpenQA.Selenium.Cookie(cookie.Name, 
                        cookie.Value, 
                        cookie.Domain, 
                        cookie.Path, 
                        dt,
                        cookie.Secure,
                        cookie.HttpOnly,
                        cookie.Comment
                        );
            }
      }
}
