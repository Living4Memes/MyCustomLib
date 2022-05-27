using System.Drawing;

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

      public enum CustomControlStyle
      {
            Pill,
            Rounded,
            Square
      }
}
