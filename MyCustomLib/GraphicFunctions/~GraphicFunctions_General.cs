using MyCustomLib.Controls;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyCustomLib.GraphicFunctions
{
      /// <summary>
      /// Содержит координаты всех точек <see cref="Rectangle"/>
      /// </summary>
      public struct RectanglePoints
      {
            /// <summary>
            /// Координаты левого верхнего угла <see cref="Rectangle"/>
            /// </summary>
            public Point TopLeft { get; set; }
            /// <summary>
            /// Координаты правого верхнего угла <see cref="Rectangle"/>
            /// </summary>
            public Point TopRight { get; set; }
            /// <summary>
            /// Координаты нижнего правого угла <see cref="Rectangle"/>
            /// </summary>
            public Point BottomRight { get; set; }
            /// <summary>
            /// Координаты нижнего левого угла <see cref="Rectangle"/>
            /// </summary>
            public Point BottomLeft { get; set; }

            /// <summary>
            /// Инициализирует структуру <see cref="RectanglePoints"/> на основе исходного <see cref="Rectangle"/>
            /// </summary>
            /// <param name="rect">Исходный <see cref="Rectangle"/></param>
            public RectanglePoints(Rectangle rect)
            {
                  this = rect.GetAllPoints();
            }
      }

      /// <summary>
      /// Закругленный <see cref="Rectangle"/>
      /// </summary>
      public struct RoundedRectangle
      {
            /// <summary>
            /// <see cref="Rectangle"/>, на основе которого строится <see cref="RoundedRectangle"/>
            /// </summary>
            public Rectangle MainRectangle { get; set; }
            /// <summary>
            /// Радиус закругления по краям <see cref="MainRectangle"/>
            /// </summary>
            public double Radius { get; set; }
            /// <summary>
            /// Построенная <see cref="GraphicsPath"/> на основе параметров <see cref="RoundedRectangle"/>
            /// </summary>
            public GraphicsPath GraphicsPath { get => CustomGraphics.GetRoundedRectanglePath(MainRectangle, Radius); }

            /// <summary>
            /// Инициализирует структуру <see cref="RoundedRectangle"/> на основе исходного <see cref="Rectangle"/>
            /// </summary>
            /// <param name="rect">Исходный <see cref="Rectangle"/></param>
            /// <param name="radius">Радиус закругления по краям</param>
            public RoundedRectangle(Rectangle rect, double radius = 20)
            {
                  MainRectangle = rect;
                  Radius = radius;
            }
      }

      #region === Настройки EnhancedImage ===

      /// <summary>
      /// Настройки <see cref="EnhancedImage"/>
      /// </summary>
      public struct EnhancedImageProperties : IEnhancedPlainImageWithBorder, IEnhancedImageWithShadow, IEnhancedImageWithShadowAndBorder, IEnhancedPlainImage
      {
            /// <summary>
            /// Исходное изображение
            /// </summary>
            public Image PlainImage { get; set; }
            /// <summary>
            /// Настройки кастомного контейнера, в который вставляется изображение
            /// </summary>
            public CustomContainerProperties ContainerProperties { get; set; }
            /// <summary>
            /// Настройки тени по краям изображения
            /// </summary>
            public ShadowProperties ShadowProperties { get; set; }
            /// <summary>
            /// Настройки рамки вокруг изображения
            /// </summary>
            public BorderProperties BorderProperties { get; set; }
      }

      /// <summary>
      /// Интерфейс, реализующий настройки <see cref="EnhancedImage"/> с тенью и рамкой
      /// </summary>
      public interface IEnhancedImageWithShadowAndBorder : IEnhancedImageWithShadow
      {
            BorderProperties BorderProperties { get; set; }
      }

      /// <summary>
      /// Интерфейс, реализующий настройки <see cref="EnhancedImage"/> с рамкой
      /// </summary>
      public interface IEnhancedPlainImageWithBorder : IEnhancedPlainImage
      {
            BorderProperties BorderProperties { get; set; }
      }

      /// <summary>
      /// Интерфейс, реализующий настройки <see cref="EnhancedImage"/> с тенью
      /// </summary>
      public interface IEnhancedImageWithShadow : IEnhancedPlainImage
      {
            ShadowProperties ShadowProperties { get; set; }
      }

      /// <summary>
      /// Интерфейс, реализующий настройки <see cref="EnhancedImage"/> без эффектов
      /// </summary>
      public interface IEnhancedPlainImage
      {
            Image PlainImage { get; set; }
            CustomContainerProperties ContainerProperties { get; set; }
      }

      #endregion

      /// <summary>
      /// Настройки тени
      /// </summary>
      public struct ShadowProperties
      {
            /// <summary>
            /// Стиль тени
            /// </summary>
            public ShadowStyle ShadowStyle { get; set; }
            /// <summary>
            /// Начальный цвет градиента
            /// </summary>
            public Color InitialColor { get; set; }
            /// <summary>
            /// Конечный цвет градиента
            /// </summary>
            public Color FiniteColor { get; set; }
            /// <summary>
            /// Длина градиента
            /// </summary>
            public double Range { get; set; }
            /// <summary>
            /// <see cref="Blend"/> градиента
            /// </summary>
            public Blend GradientBlend { get; set; }

            /// <summary>
            /// Инициализирует структуру <see cref="ShadowProperties"/> на основе <see cref="ShadowStyle"/>
            /// </summary>
            /// <param name="shadowStyle">Стиль тени</param>
            public ShadowProperties(ShadowStyle shadowStyle)
            {
                  ShadowStyle = shadowStyle;
                  InitialColor = Color.Black;
                  FiniteColor = Color.Transparent;
                  Range = 0.15;
                  GradientBlend = new Blend(2)
                  {
                        Factors = new float[] { 0.0f, 1.0f },
                        Positions = new float[] { 0.0f, 1.0f }
                  };
            }

            /// <summary>
            /// Инициализирует структуру на основе значений по всем полям
            /// </summary>
            /// <param name="shadowStyle">Стиль тени</param>
            /// <param name="initialColor">Начальный цвет градиента</param>
            /// <param name="finiteColor">Конечный цвет градиента</param>
            /// <param name="range">Длина градиента</param>
            /// <param name="gradientBlend"><see cref="Blend"/> градиента</param>
            public ShadowProperties(ShadowStyle shadowStyle, Color initialColor, Color finiteColor, double range, Blend gradientBlend)
            {
                  ShadowStyle = shadowStyle;
                  InitialColor = initialColor;
                  FiniteColor = finiteColor;
                  Range = range;
                  GradientBlend = gradientBlend;
            }
      }

      /// <summary>
      /// Настройки рамки
      /// </summary>
      public struct BorderProperties
      {
            /// <summary>
            /// Цвет границы
            /// </summary>
            public Color BorderColor { get; set; }
            /// <summary>
            /// Толщина границы
            /// </summary>
            public float BorderWidth { get; set; }
      }

      /// <summary>
      /// Стиль тени
      /// </summary>
      public enum ShadowStyle
      {
            /// <summary>
            /// По всему периметру
            /// </summary>
            Full,
            /// <summary>
            /// Справа и слева
            /// </summary>
            Vertical,
            /// <summary>
            /// Снизу и сверху
            /// </summary>
            Horizontal,
            /// <summary>
            /// Сверху
            /// </summary>
            Top,
            /// <summary>
            /// Снизу
            /// </summary>
            Bottom,
            /// <summary>
            /// Слева
            /// </summary>
            Left,
            /// <summary>
            /// Справа
            /// </summary>
            Right
      }

      public static class Extensions
      {
            /// <summary>
            /// Получение <see cref="Rectangle"/> из изображения
            /// </summary>
            /// <param name="image">Исходное изображение</param>
            /// <returns><see cref="Rectangle"/></returns>
            public static Rectangle GetRectangle(this Image image)
            {
                  return new Rectangle(0, 0, image.Width, image.Height);
            }

            /// <summary>
            /// Получение <see cref="RectanglePoints"/> из исходного <see cref="Rectangle"/>
            /// </summary>
            /// <param name="rect">Исходный <see cref="Rectangle"/></param>
            /// <returns><see cref="RectanglePoints"/></returns>
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

            /// <summary>
            /// Получение <see cref="Graphics"/> из исходного изображения
            /// </summary>
            /// <param name="image">Исходное изображение</param>
            /// <returns><see cref="Graphics"/> исходного изображения</returns>
            public static Graphics CreateGraphics(this Image image)
            {
                  return Graphics.FromImage(image);
            }
      }
}
