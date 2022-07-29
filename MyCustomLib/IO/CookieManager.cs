using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyCustomLib.IO
{
      public class CookieManager
      {
            private static string _defaultFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Living4Memes";
            private XmlDocument _document = new XmlDocument();

            public string FolderPath { get; set; } = _defaultFolderPath;
            public string DocumentName { get; set; } = "Cookies.xml";
            public List<Cookie> Cookies { get; private set; }

            public CookieManager()
            {
                  TestFile();
                  Load();
            }

            public void Save()
            {
                  TestFile();

                  try { _document.Save(FolderPath + DocumentName); }
                  catch (Exception ex) { throw ex; }
            }

            public void Load()
            {
                  TestFile();

                  try { _document.Load(FolderPath + DocumentName); }
                  catch (Exception ex) { throw ex; }

                  Cookies = ParseCookiesXml();
            }

            public void AddCookie(Cookie cookie)
            {
                  if (!Cookies.Contains(cookie))
                        Cookies.Add(cookie);
                  else
                        return;

                  AppendCookieXML(GetCookieXml(cookie));
            }

            public void RemoveCookie(Cookie cookie)
            {
                  Cookies.Remove(cookie);

                  DeleteCookieNode(cookie.Name);
            }

            private List<Cookie> ParseCookiesXml()
            {
                  List<Cookie> cookies = new List<Cookie>();
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName("c_info").Item(0);

                  foreach (XmlNode node in cookiesNode)
                        cookies.Add(ParseCookieNode(node));

                  return cookies;
            }

            private Cookie ParseCookieNode(XmlNode cookieNode)
            {
                  try
                  {
                        return new Cookie()
                        {
                              Name = cookieNode.Attributes["c_name"].Value,
                              Value = cookieNode.Attributes["c_value"].Value,
                              Domain = cookieNode.Attributes["c_domain"].Value,
                              Path = cookieNode.Attributes["c_path"].Value,
                              Expires = Convert.ToDateTime(cookieNode.Attributes["c_expires"].Value),
                              Secure = Convert.ToBoolean(cookieNode.Attributes["c_secure"].Value)
                        };
                  }
                  catch
                  {
                        throw new InvalidDataException("Invalid data in cookie XML");
                  }
            }

            private XmlElement GetCookieXml(Cookie cookie)
            {
                  XmlElement cookieXml = _document.CreateElement("c_data");

                  XmlAttribute name = _document.CreateAttribute("c_name");
                  XmlAttribute value = _document.CreateAttribute("c_value");
                  XmlAttribute domain = _document.CreateAttribute("c_domain");
                  XmlAttribute path = _document.CreateAttribute("c_path");
                  XmlAttribute expires = _document.CreateAttribute("c_expires");
                  XmlAttribute secure = _document.CreateAttribute("c_secure");

                  name.Value = cookie.Name;
                  value.Value = cookie.Value;
                  domain.Value = cookie.Domain;
                  path.Value = cookie.Path;
                  expires.Value = cookie.Expires.ToString();
                  secure.Value = cookie.Secure.ToString();

                  cookieXml.Attributes.Append(name);
                  cookieXml.Attributes.Append(value);
                  cookieXml.Attributes.Append(domain);
                  cookieXml.Attributes.Append(path);
                  cookieXml.Attributes.Append(expires);
                  cookieXml.Attributes.Append(secure);

                  return cookieXml;
            }

            private void AppendCookieXML(XmlNode cookieNode)
            {
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName("c_info").Item(0);

                  cookiesNode.AppendChild(cookieNode);
            }

            private void DeleteCookieNode(string cookieName)
            {
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName("c_info").Item(0);

                  foreach (XmlNode node in cookiesNode.ChildNodes)
                        if (node.Attributes["c_name"].Value == cookieName)
                              cookiesNode.RemoveChild(node);
            }

            private void TestFile()
            {
                  if (File.Exists(FolderPath + DocumentName))
                        return;
                  else
                  {
                        File.Create(FolderPath + DocumentName).Close();

                        XmlElement root = _document.CreateElement("root");
                        XmlElement cookies = _document.CreateElement("c_info");

                        root.AppendChild(cookies);
                        _document.AppendChild(root);

                        Save();
                  }
            }
      }
}
