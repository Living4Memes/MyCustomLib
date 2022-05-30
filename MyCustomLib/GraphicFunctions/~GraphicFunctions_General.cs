using System.Drawing;
using System.Drawing.Drawing2D;

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

      #region === Настройки EnhancedImage ===

      public struct EnhancedImageProperties : IEnhancedPlainImageWithBorder, IEnhancedImageWithShadow, IEnhancedImageWithShadowAndBorder, IEnhancedPlainImage
      {
            public Image PlainImage { get; set; }
            public GraphicsPath ImagePath { get; set; }
            public ShadowProperties ShadowProperties { get; set; }
            public BorderProperties BorderProperties { get; set; }
      }

      public interface IEnhancedImageWithShadowAndBorder : IEnhancedImageWithShadow
      {
            BorderProperties BorderProperties { get; set; }
      }

      public interface IEnhancedPlainImageWithBorder : IEnhancedPlainImage
      {
            BorderProperties BorderProperties { get; set; }
      }

      public interface IEnhancedImageWithShadow : IEnhancedPlainImage
      {
            ShadowProperties ShadowProperties { get; set; }
      }

      public interface IEnhancedPlainImage
      {
            Image PlainImage { get; set; }
            GraphicsPath ImagePath { get; set; }
      }

      #endregion

      public struct ShadowProperties
      {
            public ShadowStyle ShadowStyle { get; set; }
            public Color InitialColor { get; set; }
            public Color FiniteColor { get; set; }
            public double Range { get; set; }
            public Blend GradientBlend { get; set; }

            public ShadowProperties(ShadowStyle shadowType)
            {
                  ShadowStyle = shadowType;
                  InitialColor = Color.Black;
                  FiniteColor = Color.Transparent;
                  Range = 0.15;
                  GradientBlend = new Blend(2)
                  {
                        Factors = new float[] { 0.0f, 1.0f },
                        Positions = new float[] { 0.0f, 1.0f }
                  };
            }

            public ShadowProperties(ShadowStyle shadowType, Color initialColor, Color finiteColor, double range, Blend gradientBlend)
            {
                  ShadowStyle = shadowType;
                  InitialColor = initialColor;
                  FiniteColor = finiteColor;
                  Range = range;
                  GradientBlend = gradientBlend;
            }
      }

      public struct BorderProperties
      {
            public Color BorderColor { get; set; }
            public float BorderWidth { get; set; }
      }

      public enum ShadowStyle
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
