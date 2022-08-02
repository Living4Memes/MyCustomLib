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
      internal static class XmlNodeNames
      {
            internal static string Root = "root";
            internal static string CookieList = "c_collection";
            internal static string CookieInfo = "c_data";
            internal static string Name = "c_name";
            internal static string Value = "c_value";
            internal static string Domain = "c_domain";
            internal static string Path = "c_path";
            internal static string Expires = "c_expires";
            internal static string Secure = "c_secure";
            internal static string Comment = "c_comment";
      }

      public class CookieManager
      {
            protected static string _defaultFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) 
                  + "\\Living4Memes\\Unmanaged";
            protected XmlDocument _document = new XmlDocument();

            public string FolderPath { get; set; } = _defaultFolderPath;
            public string DocumentName { get; set; } = "Cookies.xml";
            public virtual List<Cookie> Cookies { get; private set; }

            public CookieManager(string folderPath = "CookieManager\\Cookies")
            {
                  FolderPath = folderPath;

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

            public void AddCookies(params Cookie[] cookies)
            {
                  Load();

                  foreach (Cookie cookie in cookies)
                  {
                        if (Cookies.SingleOrDefault(x => x.Name == cookie.Name) == null)
                        {
                              Cookies.Add(cookie);

                              AppendCookieXML(GetCookieXml(cookie));
                        }
                  }

                  Save();
            }

            public void RemoveCookies(params Cookie[] cookies)
            {
                  Load();

                  foreach(Cookie cookie in cookies)
                        Cookies.Remove(cookie);

                  Save();
            }

            private List<Cookie> ParseCookiesXml()
            {
                  List<Cookie> cookies = new List<Cookie>();
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName(XmlNodeNames.CookieList).Item(0);

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
                              Name = cookieNode.Attributes[XmlNodeNames.Name].Value,
                              Value = cookieNode.Attributes[XmlNodeNames.Value].Value,
                              Domain = cookieNode.Attributes[XmlNodeNames.Domain].Value,
                              Path = cookieNode.Attributes[XmlNodeNames.Path].Value,
                              Expires = Convert.ToDateTime(cookieNode.Attributes[XmlNodeNames.Expires].Value),
                              Secure = Convert.ToBoolean(cookieNode.Attributes[XmlNodeNames.Secure].Value),
                              Comment = cookieNode.Attributes[XmlNodeNames.Comment].Value
                        };
                  }
                  catch
                  {
                        throw new InvalidDataException("Invalid data in cookie XML");
                  }
            }

            private XmlElement GetCookieXml(Cookie cookie)
            {
                  XmlElement cookieXml = _document.CreateElement(XmlNodeNames.CookieInfo);

                  XmlAttribute name = _document.CreateAttribute(XmlNodeNames.Name);
                  XmlAttribute value = _document.CreateAttribute(XmlNodeNames.Value);
                  XmlAttribute domain = _document.CreateAttribute(XmlNodeNames.Domain);
                  XmlAttribute path = _document.CreateAttribute(XmlNodeNames.Path);
                  XmlAttribute expires = _document.CreateAttribute(XmlNodeNames.Expires);
                  XmlAttribute secure = _document.CreateAttribute(XmlNodeNames.Secure);
                  XmlAttribute comment = _document.CreateAttribute(XmlNodeNames.Comment);

                  name.Value = cookie.Name;
                  value.Value = cookie.Value;
                  domain.Value = cookie.Domain;
                  path.Value = cookie.Path;
                  expires.Value = cookie.Expires.ToString();
                  secure.Value = cookie.Secure.ToString();
                  comment.Value = cookie.Comment;

                  cookieXml.Attributes.Append(name);
                  cookieXml.Attributes.Append(value);
                  cookieXml.Attributes.Append(domain);
                  cookieXml.Attributes.Append(path);
                  cookieXml.Attributes.Append(expires);
                  cookieXml.Attributes.Append(secure);
                  cookieXml.Attributes.Append(comment);

                  return cookieXml;
            }

            private void AppendCookieXML(XmlNode cookieNode)
            {
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName(XmlNodeNames.CookieList).Item(0);

                  cookiesNode.AppendChild(cookieNode);
            }

            private void DeleteCookieNode(string cookieName)
            {
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName(XmlNodeNames.CookieList).Item(0);

                  foreach (XmlNode node in cookiesNode.ChildNodes)
                        if (node.Attributes["c_name"].Value == cookieName)
                              cookiesNode.RemoveChild(node);
            }

            private void TestFile()
            {
                  if(!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                  if (File.Exists(FolderPath + DocumentName))
                        return;
                  else
                  {
                        File.Create(FolderPath + DocumentName).Close();

                        XmlElement root = _document.CreateElement(XmlNodeNames.Root);
                        XmlElement cookies = _document.CreateElement(XmlNodeNames.CookieList);

                        root.AppendChild(cookies);
                        _document.AppendChild(root);

                        Save();
                  }
            }
      }
}
