using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using MyCustomLib.GraphicFunctions;
using Svg;
using System.ComponentModel;


namespace MyCustomLib
{
      public static class Extensions
      {
            #region === Конвертация типов ===
            public static int ToInt(this string str)
            {
                  int number = 0;

                  if (int.TryParse(str, out number))
                        return number;

                  throw new ArgumentException("Input string was not correct int value!", nameof(str));
            }

            public static int ToInt(this float flt)
            {
                  return Convert.ToInt32(flt);
            }

            public static double ToDouble(this int number)
            {
                  return Convert.ToDouble(number);
            }

            public static double ToDouble(this string str)
            {
                  string input = str.Replace('.', ',');
                  double dbl = 0;

                  if (double.TryParse(str, out dbl))
                        return dbl;

                  throw new ArgumentException("Input string was not correct double value!", nameof(str));
            }

            public static byte ToByte(this string str)
            {
                  return Convert.ToByte(str);
            }

            public static byte[] ToByte(this Bitmap bitmap)
            {
                  if (bitmap == null)
                        throw new ArgumentNullException("Input image was null value", nameof(bitmap));

                  using (MemoryStream stream = new MemoryStream())
                  {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                        return stream.ToArray();
                  }
            }

            public static byte[] ToByte(this Image image)
            {
                  if (image == null)
                        throw new ArgumentNullException("Input image was null value", nameof(image));

                  return image.ToBitmap().ToByte();
            }

            public static string ToBase64String(this Bitmap bitmap)
            {
                  if (bitmap == null)
                        throw new ArgumentNullException("Input image was null value", nameof(bitmap));

                  return Convert.ToBase64String(bitmap.ToByte());
            }

            public static string ToBase64String(this Image image)
            {
                  if (image == null)
                        throw new ArgumentNullException("Input image was null value", nameof(image));

                  return image.ToBitmap().ToBase64String();
            }

            public static Bitmap ToBitmap(this Image image)
            {
                  if (image == null)
                        throw new ArgumentNullException("Input image was null value", nameof(image));

                  return new Bitmap(image);
            }
            #endregion

            #region === Представление данных в удобном виде ===
            public static string TableView(this int[,] array)
            {
                  string result = String.Empty;

                  for (int i = 0; i < array.GetLength(0); i++)
                  {
                        for(int j = 0; j < array.GetLength(1); j++)
                              result += array[i, j] + ' ';

                        result += '\n';
                  }

                  return result;
            }

            public static string TableView(this string[,] array)
            {
                  string result = String.Empty;

                  for (int i = 0; i < array.GetLength(0); i++)
                  {
                        for (int j = 0; j < array.GetLength(1); j++)
                              result += array[i, j] + ' ';

                        result += '\n';
                  }

                  return result;
            }

            public static string TableView(this double[,] array)
            {
                  string result = String.Empty;

                  for (int i = 0; i < array.GetLength(0); i++)
                  {
                        for (int j = 0; j < array.GetLength(1); j++)
                              result += array[i, j] + ' ';

                        result += '\n';
                  }

                  return result;
            }

            public static string TableView(this float[,] array)
            {
                  string result = String.Empty;

                  for (int i = 0; i < array.GetLength(0); i++)
                  {
                        for (int j = 0; j < array.GetLength(1); j++)
                              result += array[i, j] + ' ';

                        result += '\n';
                  }

                  return result;
            }
            #endregion
      }
}

