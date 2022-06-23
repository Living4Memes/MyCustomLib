using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MyCustomLib.GraphicFunctions;
using System.Threading.Tasks;
using System;

using MyCustomLib;

namespace MyCustomLib.Controls
{
      public abstract class CustomControl : Control
      {
            #region === Переопределение делегатов событий ===
            public new event EventHandler Click
            {
                  add
                  {
                        base.Click += value;
                        foreach (Control control in Controls)
                              control.Click += value;
                  }
                  remove
                  {
                        base.Click -= value;
                        foreach (Control control in Controls)
                              control.Click -= value;
                  }
            }

            public new event EventHandler DoubleClick
            {
                  add
                  {
                        base.DoubleClick += value;
                        foreach (Control control in Controls)
                              control.DoubleClick += value;
                  }
                  remove
                  {
                        base.DoubleClick -= value;
                        foreach (Control control in Controls)
                              control.DoubleClick -= value;
                  }
            }

            public new event MouseEventHandler MouseClick
            {
                  add
                  {
                        base.MouseClick += value;
                        foreach (Control control in Controls)
                              control.MouseClick += value;
                  }
                  remove
                  {
                        base.MouseClick -= value;
                        foreach (Control control in Controls)
                              control.MouseClick -= value;
                  }
            }

            public new event EventHandler MouseEnter
            {
                  add
                  {
                        base.MouseEnter += value;
                        foreach (Control control in Controls)
                              control.MouseEnter += value;
                  }
                  remove
                  {
                        base.MouseEnter -= value;
                        foreach (Control control in Controls)
                              control.MouseEnter -= value;
                  }
            }

            public new event EventHandler MouseLeave
            {
                  add
                  {
                        base.MouseLeave += value;
                        foreach (Control control in Controls)
                              control.MouseLeave += value;
                  }
                  remove
                  {
                        base.MouseLeave -= value;
                        foreach (Control control in Controls)
                              control.MouseLeave -= value;
                  }
            }

            public new event EventHandler TextChanged
            {
                  add
                  {
                        base.TextChanged += value;
                        foreach (Control control in Controls)
                              control.TextChanged += value;
                  }
                  remove
                  {
                        base.TextChanged -= value;
                        foreach (Control control in Controls)
                              control.TextChanged -= value;
                  }
            }

            public new event KeyEventHandler KeyDown
            {
                  add
                  {
                        base.KeyDown += value;
                        foreach (Control control in Controls)
                              control.KeyDown += value;
                  }
                  remove
                  {
                        base.KeyDown -= value;
                        foreach (Control control in Controls)
                              control.KeyDown -= value;
                  }
            }

            public new event KeyEventHandler KeyUp
            {
                  add
                  {
                        base.KeyUp += value;
                        foreach (Control control in Controls)
                              control.KeyDown += value;
                  }
                  remove
                  {
                        base.KeyUp -= value;
                        foreach (Control control in Controls)
                              control.KeyUp -= value;
                  }
            }

            public new event KeyPressEventHandler KeyPress
            {
                  add
                  {
                        base.KeyPress += value;
                        foreach (Control control in Controls)
                              control.KeyPress += value;
                  }
                  remove
                  {
                        base.KeyPress -= value;
                        foreach (Control control in Controls)
                              control.KeyPress -= value;
                  }
            }
            #endregion

            public event EmptyEventHandler CustomStyleChanged;
            public event EmptyEventHandler RadiusChanged;
            public event EmptyEventHandler BorderColorChanged;
            public event EmptyEventHandler BorderWidthChanged;
            public event EmptyEventHandler DrawBorderChanged;
            public event EmptyEventHandler BackgroundDraw;

            #region === Свойства и поля ===

            // Стиль контрола
            protected CustomContainerStyle _style = CustomContainerStyle.Rounded;
            // Радиус закругления у кнопки стиля RoundedButton
            protected double _roundedRadius = 20;

            protected StringFormat _stringFormat = new StringFormat();
            // Отслеживание наведения курсора
            protected bool _mouseEntered = false;
            // Цвет границы кнопки
            protected Color _borderColor = Color.Orange;
            // Флаг рисования рамки
            protected bool _drawBorder = true;
            // Ширина границы кнопки
            protected float _borderWidth = 5f;

            [Description("Sets style of the control."), Category("Custom settings")]
            public virtual CustomContainerStyle Style { get => _style; set { _style = value; CustomStyleChanged?.Invoke(); } }
            [Description("Sets turning radius for RoundedControl style."), Category("Custom settings")]
            public virtual double Radius { get => _roundedRadius; set { _roundedRadius = value; RadiusChanged?.Invoke(); } }
            [Description("Sets color of the border."), Category("Custom settings")]
            public virtual Color BorderColor { get => _borderColor; set { _borderColor = value; BorderColorChanged?.Invoke(); } }
            [Description("Sets width of the border."), Category("Custom settings")]
            public virtual float BorderWidth { get => _borderWidth; set { _borderWidth = value; BorderWidthChanged?.Invoke(); } }
            [Description("Shows/hides the border."), Category("Custom settings")]
            public virtual bool DrawBorder { get => _drawBorder; set { _drawBorder = value; DrawBorderChanged?.Invoke(); } }

            #endregion

            // Конструктор
            public CustomControl()
            {
                  DoubleBuffered = true;

                  _stringFormat.Alignment = StringAlignment.Center;
                  _stringFormat.LineAlignment = StringAlignment.Center;

                  CustomStyleChanged += () => OnStyleChanged();
                  RadiusChanged += () => OnRadiusChanged();
                  BorderColorChanged += () => OnBorderColorChanged();
                  BorderWidthChanged += () => OnBorderWidthChanged();
                  DrawBorderChanged += () => OnDrawBorderChanged();
            }

            #region === Обработка событий ===

            protected override void OnPaint(PaintEventArgs e)
            {
                  DrawBody(e.Graphics, ClientRectangle);
            }

            protected virtual void OnStyleChanged() => Invalidate();
            protected virtual void OnRadiusChanged() => Invalidate();
            protected virtual void OnBorderColorChanged() => Invalidate();
            protected virtual void OnBorderWidthChanged() => Invalidate();
            protected virtual void OnDrawBorderChanged() => Invalidate();

            #endregion

            protected virtual GraphicsPath GetGPath(Rectangle rect, CustomContainerStyle containerStyle)
            {
                  CustomContainerProperties properties = new CustomContainerProperties()
                  {
                        ClientRectangle = ClientRectangle,
                        ContainerStyle = _style,
                        RoundedRadius = _roundedRadius
                  };

                  switch(containerStyle)
                  {
                        case CustomContainerStyle.Square: return CustomGraphics.GetContainerGraphicsPath(properties as ISquareContainer);
                        case CustomContainerStyle.Rounded: return CustomGraphics.GetContainerGraphicsPath(properties as IRoundedContainer);
                        case CustomContainerStyle.Pill: return CustomGraphics.GetContainerGraphicsPath(properties as IPillContainer);
                        default: throw new ArgumentException("Unknown container style! Function: GetGPath.", nameof(containerStyle));
                  }
            }

            protected virtual double FixRadius(double radius)
            {
                  if (Width >= Height)
                        return radius <= Height * 0.5 ? radius : Height * 0.5;
                  else
                        return radius <= Width * 0.5 ? radius : Width * 0.5;
            }

            protected virtual void DrawBody(Graphics g, Rectangle client)
            {
                  Rectangle borderRectangle = client; borderRectangle.Inflate(-1, -1);
                  GraphicsPath body = GetGPath(client, _style);
                  GraphicsPath border = GetGPath(borderRectangle, _style);

                  Region = new Region(body);

                  g.SmoothingMode = SmoothingMode.HighQuality;

                  g.FillPath(new SolidBrush(BackColor), body);

                  if (DrawBorder)
                        AddBorder(g, border);
            }

            protected virtual async void DrawImageAsync(Image image, Rectangle rect) => await Task.Run(() => DrawImage(image, rect));
            protected virtual async void DrawBackgroundImage() => await Task.Run(() => { DrawImage(BackgroundImage, ClientRectangle); BackgroundDraw?.Invoke(); });
            protected virtual async void DrawBackgroundImageAsync() => await Task.Run(DrawBackgroundImage);

            // Синхронное рисование фона формы (картинки)
            protected virtual void DrawImage(Image image, Rectangle rect) => DrawImage(image, rect, CreateGraphics());
            protected virtual void DrawImage(Image image, Rectangle rect, Graphics g)
            {
                  if (image == null)
                        return;

                  g.Clear(BackColor);

                  Bitmap bitmap = new Bitmap(image, rect.Size);

                  g.DrawImage(bitmap, rect.Location);
            }

            protected virtual void AddBorder(Graphics g, GraphicsPath borderPath)
            {
                  g.DrawPath(new Pen(_borderColor, _borderWidth), borderPath);
            }

            #region === Функции конструкции контрола ===

            public GraphicsPath GetPillRectanglePath(Rectangle rect) => CustomGraphics.GetPillRectanglePath(rect);
            public GraphicsPath GetRoundedRectanglePath(Rectangle rect, double radius) => CustomGraphics.GetRoundedRectanglePath(rect, radius);
            public GraphicsPath GetRectanglePath(Rectangle rect) => CustomGraphics.GetRectanglePath(rect);
            public void DrawText(Graphics graph, Rectangle rect) => CustomGraphics.DrawText(Text, Font, graph, rect);

            #endregion
      }
}
