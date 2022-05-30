using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyCustomLib.GraphicFunctions
{
      public static class CustomGraphics
      {
            public static GraphicsPath GetPillRectanglePath(Rectangle rect)
            {
                  return GetRoundedRectanglePath(rect, rect.Height * 0.5);
            }

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


            public static void DrawBorder (Graphics g, GraphicsPath path, BorderProperties borderProperties)
            {
                  g.DrawPath(new Pen(borderProperties.BorderColor, borderProperties.BorderWidth), path);
            }

            public static void DrawBorder(Image image, GraphicsPath path, BorderProperties borderProperties) => DrawBorder(Graphics.FromImage(image), path, borderProperties);

            public static Image AddBorder(Image image, GraphicsPath path, BorderProperties borderProperties)
            {
                  Image result = new Bitmap(image);

                  DrawBorder(result, path, borderProperties);

                  return result;
            }

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

            public static void DrawShadow(Image image, ShadowProperties shadowProperties) => DrawShadow(Graphics.FromImage(image), image.GetRectangle(), shadowProperties);

            public static Image AddShadow(Image image, ShadowProperties shadowProperties)
            {
                  Image result = new Bitmap(image);

                  DrawShadow(result, shadowProperties);

                  return result;
            }

            public static void DrawBlackout(Graphics g, Rectangle rect, Color color, int alpha = 200)
            {
                  g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, color)), rect);
            }

            public static void DrawBlackout(Image image, Color color, int alpha = 200) => DrawBlackout(Graphics.FromImage(image), image.GetRectangle(), color, alpha);

            public static Image AddBlackout(Image image, Color color, int alpha = 200)
            {
                  Image result = new Bitmap(image);

                  DrawBlackout(image, color, alpha);

                  return result;
            }

            public static void DrawText(string text, Font font, Graphics graph, Rectangle rect)
            {
                  graph.DrawString(text, font, new SolidBrush(Color.Black), rect);
            }
      }
}
