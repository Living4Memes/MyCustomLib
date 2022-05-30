using MyCustomLib.GraphicFunctions;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyCustomLib.Controls
{
      internal static class Colors
      {
            public static Color FormHeaderColor = Color.FromArgb(29, 34, 43);
            public static Color FormFontColor = Color.FromArgb(182, 182, 182);
            public static Color FormBackgroundColor = Color.FromArgb(38, 44, 58);
            public static Color MenuBackgroundColor = Color.FromArgb(49, 54, 63);
            public static Color MenuButtonBackgroundColor = MenuBackgroundColor;
            public static Color MenuFontColor = FormFontColor;
            public static Color ButtonColor = Color.FromArgb(55, 61, 70);
            public static Color ButtonMouseEnterColor = Color.FromArgb(210, 137, 45);
            public static Color ButtonFontColor = Color.White;
            public static Color TextBoxColor = Color.FromArgb(49, 54, 64);
            public static Color TextBoxFontColor = FormFontColor;
            public static Color PosterColor = FormBackgroundColor;
            public static Color PosterFontColor = FormFontColor;
            public static Color SliderColor = FormBackgroundColor;
            public static Color SliderFontColor = FormFontColor;
            public static Color SliderArrowColor = Color.LightGray;
            public static Color ProgressBarBackColor = Color.DarkGray;
            public static Color ProgressBarProgressColor = Color.FromArgb(83, 99, 115);
            public static Color ProgressBarBorderColor = Color.White;
            public static Color ListBoxColor = FormBackgroundColor;
            public static Color ListBoxFontColor = FormFontColor;
            public static Color ListBoxChoosenItemColor = Color.LightGray;
      }

      public enum CustomContainerStyle
      {
            Pill,
            Rounded,
            Square
      }

      public struct CustomContainerProperties : ISquareContainer, IRoundedContainer, IPillContainer
      {
            private double _roundedRadius;

            public Rectangle ClientRectangle { get; set; }
            public CustomContainerStyle ContainerStyle { get; set; }
            public GraphicsPath ClientPath { get => GetGPath(); }
            public double RoundedRadius { get => _roundedRadius; set => _roundedRadius = FixRadius(value); }

            private double FixRadius(double radius)
            {
                  if (ClientRectangle.Width >= ClientRectangle.Height)
                        return radius <= ClientRectangle.Height * 0.5 ? radius : ClientRectangle.Height * 0.5;
                  else
                        return radius <= ClientRectangle.Width * 0.5 ? radius : ClientRectangle.Width * 0.5;
            }

            private GraphicsPath GetGPath()
            {
                  switch (ContainerStyle)
                  {
                        case CustomContainerStyle.Square: return CustomGraphics.GetContainerGraphicsPath(this as ISquareContainer);
                        case CustomContainerStyle.Rounded: return CustomGraphics.GetContainerGraphicsPath(this as IRoundedContainer);
                        case CustomContainerStyle.Pill: return CustomGraphics.GetContainerGraphicsPath(this as IPillContainer);
                        default: throw new ArgumentException("Unknown container style! Function: GetGPath.", nameof(ContainerStyle));
                  }
            }
      }
      
      public interface ISquareContainer
      {
            Rectangle ClientRectangle { get; set; }
      }

      public interface IRoundedContainer : ISquareContainer
      {
            double RoundedRadius { get; set; }
      }

      public interface IPillContainer : ISquareContainer 
      {
            
      }


}
