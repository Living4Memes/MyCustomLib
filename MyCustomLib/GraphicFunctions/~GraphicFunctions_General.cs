using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyCustomLib.GraphicFunctions
{
      public struct RectanglePoints
      {
            public Point TopLeft { get; set; }
            public Point TopRight { get; set; }
            public Point BottomRight { get; set; }
            public Point BottomLeft { get; set; }
      }

      public struct RoundedRectangle
      {
            public Rectangle MainRectangle { get; set; }
            public double Radius { get; set; }
            public GraphicsPath GraphicsPath { get => CustomGraphics.GetRoundedRectanglePath(MainRectangle, Radius); }

            public RoundedRectangle(Rectangle rect, double radius = 20)
            {
                  MainRectangle = rect;
                  Radius = radius;
            }
      }

      public struct EnhancedImage
      {
            public Image PlainImage { get; private set; }
            public Image ShadowedImage { get; private set; }

            public EnhancedImage(Image image)
            {
                  PlainImage = new Bitmap(image);
                  ShadowedImage = new Bitmap(image);

                  CustomGraphics.AddShadow(ShadowedImage, new ShadowStyle(ShadowType.Full));
            }

            public Image GetImageWithBorder(GraphicsPath path, Color color, float width)
            {
                  return AddBorder(PlainImage, path, color, width);
            }

            public Image GetShadowedImageWithBorder(GraphicsPath path, Color color, float width)
            {
                  return AddBorder(ShadowedImage, path, color, width);
            }

            private Image AddBorder(Image image, GraphicsPath path, Color color, float width)
            {
                  Image resultImage = new Bitmap(image);

                  using (Graphics g = Graphics.FromImage(resultImage))
                        g.DrawPath(new Pen(color, width), path);

                  return resultImage;
            }
      }

      public struct ShadowStyle
      {
            public ShadowType ShadowType { get; set; }
            public Color InitialColor { get; set; }
            public Color FiniteColor { get; set; }
            public double Range { get; set; }
            public Blend GradientBlend { get; set; }

            public ShadowStyle(ShadowType shadowType)
            {
                  ShadowType = shadowType;
                  InitialColor = Color.Black;
                  FiniteColor = Color.Transparent;
                  Range = 0.15;
                  GradientBlend = new Blend(2)
                  {
                        Factors = new float[] { 0.0f, 1.0f },
                        Positions = new float[] { 0.0f, 1.0f }
                  };
            }

            public ShadowStyle(ShadowType shadowType, Color initialColor, Color finiteColor, double range, Blend gradientBlend)
            {
                  ShadowType = shadowType;
                  InitialColor = initialColor;
                  FiniteColor = finiteColor;
                  Range = range;
                  GradientBlend = gradientBlend;
            }
      }

      public enum ShadowType
      { 
            Full,
            Vertical,
            Horizontal,
            Top,
            Bottom,
            Left,
            Right
      }

      public static class Extensions
      {
            public static Rectangle GetRectangle(this Image image)
            {
                  return new Rectangle(0, 0, image.Width, image.Height);
            }

            public static RectanglePoints GetAllPoints(this Rectangle rect)
            {
                  return new RectanglePoints()
                  {
                        TopLeft = new Point(rect.Left, rect.Top),
                        TopRight = new Point(rect.Right, rect.Top),
                        BottomLeft = new Point(rect.Left, rect.Bottom),
                        BottomRight = new Point(rect.Right, rect.Bottom)
                  };
            }

            public static Graphics CreateGraphics(this Image image)
            {
                  return Graphics.FromImage(image);
            }
      }
}
