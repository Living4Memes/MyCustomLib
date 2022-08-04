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

            public ManagedCookieCollection()
            {
                  _allCookies = new Dictionary<string, CookieCollection>();
            }

            public void Add(string host, CookieCollection collection)
            {
                  if (!Hosts.Contains(host))
                        _allCookies.Add(host, collection);
                  else foreach(Cookie cookie in collection)
                              _allCookies[host].Add(cookie);
            }

            public IEnumerator GetEnumerator() => _allCookies.GetEnumerator();
      }
}
