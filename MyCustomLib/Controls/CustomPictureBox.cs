using System.Drawing;
using System.Windows.Forms;
using System;
using MyCustomLib.GraphicFunctions;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace MyCustomLib.Controls
{
      public enum ShadowStyle
      {
            None,
            ShadowOnHover,
            ShadowWithoutHover,
            Always
      }

      // Кастомный PictureBox ==================================================
      public class CustomPictureBox : CustomControl
      {
            public event EmptyEventHandler ImageChanged;
            public event EmptyEventHandler HoverStyleChanged;

            #region === Свойства и поля ===
            protected EnhancedImage _enhancedImage;
            protected PictureBox _mainPictureBox = new PictureBox();
            protected ShadowStyle _shadowStyle = ShadowStyle.None;

            [Category("Custom settings"), Description("Sets image of the PictureBox.")]
            public Image Image { get => _enhancedImage.PlainImage; set { SetImage(value); ImageChanged?.Invoke(); } }
            [Category("Custom settings"), Description("Sets hover style of the picturebox.")]
            public ShadowStyle HoverShadowStyle { get => _shadowStyle; set { _shadowStyle = value; HoverStyleChanged?.Invoke(); } }
            #endregion

            public CustomPictureBox() : base()
            {
                  Size = new Size(300, 200);
                  _drawBorder = false;
                  BackgroundImage = Properties.Resources.PosterBG;
                  BackgroundImageLayout = ImageLayout.Stretch;

                  ImageChanged += OnImageChanged;
                  HoverStyleChanged += OnHoverStyleChanged;

                  Initialize();
            }

            #region === Обработка событий ===

            protected override void OnPaint(PaintEventArgs e)
            {
                  DrawBody(e.Graphics, ClientRectangle);
            }

            protected override void OnLayout(LayoutEventArgs levent)
            {
                  base.OnLayout(levent);

                  UpdateImage();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  _mainPictureBox.Size = Size;
                  base.OnSizeChanged(e);
            }

            protected virtual void OnImageChanged() => Invalidate();
            protected virtual void OnHoverStyleChanged()
            {
                  UpdateImage();
            }
            #endregion

            protected override void DrawBody(Graphics g, Rectangle client)
            {
                  Rectangle borderRectangle = client; borderRectangle.Inflate(-1, -1);
                  GraphicsPath body = GetGPath(client);
                  GraphicsPath border = GetGPath(borderRectangle);

                  Region = new Region(body);

                  g.SmoothingMode = SmoothingMode.HighQuality;

                  if (Image == null)
                        DrawBackgroundImageAsync();
                  else
                        g.Clear(BackColor);

                  if (DrawBorder && _mainPictureBox != null)
                  {
                        AddBorder(g, border);
                  }
            }

            private void SetImage(Image image)
            {
                  if (image == null)
                        return;

                  _enhancedImage = new EnhancedImage(image);

                  _mainPictureBox.Image = _enhancedImage.PlainImage;
            }

            private Image GetCorrectImage()
            {
                  if (HoverShadowStyle == ShadowStyle.ShadowWithoutHover || HoverShadowStyle == ShadowStyle.Always)
                  {
                        if (_drawBorder)
                              return _enhancedImage.GetShadowedImageWithBorder(GetGPath(ClientRectangle), _borderColor, _borderWidth);
                        else
                              return _enhancedImage.ShadowedImage;
                  }
                  else if (_drawBorder)
                        return _enhancedImage.GetImageWithBorder(GetGPath(ClientRectangle), _borderColor, _borderWidth);
                  else
                        return _enhancedImage.PlainImage;
            }

            private void UpdateImage()
            {
                  _mainPictureBox.Image = GetCorrectImage();

                  Invalidate();
            }

            private void Initialize()
            {
                  _mainPictureBox.Dock = DockStyle.Fill;
                  _mainPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                  _mainPictureBox.BackColor = Color.Transparent;
                  _mainPictureBox.Region = Region;

                  _mainPictureBox.Parent = this;
                  Controls.Add(_mainPictureBox);

                  LoadEvents();
            }

            private void LoadEvents()
            {
                  _mainPictureBox.MouseLeave += (s, e) =>
                  {
                        if(HoverShadowStyle != ShadowStyle.Always && HoverShadowStyle != ShadowStyle.None)
                              switch(HoverShadowStyle)
                              {
                                    case ShadowStyle.ShadowWithoutHover: { _mainPictureBox.Image = _enhancedImage.ShadowedImage; break; }
                                    case ShadowStyle.ShadowOnHover: { _mainPictureBox.Image = _enhancedImage.PlainImage; break; }
                              }
                  };
                  _mainPictureBox.MouseEnter += (s, e) =>
                  {
                        if(HoverShadowStyle != ShadowStyle.Always && HoverShadowStyle != ShadowStyle.None)
                              switch (HoverShadowStyle)
                              {
                                    case ShadowStyle.ShadowWithoutHover: { _mainPictureBox.Image = _enhancedImage.PlainImage; break; }
                                    case ShadowStyle.ShadowOnHover: { _mainPictureBox.Image = _enhancedImage.ShadowedImage; break; }
                              }
                  };
            }
      }
}
