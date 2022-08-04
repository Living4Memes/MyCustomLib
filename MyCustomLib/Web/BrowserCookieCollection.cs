using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomLib.Web
{
      public sealed class ManagedCookieCollection : IEnumerable
      {
            private Dictionary<string, CookieCollection> _allCookies;

            public CookieCollection this[string host]
            {
                  get => _allCookies[host];
            }

            public string[] Hosts => _allCookies.Keys.ToArray();

            public IEnumerator GetEnumerator() => _allCookies.GetEnumerator();
      }
}
