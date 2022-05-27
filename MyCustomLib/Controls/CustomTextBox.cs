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

namespace MyCustomLib.Controls
{
      // Кастомный TextBox =====================================================
      public class CustomTextBox : CustomControl
      {
            public event EmptyEventHandler VerticalIndentChanged;
            public event EmptyEventHandler HorizontalIndentChanged;

            #region === Свойства и поля ===
            // Основной TextBox для написания
            private RichTextBox _textBox = new RichTextBox();
            // Вертикальный отступ
            private int _verticalIndent = 5;
            // Горизонтальный отступ
            private int _horizontalIndent = 15;

            public override CustomControlStyle Style { get => CustomControlStyle.Pill; }

            [Description("Vertical indent between text and top/bottom borders."), Category("Custom settings")]
            public int VerticalIndent { get => _verticalIndent; set { _verticalIndent = value; VerticalIndentChanged?.Invoke(); } }
            [Description("Horizontal indent between text and left/right borders"), Category("Custom settings")]
            public int HorizontalIndent { get => _horizontalIndent; set { _horizontalIndent = value; HorizontalIndentChanged?.Invoke(); } }

            #endregion

            // Конструктор
            public CustomTextBox() : base()
            {
                  Size = new Size(300, 40);
                  _stringFormat.Alignment = StringAlignment.Near;

                  _style = CustomControlStyle.Pill;
                  BackColor = Colors.TextBoxColor;
                  ForeColor = Colors.TextBoxFontColor;

                  VerticalIndentChanged += () => OnVerticalIndentChanged();
                  HorizontalIndentChanged += () => OnHorizontalIndentChanged();

                  Initalize();
            }

            #region === Обработка событий ===
            protected override void OnMouseEnter(EventArgs e)
            {
                  Cursor = Cursors.IBeam;
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                  Cursor = Cursors.Arrow;
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                  _textBox.Focus();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  if (Width > _horizontalIndent * 2 + 10)
                        UpdateSizeAndLocation();
                  else
                        Width = _horizontalIndent * 2 + 10;
            }

            protected override void OnTextChanged(EventArgs e)
            {
                  _textBox.Text = Text;
            }

            protected override void OnFontChanged(EventArgs e)
            {
                  _textBox.Font = Font;
                  UpdateSizeAndLocation();
            }

            protected override void OnRadiusChanged()
            {
                  _horizontalIndent = (int)Radius;
                  base.OnRadiusChanged();
            }

            protected override void OnBackColorChanged(EventArgs e)
            {
                  base.OnBackColorChanged(e);
                  _textBox.BackColor = BackColor;
            }

            protected virtual void OnVerticalIndentChanged()
            {
                  _verticalIndent = FixVerticalIndent();
                  UpdateSizeAndLocation();
            }

            protected virtual void OnHorizontalIndentChanged()
            {
                  _horizontalIndent = FixHorizontalIndent();
                  UpdateSizeAndLocation();
            }
            #endregion

            protected virtual void UpdateSizeAndLocation()
            {
                  if (!_textBox.Multiline)
                  {
                        _textBox.Height = Font.Height; // Почему-то не масштабируется автоматически. Низ обрезается

                        int tempHeight = _textBox.Height + _verticalIndent * 2;
                        if (_drawBorder) tempHeight += (int)_borderWidth;
                        Height = tempHeight;
                  }

                  _textBox.Width = Width - _horizontalIndent * 2;
                  _textBox.Location = new Point(_horizontalIndent, _verticalIndent);
            }

            protected virtual int FixHorizontalIndent()
            {
                  if (_horizontalIndent <= (int)_roundedRadius)
                        return (int)_roundedRadius;
                  else if (_horizontalIndent * 2 < Width)
                        return _horizontalIndent;
                  else
                        return Height / 2;
            }

            protected virtual int FixVerticalIndent()
            {
                  if (_verticalIndent * 2 < Height)
                        return _verticalIndent;
                  else
                        return Height - 1 / 2;
            }

            private void Initalize()
            {
                  _textBox.BorderStyle = BorderStyle.None;
                  _textBox.Font = Font;
                  _textBox.ForeColor = ForeColor;
                  _textBox.Text = Text;
                  _textBox.BackColor = BackColor;
                  _textBox.Multiline = false;
                  _textBox.ScrollBars = RichTextBoxScrollBars.None;

                  Controls.Add(_textBox);

                  UpdateSizeAndLocation();
            }

            #region === Дополнительные функции ===
            public void PaintCSSText()
            {
                  DoPainting(@"nth-child\(\d\)", Color.Yellow);
                  DoPainting(@"\.u-carousel:", Color.DarkOrange);
                  DoPainting(@"[0-9]", Color.Magenta);
                  DoPainting(@"\.active:", Color.DarkOrange);
                  DoPainting(@"\.b-image:", Color.DarkOrange);
                  DoPainting("after", Color.Magenta);
                  DoPainting("content: ", Color.RosyBrown);
                  DoPainting(@""" *\w* *\w* * *\w* *\w* * *\w* *\w* * *\w* *\w* * *\w* *\w* * *\w* *\w* * *\w* *\w* *""", Color.LightGreen);
                  _textBox.Invalidate();
            }

            protected void DoPainting(string text, Color color)
            {
                  Regex re = new Regex(text, RegexOptions.IgnoreCase);
                  MatchCollection mc = re.Matches(_textBox.Text);

                  foreach (Match match in mc)
                  {
                        _textBox.Select(match.Index, match.Value.Length);
                        _textBox.SelectionColor = color;
                  }
            }
            #endregion
      }

      /*
      public class ShikiListBox : CustomControl
      {
            private ListBox MainListBox = new ListBox();
            public ShikiListBox() : base()
            {
                  Controls.Add(MainListBox);
                  BackColor = Colors.ListBoxColor;
                  ForeColor = Colors.ListBoxFontColor;
                  SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                  AdjustListBox();
                  LoadEvents();
            }

            private void AdjustListBox()
            {
                  MainListBox.BackColor = Colors.ListBoxColor;
                  MainListBox.ForeColor = Colors.ListBoxFontColor;
                  MainListBox.BorderStyle = BorderStyle.None;
                  MainListBox.Dock = DockStyle.Fill;

                  MainListBox.Items.Add("123");
                  MainListBox.Items.Add("123");
                  MainListBox.Items.Add("123");
                  MainListBox.Items.Add("123");
                  MainListBox.Items.Add("123");

                  MainListBox.SelectedIndex = 3;
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {

            }

            private void LoadEvents()
            {
                  MainListBox.MouseClick += (s, e) => MainListBox.ClearSelected();
            }
      }

      public class ShikiSliderImage : PictureBox
      {
            public Label Label { get; set; } = new Label();

            public ShikiSliderImage() : base()
            {
                  Controls.Add(Label);
                  BackColor = Colors.FormBackgroundColor;
                  Image = MyLib.Properties.Resources.SliderBG;
                  SizeMode = PictureBoxSizeMode.StretchImage;
                  Text = "Picture title";
                  Size = new Size(180, 270);
                  AdjustLabel();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                  base.OnPaint(e);

                  //DrawString(e.Graphics);
                  UpdateLocation();
            }

            protected override void OnTextChanged(EventArgs e)
            {
                  Label.Text = Text;
                  UpdateLocation();
            }

            protected override void OnFontChanged(EventArgs e)
            {
                  Label.Font = Font;
                  UpdateLocation();
            }

            private void AdjustLabel()
            {
                  Label.AutoSize = true;
                  Label.TextAlign = ContentAlignment.MiddleCenter;
                  Label.BackColor = Color.Transparent;
                  Label.ForeColor = Colors.PosterFontColor;
                  Label.Font = Font;
            }

            private void UpdateLocation()
            {
                  Label.Location = new Point(Width / 2 - Label.Width / 2, Height - Label.Height - 5);
            }

      }

      /*
      public class ShikiSlider : CustomControl
      {
            public delegate void Dlg();
            event Dlg ImageIndexChanged;
            event Dlg ImageChanged;

            private PictureBox EffectArrows;
            private PictureBox EffectGradient;

            public List<ShikiSliderImage> Images { get; private set; } = new List<ShikiSliderImage>();
            public int ImageIndex { get; set; } = -1;
            public ShikiSliderImage SliderImage { get; set; } = new ShikiSliderImage();

            public ShikiSlider()
            {
                  EffectArrows = new PictureBox();
                  EffectGradient = new PictureBox();

                  SliderImage.Dock = DockStyle.Fill;
                  this.Controls.Add(SliderImage);
                  AdjustEffects();
                  LoadEvents();
                  Size = new Size(180, 270);
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  base.OnSizeChanged(e);

                  if (EffectArrows != null && EffectGradient != null)
                        DrawEffects();
            }

            private void AdjustEffects()
            {
                  EffectGradient.BackColor = Color.Transparent;
                  EffectGradient.Size = Size;
                  EffectGradient.Dock = DockStyle.Fill;

                  EffectArrows.BackColor = Color.Transparent;
                  EffectArrows.Size = Size;
                  EffectArrows.Dock = DockStyle.Fill;

                  DrawEffects();

                  SliderImage.Controls.Add(EffectGradient);
                  SliderImage.Controls.Add(EffectArrows);
            }

            private void DrawEffects()
            {
                  Bitmap arrowsBM = new Bitmap(Width, Height);
                  Bitmap gradientBM = new Bitmap(Width, Height);

                  DrawArrows(arrowsBM, new Point(0, Height / 2));
                  DrawArrows(gradientBM, new Point(0, Height / 2));

                  DrawVerticalLineGradient(gradientBM);

                  EffectArrows.Image = arrowsBM;
                  EffectGradient.Image = gradientBM;
            }

            private void DrawArrows(Bitmap bitmap, Point where)
            {
                  Graphics graph = Graphics.FromImage(bitmap);

                  int width = 15, height = 30, indent = 15;
                  string path = Path.GetTempFileName();

                  File.WriteAllText(path, MyLib.Properties.Resources.angle_left);
                  SvgDocument document = SvgDocument.Open(path);
                  document.Width = width;
                  document.Height = height;
                  Bitmap left = document.Draw();
                  Point AlignPointL = new Point(where.X + indent, where.Y - left.Height / 2);
                  graph.DrawImage(left, AlignPointL);

                  document = new SvgDocument();

                  File.WriteAllText(path, MyLib.Properties.Resources.angle_right);
                  document = SvgDocument.Open(path);
                  document.Width = width;
                  document.Height = height;
                  Bitmap right = document.Draw();
                  Point AlignPointR = new Point(where.X + bitmap.Width - indent * 2, where.Y - left.Height / 2);
                  graph.DrawImage(right, AlignPointR);
                  File.Delete(path);
            }

            private void LoadEvents()
            {
                  EffectGradient.MouseEnter += (s, e) =>
                  {
                        EffectGradient.Visible = false;
                  };
                  EffectArrows.MouseLeave += (s, e) =>
                  {
                        EffectGradient.Visible = true;
                  };
            }

            private void ChangeImageIndex(int index)
            {
                  ImageIndex = index;
                  SliderImage = Images[ImageIndex];
                  ImageIndexChanged?.Invoke();
            }

            private void ChangeImage(ShikiSliderImage Image)
            {
                  SliderImage = Image;
                  ImageChanged?.Invoke();
            }

            public void AddImage(Image image)
            {
                  ShikiSliderImage newImage = new ShikiSliderImage();
                  newImage.Image = image;
                  Images.Add(newImage);
                  ImageIndex++;
            }
      }

      public class ShikiMenuButton : Panel
      {
            public PictureBox Icon { get; set; } = new PictureBox();
            public Label Label { get; set; } = new Label();
            public int Number { get; set; } = -1;

            private Image IconImage = null;

            public ShikiMenuButton() : base()
            {
                  Controls.Add(Icon);
                  Size = new Size(200, 50);
                  BackColor = Color.Transparent;
                  ForeColor = Colors.MenuFontColor;
                  Icon.SizeMode = PictureBoxSizeMode.Zoom;
                  AdjustLabel();
                  UpdateSizeAndLocation();
                  LoadEvents();
            }

            protected override void OnPaint(PaintEventArgs pe)
            {
                  Label.Invalidate();
                  UpdateSizeAndLocation();
            }

            protected override void OnFontChanged(EventArgs e)
            {
                  Label.Font = Font;
                  Invalidate();
            }

            protected override void OnTextChanged(EventArgs e)
            {
                  Label.Text = Text;
                  Invalidate();
            }

            private void AdjustLabel()
            {
                  Label.BackColor = Colors.MenuButtonBackgroundColor;
                  Label.ForeColor = Colors.PosterFontColor;
                  Controls.Add(Label);
                  Label.AutoSize = false;
                  Label.TextAlign = ContentAlignment.MiddleCenter;
                  Label.Size = new Size(Width - Icon.Width, Height);
                  Label.Text = Text;
            }

            private void UpdateSizeAndLocation()
            {
                  Icon.Size = new Size(Height, Height);
                  Icon.Location = new Point(Right - Icon.Width, 0);
                  Label.Location = new Point((Width - Icon.Width) / 2 - Label.Width / 2, Height / 2 - Label.Height / 2);
            }

            private void LoadEvents()
            {
                  Icon.MouseEnter += (s, e) =>
                  {
                        if (IconImage == null)
                              IconImage = Icon.Image;
                        Bitmap bm = new Bitmap(Icon.Image);
                        Graphics.FromImage(bm).FillRectangle(new SolidBrush(Color.FromArgb(50, Color.LightGray)), new Rectangle(0, 0, bm.Width, bm.Height));
                        Icon.Image = bm;
                  };
                  Icon.MouseLeave += (s, e) =>
                  {
                        Icon.Image = IconImage;
                  };
                  Icon.MouseClick += (s, e) =>
                  {
                        OnMouseClick(e);
                  };
            }
      }

      public class ShikiProgressBar : Control
      {
            public int Minimum { get; set; } = 0;
            public int Maximum { get; set; } = 100;
            public int Value { get { return Val; } set => SetValue(value); }
            public int Step { get { return Stp; } set => SetStep(value); }

            private int Val;
            private int PrevVal;
            private int Stp;
            private bool animationStarted = false;

            public Color ProgressColor { get; set; } = Colors.ProgressBarProgressColor;
            public Color ProgressBorderColor { get; set; } = Colors.ProgressBarBorderColor;

            public ShikiProgressBar() : base()
            {
                  BackColor = Colors.ProgressBarBackColor;
                  Size = new Size(100, 30);
                  Value = 0;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                  base.OnPaint(e);

                  Graphics graph = e.Graphics;

                  graph.DrawRectangle(new Pen(ProgressBorderColor, 1f), new Rectangle(0, 0, Width - 1, Height - 1));

                  if (Val != PrevVal)
                        AnimateProgress();
                  else
                  {
                        graph.DrawRectangle(new Pen(ProgressColor, 1f), new Rectangle(0, 0, Width * Val / Maximum, Height));
                        graph.FillRectangle(new SolidBrush(ProgressColor), new Rectangle(0, 0, Width * Val / Maximum, Height));
                  }
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  base.OnSizeChanged(e);

                  Invalidate();
            }

            public void PerformStep()
            {
                  int temp = Val + Stp;
                  if (temp > Maximum)
                        SetValue(Maximum);
                  else if (temp < Minimum)
                        SetValue(Minimum);
                  else
                        SetValue(temp);
            }

            private void SetValue(int value)
            {
                  PrevVal = Val;
                  if (value <= Maximum && value >= Minimum)
                        Val = value;
                  else if (value > Maximum)
                        Val = Maximum;
                  else if (value < Minimum)
                        Val = Minimum;
                  Invalidate();
            }

            private void SetStep(int value)
            {
                  if (value <= Maximum)
                        Stp = value;
                  else
                        Stp = Maximum;
            }

            private async void AnimateProgress()
            {
                  if (!animationStarted)
                  {
                        animationStarted = true;
                        int counter = PrevVal == 0 ? 1 : PrevVal;
                        while (counter <= Val)
                        {
                              CreateGraphics().FillRectangle(new SolidBrush(ProgressColor), new Rectangle(0, 0, Width * counter / Maximum, Height));
                              counter++;
                              await Task.Delay(new TimeSpan(10000));
                        }
                        PrevVal = Val;
                        animationStarted = false;
                  }
            }
      }


      */
}
