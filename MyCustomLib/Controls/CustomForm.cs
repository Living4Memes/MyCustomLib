using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using MyCustomLib.GraphicFunctions;

namespace MyCustomLib.Controls
{

      public enum CustomFormBorderStyle
      {
            Sizable,
            None,
            Fixed,
            FixedDialog
      }

      [DesignerCategory("Form")]
      //Основной  класс формы
      public class CustomForm : Form
      {
            public event EventHandler BackgroundDraw;

            // Рамка управления формой
            private MyFormHeader _header = new MyFormHeader();

            #region === Свойства и поля ===

            private const double TRANSITION_STEP = 0.02;
            // Стиль кастомной рамки
            private CustomFormBorderStyle _borderStyle = CustomFormBorderStyle.Sizable;
            // Флаг для отслеживания ресайза (при применении ресайза становится true). Для предотвращения перерисовки фона.
            private bool _beingResized = false;
            // Флаг-костыль для предотвращения двойной прорисовки фона
            private bool _firstPaintSkipped = false;
            // Предыдущий размер при ресайзе
            private Size _previousSize = new Size(0, 0);
            // Фоновое изображение
            private Image _backgroundImage = null;
            // Цвет затемнения фонового изображения
            private Color _blackoutColor = Colors.FormBackgroundColor;
            // Интенсивность затемнения фонового изображения
            private int _blackoutAlpha = 200;
            // Высота кастомной рамки
            private int _headerHeight = 25;
            // Иконка приложения
            private Icon _icon = null;

            // Переопределения Icon формы
            public new Icon Icon { get => _icon; set { _icon = value; UpdateHeaderIcon(); } }
            // Переопределение фонового изображения формы
            public new Image BackgroundImage { get => _backgroundImage; set { _backgroundImage = value; Invalidate(); } }
            // Переопределение стиля растягивания формы на Stretch
            public new ImageLayout BackgroundImageLayout { get => ImageLayout.Stretch; }

            [Description("Enables transition of the window on startup."), Category("Custom settings")]
            public bool Transition { get; set; } = false;
            [Description("Sets the transition time in milliseconds for the window to appear."), Category("Custom settings")]
            public ushort TransitionDelay { get; set; } = 100;
            [Description("Sets height of the custom header."), Category("Custom settings")]
            public int HeaderHeight { get => _headerHeight; set { _headerHeight = value; UpdateHeaderHeight(); } }
            [Description("Blackout color that is overlaid on top of the image."), Category("Custom settings")]
            public Color BlackoutColor { get => _blackoutColor; set { _blackoutColor = value; Invalidate(); } }
            [Description("Sets alpha of the blackout."), Category("Custom settings")]
            public int BlackoutAlpha { get => _blackoutAlpha; set { _blackoutAlpha = value <= 255 ? value : 255; Invalidate(); } }
            [Description("Custom header styles."), Category("Custom settings")]
            public CustomFormBorderStyle CustomFormBorderStyle { get => _borderStyle; set { _borderStyle = value; UpdateBorderStyle(); } }

            #endregion

            // Конструктор формы
            public CustomForm()
            {
                  DoubleBuffered = true;
                  AllowTransparency = true;
                  AutoScaleMode = AutoScaleMode.None;
                  StartPosition = FormStartPosition.CenterScreen;
                  FormBorderStyle = FormBorderStyle.None;
                  CustomFormBorderStyle = CustomFormBorderStyle.Sizable;
                  BackColor = Colors.FormBackgroundColor;
                  Font = new Font("Century Gothic", 10f, FontStyle.Bold);
                  BlackoutColor = Colors.FormBackgroundColor;
                  BlackoutAlpha = 200;

                  MinimumSize = new Size(100, 100);
                  Opacity = 0;

                  Initialize();
            }

            #region === Обработка событий ===

            // Обработка события Show
            protected override void OnShown(EventArgs e)
            {
                  if (!Transition)
                  {
                        Opacity = 1;
                        return;
                  }

                  Opacity = 0;

                  DoTransition();
            }

            //Обработка события Load
            protected override void OnLoad(EventArgs e)
            {
                  base.OnLoad(e);
                  ResumeLayout(false);
            }

            // Обработка события HandleCreated
            protected override void OnHandleCreated(EventArgs e)
            {
                  SuspendLayout();
                  base.OnHandleCreated(e);
            }

            // Обработка события Paint
            protected override void OnPaint(PaintEventArgs e)
            {
                  if (!_firstPaintSkipped && _borderStyle != CustomFormBorderStyle.None)
                  {
                        _firstPaintSkipped = true;
                        return;
                  }

                  base.OnPaint(e);

                  Graphics g = e.Graphics;
                  Rectangle clientRectangle = ClientRectangle;

                  if (_borderStyle != CustomFormBorderStyle.None)
                        clientRectangle.Intersect(new Rectangle(_header.Left, _header.Bottom - 3, Width - 1, Height - _header.Height + 1));

                  g.Clear(BackColor);

                  if (BackgroundImage != null && !_beingResized) DrawBackgroundAsync();

                  if (_borderStyle != CustomFormBorderStyle.None)
                        g.DrawRectangle(new Pen(Colors.FormHeaderColor, 5f), clientRectangle);
            }

            // Обработка события FontChanged
            protected override void OnFontChanged(EventArgs e)
            {
                  AutoScaleMode = AutoScaleMode.None;
                  _header.Font = Font;
            }

            // Обработка события TextChanged
            protected override void OnTextChanged(EventArgs e)
            {
                  _header.Text = Text;
            }

            #endregion

            // Асинхронное рисование фона формы (картинки)
            private async void DrawBackgroundAsync() => await Task.Run(DrawBackground);

            // Синхронное рисование фона формы (картинки)
            private void DrawBackground()
            {
                  BackgroundDraw?.Invoke(this, null);

                  if (BackgroundImage == null)
                        return;

                  Graphics g = CreateGraphics();

                  g.Clear(BackColor);

                  int interval = _borderStyle == CustomFormBorderStyle.None ? 0 : _header.Height;

                  Bitmap bitmap = new Bitmap(BackgroundImage, new Size(Width, Height - interval));

                  if (BlackoutAlpha > 0)
                        CustomGraphics.AddBlackout(bitmap, BlackoutColor, BlackoutAlpha);

                  g.DrawImage(bitmap, 0, interval);
            }

            // Обновление стиля рамки формы
            private void UpdateBorderStyle()
            {
                  if (_borderStyle == CustomFormBorderStyle.None)
                        Controls.Remove(_header);
                  else
                  {
                        if (!Controls.Contains(_header))
                              Controls.Add(_header);

                        _header.BorderStyle = _borderStyle;
                  }

                  DrawBackgroundAsync();
            }

            // Обновление высоты рамки формы
            private void UpdateHeaderHeight()
            {
                  _header.Height = _headerHeight;
            }

            // Обновление высоты заголовка формы
            private void UpdateHeaderIcon()
            {
                  _header.Icon = _icon;
            }

            // Функция плавного появления формы
            private async void DoTransition()
            {
                  int delay = (int)(TransitionDelay * TRANSITION_STEP / 2);
                  while (Opacity != 1)
                  {
                        Opacity += TRANSITION_STEP;
                        await Task.Delay(delay);
                  }
            }

            // Первичное присвоение базовых свойств элементам формы
            private void Initialize()
            {
                  _header.Parent = this;
                  _header.Height = 25;
                  _header.Dock = DockStyle.Top;
                  _header.BackColor = Colors.FormHeaderColor;
                  _header.Text = "Header text";

                  LoadEvents();
            }

            // Обработка основных событий
            private void LoadEvents()
            {
                  ResizeBegin += (s, e) => { _beingResized = true; _previousSize = Size; };
                  ResizeEnd += (s, e) =>
                  {
                        _beingResized = false;
                        if (_previousSize != Size)
                        {
                              _previousSize = Size;
                              DrawBackgroundAsync();
                        }
                  };

                  ClientSizeChanged += (s, e) =>
                  {
                        Invalidate();
                  };
            }

            #region === Обработка изменения размера ===

            private const int
                HTLEFT = 10,
                HTRIGHT = 11,
                HTTOP = 12,
                HTTOPLEFT = 13,
                HTTOPRIGHT = 14,
                HTBOTTOM = 15,
                HTBOTTOMLEFT = 16,
                HTBOTTOMRIGHT = 17;

            private const int AreaIndent = 10;

            private Rectangle TopArea { get { return new Rectangle(0, 0, this.ClientSize.Width, AreaIndent); } }
            private Rectangle LeftArea { get { return new Rectangle(0, 0, AreaIndent, this.ClientSize.Height); } }
            private Rectangle BottomArea { get { return new Rectangle(0, this.ClientSize.Height - AreaIndent, this.ClientSize.Width, AreaIndent); } }
            private Rectangle RightArea { get { return new Rectangle(this.ClientSize.Width - AreaIndent, 0, AreaIndent, this.ClientSize.Height); } }

            private Rectangle TopLeftArea { get { return new Rectangle(0, 0, AreaIndent, AreaIndent); } }
            private Rectangle TopRightArea { get { return new Rectangle(this.ClientSize.Width - AreaIndent, 0, AreaIndent, AreaIndent); } }
            private Rectangle BottomLeftArea { get { return new Rectangle(0, this.ClientSize.Height - AreaIndent, AreaIndent, AreaIndent); } }
            private Rectangle BottomRightArea { get { return new Rectangle(this.ClientSize.Width - AreaIndent, this.ClientSize.Height - AreaIndent, AreaIndent, AreaIndent); } }


            protected override void WndProc(ref Message message)
            {
                  base.WndProc(ref message);

                  if (CustomFormBorderStyle == CustomFormBorderStyle.Sizable && message.Msg == 0x84) // WM_NCHITTEST
                  {
                        var cursor = this.PointToClient(Cursor.Position);

                        if (TopLeftArea.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                        else if (TopRightArea.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                        //else if (BottomLeftArea.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                        else if (BottomRightArea.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                        else if (TopArea.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                        //else if (LeftArea.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                        else if (RightArea.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                        else if (BottomArea.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
                  }
            }

            #endregion
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      [Browsable(false)]
      // Рамка формы
      internal class MyFormHeader : Panel
      {
            private const float MAX_FONT_SIZE = 25f;

            #region === События ===

            public event MouseEventHandler MinimizeMouseClick;
            public event MouseEventHandler MaximizeMouseClick;
            public event MouseEventHandler CloseMouseClick;

            public event MouseEventHandler IconMouseDown;
            public event MouseEventHandler TitleMouseDown;

            public event MouseEventHandler IconMouseDoubleClick;
            public event MouseEventHandler TitleMouseDoubleClick;

            public event MouseEventHandler IconMouseMove;
            public event MouseEventHandler TitleIconMouseMove;

            #endregion

            #region === Свойства и поля ===

            private Icon icon;
            private Label title;
            private PictureBox iconBox;
            private PictureBox closeBar;
            private PictureBox maximizeBar;
            private PictureBox minimizeBar;
            private CustomFormBorderStyle borderStyle = CustomFormBorderStyle.Sizable;
            private FormWindowState previousWindowState = FormWindowState.Normal;

            public Icon Icon { get => icon; set { icon = value; iconBox.Image = icon?.ToBitmap(); } }
            public new string Text { get => title.Text; set => title.Text = value; }
            public new CustomFormBorderStyle BorderStyle { get => borderStyle; set { borderStyle = value; UpdateBorderStyle(); } }

            #endregion

            public MyFormHeader() : base()
            {
                  DoubleBuffered = true;

                  iconBox = new PictureBox(); iconBox.Name = "FormIcon";
                  title = new Label(); title.Name = "HeaderTitle";
                  closeBar = new PictureBox(); closeBar.Name = "CloseBar";
                  maximizeBar = new PictureBox(); maximizeBar.Name = "MaximizeBar";
                  minimizeBar = new PictureBox(); minimizeBar.Name = "MinimizeBar";
                  Font = new Font("Century Gothic", 10f, FontStyle.Bold);

                  Initialize();
            }

            #region === Обработка событий ===

            protected override void OnFontChanged(EventArgs e)
            {
                  if (Font.Size > MAX_FONT_SIZE)
                  {
                        title.Font = new Font(Font.FontFamily, 30f, Font.Style);
                        return;
                  }
                  else
                        title.Font = Font;

                  int newHeight = Convert.ToInt32(title.CreateGraphics().MeasureString(title.Text, title.Font).Height);

                  Height = newHeight < 25 ? 25 : newHeight;
                  //Height = Height > newHeight + 10 ? newHeight : Height;

                  if (FindForm() != null)
                        UpdateIcons();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  base.OnSizeChanged(e);
                  if (FindForm() != null)
                        OnFontChanged(null);
            }

            #endregion

            private void UpdateBorderStyle()
            {
                  if (borderStyle == CustomFormBorderStyle.None)
                        return;

                  Controls.Clear();

                  Controls.Add(closeBar);
                  Controls.Add(title);
                  Controls.Add(iconBox);

                  if (borderStyle != CustomFormBorderStyle.Fixed && borderStyle != CustomFormBorderStyle.FixedDialog)
                        Controls.Add(maximizeBar);
                  if (borderStyle != CustomFormBorderStyle.FixedDialog)
                        Controls.Add(minimizeBar);

                  closeBar?.BringToFront();
                  maximizeBar?.BringToFront();
                  minimizeBar?.BringToFront();
                  iconBox?.BringToFront();
            }

            private void UpdateIcons()
            {
                  iconBox.Size = new Size(Height, Height);
                  closeBar.Size = new Size(Height, Height);
                  maximizeBar.Size = new Size(Height, Height); SwitchMaxMinButton(FindForm().WindowState);
                  minimizeBar.Size = new Size(Height, Height);
            }

            public void SwitchMaxMinButton(FormWindowState ws)
            {
                  switch (ws)
                  {
                        case FormWindowState.Normal:
                              {
                                    if (previousWindowState != FormWindowState.Maximized)
                                          return;
                                    maximizeBar.Image = new Bitmap(MyCustomLib.Properties.Resources.MaximizeBarCompress, Height, Height);
                                    previousWindowState = FormWindowState.Maximized;
                                    break;
                              }
                        case FormWindowState.Maximized:
                              {
                                    if (previousWindowState != FormWindowState.Minimized)
                                          return;
                                    maximizeBar.Image = new Bitmap(MyCustomLib.Properties.Resources.MaximizeBarEnlarge, Height, Height);
                                    previousWindowState = FormWindowState.Normal;
                                    break;
                              }
                  }
            }

            private void ChangeWindowState()
            {
                  Form parentForm = FindForm();

                  switch (parentForm.WindowState)
                  {
                        case FormWindowState.Normal:
                              {
                                    parentForm.WindowState = FormWindowState.Maximized;
                                    maximizeBar.Image = new Bitmap(MyCustomLib.Properties.Resources.MaximizeBarCompress, maximizeBar.Size);
                                    break;
                              }
                        case FormWindowState.Maximized:
                              {
                                    parentForm.WindowState = FormWindowState.Normal;
                                    maximizeBar.Image = new Bitmap(MyCustomLib.Properties.Resources.MaximizeBarEnlarge, maximizeBar.Size);
                                    break;
                              }
                  }
            }

            private void Initialize()
            {
                  Height = 25;
                  Dock = DockStyle.Top;
                  BackColor = Colors.FormHeaderColor;
                  title.Text = "Header text";

                  closeBar.Size = new Size(Height, Height);
                  maximizeBar.Size = new Size(Height, Height);
                  minimizeBar.Size = new Size(Height, Height);

                  closeBar.SizeMode = PictureBoxSizeMode.Zoom;
                  maximizeBar.SizeMode = PictureBoxSizeMode.CenterImage;
                  minimizeBar.SizeMode = PictureBoxSizeMode.Zoom;

                  closeBar.Image = MyCustomLib.Properties.Resources.CloseBar;
                  maximizeBar.Image = new Bitmap(MyCustomLib.Properties.Resources.MaximizeBarEnlarge, Height, Height);
                  minimizeBar.Image = MyCustomLib.Properties.Resources.MinimizeBar;

                  closeBar.Parent = this;
                  maximizeBar.Parent = this;
                  minimizeBar.Parent = this;

                  closeBar.Dock = DockStyle.Right;
                  maximizeBar.Dock = DockStyle.Right;
                  minimizeBar.Dock = DockStyle.Right;

                  title.Parent = this;
                  title.Dock = DockStyle.Fill;
                  title.TextAlign = ContentAlignment.MiddleCenter;
                  title.Font = Font;
                  title.BackColor = Color.Transparent;
                  title.ForeColor = Colors.FormFontColor;

                  iconBox.Parent = this;
                  iconBox.Dock = DockStyle.Left;
                  iconBox.SizeMode = PictureBoxSizeMode.Zoom;
                  iconBox.Width = Height;
                  iconBox.BackColor = Color.Transparent;

                  LoadEvents();
                  OnFontChanged(null);
            }

            private void LoadEvents()
            {
                  MouseMove += (s, e) =>
                  {
                        if (!Capture)
                              return;
                        else
                              Capture = false;

                        Message m = Message.Create(Parent.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                        this.WndProc(ref m);
                  };
                  MouseDoubleClick += (s, e) =>
                  {
                        ChangeWindowState();
                  };

                  closeBar.MouseEnter += (s, e) =>
                  {
                        closeBar.BackColor = Color.FromArgb(80, Color.Red);
                  };
                  closeBar.MouseLeave += (s, e) =>
                  {
                        closeBar.BackColor = Color.Transparent;
                  };
                  closeBar.MouseClick += (s, e) =>
                  {
                        CloseMouseClick?.Invoke(s, e);
                        FindForm().Close();
                  };

                  maximizeBar.MouseEnter += (s, e) =>
                  {
                        maximizeBar.BackColor = Color.FromArgb(80, Color.White);
                  };
                  maximizeBar.MouseLeave += (s, e) =>
                  {
                        maximizeBar.BackColor = Color.Transparent;
                  };
                  maximizeBar.MouseClick += (s, e) =>
                  {
                        MaximizeMouseClick?.Invoke(s, e);
                        ChangeWindowState();
                  };

                  minimizeBar.MouseEnter += (s, e) =>
                  {
                        minimizeBar.BackColor = Color.FromArgb(80, Color.White);
                  };
                  minimizeBar.MouseLeave += (s, e) =>
                  {
                        minimizeBar.BackColor = Color.Transparent;
                  };
                  minimizeBar.MouseClick += (s, e) =>
                  {
                        MinimizeMouseClick?.Invoke(s, e);
                        FindForm().WindowState = FormWindowState.Minimized;
                  };

                  title.MouseDown += (s, e) =>
                  {
                        OnMouseDown(e);
                        TitleMouseDown?.Invoke(s, e);
                  };
                  title.MouseMove += (s, e) =>
                  {
                        TitleIconMouseMove?.Invoke(s, e);
                        if (!title.Capture)
                              return;
                        else
                              title.Capture = false;

                        Message m = Message.Create(Parent.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                        this.WndProc(ref m);
                  };
                  title.MouseDoubleClick += (s, e) =>
                  {
                        TitleMouseDoubleClick?.Invoke(s, e);
                        ChangeWindowState();
                  };

                  iconBox.MouseDown += (s, e) =>
                  {
                        IconMouseDown?.Invoke(s, e);
                        OnMouseDown(e);
                  };
                  iconBox.MouseMove += (s, e) =>
                  {
                        IconMouseMove?.Invoke(s, e);
                        if (!iconBox.Capture)
                              return;
                        else
                              iconBox.Capture = false;

                        Message m = Message.Create(Parent.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                        this.WndProc(ref m);
                  };
                  iconBox.MouseDoubleClick += (s, e) =>
                  {
                        IconMouseDoubleClick?.Invoke(s, e);
                        ChangeWindowState();
                  };
            }
      }

}
