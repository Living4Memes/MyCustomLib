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



            public static void DrawShadow(Graphics g, Rectangle rect, ShadowStyle style)
            {
                  Size size_vertical = new Size((int)(rect.Width * style.Range - style.Range * 80), rect.Height); // Не знаю, почему 80
                  Size size_horizontal = new Size(rect.Width, (int)(rect.Height * style.Range) - (int)(style.Range * 80)); // Не знаю, почему 80. Добавил, чтобы не было лишней линии

                  Rectangle leftGradient = new Rectangle(new Point(0, 0), size_vertical);
                  Rectangle topGradient = new Rectangle(new Point(0, 0), size_horizontal);
                  Rectangle rightGradient = new Rectangle(new Point(rect.Right - size_vertical.Width), size_vertical);
                  Rectangle botGradient = new Rectangle(new Point(0, rect.Bottom - size_horizontal.Height), size_horizontal);

                  using (LinearGradientBrush brushTop = new LinearGradientBrush(topGradient, style.InitialColor, style.FiniteColor, 90, true))
                  using (LinearGradientBrush brushBot = new LinearGradientBrush(botGradient, style.InitialColor, style.FiniteColor, 270, true))
                  using (LinearGradientBrush brushLeft = new LinearGradientBrush(leftGradient, style.InitialColor, style.FiniteColor, 360, true))
                  using (LinearGradientBrush brushRight = new LinearGradientBrush(rightGradient, style.InitialColor, style.FiniteColor, 180, true))
                  {
                        brushTop.Blend = style.GradientBlend;
                        brushBot.Blend = style.GradientBlend;
                        brushLeft.Blend = style.GradientBlend;
                        brushRight.Blend = style.GradientBlend;

                        if (style.ShadowType != ShadowType.Vertical)
                        {
                              if(style.ShadowType != ShadowType.Bottom)
                                    g.FillPath(brushTop, GetRectanglePath(topGradient));
                              if(style.ShadowType != ShadowType.Top)
                                    g.FillPath(brushBot, GetRectanglePath(botGradient));
                        }
                        if (style.ShadowType != ShadowType.Horizontal)
                        {
                              if (style.ShadowType != ShadowType.Left)
                                    g.FillPath(brushRight, GetRectanglePath(rightGradient));
                              if (style.ShadowType != ShadowType.Right)
                                    g.FillPath(brushLeft, GetRectanglePath(leftGradient));
                        }
                  }

            }

            public static void AddShadow(Image image, ShadowStyle style) => DrawShadow(Graphics.FromImage(image), image.GetRectangle(), style);

            public static void DrawBlackout(Graphics g, Rectangle rect, Color color, int alpha = 200)
            {
                  g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, color)), rect);
            }

            public static void AddBlackout(Image image, Color color, int alpha = 200) => DrawBlackout(Graphics.FromImage(image), image.GetRectangle(), color, alpha);

            public static void DrawText(string text, Font font, Graphics graph, Rectangle rect)
            {
                  graph.DrawString(text, font, new SolidBrush(Color.Black), rect);
            }
      }
}
