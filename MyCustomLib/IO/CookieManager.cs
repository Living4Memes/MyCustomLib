using MyCustomLib.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace MyCustomLib.IO
{
      internal static class XmlNames
      {
            internal static string Managed = "managed";
            internal static string Root = "root";
            internal static string CookieCollection = "c_collection";
            internal static string Host = "host";
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
            protected XmlDocument _document;

            public string FolderPath { get; set; } = _defaultFolderPath;
            public string DocumentName { get; set; } = "Cookies.xml";
            public virtual ManagedCookieCollection Cookies { get; private set; }

            public CookieManager(string folderPath = "CookieManager\\Cookies")
            {
                  _document = new XmlDocument();
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

            public void AddCookies(string host, params Cookie[] cookies)
            {
                  if (host.LastIndexOf('/') != -1)
                        throw new ArgumentException("Wrong host!");

                  Load();

                  CookieCollection collection = new CookieCollection();

                  cookies.ToList().ForEach(x => collection.Add(x));

                  if (Cookies.Hosts.Contains(host))
                  {
                        foreach (Cookie cookie in cookies)
                        {
                              Cookies[host].Add(cookie);

                              AppendCookieXML(host, GetCookieXml(cookie));
                        }
                  }
                  else
                  {
                        Cookies.Add(host, collection);
                        AppendCookieCollectionXml(host, cookies);
                  }

                  Save();
            }

            public void RemoveCookies(string host, params Cookie[] cookies)
            {
                  Load();

                  if (!Cookies.Hosts.Contains(host))
                        throw new KeyNotFoundException("No such host!");

                  foreach (Cookie cookie in cookies)
                        DeleteCookieNode(cookie.Name);

                  Save();
            }

            private ManagedCookieCollection ParseCookiesXml()
            {
                  ManagedCookieCollection collection = new ManagedCookieCollection();

                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName(XmlNames.CookieCollection).Item(0);

                  foreach (XmlNode hostNode in cookiesNode.ChildNodes)
                  {
                        (string, CookieCollection) result = ParseCookieCollectionNode(hostNode);

                        collection.Add(result.Item1, result.Item2);
                  }

                  return collection;
            }

            private (string, CookieCollection) ParseCookieCollectionNode(XmlNode cookieCollectionNode)
            {
                  string host = cookieCollectionNode.Attributes[XmlNames.Host].Value;
                  CookieCollection collection = new CookieCollection();

                  foreach (XmlNode cookieNode in cookieCollectionNode)
                        collection.Add(ParseCookieNode(cookieNode));

                  return (host, collection);
            }

            private Cookie ParseCookieNode(XmlNode cookieNode)
            {
                  try
                  {
                        return new Cookie()
                        {
                              Name = cookieNode.Attributes[XmlNames.Name].Value,
                              Value = cookieNode.Attributes[XmlNames.Value].Value,
                              Domain = cookieNode.Attributes[XmlNames.Domain].Value,
                              Path = cookieNode.Attributes[XmlNames.Path].Value,
                              Expires = Convert.ToDateTime(cookieNode.Attributes[XmlNames.Expires].Value),
                              Secure = Convert.ToBoolean(cookieNode.Attributes[XmlNames.Secure].Value),
                              Comment = cookieNode.Attributes[XmlNames.Comment].Value
                        };
                  }
                  catch
                  {
                        throw new InvalidDataException("Invalid data in cookie XML");
                  }
            }

            private XmlElement GetCookieXml(Cookie cookie)
            {
                  XmlElement cookieXml = _document.CreateElement(XmlNames.CookieInfo);

                  XmlAttribute name = _document.CreateAttribute(XmlNames.Name);
                  XmlAttribute value = _document.CreateAttribute(XmlNames.Value);
                  XmlAttribute domain = _document.CreateAttribute(XmlNames.Domain);
                  XmlAttribute path = _document.CreateAttribute(XmlNames.Path);
                  XmlAttribute expires = _document.CreateAttribute(XmlNames.Expires);
                  XmlAttribute secure = _document.CreateAttribute(XmlNames.Secure);
                  XmlAttribute comment = _document.CreateAttribute(XmlNames.Comment);

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

            private XmlElement GetCookieCollectionXml(string host, CookieCollection cookies)
            {
                  XmlElement collection = _document.CreateElement(XmlNames.CookieCollection);
                  XmlAttribute hostXml = _document.CreateAttribute(XmlNames.Host);

                  hostXml.Value = host;

                  collection.AppendChild(hostXml);

                  foreach (Cookie cookie in cookies)
                        collection.AppendChild(GetCookieXml(cookie));

                  return collection;
            }

            private void AppendCookieXML(string host, XmlNode cookieNode)
            {
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName(XmlNames.CookieCollection).Item(0);

                  XmlNode parent = cookiesNode.ChildNodes.ToList().SingleOrDefault(x => x.Attributes[XmlNames.Host].Value == host);

                  parent.AppendChild(cookieNode);
            }

            private void AppendCookieCollectionXml(string host, Cookie[] cookies)
            {
                  XmlElement collection = _document.CreateElement(XmlNames.Managed);
                  XmlAttribute hostXml = _document.CreateAttribute(XmlNames.Host);
                  hostXml.Value = host;
                  collection.Attributes.Append(hostXml);

                  foreach (Cookie cookie in cookies)
                        collection.AppendChild(GetCookieXml(cookie));

                  _document.GetElementsByTagName(XmlNames.CookieCollection).Item(0).AppendChild(collection);
            }

            private void DeleteCookieNode(string cookieName)
            {
                  XmlNode cookiesNode = _document.DocumentElement.GetElementsByTagName(XmlNames.CookieCollection).Item(0);

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

                        XmlElement root = _document.CreateElement(XmlNames.Root);
                        XmlElement cookies = _document.CreateElement(XmlNames.CookieCollection);

                        root.AppendChild(cookies);
                        _document.AppendChild(root);

                        Save();
                  }
            }
      }
}
