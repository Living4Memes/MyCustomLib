using MyCustomLib.Controls;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyCustomLib.GraphicFunctions
{
      /// <summary>
      /// Статический класс реализации основных графических функций
      /// </summary>
      public static class CustomGraphics
      {
            /// <summary>
            /// Строит <see cref="GraphicsPath"/> стиля "Pill"
            /// </summary>
            /// <param name="rect">Исходный <see cref="Rectangle"/></param>
            /// <returns><see cref="GraphicsPath"/></returns>
            public static GraphicsPath GetPillRectanglePath(Rectangle rect)
            {
                  return GetRoundedRectanglePath(rect, rect.Height * 0.5);
            }

            /// <summary>
            /// Строит <see cref="GraphicsPath"/> закругленного <see cref="Rectangle"/>
            /// </summary>
            /// <param name="rect">Исходный <see cref="Rectangle"/></param>
            /// <returns><see cref="GraphicsPath"/></returns>
            public static GraphicsPath GetRoundedRectanglePath(Rectangle rect, double radius = 20)
            {
                  int xradius = Convert.ToInt32(radius);

                  int yradius = xradius;
                  GraphicsPath body = new GraphicsPath();
                  Point point1 = new Point(0, 0), point2 = new Point(0, 0);

                  //Левый верхний угол
                  Rectangle cornerRect = new Rectangle(rect.X, rect.Y, xradius * 2, yradius * 2);
                  body.AddArc(cornerRect, 180, 90);

                  //Верхняя граница 
                  point1 = new Point(rect.X + xradius, rect.Y);
                  point2 = new Point(rect.Right - xradius, rect.Y);
                  body.AddLine(point1, point2);

                  //Правый верхний угол
                  cornerRect = new Rectangle(rect.Right - 2 * xradius, rect.Y, xradius * 2, xradius * 2);
                  body.AddArc(cornerRect, 270, 90);

                  //Правая граница
                  point1 = new Point(rect.Right, rect.Y + yradius);
                  point2 = new Point(rect.Right, rect.Bottom - yradius);
                  body.AddLine(point1, point2);

                  //Правый нижний угол
                  cornerRect = new Rectangle(rect.Right - 2 * xradius, rect.Bottom - 2 * yradius, xradius * 2, xradius * 2);
                  body.AddArc(cornerRect, 0, 90);

                  //Нижняя граница
                  point1 = new Point(rect.Right - xradius, rect.Bottom);
                  point2 = new Point(rect.X + xradius, rect.Bottom);
                  body.AddLine(point1, point2);

                  //Левый нижний угол
                  cornerRect = new Rectangle(rect.X, rect.Bottom - yradius * 2, xradius * 2, xradius * 2);
                  body.AddArc(cornerRect, 90, 90);

                  //Левая граница
                  point1 = new Point(rect.X, rect.Bottom - yradius);
                  point2 = new Point(rect.X, rect.Y + yradius);
                  body.AddLine(point1, point2);

                  return body;
            }

            /// <summary>
            /// Строит <see cref="GraphicsPath"/> входного <see cref="Rectangle"/>
            /// </summary>
            /// <param name="rect">Прямоугольник, на котором основывается построение</param>
            /// <returns><see cref="GraphicsPath"/> входного <see cref="Rectangle"/></returns>
            public static GraphicsPath GetRectanglePath(Rectangle rect)
            {
                  GraphicsPath body = new GraphicsPath();
                  RectanglePoints allPoints = rect.GetAllPoints();

                  body.AddLine(allPoints.TopLeft, allPoints.TopRight);
                  body.AddLine(allPoints.TopRight, allPoints.BottomRight);
                  body.AddLine(allPoints.BottomRight, allPoints.BottomLeft);
                  body.AddLine(allPoints.BottomLeft, allPoints.TopLeft);

                  return body;
            }

            /// <summary>
            /// Построение <see cref="GraphicsPath"/> контейнера
            /// </summary>
            /// <param name="squareContainer">Прямоугольный контейнер</param>
            /// <returns><see cref="GraphicsPath"/> контейнера</returns>
            public static GraphicsPath GetContainerGraphicsPath(ISquareContainer squareContainer)
            {
                  return GetRectanglePath(squareContainer.ClientRectangle);
            }

            /// <summary>
            /// Построение <see cref="GraphicsPath"/> контейнера
            /// </summary>
            /// <param name="roundedContainer">Контейнер с закругленными краями</param>
            /// <returns><see cref="GraphicsPath"/> контейнера</returns>
            public static GraphicsPath GetContainerGraphicsPath(IRoundedContainer roundedContainer)
            {
                  return GetRoundedRectanglePath(roundedContainer.ClientRectangle, roundedContainer.RoundedRadius);
            }

            /// <summary>
            /// Построение <see cref="GraphicsPath"/> контейнера
            /// </summary>
            /// <param name="pillContainer">Контейнер типа "Pill"</param>
            /// <returns><see cref="GraphicsPath"/> контейнера</returns>
            public static GraphicsPath GetContainerGraphicsPath(IPillContainer pillContainer)
            {
                  return GetPillRectanglePath(pillContainer.ClientRectangle);
            }

            /// <summary>
            /// Рисует рамку, используя входную <see cref="Graphics"/>
            /// </summary>
            /// <param name="g">Графика, где будет происходить рисование</param>
            /// <param name="path"><see cref="GraphicsPath"/> самой рамки</param>
            /// <param name="borderProperties">Настройки рамки</param>
            public static void DrawBorder (Graphics g, GraphicsPath path, BorderProperties borderProperties)
            {
                  g.DrawPath(new Pen(borderProperties.BorderColor, borderProperties.BorderWidth), path);
            }

            /// <summary>
            /// Рисует рамку на входном изображении
            /// </summary>
            /// <param name="image">Ссылка на изображение, на котором будет нарисована рамка</param>
            /// <param name="path"><see cref="GraphicsPath"/> самой рамки</param>
            /// <param name="borderProperties">Настройки рамки</param>
            public static void DrawBorder(ref Image image, GraphicsPath path, BorderProperties borderProperties) => DrawBorder(Graphics.FromImage(image), path, borderProperties);

            /// <summary>
            /// Добавляет рамку к входному изображению
            /// </summary>
            /// <param name="image">Исходное изображение</param>
            /// <param name="path"><see cref="GraphicsPath"/> самой рамки</param>
            /// <param name="borderProperties">Настройки рамки</param>
            /// <returns>Изображение с рамкой</returns>
            public static Image AddBorder(Image image, GraphicsPath path, BorderProperties borderProperties)
            {
                  Image result = new Bitmap(image);

                  DrawBorder(ref result, path, borderProperties);

                  return result;
            }

            /// <summary>
            /// Рисует тень, используя входную <see cref="Graphics"/>
            /// </summary>
            /// <param name="g">Графика, где будет происходить рисование</param>
            /// <param name="rect">Прямоугольник, внутри которого будет нарисована тень</param>
            /// <param name="shadowProperties">Настройки тени</param>
            public static void DrawShadow(Graphics g, Rectangle rect, ShadowProperties shadowProperties)
            {
                  Size size_vertical = new Size((int)(rect.Width * shadowProperties.Range - shadowProperties.Range * 80), rect.Height); // Не знаю, почему 80
                  Size size_horizontal = new Size(rect.Width, (int)(rect.Height * shadowProperties.Range) - (int)(shadowProperties.Range * 80)); // Не знаю, почему 80. Добавил, чтобы не было лишней линии

                  Rectangle leftGradient = new Rectangle(new Point(0, 0), size_vertical);
                  Rectangle topGradient = new Rectangle(new Point(0, 0), size_horizontal);
                  Rectangle rightGradient = new Rectangle(new Point(rect.Right - size_vertical.Width), size_vertical);
                  Rectangle botGradient = new Rectangle(new Point(0, rect.Bottom - size_horizontal.Height), size_horizontal);

                  using (LinearGradientBrush brushTop = new LinearGradientBrush(topGradient, shadowProperties.InitialColor, shadowProperties.FiniteColor, 90, true))
                  using (LinearGradientBrush brushBot = new LinearGradientBrush(botGradient, shadowProperties.InitialColor, shadowProperties.FiniteColor, 270, true))
                  using (LinearGradientBrush brushLeft = new LinearGradientBrush(leftGradient, shadowProperties.InitialColor, shadowProperties.FiniteColor, 360, true))
                  using (LinearGradientBrush brushRight = new LinearGradientBrush(rightGradient, shadowProperties.InitialColor, shadowProperties.FiniteColor, 180, true))
                  {
                        brushTop.Blend = shadowProperties.GradientBlend;
                        brushBot.Blend = shadowProperties.GradientBlend;
                        brushLeft.Blend = shadowProperties.GradientBlend;
                        brushRight.Blend = shadowProperties.GradientBlend;

                        if (shadowProperties.ShadowStyle != ShadowStyle.Vertical)
                        {
                              if(shadowProperties.ShadowStyle != ShadowStyle.Bottom)
                                    g.FillPath(brushTop, GetRectanglePath(topGradient));
                              if(shadowProperties.ShadowStyle != ShadowStyle.Top)
                                    g.FillPath(brushBot, GetRectanglePath(botGradient));
                        }
                        if (shadowProperties.ShadowStyle != ShadowStyle.Horizontal)
                        {
                              if (shadowProperties.ShadowStyle != ShadowStyle.Left)
                                    g.FillPath(brushRight, GetRectanglePath(rightGradient));
                              if (shadowProperties.ShadowStyle != ShadowStyle.Right)
                                    g.FillPath(brushLeft, GetRectanglePath(leftGradient));
                        }
                  }

            }

            /// <summary>
            /// Рисует тень на входном изображении
            /// </summary>
            /// <param name="image">Ссылка на изображение, на котором будет нарисована тень</param>
            /// <param name="shadowProperties">Настройки тени</param>
            public static void DrawShadow(ref Image image, ShadowProperties shadowProperties) => DrawShadow(Graphics.FromImage(image), image.GetRectangle(), shadowProperties);

            /// <summary>
            /// Рисует тень на входном изображении
            /// </summary>
            /// <param name="image">Исходное изображение</param>
            /// <param name="shadowProperties">Настройки тени</param>
            /// <returns>Изображение с тенью</returns>
            public static Image AddShadow(Image image, ShadowProperties shadowProperties)
            {
                  Image result = new Bitmap(image);

                  DrawShadow(ref result, shadowProperties);

                  return result;
            }

            /// <summary>
            /// Рисует затемнение, используя входную <see cref="Graphics"/>
            /// </summary>
            /// <param name="g">Графика, где будет происходить рисование</param>
            /// <param name="rect">Прямоугольник, внутри которого будет нарисовано затемнение</param>
            /// <param name="color">Цвет затемнения</param>
            /// <param name="alpha">Альфа-канал затемнения</param>
            public static void DrawBlackout(Graphics g, Rectangle rect, Color color, int alpha = 200)
            {
                  g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, color)), rect);
            }

            /// <summary>
            /// Рисует затемнение на входном изображении
            /// </summary>
            /// <param name="image">Ссылка на изображение, на котором будет нарисовано затемнение</param>
            /// <param name="color">Цвет затемнения</param>
            /// <param name="alpha">Альфа-канал затемнения</param>
            public static void DrawBlackout(ref Image image, Color color, int alpha = 200) => DrawBlackout(Graphics.FromImage(image), image.GetRectangle(), color, alpha);

            /// <summary>
            /// Рисует затемнение на входном изображении
            /// </summary>
            /// <param name="image">Исходное изображение</param>
            /// <param name="color">Цвет затемнения</param>
            /// <param name="alpha">Альфа-канал затемнения</param>
            /// <returns>Изображение с затемнением</returns>
            public static Image AddBlackout(Image image, Color color, int alpha = 200)
            {
                  Image result = new Bitmap(image);

                  DrawBlackout(ref image, color, alpha);

                  return result;
            }

            public static void DrawText(string text, Font font, Graphics graph, Rectangle rect)
            {
                  graph.DrawString(text, font, new SolidBrush(Color.Black), rect);
            }
      }
}
